package com.Localytics.LocalyticsSession;

/**
 * LocalyticsSession.java
 * Copyright (C) 2009 Char Software Inc., DBA Localytics
 * 
 *  This code is provided under the Localytics Modified BSD License.
 *  A copy of this license has been distributed in a file called LICENSE
 *  with this source code.  
 *  
 *  Please visit www.localytics.com for more information.
 */

import net.rim.device.api.system.*;
import net.rim.device.api.i18n.Locale;
import java.util.Hashtable;
import java.util.Enumeration;

/**
 * The class which manages creating, collecting, & uploading a Localytics session.
 * Please see the following guides for information on how to best use this
 * library, sample code, and other useful information:
 * <ul>
 * </ul>
 * <p>
 * 
 * <strong>Best Practices</strong>
 * <ul>
 * <li>Instantiate the LocalyticsSession object once inside your class which extends UiApplication and</li>
 * provide an accessory function so any method anywhere inside your app can access this object.  This makes
 * it easy to record events from anywhere in your application.</li>
 * <li>Call .open() in your first screen's constructor.  This way your session start is the same as your 
 * your application start.</li>
 * <li>Call .upload() immediately after .open().  See the description of the upload function for the reasons why</li>
 * <li>Create one application exit point and have all onClose and exit methods call this exit point.  This is
 * a good practice because it allows you to easily add any global application cleanup or show a last dialog
 * and it makes it easier to gaurantee close() is always called.</li>
 * <li>Do not call any Localytics functions inside a loop.  Instead, calls
 * such as <code>tagEvent</code> should follow user actions.  This limits the
 * amount of data which is stored and uploaded.</li>
 * <li>Do not use multiple LocalticsSession objects to collect data with 
 * multiple application keys.  This can cause invalid state.</li>
 * </ul>
 * @author Localytics
 * @version 1.5
 */
final public class LocalyticsSession
{   
    // Client constants
    private static final String CLIENT_VERSION = "bb(4.2.1)_1.5";
    private static final String LOG_PREFIX = "(LocalyticsSession) ";
    private static final String TAG_LIMIT_EVENT = "TAG_LIMIT_REACHED";
    private static final int MAX_NUM_SESSIONS = 5;
    private static final int MAX_NUM_ATTRIBUTES = 10;    // Maximum attributes per event session
    protected static final int MAX_NAME_LENGTH = 128;      // Maximum characters in an event name or attribute key/value

    // Whether this application is opted in
    private static boolean _isOptedIn = true;    
    
    // A flag gauranteeing only one upload can happen at a time for this app.
    private static boolean _isUploading = false;
    
    private int _numTags = 0;                      // number of tags recorded
    private int _numTagsSinceUpload = 0;                   // how many tags were recorded before uploading
    private int _maxNumTags = 100;                 // the maximum number of tags to allow

    private boolean _wroteTagLimitEvent = false;   // Whether or not the limit has been reached and noted
    private boolean _isSessionOpen = false;        // whether or not the current session is open
    private boolean _isSessionDone = false;        // whether or not teh current session has been closed and is done

    private String _applicationKey = null;         // The unique key identifying this application
    private String _sessionUUID = null;            // A unique ID identifying this session
    private PersistentObject _sessionStore = null; // The store object where data is saved for this session
     
    /**
     * Creates the Localytics Object.
     * @param applicationKey The unique key for this application.
     */               
    public LocalyticsSession(final String applicationKey)
    {        
        this._applicationKey = applicationKey;
        
        // Check to see if this application has set an opt out.
        LocalyticsSession._isOptedIn = StorageHelper.isAppOptedIn(applicationKey);          
        
        logMessage("LocalyticsSession object created.  Optin state is: " + new Boolean(LocalyticsSession._isOptedIn).toString());
    }

    /**
     * (OPTIONAL) Allows the application to control whether or not it will collect user data.  This is not
     * recommended because it skews data in several ways (all opted out users will appear as though they
     * stopped using your app after they opted out lowering returning user rate.  Also some countries tend
     * to opt out more than others removing them from your sample, etc).  However, it is provided to keep
     * developers who still wish to provide this functionality from having to implement their own optin.  
     * 
     * Even if this call is used, it is necessary to continue calling upload().  No no data will be
     * collected, so nothing new will be uploaded but it is necessary to upload an event telling the
     * server this user has opted out.
     * @param optedIn True if the user is opted in, false otherwise.
     */
    public void setOptIn(final boolean optedIn)
    {
        // Swallowing exceptions is generally not a good practice, but
        // Localytics is an instrumentation technology and as a result it must
        // not impact the instrumenting application under any circumstances
        try
        {
            if(optedIn != LocalyticsSession._isOptedIn && this._isSessionOpen == true)
            {
                LocalyticsSession._isOptedIn = optedIn;
                StorageHelper.storeAppOptin(this._applicationKey, optedIn);
                    
                StorageHelper.storeSessionBlob(this._sessionStore, createOptEvent(optedIn));
                    
                logMessage("Changed optin state to: " + new Boolean(optedIn).toString());
            }
        }
        catch (Exception e) { }                 
    }
    
    /**
     * (OPTIONAL) Whether or not this user has is opted in or out.  The only way they can be
     * opted out is if setOptIn(false) has been called before this.  This function should only be
     * used to pre-populate a checkbox in an options menu.  It is not recommended that an application
     * branch based on Localytics instrumentation because this creates an additional test case.  If
     * the app is opted out, all localytics calls will return immediately.
     * @return true if the user is opted in, false otherwise.
     */
    public boolean isOptedIn()
    {
        return LocalyticsSession._isOptedIn;
    }
    
    /**
     * Opens the session.  This must be the first call because until this happens no data will be
     * collected.  If there are too many sessions already stored, then the session will not be 
     * opened and all subsequent Localytics calls will return immediately.  This is a synchronous
     * call so once it is completed Localytics will not affect the app at all until the next
     * Localytics call.
     * 
     * If open gets called multiple times, the server ignores all opens after the first one.
     */
    public void open()
    {
        try
        {
            synchronized(LocalyticsSession.class)
            {
                if(LocalyticsSession._isOptedIn == false ||
                    this._isSessionOpen == true ||
                    this._isSessionDone == true)
                    {
                        logMessage("Session not open because it is already open, user is opetd out, or it already done.");
                        return;
                    }
                
                    // Setting this to true causes any other open calls happening at this moment to fail.
                    this._isSessionOpen = true;
                }
                
                // get the storage object for this session.  If it is null, then the session can't be opened.
                this._sessionStore = StorageHelper.getNewSessionStore(this._applicationKey, LocalyticsSession.MAX_NUM_SESSIONS);
                if(this._sessionStore == null)
                {
                    this._isSessionOpen = false;
                    logMessage("Session not opened because object coming back from store was null.  Most likely, queue is full.");
                }
                else
                {
                    this._sessionUUID = DatapointHelper.generateRandomId();                 
                    StorageHelper.storeSessionBlob(this._sessionStore, createOpenSessionString());        
                    logMessage("Session succesfully opened.");
                }
        }
        catch (Exception e) { }
    }
    
    /**
     * Closes the opened session.  One this is called data will no longer be written to the
     * session and it cannot be reopened.  Closing a session does not cause data to be
     * uploaded.  This is a synchronous call.
     */
    public void close()
    {   
        try
        {       
            synchronized(LocalyticsSession.class)
            {
                if(LocalyticsSession._isOptedIn == false || this._isSessionOpen == false)
                {
                    logMessage("Session not closed because user is opted out, or the session wasn't open.");
                    return;
                }
                    
                this._isSessionOpen = false;
                this._isSessionDone = true;
            }
                
            StorageHelper.storeSessionBlob(this._sessionStore, createCloseSessionString());
            logMessage("Session closed.");
        }
        catch (Exception e) { }
    }
    
    /**
     * Allows a session to tag a particular event as having occurred.  For
     * example, if a view has three buttons, it might make sense to tag
     * each button click with the name of the button which was clicked.
     * For another example, in a game with many levels it might be valuable
     * to create a new tag every time the user gets to a new level in order
     * to determine how far the average user is progressing in the game.
     * <br>
     * <strong>Tagging Best Practices</strong>
     * <ul>
     * <li>DO NOT use tags to record personally identifiable information.</li>
     * <li>The best way to use tags is to create all the tag strings as predefined
     * constants and only use those.  This is more efficient and removes the risk of
     * collecting personal information.</li>
     * <li>Do not set tags inside loops or any other place which gets called
     * frequently.  This can cause a lot of data to be stored and uploaded.</li>
     * </ul>
     * <br>
     * @param event The name of the event which occurred.
     */
    public void tagEvent(final String event)
    {
        tagEvent(event, null);
    }
    
    
    /**
     * Allows a session to tag a particular event as having occurred, and
     * optionally attach a collection of attributes to it.  For example, if a
     * screen has three buttons, it might make sense to tag each button with the
     * name of the button which was clicked.
     * For another example, in a game with many levels it might be valuable
     * to create a new tag every time the user gets to a new level.  You could
     * attach the player's lives as an attribute in order to determine how far
     * the average user is progressing in the game.
     * 
     * Because of the way data is stored on the BlackBerry, every time tagEvent
     * is called it gets more expensive.  For this reason there is a limit on
     * the number of times you may use this function.  This way, if the app goes
     * into invalid state the storage is not overly taxed and the application
     * experience is not impacted.  Once the limit is reached, a single event named
     * TAG_LIMIT_REACHED will be uploaded.
     * <br>
     * The default limit on the number of times this function may be called is 100.  If
     * absolutely necessary, this may be changed with setTagLimit().
     * <br>
     * <strong>Tagging Best Practices</strong>
     * <ul>
     * <li>It is a violation of the Terms of Service to use tags to record personally identifiable information.</li>
     * <li>The best way to use tags is to create all the tag strings as predefined
     * constants and only use those.  This is more efficient and removes the risk of
     * collecting personal information.</li>
     * <li>Do not set tags inside loops or any other place which gets called
     * frequently.  This can cause a lot of data to be stored and uploaded.</li>
     * </ul>
     * <br>
     * See the tagging section of Localytics Integration Guide at: http://wiki.localytics.com
     *
     * @param event The name of the event which occurred.
     * @param attributes The collection of attributes for this particular event. This is ignored if the value is null.
     */
    public void tagEvent(final String event, final Hashtable attributes)
    {
        try
        {
            if(LocalyticsSession._isOptedIn == false ||
               this._isSessionOpen == false)
             {
                logMessage("Event not tagged because user was opted out or the session wasn't open.");
                return;
             }        
                
             // Regardless of what the limits are right now, once the tagLimitEvent
             // is recorded, don't allow any more events to be tagged.  This is because once the limit
             // is reached subsequent events would be lost and the data would be skewed on the webservice.
             if(this._wroteTagLimitEvent == true)
             {
                logMessage("Event not tagged because tag limit was reached.");
                return;
             }
                
             if(this._numTags >= this._maxNumTags)
             {                              
                StorageHelper.storeSessionBlob(this._sessionStore, createEventTag(LocalyticsSession.TAG_LIMIT_EVENT, null));            
                this._wroteTagLimitEvent = true;
                logMessage("Tagged Event: " + LocalyticsSession.TAG_LIMIT_EVENT);
             }
             else
             {
                StorageHelper.storeSessionBlob(this._sessionStore, createEventTag(event, attributes));
                this._numTags++;
                logMessage("Tagged Event: " + event);            
             }
        }
        catch(Exception e) { }
    }
    
    /**
     * (OPTIONAL) Changes the limit on the number of tags which get recorded.  It is
     * not recommended to use this function because the limit is there to gaurantee
     * the user's application experience is not impacated by costly writes of many
     * events to the persisted store.  However, it is provided for people with
     * specific needs for more events.
     * <br>
     * Once the limit has been reached changing this value will have no effect.
     * @param limit The number of events which can be written before all event tagging
     * is stopped.
     */
    public void setTagLimit(final int limit)
    {
        this._maxNumTags = limit;
        logMessage("Tag Limit set to: " + Integer.toString(limit));
    }
    
    /**
     * Creates a low priority thread which uploads the data for this application on the device.
     * Once the upload is done the thread terminates and has no further impact on the application.
     * 
     * While this function returns a thread, it is not required (and in fact not recommended) that
     * an application consume this.  It is best to start the upload and forget about it so that it
     * does not impact the user experience.  However, in some cases short lived applications may wish
     * to wait for the upload thread to complete before finishing.
     * 
     * It is recommended that upload() be called at the start of the application immediately after
     * the call to open().  This beneficial for three reasons:
     * 1) It causes the open session event to be uploaded so this usage of the app will appear on
     * the webservice even if something goes wrong before it ends.
     * 2) On many devices a dialog is popped up when upload is called asking the user for
     * permission.  By frontloading this it is less distracting to the user and will not interfere
     * with any tasks they may have started.
     * 3) Starting the upload at the beginning gives the upload time to finish.  If it is done at the
     * end of the application the app could terminate before the upload has time to complete.
     * @return The uploader thread object.  It is recommended that this object be ignored.  It is
     * only provided for applications which require knowledge of when the upload thread is complete.
     */
    public Thread upload()
    {
        try
        {
             // Synchronize the check to make sure the upload is not already happening. This avoids the
             // possibility of two uploader threads being started at once protecting the app from any
             // bugs which could otherwise flood the app with threads.        
             synchronized(LocalyticsSession.class)
             {        
                if(LocalyticsSession._isUploading)  {
                    return null; 
                }
                              
                LocalyticsSession._isUploading = true;
             }
                      
             logMessage("Starting uploader.");
             this._numTagsSinceUpload = this._numTags;
             UploaderThread uploader = new UploaderThread(
                                                          this._applicationKey, 
                                                          LocalyticsSession.MAX_NUM_SESSIONS, 
                                                          this.uploadNotifier);                                                     
             uploader.setPriority(Thread.MIN_PRIORITY);
             uploader.start();
             return uploader;
        }
        catch (Exception e) { return null; }
    }
    
    
    /**
     * Sorts an int value into a set of regular intervals as defined by the
     * minimum, maximum, and step size. Both the min and max values are
     * inclusive, and in the instance where (max - min + 1) is not evenly
     * divisible by step size, the method guarantees only the minimum and the
     * step size to be accurate to specification, with the new maximum will be
     * moved to the next regular step.
     * 
     * @param actualValue The int value to be sorted.
     * @param minValue The int value representing the inclusive minimum interval.
     * @param maxValue The int value representing the inclusive maximum interval.
     * @param step The int value representing the increment of each interval.
     */
    public static String createRangedAttribute(int actualValue, int minValue, int maxValue, int step)
    {
        // Confirm there is at least one bucket
        if (step < 1)
        {
            logMessage("Step must not be less than zero.  Returning null.");
            return null;
        }
        if (minValue >= maxValue)
        {
            logMessage("maxValue must not be less than minValue.  Returning null.");
            return null;
        }

        // Determine the number of steps, rounding up using int math
        int stepQuantity = (maxValue - minValue + step) / step;
        int[] steps = new int[stepQuantity + 1];
        for (int currentStep = 0; currentStep <= stepQuantity; currentStep++)
        {
            steps[currentStep] = minValue + (currentStep) * step;
        }
        return createRangedAttribute(actualValue, steps);
    }

    /**  
     * Sorts an int value into a predefined, pre-sorted set of intervals, returning a string representing the
     * new expected value.  The array must be sorted in ascending order, with the first element representing
     * the inclusive lower bound and the last element representing the exclusive upper bound.  For instance,
     * the array [0,1,3,10] will provide the following buckets: less than 0, 0, 1-2, 3-9, 10 or greater.
     * 
     * @param actualValue The int value to be bucketed.
     * @param steps The sorted int array representing the bucketing intervals.
     */
    public static String createRangedAttribute(int actualValue, int[] steps)
    {
        String bucket = null;

        // if less than smallest value
        if (actualValue < steps[0])
        {
            bucket = "less than " + steps[0];
        }
        // if greater than largest value
        else if (actualValue >= steps[steps.length - 1])
        {
            bucket = steps[steps.length - 1] + " and above";            
        }
        else
        {
            int bucketIndex = binarySearch(steps, actualValue);
            // If the buckets are only 1 value, display that value rather than a range
            if (steps[bucketIndex] == (steps[bucketIndex + 1] - 1))
            {
                bucket = Integer.toString(steps[bucketIndex]);
            }
            else
            {
                bucket = steps[bucketIndex] + "-" + (steps[bucketIndex + 1] - 1);
            }
        }
        return bucket;
    }

    ///////////////////////////////////////////////////
    // Private Functions //////////////////////////////
    ///////////////////////////////////////////////////
    
    
    /**
     * Creates the YAML blob for an open session action
     * @return A YAML formatted string which can be stored and uploaded
     */
    private String createOpenSessionString()
    {
        StringBuffer openString = new StringBuffer();
        
        // the YAML header for a session creation
        openString.append(DatapointHelper.CONTROLLER_SESSION);
        openString.append(DatapointHelper.ACTION_CREATE);
        openString.append(DatapointHelper.OBJECT_SESSION_DP);
        
        // Application and session information
        openString.append(DatapointHelper.formatYAMLLine(
                                             DatapointHelper.PARAM_UUID, this._sessionUUID, 3));
        openString.append(DatapointHelper.formatYAMLLine(
                                             DatapointHelper.PARAM_APP_UUID, this._applicationKey, 3));
        openString.append(DatapointHelper.formatYAMLLine(                
                                             DatapointHelper.PARAM_APP_VERSION, ApplicationDescriptor.currentApplicationDescriptor().getVersion(), 3));
        openString.append(DatapointHelper.formatYAMLLine(
                                             DatapointHelper.PARAM_LIBRARY_VERSION, LocalyticsSession.CLIENT_VERSION, 3));
        openString.append(DatapointHelper.formatYAMLLine(
                                             DatapointHelper.PARAM_CLIENT_TIME, DatapointHelper.getTimeAsDatetime(), 3));                                                     
                                           
        // Other device information
        openString.append(DatapointHelper.formatYAMLLine(
                                            DatapointHelper.PARAM_DEVICE_UUID, StorageHelper.getDeviceUUID(), 3));         
        openString.append(DatapointHelper.formatYAMLLine(
                                            DatapointHelper.PARAM_DEVICE_PLATFORM, "BlackBerry", 3));
        openString.append(DatapointHelper.formatYAMLLine(                
                                            DatapointHelper.PARAM_OS_VERSION, DatapointHelper.getOsVersion(), 3));
        openString.append(DatapointHelper.formatYAMLLine(
                                            DatapointHelper.PARAM_DEVICE_MAKE, DeviceInfo.getManufacturerName(), 3));
        openString.append(DatapointHelper.formatYAMLLine(
                                            DatapointHelper.PARAM_DEVICE_MODEL, DeviceInfo.getDeviceName(), 3));
        openString.append(DatapointHelper.formatYAMLLine(
                                            DatapointHelper.PARAM_LOCALE_LANGUAGE, Locale.getDefaultForSystem().getDisplayLanguage(), 3));
        openString.append(DatapointHelper.formatYAMLLine(
                                            DatapointHelper.PARAM_LOCALE_COUNTRY, Locale.getDefaultForSystem().getDisplayCountry(), 3));
                
        // Network information
        openString.append(DatapointHelper.formatYAMLLine(
                                            DatapointHelper.PARAM_NETWORK_CARRIER, RadioInfo.getCurrentNetworkName(), 3));
        openString.append(DatapointHelper.formatYAMLLine(
                                            DatapointHelper.PARAM_NETWORK_MCC, Integer.toString(RadioInfo.getMCC(RadioInfo.getCurrentNetworkIndex())), 3));
        openString.append(DatapointHelper.formatYAMLLine(
                                            DatapointHelper.PARAM_NETWORK_MNC, Integer.toString(RadioInfo.getMNC(RadioInfo.getCurrentNetworkIndex())), 3));
        openString.append(DatapointHelper.formatYAMLLine(
                                            DatapointHelper.PARAM_DATA_CONNECTION, DatapointHelper.getNetworkType(), 3));
        return openString.toString();
    }

    /**
     * Creates the YAML blob for a close session action
     * @return A YAML formatted string which can be stored and uploaded
     */
    private String createCloseSessionString()
    {
        StringBuffer closeString = new StringBuffer();

        closeString.append(DatapointHelper.CONTROLLER_SESSION);
        closeString.append(DatapointHelper.ACTION_UPDATE);
        closeString.append(DatapointHelper.formatYAMLLine(
                                            DatapointHelper.PARAM_UUID, this._sessionUUID, 2));
        closeString.append(DatapointHelper.OBJECT_SESSION_DP);        
        closeString.append(DatapointHelper.formatYAMLLine(
        									DatapointHelper.PARAM_APP_UUID, this._applicationKey, 3));
        closeString.append(DatapointHelper.formatYAMLLine(
                                            DatapointHelper.PARAM_CLIENT_CLOSED_TIME, DatapointHelper.getTimeAsDatetime(), 3));

        return closeString.toString();
    }
    
    /**
     * Creates the YAML blob for an event tagged with tagEvent()
     * @param event The string describing the event which occured
     * @return A YAML formatted string which can be stored and uploaded
     */
    private String createEventTag(final String event, Hashtable attributes)
    {
        StringBuffer eventString = new StringBuffer();
    
        eventString.append(DatapointHelper.CONTROLLER_EVENT);
        eventString.append(DatapointHelper.ACTION_CREATE);
        eventString.append(DatapointHelper.OBJECT_EVENT_DP);
        eventString.append(DatapointHelper.formatYAMLLine(
        						DatapointHelper.PARAM_APP_UUID, this._applicationKey, 3));
        eventString.append(DatapointHelper.formatYAMLLine(
                                DatapointHelper.PARAM_UUID, DatapointHelper.generateRandomId(), 3));                                             
        eventString.append(DatapointHelper.formatYAMLLine(        
                                DatapointHelper.PARAM_SESSION_UUID, this._sessionUUID, 3));
        eventString.append(DatapointHelper.formatYAMLLine(
                                DatapointHelper.PARAM_CLIENT_TIME, DatapointHelper.getTimeAsDatetime(), 3));

        eventString.append(DatapointHelper.formatYAMLLine(
                    DatapointHelper.PARAM_EVENT_NAME, event, 3));

        if (attributes != null)
        {
            eventString.append(DatapointHelper.EVENT_ATTRIBUTE);

            // enumerate through the hashtable's elements and append a line for each one
            Enumeration attr_enum = attributes.keys();
            for (int currentAttr = 0; attr_enum.hasMoreElements() && (currentAttr < MAX_NUM_ATTRIBUTES); currentAttr++)
            {
                String key = (String) attr_enum.nextElement();
                String value = (String) attributes.get(key);
                eventString.append(DatapointHelper.formatYAMLLine(key, value, 4));
            }
        }

        return eventString.toString();
    }
    
    /**
     * Creates an event telling the webservice that the user opted in or out.
     * @param optState True if they opted in, false if they opted out.
     */
    private String createOptEvent(boolean optState)
    {            
        StringBuffer optString = new StringBuffer();
                
        optString.append(DatapointHelper.CONTROLLER_OPT);
        optString.append(DatapointHelper.ACTION_OPTIN);
        optString.append(DatapointHelper.OBJECT_OPT);
                        
        optString.append(DatapointHelper.formatYAMLLine(
                            DatapointHelper.PARAM_DEVICE_UUID, StorageHelper.getDeviceUUID(), 3));
                        
        optString.append(DatapointHelper.formatYAMLLine(
                            DatapointHelper.PARAM_APP_UUID, this._applicationKey, 3));
                        
        optString.append(DatapointHelper.formatYAMLLine(
                            DatapointHelper.PARAM_OPT_VALUE,  new Boolean(optState).toString(), 3));                                           
                                                                                        
         return optString.toString();
    }

    /**
     * Prepends a LocalyticsSession String and logs a message to the console.
     * @param message Message to log.
     */
    private static void logMessage(String message)
    {
        System.out.println(LocalyticsSession.LOG_PREFIX + message);
    }    
    
    /**
     * Callback, defined by the Uploader which is is called when the upload is complete.
     * This clears the tag count for any uploaded tags, and allows another upload
     * to happen.
     */
    private UploaderThread.UploadNotifier uploadNotifier = new UploaderThread.UploadNotifier()
    {
        public void uploadComplete(boolean wasSuccess) 
        {
            if(wasSuccess)
            {                               
                if(_numTagsSinceUpload > 0)
                {
                    logMessage("Discounting the " + _numTagsSinceUpload + " tags which were just uploaded from the count");
                    _numTags -= _numTagsSinceUpload;                                        
                }
            }
            LocalyticsSession._isUploading = false;
        }       
    };    
    
    /**
     * Searches a sorted int array for a value, returning the index if found.
     * If the value is not found, returns the next lowest value.
     * @param sorted The sorted int array to be searched.
     * @param searchValue The int to be searched for.
     * @return The index of the value if found, or the next lowest value if not found.
     */
    private static int binarySearch(int[] sorted, int searchValue)
    {
    	int low = 0;
    	int high = sorted.length - 1;
    	int mid = (high + low) / 2;
    	
    	while (low < high)
    	{
    		if (searchValue < sorted[mid])
    		{
    			high = mid;
    		}
    		else if (searchValue > sorted[mid])
    		{
    			low = mid + 1;
    		}
    		else
    		{
    			return mid;
    		}
    		mid = (low + high) / 2;
    	}
    	return mid - 1;
    }
}