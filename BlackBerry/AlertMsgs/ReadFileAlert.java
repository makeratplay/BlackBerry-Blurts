package com.mlhsoftware.Blurts;

import org.json.me.*;


class ReadFileAlert extends AlertBase 
{
  // Tag Names
  private static final String ALERT_NAME = "ReadFile";
  private static final String KEY_FILENAME = "FileName";


  public ReadFileAlert( String string ) throws JSONException
  {
    super( string );
  }

  public ReadFileAlert()
  {
    super( TYPE_READFILE, ALERT_NAME );
  }

  public void setFileName( String value )
  {
    try
    {
      put( KEY_FILENAME, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }
}
