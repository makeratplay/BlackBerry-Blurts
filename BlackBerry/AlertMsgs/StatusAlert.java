package com.mlhsoftware.Blurts;

import org.json.me.*;


class StatusAlert extends AlertBase 
{
  // Tag Names
  private static final String ALERT_NAME = "Status";
  private static final String KEY_STATUS = "Text";


  public StatusAlert( String string ) throws JSONException
  {
    super( string );
  }

  public StatusAlert()
  {
    super( TYPE_STATUS, ALERT_NAME );
  }

  public void setStatus( String value )
  {
    try
    {
      put( KEY_STATUS, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  String getStatus()
  {
    return optString( KEY_STATUS );
  }
}
