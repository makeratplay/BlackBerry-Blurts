//#preprocess
/*
 * MethodsBase.java
 *
 * MLH Software
 * Copyright 2010
 */

//#ifdef BLURTS
package com.mlhsoftware.webapi.blurts;
import com.mlhsoftware.ui.blurts.AboutScreen;
//#endif 

//#ifdef SIMPLYREMINDME
package com.mlhsoftware.webapi.SimplyRemindMe;
//#endif 

//#ifdef SIMPLYTASKS
package com.mlhsoftware.webapi.SimplyTasks;
//#endif 

//#ifdef MLHKEY
package com.mlhsoftware.webapi.MLHKey;
import com.mlhsoftware.ui.MLHKey.AboutScreen;
//#endif 

import org.json.me.*;
import net.rim.device.api.system.Application;
import net.rim.device.api.system.DeviceInfo;
import net.rim.device.api.crypto.MD5Digest;
import net.rim.device.api.system.CodeModuleManager;

public class MethodsBase extends JSONObject
{
  // Key Names
  private static String KEY_METHOD = "Method";
  private static String KEY_TAG = "Tag";        // HASHED PIN
  private static String KEY_DEVICE_MODEL = "Model";
  private static String KEY_DEVICE_OS_VER = "OSVer";
  private static String KEY_APP_NAME = "AppName";
  private static String KEY_APP_VERSION = "AppVer";
  private static String KEY_DEVICE_COUNTRY = "C";
  private static String KEY_LOCALE_COUNTRY = "LC";
  private static String KEY_LOCALE_LANGUAGE = "LL";
  private static String KEY_NETWORK_COUNTRY = "NC";
  private static String KEY_NETWORK_CARRIER = "NR";
  private static String KEY_NETWORK_MNC = "MNC";
  private static String KEY_NETWORK_MCC = "MCC";
  private static String KEY_DATA_CONNECTION = "DC";
  private static String KEY_LOCATION_SOURCE = "LS";
  private static String KEY_LOCATION_LAT = "Lat";
  private static String KEY_LOCATION_LNG = "Lng";

  private static String KEY_RESULT_ERROR_TEXT = "EText";
  private static String KEY_RESULT_ERROR_CODE = "ECode";

  // a callback to notify when the upload is done
  protected WebApiBase.WebAPICallback _completeCallback;
  
  protected JSONObject _response;

  public MethodsBase()
  {
    super();
    init();
  }

  public MethodsBase(String s)  throws JSONException
  {
    super(s);
  }

  private void init()
  {
    try
    {
      setAppVer( AboutScreen.APP_VERSION );
      remove(KEY_TAG);
      put(KEY_TAG, HashPin());
      remove(KEY_DEVICE_MODEL);
      put(KEY_DEVICE_MODEL, DeviceInfo.getDeviceName());
      remove(KEY_DEVICE_OS_VER);
      put(KEY_DEVICE_OS_VER, CodeModuleManager.getModuleVersion( CodeModuleManager.getModuleHandleForObject( "" ) ));
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }

  public String HashPin()
  {
    String hash = "";
    String deviceIdText = getPin() + "BLACKBERRYPIN08252010";
    try
    {
      byte[] bytes = deviceIdText.getBytes( "UTF-8" );
      MD5Digest digest = new MD5Digest();
      digest.update( bytes, 0, bytes.length );
      int length = digest.getDigestLength();
      byte[] md5 = new byte[length];
      digest.getDigest( md5, 0, true );
      hash = toHexString( md5 );
    }
    catch (Exception e )
    {
      String msg = "isKeyValid Error: " + e.toString();
      //EventLogger.logEvent( LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
    }
    return hash;
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


  public void setCallback( WebApiBase.WebAPICallback callBack )
  {
    _completeCallback = callBack;
  }

  public void process()
  {
    WebApiBase.callMethod( this.toString(), this.callNotifier );
  }

 /**
   * Callback, defined by the CallerThread which is is called when the web call is complete.
   */
  private CallerThread.CallerNotifier callNotifier = new CallerThread.CallerNotifier()
  {
    public void callComplete( boolean wasSuccess, String resultData )
    {
      try
      {
        try
        {
          _response = new JSONObject( new JSONTokener( resultData ) );
        }
        catch ( JSONException e )
        {
          System.out.println( "JSONException" + e.toString() );
        }          
        Application app = Application.getApplication();
        if ( app != null )
        {
          if ( wasSuccess )
          {
            app.invokeLater( new Runnable() { public void run() { callSucceed(); } } );
          }
          else
          {
            app.invokeLater( new Runnable() { public void run() { callFailed(); } } );
          }
        }
      }
      catch ( Exception e )
      {
        System.out.println( "callComplete Exception " + e.toString() );
      }
    }
  };

 public void callSucceed()
  {
    System.out.println( "callSucceed" );
    if ( _completeCallback != null )
    {
      _completeCallback.callComplete( true, this );
    }
  }

  public void callFailed()
  {
    String msg = "webAPI:" + getMethod() +  " failed";
    System.out.println( msg );
    if ( _completeCallback != null )
    {
      _completeCallback.callComplete( false, this );
    }
  }


  public String getMethod()
  {
    return optString(KEY_METHOD);
  }
  
  public void setMethod( String value )
  {
    try
    {
      remove(KEY_METHOD);
      put(KEY_METHOD, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }

  public String getTag()
  {
    return optString(KEY_TAG);
  }
  
  public void setTag( String value )
  {
    try
    {
      remove(KEY_TAG);
      put(KEY_TAG, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }

  public String getModel()
  {
    return optString(KEY_DEVICE_MODEL);
  }

  public void setModel( String value )
  {
    try
    {
      remove(KEY_DEVICE_MODEL);
      put(KEY_DEVICE_MODEL, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }

  
  public String getOSVer()
  {
    return optString(KEY_DEVICE_OS_VER);
  }
    
  public void setOSVer( String value )
  {
    try
    {
      remove(KEY_DEVICE_OS_VER);
      put(KEY_DEVICE_OS_VER, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }


  
  public String getAppName()
  {
    return optString(KEY_APP_NAME);
  }
    
  public void setAppName( String value )
  {
    try
    {
      remove(KEY_APP_NAME);
      put(KEY_APP_NAME, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }



  public String getAppVer()
  {
    return optString(KEY_APP_VERSION);
  }
  public void setAppVer( String value )
  {
    try
    {
      remove(KEY_APP_VERSION);
      put(KEY_APP_VERSION, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }


  public String getDeviceCountry()
  {
    return optString(KEY_DEVICE_COUNTRY);
  }
  public void setDeviceCountry( String value )
  {
    try
    {
      remove(KEY_DEVICE_COUNTRY);
      put(KEY_DEVICE_COUNTRY, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }


  public String getCountry()
  {
    return optString(KEY_LOCALE_COUNTRY);
  }
  public void setCountry( String value )
  {
    try
    {
      remove(KEY_LOCALE_COUNTRY);
      put(KEY_LOCALE_COUNTRY, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }



 
  public String getLanguage()
  {
    return optString(KEY_LOCALE_LANGUAGE);
  }
  public void setLanguage( String value )
  {
    try
    {
      remove(KEY_LOCALE_LANGUAGE);
      put(KEY_LOCALE_LANGUAGE, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }

 
  public String getNetworkCountry()
  {
    return optString(KEY_NETWORK_COUNTRY);
  }
  public void setNetworkCountry( String value )
  {
    try
    {
      remove(KEY_NETWORK_COUNTRY);
      put(KEY_NETWORK_COUNTRY, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }

 
  public String getNetworkCarrier()
  {
    return optString(KEY_NETWORK_CARRIER);
  }
  public void setNetworkCarrier( String value )
  {
    try
    {
      remove(KEY_NETWORK_CARRIER);
      put(KEY_NETWORK_CARRIER, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }

  public String getNetworkMNC()
  {
    return optString(KEY_NETWORK_MNC);
  }
  public void  setNetworkMNC( String value )
  {
    try
    {
      remove(KEY_NETWORK_MNC);
      put(KEY_NETWORK_MNC, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }


  public String getNetworkMCC()
  {
    return optString(KEY_NETWORK_MCC);
  }
  public void setNetworkMCC( String value )
  {
    try
    {
      remove(KEY_NETWORK_MCC);
      put(KEY_NETWORK_MCC, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }


  public String getDataConnction()
  {
    return optString(KEY_DATA_CONNECTION);
  }
  public void setDataConnction( String value )
  {
    try
    {
      remove(KEY_DATA_CONNECTION);
      put(KEY_DATA_CONNECTION, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }


  public String getLocationSource()
  {
    return optString(KEY_LOCATION_SOURCE);
  }
  public void setLocationSource( String value )
  {
    try
    {
      remove(KEY_LOCATION_SOURCE);
      put(KEY_LOCATION_SOURCE, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }


  public String getLocationLat()
  {
    return optString(KEY_LOCATION_LAT);
  }
  public void setLocationLat( String value )
  {
    try
    {
      remove(KEY_LOCATION_LAT);
      put(KEY_LOCATION_LAT, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }


  public String getLocationLng()
  {
    return optString(KEY_LOCATION_LNG);
  }
  public void setLocationLng( String value )
  {
    try
    {
      remove(KEY_LOCATION_LNG);
      put(KEY_LOCATION_LNG, value);
    }
    catch (JSONException e)
    {
      //Console.WriteLine(e.ToString());
    }
  }

  public String getResultErrorText()
  {
    return _response.optString(KEY_RESULT_ERROR_TEXT);
  }

  public String getResultErrorCode()
  {
    return _response.optString(KEY_RESULT_ERROR_CODE);
  }
}
