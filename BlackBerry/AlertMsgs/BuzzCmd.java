package com.mlhsoftware.Blurts;

import org.json.me.*;


class BuzzCmd extends CmdBase 
{
  // Tag Names
  private static final String KEY_BUZZ_LENGTH = "BuzzLength";


  public BuzzCmd( String string ) throws JSONException
  {
    super( string );
  }

  public BuzzCmd()
  {
    super( CMD_BUZZ );
  }

  public void setBuzzLength( int value )
  {
    try
    {
      put( KEY_BUZZ_LENGTH, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  int getBuzzLength()
  {
    return optInt( KEY_BUZZ_LENGTH );
  }
}
