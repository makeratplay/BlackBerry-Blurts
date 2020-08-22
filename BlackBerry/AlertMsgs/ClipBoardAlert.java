package com.mlhsoftware.Blurts;

import org.json.me.*;


class ClipBoardAlert extends AlertBase 
{
  // Tag Names
  private static final String ALERT_NAME = "Clipboard";
  private static final String KEY_TEXT = "Text";


  public ClipBoardAlert( String string ) throws JSONException
  {
    super( string );
  }

  public ClipBoardAlert()
  {
    super( TYPE_CLIPBOARD, ALERT_NAME );
  }

  public void setText( String value )
  {
    try
    {
      put( KEY_TEXT, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }
}
