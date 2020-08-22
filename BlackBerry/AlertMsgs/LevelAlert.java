package com.mlhsoftware.Blurts;

import org.json.me.*;


class LevelAlert extends AlertBase 
{
  // Tag Names
  private static final String ALERT_NAME  = "Level";
  private static final String KEY_BATTERY = "Battery";
  private static final String KEY_SIGNAL  = "Signal";


  public LevelAlert( String string ) throws JSONException
  {
    super( string );
  }

  public LevelAlert()
  {
    super( TYPE_LEVEL, ALERT_NAME );
  }



}
