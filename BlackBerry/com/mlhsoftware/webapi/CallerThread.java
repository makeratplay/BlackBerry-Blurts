//#preprocess
/*
 * CallerThread.java
 *
 * MLH Software
 * Copyright 2010
 */

//#ifdef BLURTS
package com.mlhsoftware.webapi.blurts;
//#endif 

//#ifdef SIMPLYREMINDME
package com.mlhsoftware.webapi.SimplyRemindMe;
//#endif 

//#ifdef SIMPLYTASKS
package com.mlhsoftware.webapi.SimplyTasks;
//#endif 

//#ifdef MLHKEY
package com.mlhsoftware.webapi.MLHKey;
//#endif 

import net.rim.device.api.system.PersistentObject;
import net.rim.device.api.system.DeviceInfo;
import net.rim.device.api.system.CoverageInfo;
import net.rim.device.api.servicebook.ServiceBook;
import net.rim.device.api.servicebook.ServiceRecord;
import net.rim.device.api.system.EventLogger;

import java.io.*;
import javax.microedition.io.*;
import java.util.Vector;

import net.rim.device.api.io.http.*;

/**
 * A low priority thread responsible for calling web api.
 */
final public class CallerThread extends Thread
{

  //#ifdef BLURTS
    public static String APP_NAME = "Blurts";
    public static long LOGGER_ID = 0xf0569edab4ce20faL;      //com.mlhsoftware.Blurts
  //#endif 

  private static final boolean USE_MDS_IN_SIMULATOR = false;

  /**
   * The interface used to notify the api call is complete
   */
  public static interface CallerNotifier
  {
    void callComplete( boolean wasSuccess, String resultData );
  }

  String _postData;
  String _resultData;
  String _url;

  // a callback to notify when the upload is done
  private CallerThread.CallerNotifier _completeCallback;

  CallerThread( final String url, final String postData, final CallerThread.CallerNotifier completeCallback )
  {
    this._url = url;
    this._postData = postData;
    this._completeCallback = completeCallback;
  }

  public void run()
  {
    boolean uploadResult = false;

    try
    {
      //postBody.append( _method );
      uploadResult = makeCall( _postData );

      if ( this._completeCallback != null )
      {
        this._completeCallback.callComplete( uploadResult, _resultData );
      }
    }
    catch ( Exception e ) 
    {
      logMessage( "run Exception." );
    }
  }

  /**
     * Determine the optimal way to connect to the network, and get the bits to the webservice
   */
  private boolean makeCall( String postData )
  {
    boolean retVal = false;
    int response;
    HttpConnection connection = null;

    try
    {
      // figure out how to connect, or abort the upload if nothing is found
      String connectionString = getConnectionString();
      if ( connectionString == null )
      {
        logMessage( "No connection string found." );
        return false;
      }

      String targetUrl = _url + connectionString;
      logMessage( "api URL: " + targetUrl );
      
      connection = (HttpConnection)Connector.open( targetUrl );
      connection.setRequestMethod( HttpConnection.POST );
      connection.setRequestProperty( HttpProtocolConstants.HEADER_CONTENT_TYPE, HttpProtocolConstants.CONTENT_TYPE_APPLICATION_X_WWW_FORM_URLENCODED );
      connection.setRequestProperty( HttpProtocolConstants.HEADER_CONTENT_LENGTH, String.valueOf( postData.length() ) );

      OutputStream os = connection.openOutputStream();
      os.write( postData.getBytes() );
      os.close();

      response = connection.getResponseCode();

      if ( response == HttpConnection.HTTP_OK )
      {
        retVal = true;
        InputStream reply = connection.openInputStream();
        onCallMethodSuccessful( reply );
        reply.close();
        logMessage( "postData: " + postData );
        logMessage( "SUCCESS! response was: " + Integer.toString( response ) );
      }
    }
    catch ( Exception e )
    {
      logMessage( "Web call failed: " + e.getMessage() );
      retVal = false;
    }
    finally
    {
      if ( connection != null )
      {
        try { connection.close(); }
        catch ( IOException e ) { }
      }
    }
    return retVal;
  }


  public void onCallMethodSuccessful( InputStream reply )
  {
    StringBuffer resultSB = new StringBuffer();
    try
    {
      byte[] r = new byte[1024];
      int len = reply.read( r );
      do
      {
        String tmpString = new String( r, 0, len );
        resultSB.append( tmpString );
        len = reply.read( r );
      } while ( len != -1 );

      _resultData = resultSB.toString();
      System.out.println( _resultData );
    }
    catch ( Exception e )
    {
      e.printStackTrace();
      logMessage( "onCallMethodSuccessful failed: " + e.toString() );
    }
  }

  /**
     * Determines what connection type to use and returns the necessary string to use it.
   * @return A string with the connection info
   */
  private static String getConnectionString()
  {
    String connectionString = null;
    try
    {
      // Simulator behavior is controlled by the USE_MDS_IN_SIMULATOR variable.
      if ( DeviceInfo.isSimulator() )
      {
        if ( CallerThread.USE_MDS_IN_SIMULATOR )
        {
          logMessage( "Device is a simulator and USE_MDS_IN_SIMULATOR is true" );
          connectionString = ";deviceside=false";
        }
        else
        {
          logMessage( "Device is a simulator and USE_MDS_IN_SIMULATOR is false" );
          connectionString = ";deviceside=true";
        }
      }

      // Is the carrier network the only way to connect?
      else if ( ( CoverageInfo.getCoverageStatus() & 1  ) == 1 )  //  CoverageInfo.COVERAGE_DIRECT - 5.0 , CoverageInfo.COVERAGE_CARRIER = 4.2
      {
        logMessage( "Carrier coverage." );

        String carrierUid = getCarrierBIBSUid();
        if ( carrierUid == null )
        {
          // Has carrier coverage, but not BIBS.  So use the carrier's TCP network
          logMessage( "No Uid" );
          connectionString = ";deviceside=true";
        }
        else
        {
          // otherwise, use the Uid to construct a valid carrier BIBS request
          logMessage( "uid is: " + carrierUid );
          connectionString = ";deviceside=false;connectionUID=" + carrierUid + ";ConnectionType=mds-public";
        }
      }

      // Check for an MDS connection instead (BlackBerry Enterprise Server)
      else if ( ( CoverageInfo.getCoverageStatus() & CoverageInfo.COVERAGE_MDS ) == CoverageInfo.COVERAGE_MDS )
      {
        logMessage( "MDS coverage found" );
        connectionString = ";deviceside=false";
      }

      // If there is no connection available abort to avoid bugging the user unnecssarily.
      else if ( CoverageInfo.getCoverageStatus() == CoverageInfo.COVERAGE_NONE )
      {
        logMessage( "There is no available connection." );
      }

      // In theory, all bases are covered so this shouldn't be called.
      else
      {
        logMessage( "no other options found, assuming device." );
        connectionString = ";deviceside=false";
      }
    }
    catch( Exception e )
    {
      logMessage( "getConnectionString failed: " + e.toString() );
    }
    return connectionString;
  }

  /**
     * Looks through the phone's service book for a carrier provided BIBS network
   * @return The uid used to connect to that network.
   */
  private static String getCarrierBIBSUid()
  {
    try
    {
      ServiceRecord[] records = ServiceBook.getSB().getRecords();
      int currentRecord;

      for ( currentRecord = 0; currentRecord < records.length; currentRecord++ )
      {
        if ( records[currentRecord].getCid().toLowerCase().equals( "ippp" ) )
        {
          if ( records[currentRecord].getName().toLowerCase().indexOf( "bibs" ) >= 0 )
          {
            return records[currentRecord].getUid();
          }
        }
      }
    }
    catch( Exception e )
    {
      logMessage( "getCarrierBIBSUid failed: " + e.toString() );
    }
    return null;
  }

  /**
     * Logs a message to the console
   * @param msg Message to log
   */
  private static void logMessage( final String msg )
  {
    EventLogger.logEvent( LOGGER_ID, msg.getBytes(), EventLogger.DEBUG_INFO );
    System.out.println( APP_NAME + ": " + msg );
  }
}
