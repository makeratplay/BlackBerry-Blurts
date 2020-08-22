package com.mlhsoftware.Blurts;

import org.json.me.*;
import com.mlhsoftware.ui.blurts.*;


class CmdBase extends JSONObject 
{
  // Key Names
  private static String KEY_CMD_TYPE = "CmdType";

  // Command IDs
  public static final int CMD_NOTPRO = -2;
  public static final int CMD_UNKNOWN = 0;
  public static final int CMD_PLACECALL = 1;
  public static final int CMD_PRESSKEY = 2;
  public static final int CMD_BUZZ = 3;
  public static final int CMD_SENDSMS = 4;
  public static final int CMD_CONTACTSEARCH = 5;
  public static final int CMD_SCREENSHOT = 6;
  public static final int CMD_CREATECONTACT = 7;
  public static final int CMD_READCLIPBOARD = 8;
  public static final int CMD_WRITECLIPBOARD = 9;
  public static final int CMD_LEVEL = 10;
  public static final int CMD_DTMF = 11;
  public static final int CMD_SENDEMAIL = 12;
  public static final int CMD_LOCATEME = 13;
  public static final int CMD_READFILE = 14;
  

  public CmdBase( String string ) throws JSONException
  {
    super( string );
  }

  public CmdBase( int cmdType )
  {
    try
    {
      put( KEY_CMD_TYPE, cmdType );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  int getCmdType()
  {
    return optInt( KEY_CMD_TYPE );
  }

  public boolean isProCmd()
  {
    boolean retVal = true;
    switch ( getCmdType() )
    {
      case CMD_BUZZ:
      case CMD_LEVEL:
      {
        retVal = false;
        break;
      }
    }
    return retVal;
  }
}
