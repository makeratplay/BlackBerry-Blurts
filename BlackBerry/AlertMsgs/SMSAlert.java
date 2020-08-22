package com.mlhsoftware.Blurts;

import org.json.me.*;


class SMSAlert extends AlertBase 
{
  // Tag Names
  private static final String ALERT_NAME = "SMS";
  private static final String KEY_SENDER_NAME = "SenderName";
  private static final String KEY_ADDRESS = "SenderAddress";
  private static final String KEY_BODY = "BodyText";


  public SMSAlert( String string ) throws JSONException
  {
    super( string );
  }

  public SMSAlert()
  {
    super( TYPE_SMS, ALERT_NAME );
  }


  public void setSenderName( String value )
  {
    try
    {
      put( KEY_SENDER_NAME, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  public void setSenderAddress( String value )
  {
    try
    {
      put( KEY_ADDRESS, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }


  public void setBodyText( String value )
  {
    try
    {
      put( KEY_BODY, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

}
