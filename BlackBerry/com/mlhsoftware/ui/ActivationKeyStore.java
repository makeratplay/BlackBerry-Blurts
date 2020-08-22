//#preprocess
/*
 * ActivationScreen.java
 *
 * MLH Software
 * Copyright 2010
 */

//#ifdef BLURTS
package com.mlhsoftware.ui.blurts;
import com.mlhsoftware.ui.blurts.*;
import com.mlhsoftware.webapi.blurts.*;
//#endif 

//#ifdef SIMPLYREMINDME
package com.mlhsoftware.ui.SimplyRemindMe;
import com.mlhsoftware.ui.SimplyRemindMe.*;
import com.mlhsoftware.webapi.SimplyRemindMe.*;
//#endif 

//#ifdef SIMPLYTASKS
package com.mlhsoftware.ui.SimplyTasks;
import com.mlhsoftware.ui.SimplyTasks.*;
import com.mlhsoftware.webapi.SimplyTasks.*;
//#endif 

//#ifdef MLHKEY
package com.mlhsoftware.ui.MLHKey;
import com.mlhsoftware.webapi.MLHKey.*;
//#endif 

import net.rim.device.api.ui.*;
import net.rim.device.api.ui.component.*;
import net.rim.device.api.ui.container.*;
import net.rim.device.api.ui.container.PopupScreen;
import net.rim.device.api.notification.NotificationsManager;
import net.rim.device.api.system.Characters;
import net.rim.device.api.system.ControlledAccessException;

import net.rim.device.api.system.DeviceInfo;
import net.rim.device.api.crypto.MD5Digest;
import net.rim.device.api.system.*;
import net.rim.device.api.util.*;

import net.rim.device.api.ui.MenuItem;

import java.util.*;

import net.rim.blackberry.api.phone.phonelogs.PhoneCallLogID;
import net.rim.blackberry.api.phone.phonelogs.PhoneCallLog;
import net.rim.blackberry.api.phone.phonelogs.PhoneCallLogID;
import net.rim.blackberry.api.phone.phonelogs.PhoneLogs;

import net.rim.device.api.system.EventLogger;
import net.rim.blackberry.api.browser.Browser;







public class ActivationKeyStore implements Persistable
{
    //#ifdef BLURTS
      public static String APP_NAME = "Blurts Pro";
      public static long LOGGER_ID = 0xf0569edab4ce20faL;      //com.mlhsoftware.Blurts
      protected static String SECRET_KEY = "*MLHBLURTS110409*";  // Yes I am leaving the secret key here because I no longer sell this app
      protected static long PERSISTENCE_ID =0x8050e74a7922d25fL;   //Hash of com.mlhsoftware.Blurts.keycode
      protected static int MAX_TRIALS = 0;
      protected static String MOBIHAND_PID = "40695";
      protected static String APPWORLD_PID = "4299";
    //#endif 

    //#ifdef MLHKEY
      public static String APP_NAME = "MLHKey";
      public static long LOGGER_ID = 0x14235a4ec1efd3d6L;      //com.mlhsoftware.MLHKey
      protected static String SECRET_KEY = "";
      protected static long PERSISTENCE_ID =0x8909314d276aebfdL;   //Hash of com.mlhsoftware.MLHKey.keycode
      protected static int MAX_TRIALS = 0;
      protected static String MOBIHAND_PID = "";
      protected static String APPWORLD_PID = "";
    //#endif 


  //Persistent object wrapping the effective properties instance
  private static PersistentObject _store;
  private static ActivationKeyStore Instance = null;
  
  public static ActivationKeyStore GetInstance()
  {
    if ( Instance == null )
    {
      try
      {
        _store = PersistentStore.getPersistentObject( PERSISTENCE_ID );
        synchronized ( _store )
        {
          if ( _store.getContents() == null )
          {
            _store.setContents( new ActivationKeyStore() );
            _store.commit();
          }
        }
        Instance = (ActivationKeyStore)_store.getContents();
      }
      catch ( Exception e )
      {
        String msg = "Persistent Store (" + PERSISTENCE_ID + "): "+ e.toString();
        EventLogger.logEvent( LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      }
    }
    //String msg = "Activation Key fetch";
    //EventLogger.logEvent( LOGGER_ID, msg.getBytes(), EventLogger.DEBUG_INFO );

    return Instance;
  }

  public static void reset()
  {
    try
    {
      _store = PersistentStore.getPersistentObject( PERSISTENCE_ID );
      synchronized ( _store )
      {
        _store.setContents( null );
        _store.commit();
      }
      Instance = null;
    }
    catch ( Exception e )
    {
      String msg = "reset Persistent Store (" + PERSISTENCE_ID + "): "+ e.toString();
      EventLogger.logEvent( LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
    }
  }

  public static String getBuyUrl()
  {
    String url = "https://www.mobihand.com/mobilecart/mc1.asp?posid=234&tracking1=bb&pid=" + MOBIHAND_PID + "&did=" + getPin();
    //#ifdef APPWORLD
    url = "http://appworld.blackberry.com/webstore/content/" + APPWORLD_PID;
    //#endif 
    return url;
  }

  private String _key;
  private int _trialCnt;

  private ActivationKeyStore()
  {
    _key = readKeyFromAppWorld();
    _trialCnt = 0;
  }

  //Cannonical copy constructor.
  private ActivationKeyStore( ActivationKeyStore other )
  {
    _key = other._key;
    _trialCnt = other._trialCnt;
  }

  //Causes the values within this instance to become the effective
  //properties for the application by saving this instance to the store.
  public void save()
  {
    try
    {
      _store.setContents( this );
      _store.commit();
    }
    catch ( Exception e )
    {
      String msg = "Activation Key save failed: " + e.toString();
      EventLogger.logEvent( LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
    }
  }

  public String getKey()
  {
    return _key;
  }

  public void setKey( String val )
  {
    _key = val;
  }

  public int getTrialCnt()
  {
    return _trialCnt;
  }

  public void setTrialCnt( int val )
  {
    _trialCnt = val;
  }


  void clearKey()
  {
    setKey( "" );
    save();
  }

  public static boolean isKeyValid()
  {
    boolean retVal = false;
    try
    {
      ActivationKeyStore keyStore = ActivationKeyStore.GetInstance();
      if ( keyStore != null )
      {
        int deviceId = DeviceInfo.getDeviceId();
        String deviceIdText = getPin() + SECRET_KEY;
        String key = keyStore.getKey();
        if ( key.length() > 0 )
        {

          byte[] bytes = deviceIdText.getBytes( "UTF-8" );
          MD5Digest digest = new MD5Digest();
          digest.update( bytes, 0, bytes.length );
          int length = digest.getDigestLength();
          byte[] md5 = new byte[length];
          digest.getDigest( md5, 0, true );

          String value = toHexString( md5 );
          String tmp1 = value.substring( 4, 10 ).toUpperCase();
          String tmp2 = key.trim().toUpperCase();

          if ( tmp1.compareTo( tmp2 ) == 0 )
          {
            retVal = true;
          }
          else
          {
            String msg = "Invalid Key: " + tmp2 + " (" + getPin() + ")";
            EventLogger.logEvent( LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
          }
        }
      }
      else
      {
        String msg = "ActivationKeyStore.GetInstance() return null";
        EventLogger.logEvent( LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      }
    }
    catch (Exception e )
    {
      String msg = "isKeyValid Error: " + e.toString();
      EventLogger.logEvent( LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
    }
    return retVal;
  }

  public static String toHexString( byte bytes[] )
  {
    if ( bytes == null )
    {
      return null;
    }

    StringBuffer sb = new StringBuffer();
    for ( int iter = 0; iter < bytes.length; iter++ )
    {
      byte high = (byte)( ( bytes[iter] & 0xf0 ) >> 4 );
      byte low = (byte)( bytes[iter] & 0x0f );
      sb.append( nibble2char( high ) );
      sb.append( nibble2char( low ) );
    }

    return sb.toString();
  }

  private static char nibble2char( byte b )
  {
    byte nibble = (byte)( b & 0x0f );
    if ( nibble < 10 )
    {
      return (char)( '0' + nibble );
    }
    return (char)( 'a' + nibble - 10 );
  }

  public static String getPin()
  {
    int deviceId = DeviceInfo.getDeviceId();
    String deviceIdText = java.lang.Integer.toHexString( deviceId );
    return deviceIdText.toUpperCase();
  }

  public boolean trialExpired()
  {
    boolean Expired = true;

    if ( isKeyValid() )
    {
      Expired = false;
    }
    else if ( getTrialCnt() < MAX_TRIALS )
    {
      Expired = false;
    }
    return Expired;
  }

  // returns number of free trials left
  int incTrialCnt()
  {
    int cnt = getTrialCnt();
    cnt++;
    setTrialCnt( cnt );
    save();

    if ( cnt == MAX_TRIALS  )
    {
      //SimplySolitaire._session.tagEvent( "Expired" );
    }
    return MAX_TRIALS - cnt;
  }

  public boolean betaExpired()
  {
    return false;
    /*
    boolean retVal = false;
    long now = new Date().getTime();
    Calendar expiration = Calendar.getInstance();
    expiration.set( Calendar.YEAR, 2010 );
    expiration.set( Calendar.MONTH, Calendar.FEBRUARY );
    expiration.set( Calendar.DAY_OF_MONTH, 1 );
    expiration.set( Calendar.HOUR, 11 );
    expiration.set( Calendar.MINUTE, 00 );
    if ( now > expiration.getTime().getTime() )
    {
      retVal = true;
    }
    return retVal;
     * */
  }


  public String readKeyFromAppWorld()
  {
    String key = "";
    try
    {
      // Get name of your app
      String myAppName = ApplicationDescriptor.currentApplicationDescriptor().getName();
      // It must be exact name of your application as
      // registered with the App World ISV portal
      //String myAppName = "AppName:Vendor";

      // If you are targeting 4.3+, use this:
      //CodeModuleGroup group = CodeModuleGroupManager.load( myAppName );

      // On 4.2 you would need to use the following:
      CodeModuleGroup group = null;
      if( myAppName != null ) 
      {
        CodeModuleGroup[] groups = CodeModuleGroupManager.loadAll();
        if( groups != null ) 
        {
          for( int i = 0; i < groups.length; ++i ) 
          {
            if( groups[ i ].containsModule( myAppName ) ) 
            {
              group = groups[ i ];
              break;
            }
          }
        }
      } 

      // Pull out the App World data from the CodeModuleGroup
      if ( group != null )
      {
        key = group.getProperty( "RIM_APP_WORLD_LICENSE_KEY" );
        if ( key == null )
        {
          key = "";
        }
      }
    }
    catch ( Exception e )
    {
      String msg = "readKeyFromAppWorld failed: " + e.toString();
      EventLogger.logEvent( LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
    }
    return key;
  }
}
