/**
 * UploaderThread.java
 * Copyright (C) 2009 Char Software Inc., DBA Localytics
 * 
 *  This code is provided under the Localytics Modified BSD License.
 *  A copy of this license has been distributed in a file called LICENSE
 *  with this source code.  
 *  
 *  Please visit www.localytics.com for more information.
 */

package com.Localytics.LocalyticsSession;

import net.rim.device.api.system.PersistentObject;
// import net.rim.device.api.util.StringUtilities;
import net.rim.device.api.system.DeviceInfo;
import net.rim.device.api.system.CoverageInfo;
import net.rim.device.api.servicebook.ServiceBook;
import net.rim.device.api.servicebook.ServiceRecord;

import java.io.*;
import javax.microedition.io.*;
import java.util.Vector;

/**
 * A low priority thread responsible for uploading the LocalyticsSession data.
 */
final public class UploaderThread extends Thread
{
    // Whether or not to the simulator should use MDS to connect.
    // By default this should be false, however if you are testing
    // in an environment where MDS will be the expected connection method,
    // set this to true to have the simulator attempt to use MDS.  This variable
    // has no effect on what happens on a real device.
    private static final boolean USE_MDS_IN_SIMULATOR = false;
     
    /**
     * The interface used to notify an object which causes an upload to happen
     * that the upload is complete.
     */
    public static interface UploadNotifier 
    {
        void uploadComplete(boolean wasSuccess);
    }
      
    private int _maxNumSessions;            // how many sessions can be stored
    private String _appKey;                 // the key used to identify this application
    
    // a callback to notify when the upload is done
    private UploaderThread.UploadNotifier _completeCallback;
        
    private final static String ANALYTICS_URL = "http://analytics.localytics.com/api/datapoints/bulk";
    private final static String LOG_TAG = "(localytics uploader)";
        
    /**
     * Creates a new UploaderThread which uploads the data.  If completeCallback
     * is not null, it runs this when the upload is complete.  The webservice will
     * ignore duplicated events which get uploaded.  
     * @param completeCallback A Runnable object which gets run when the upload is complete.
     * Pass null for no action.
     */
    //UploaderThread(final String appKey, final int maxNumSessions, final Runnable completeCallback) 
    UploaderThread(final String appKey, final int maxNumSessions, final UploaderThread.UploadNotifier completeCallback)
    {      
        this._appKey = appKey;
        this._maxNumSessions = maxNumSessions;
        this._completeCallback = completeCallback;
    }
    
    // Collects all the YAML blobs for all the stored sessions and combines them into one 
    // post body to upload to the webservice.  This is done in this way so that only one 
    // upload happens regardless of the number of sessions on the system.  This way on devices
    // which pop up a dialog every time an upload happens, only one dialog is needed for all sessions.
    public void run()
    {
        StringBuffer postBody = new StringBuffer();
        int[] numEventsConsumed = new int[this._maxNumSessions];
        boolean doUpload = false;
        boolean uploadResult = false;
        int numEventsInSession;
        int currentEvent;
        int currentSlot;
        Vector session;
        
        try
        {                                                 
            // Go through each slot in the persisted store.  If that slot contains a session, consume it
            // (it is stored as a vector) by appending every entry to the StringBuffer.  Meanwhile, keep track
            // of how many events were consumed in each slot so that any events which come in during uploading
            // don't get erased.    
            PersistentObject[] sessions = StorageHelper.getSessionStores(this._appKey, this._maxNumSessions);       
            for(currentSlot = 0; currentSlot < this._maxNumSessions; currentSlot++)
            {
                session = (Vector)sessions[currentSlot].getContents();
                if(session == null)
                {
                    numEventsConsumed[currentSlot] = 0;
                }
                else
                {
                    numEventsInSession = session.size();

                    for(currentEvent = 0; currentEvent < numEventsInSession; currentEvent++)
                    {
                        postBody.append((String)session.elementAt(currentEvent));
                    }
                    
                    numEventsConsumed[currentSlot] = numEventsInSession;
                    doUpload = true;
                }
            }
                      
            logMessage("Uploading session data.");
            if(doUpload && (uploadResult = uploadSessions(postBody.toString())) == true)
            {        
                // Go through each slot and delete the items which got uploaded.  It is necessary
                // to get the contents of each slot again in case new events have come up.
                logMessage("Deleting the data which was successfully uploaded.");
                   
                for(currentSlot = 0; currentSlot < this._maxNumSessions; currentSlot++)               
                {                                
                    if(numEventsConsumed[currentSlot] == 0) {
                        continue;
                    }
                  
                    // Getting and delete the data needs to be synchronized so that a new event coming
                    // in this very moment doesn't get deleted.
                    synchronized(sessions[currentSlot])
                    {
                        session = (Vector)sessions[currentSlot].getContents(); 
                                           
                        if(session == null) 
                        {
                            continue;
                        }
                      
                        // If all the events were consumed, free the slot for a new session
                        if(numEventsConsumed[currentSlot] == session.size())
                        {
                            sessions[currentSlot].setContents(null);
                            sessions[currentSlot].commit();
                        }
                        else // if new events came in, this slot is still in use so only remove the extras
                        {
                            // There shouldn't be too many events which came in while the uploader was running so rather than
                            // delete all the events which got uploaded, it's cheaper to copy the new events and discard the array entirely.
                            Vector newSession = new Vector();
                            for(currentEvent = numEventsConsumed[currentSlot]; currentEvent < session.size(); currentEvent++)
                            {
                                newSession.addElement((String)session.elementAt(currentEvent));
                            }
                              
                            sessions[currentSlot].setContents(newSession);
                            sessions[currentSlot].commit();
                        }
                    }  // end synchronized block
                   
                    session.removeAllElements();
                }
            } 
             
            if(this._completeCallback != null)
            {
                this._completeCallback.uploadComplete(uploadResult);               
            }
        }
        catch (Exception e) { }
    }
    
    /**
     * Determine the optimal way to connect to the network, and get the bits to the webserice
     * @param sessions the sessions to upload
     * @return true if an upload succeeded, false otherwise.
     */
    private boolean uploadSessions(String sessions)
    {
        int response;
        
        // figure out how to connect, or abort the upload if nothing is found
        String connectionString = getConnectionString();
        if(connectionString == null)
        {
            logMessage("No connection string found.");
            return false;
        }
        
        String targetUrl = UploaderThread.ANALYTICS_URL + connectionString;
        logMessage("upload URL: " + targetUrl);
        
        try
        {
            HttpConnection connection = (HttpConnection)Connector.open(targetUrl);
            connection.setRequestMethod(HttpConnection.POST);                    
            OutputStream os = connection.openOutputStream(); 
            os.write(sessions.getBytes());
                
            response = connection.getResponseCode();
            connection.close();
            if(response != -1)
            {
                logMessage("SUCCESS! response was: " + Integer.toString(response));
                return true;
            }
        }
        catch(IOException e)
        {
            logMessage("Upload failed: " + e.getMessage());
            return false;
        }
        
        logMessage("Upload succeeded but server response was invalid.");
        return false;        
    }
    
    /**
     * Determines what connection type to use and returns the necessary string to use it.
     * @return A string with the connection info
     */
    private static String getConnectionString()
    {
        // This code is based on the connection code developed by Mike Nelson of AccelGolf.
        // http://blog.accelgolf.com/2009/05/22/blackberry-cross-carrier-and-cross-network-http-connection        
        String connectionString = null;
                        
        // Simulator behavior is controlled by the USE_MDS_IN_SIMULATOR variable.
        if(DeviceInfo.isSimulator())
        {
            if(UploaderThread.USE_MDS_IN_SIMULATOR)
            {
                logMessage("Device is a simulator and USE_MDS_IN_SIMULATOR is true");
                connectionString = ";deviceside=false";                 
            }
            else
            {
                logMessage("Device is a simulator and USE_MDS_IN_SIMULATOR is false");
                connectionString = ";deviceside=true";
            }
        }        
                
        // Is the carrier network the only way to connect?
        else if((CoverageInfo.getCoverageStatus() & CoverageInfo.COVERAGE_CARRIER) == CoverageInfo.COVERAGE_CARRIER)
        {
            logMessage("Carrier coverage.");
                        
            String carrierUid = getCarrierBIBSUid();
            if(carrierUid == null) 
            {
                // Has carrier coverage, but not BIBS.  So use the carrier's TCP network
                logMessage("No Uid");
                connectionString = ";deviceside=true";
            }
            else 
            {
                // otherwise, use the Uid to construct a valid carrier BIBS request
                logMessage("uid is: " + carrierUid);
                connectionString = ";deviceside=false;connectionUID="+carrierUid + ";ConnectionType=mds-public";
            }
        }                
        
        // Check for an MDS connection instead (BlackBerry Enterprise Server)
        else if((CoverageInfo.getCoverageStatus() & CoverageInfo.COVERAGE_MDS) == CoverageInfo.COVERAGE_MDS)
        {
            logMessage("MDS coverage found");
            connectionString = ";deviceside=false";
        }
        
        // If there is no connection available abort to avoid bugging the user unnecssarily.
        else if(CoverageInfo.getCoverageStatus() == CoverageInfo.COVERAGE_NONE)
        {
            logMessage("There is no available connection.");
        }
        
        // In theory, all bases are covered so this shouldn't be called.
        else
        {
            logMessage("no other options found, assuming device.");
            connectionString = ";deviceside=false";
        }        
        
        return connectionString;
    }
    
    /**
     * Looks through the phone's service book for a carrier provided BIBS network
     * @return The uid used to connect to that network.
     */
    private static String getCarrierBIBSUid()
    {
        ServiceRecord[] records = ServiceBook.getSB().getRecords();
        int currentRecord;
        
        for(currentRecord = 0; currentRecord < records.length; currentRecord++)
        {
            if(records[currentRecord].getCid().toLowerCase().equals("ippp"))
            {
                if(records[currentRecord].getName().toLowerCase().indexOf("bibs") >= 0)
                {
                    return records[currentRecord].getUid();
                }
            }
        }
        
        return null;
    }    
    
    /**
     * Logs a message to the console
     * @param msg Message to log
     */
    private static void logMessage(final String msg)
    {
        System.out.println(UploaderThread.LOG_TAG + msg);
    }
} 
