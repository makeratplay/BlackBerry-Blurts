package com.mlhsoftware.Blurts;

import org.json.me.*;


class ReadClipboardCmd extends CmdBase 
{
  // Tag Names
  private static final String KEY_TEXT = "Text";


  public ReadClipboardCmd( String string ) throws JSONException
  {
    super( string );
  }

  public ReadClipboardCmd()
  {
    super( CMD_READCLIPBOARD );
  }
}
