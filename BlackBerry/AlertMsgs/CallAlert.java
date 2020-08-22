package com.mlhsoftware.Blurts;

import org.json.me.*;


class CallAlert extends AlertBase 
{
  // Tag Names
  private static final String ALERT_NAME = "Call";
  private static final String KEY_ACTION = "Action";
  private static final String KEY_NUMBER = "PhoneNumber";
  private static final String KEY_NAME = "CallerName";

  public CallAlert( String string ) throws JSONException
  {
    super( string );
  }

  public CallAlert()
  {
    super( TYPE_CALL, ALERT_NAME );
  }

  public void setNumber( String value )
  {
    try
    {
      put( KEY_NUMBER, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  String getNumber()
  {
    return optString( KEY_NUMBER );
  }

  public void setName( String value )
  {
    try
    {
      put( KEY_NAME, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  String getName()
  {
    return optString( KEY_NAME );
  }

  public void setAction( String value )
  {
    try
    {
      put( KEY_ACTION, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  int getAddress()
  {
    return optInt( KEY_ACTION );
  }
}
