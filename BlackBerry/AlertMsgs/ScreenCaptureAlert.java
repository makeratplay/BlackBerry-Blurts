package com.mlhsoftware.Blurts;

import org.json.me.*;


class ScreenCaptureAlert extends AlertBase 
{
  // Tag Names
  private static final String ALERT_NAME = "Screen";
  private static final String KEY_DATA = "Data";


  public ScreenCaptureAlert( String string ) throws JSONException
  {
    super( string );
  }

  public ScreenCaptureAlert()
  {
    super( TYPE_SCREEN, ALERT_NAME );
  }

  public void setData( String value )
  {
    try
    {
      put( KEY_DATA, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }
}
