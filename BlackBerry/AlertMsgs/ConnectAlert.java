package com.mlhsoftware.Blurts;

import org.json.me.*;


class ConnectAlert extends AlertBase 
{
  // Tag Names
  private static final String ALERT_NAME = "Connect";
  private static final String KEY_TEXT = "Text";
  private static final String KEY_BT_NAME  = "BluetoothName";
  private static final String KEY_DEVICE_PIN   = "DevicePIN";
  private static final String KEY_DEVICE_MODEL = "DeviceModel";

  public ConnectAlert( String string ) throws JSONException
  {
    super( string );
  }

  public ConnectAlert()
  {
    super( TYPE_CONNECT, ALERT_NAME );
  }

  public void setText( String value )
  {
    try
    {
      put( KEY_TEXT, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  public void setBTName( String value )
  {
    try
    {
      put( KEY_BT_NAME, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  public void setDevicePIN( String value )
  {
    try
    {
      put( KEY_DEVICE_PIN, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  public void setDeviceModel( String value )
  {
    try
    {
      put( KEY_DEVICE_MODEL, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }
}
