package com.mlhsoftware.Blurts;

import org.json.me.*;


class ScreenCaptureCmd extends CmdBase 
{
  // Tag Names
  private static final String KEY_QUALITY = "Quality";
  private static final String KEY_TOP = "Top";
  private static final String KEY_LEFT = "Left";
  private static final String KEY_WIDTH = "Width";
  private static final String KEY_HEIGHT = "Height";


  public ScreenCaptureCmd( String string ) throws JSONException
  {
    super( string );
  }

  public ScreenCaptureCmd()
  {
    super( CMD_SCREENSHOT );
  }

  int getQuality()
  {
    return optInt( KEY_QUALITY );
  }

  int getTop()
  {
    return optInt( KEY_TOP );
  }

  int getLeft()
  {
    return optInt( KEY_LEFT );
  }

  int getWidth()
  {
    return optInt( KEY_WIDTH );
  }

  int getHeight()
  {
    return optInt( KEY_HEIGHT );
  }
}
