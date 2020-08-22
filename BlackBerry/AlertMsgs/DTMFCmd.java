package com.mlhsoftware.Blurts;

import org.json.me.*;


class DTMFCmd extends CmdBase 
{
  // Tag Names
  private static final String KEY_DTMF = "DTMFTones";


  public DTMFCmd( String string ) throws JSONException
  {
    super( string );
  }

  public DTMFCmd()
  {
    super( CMD_DTMF );
  }

  String getDTMFTones()
  {
    return optString( KEY_DTMF );
  }
}
