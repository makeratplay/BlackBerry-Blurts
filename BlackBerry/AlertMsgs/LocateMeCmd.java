package com.mlhsoftware.Blurts;

import org.json.me.*;


class LocateMeCmd extends CmdBase 
{
  // Tag Names


  public LocateMeCmd( String string ) throws JSONException
  {
    super( string );
  }

  public LocateMeCmd()
  {
    super( CMD_LOCATEME );
  }
}
