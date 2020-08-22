package com.mlhsoftware.Blurts;

import org.json.me.*;


class LockAlert extends AlertBase 
{
  // Tag Names
  private static final String ALERT_NAME = "Lock";
  private static final String KEY_TEXT = "Text";


  public LockAlert( String string ) throws JSONException
  {
    super( string );
  }

  public LockAlert()
  {
    super( TYPE_LOCK, ALERT_NAME );
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
}
