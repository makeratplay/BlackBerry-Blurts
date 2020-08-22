package com.mlhsoftware.Blurts;

import org.json.me.*;


class LevelCmd extends CmdBase 
{
  // Tag Names


  public LevelCmd( String string ) throws JSONException
  {
    super( string );
  }

  public LevelCmd()
  {
    super( CMD_LEVEL );
  }
}
