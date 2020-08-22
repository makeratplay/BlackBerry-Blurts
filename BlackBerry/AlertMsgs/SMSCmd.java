package com.mlhsoftware.Blurts;

import org.json.me.*;


class SMSCmd extends CmdBase 
{
  // Tag Names
  private static final String KEY_ADDRESS = "SenderAddress";
  private static final String KEY_BODY = "BodyText";


  public SMSCmd( String string ) throws JSONException
  {
    super( string );
  }

  public SMSCmd()
  {
    super( CMD_SENDSMS );
  }

  String getAddress()
  {
    return optString( KEY_ADDRESS );
  }

  String getText()
  {
    return optString( KEY_BODY );
  }
}
