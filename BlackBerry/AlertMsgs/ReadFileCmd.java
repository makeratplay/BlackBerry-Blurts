package com.mlhsoftware.Blurts;

import org.json.me.*;


class ReadFileCmd extends CmdBase 
{
  // Tag Names
  private static final String KEY_FILENAME = "FileName";


  public ReadFileCmd( String string ) throws JSONException
  {
    super( string );
  }

  public ReadFileCmd()
  {
    super( CMD_READFILE );
  }

  String getFileName()
  {
    return optString( KEY_FILENAME );
  }
}
