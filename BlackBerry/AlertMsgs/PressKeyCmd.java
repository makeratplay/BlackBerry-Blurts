package com.mlhsoftware.Blurts;

import org.json.me.*;


class PressKeyCmd extends CmdBase 
{
  // Tag Names
  private static final String KEY_FUNC_KEY = "FunctionKey";
  private static final String KEY_KEY_CODE = "KeyCode";
  private static final String KEY_STATUS_CODE = "StatusCode";


  public PressKeyCmd( String string ) throws JSONException
  {
    super( string );
  }

  public PressKeyCmd()
  {
    super( CMD_PRESSKEY );
  }

  int getFunctionKey()
  {
    return optInt( KEY_FUNC_KEY );
  }
  
  int getKeyCode()
  {
    return optInt( KEY_KEY_CODE );
  }

  int getStatusCode()
  {
    return optInt( KEY_STATUS_CODE );
  }
}
