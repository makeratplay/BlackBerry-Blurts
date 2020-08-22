package com.mlhsoftware.Blurts;

import org.json.me.*;


class CallCmd extends CmdBase 
{
  // Tag Names
   private static final String KEY_NUMBER = "PhoneNumber";


  public CallCmd( String string ) throws JSONException
  {
    super( string );
  }

  public CallCmd()
  {
    super( CMD_PLACECALL );
  }

  String getPhoneNumber()
  {
    return optString( KEY_NUMBER );
  }
}
