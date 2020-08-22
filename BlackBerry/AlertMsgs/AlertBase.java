//#preprocess
package com.mlhsoftware.Blurts;

import org.json.me.*;
import com.mlhsoftware.ui.blurts.*;
import net.rim.device.api.system.DeviceInfo;
import net.rim.device.api.system.RadioInfo;

class AlertBase extends JSONObject 
{
  // Key Names
  private static String KEY_VERSION       = "Version";
  private static String KEY_ALERT_NAME    = "AlertName";
  private static String KEY_ALERT_TYPE    = "AlertType";
  private static String KEY_ACTIVATED     = "Activated";
  private static final String KEY_BATTERY = "Battery";
  private static final String KEY_SIGNAL  = "Signal";
  private static final String KEY_CHANNEL = "Channel";

  // Alert Types
  public static final int TYPE_UNKNOWN = 0;
  public static final int TYPE_STATUS = 1;
  public static final int TYPE_EMAIL = 2;
  public static final int TYPE_CALL = 3;
  public static final int TYPE_LOCK = 4;
  public static final int TYPE_SMS = 5;
  public static final int TYPE_SCREEN = 6;
  public static final int TYPE_CONTACTS = 7;
  public static final int TYPE_CLIPBOARD = 9;
  public static final int TYPE_CONNECT = 10;
  public static final int TYPE_DISCONNECT = 11;
  public static final int TYPE_LEVEL = 12;
  public static final int TYPE_MACRO = 13;
  public static final int TYPE_PIN_MSG = 14;
  public static final int TYPE_INPUT_MSG = 15;
  public static final int TYPE_READFILE = 16;

  public AlertBase( String string ) throws JSONException
  {
    super( string );
  }

  public AlertBase( int alertType, String alertName )
  {
    try
    {
      String channel = "1";
      //#ifdef APPWORLD
      channel = "2";
      //#endif 

      put( KEY_CHANNEL, channel );
      put( KEY_ALERT_NAME, alertName );
      put( KEY_ALERT_TYPE, alertType );
      put( KEY_VERSION, AboutScreen.APP_VERSION );
      put( KEY_ACTIVATED, ActivationKeyStore.isKeyValid() ? "true" : "false" );
      setBattery( DeviceInfo.getBatteryLevel() );
      setSignal( RadioInfo.getSignalLevel() );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  public void setBattery( int value )
  {
    try
    {
      put( KEY_BATTERY, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  public void setSignal( int value )
  {
    try
    {
      put( KEY_SIGNAL, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }
}
