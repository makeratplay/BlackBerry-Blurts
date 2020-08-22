package com.mlhsoftware.Blurts;

import org.json.me.*;


class EmailCmd extends CmdBase 
{
  // Tag Names
  private static final String ALERT_NAME = "Email";
  private static final String KEY_SENDER_NAME = "SenderName";
  private static final String KEY_ACCOUNT = "ReceivingAccount";
  private static final String KEY_ADDRESS = "SenderAddress";
  private static final String KEY_SUBJECT = "Subject";
  private static final String KEY_BODY = "BodyText";


  public EmailCmd( String string ) throws JSONException
  {
    super( string );
  }

  public EmailCmd()
  {
    super( CMD_SENDEMAIL, ALERT_NAME );
  }

  public void setAccount( String value )
  {
    try
    {
      put( KEY_ACCOUNT, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  String getAccount()
  {
    return optString( KEY_ACCOUNT );
  }

  public void setName( String value )
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

  String getName()
  {
    return optString( KEY_ADDRESS );
  }

  public void setAddress( String value )
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

  String getAddress()
  {
    return optString( KEY_ADDRESS );
  }

  public void setSubject( String value )
  {
    try
    {
      put( KEY_SUBJECT, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  String getSubject()
  {
    return optString( KEY_SUBJECT );
  }

  public void setBody( String value )
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

  String getBody()
  {
    return optString( KEY_BODY );
  }
}
