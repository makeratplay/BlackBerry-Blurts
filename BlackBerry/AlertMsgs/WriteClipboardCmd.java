package com.mlhsoftware.Blurts;

import org.json.me.*;


class WriteClipboardCmd extends CmdBase 
{
  // Tag Names
  private static final String KEY_TEXT = "Text";


  public WriteClipboardCmd( String string ) throws JSONException
  {
    super( string );
  }

  public WriteClipboardCmd()
  {
    super( CMD_WRITECLIPBOARD );
  }

  String getText()
  {
    return optString( KEY_TEXT );
  }
}
