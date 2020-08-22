package com.mlhsoftware.Blurts;

import org.json.me.*;


class InputAlert extends AlertBase 
{
  // Tag Names
  private static final String ALERT_NAME = "Input";
  private static final String KEY_TYPE = "Type";      // mouse, keyboard
  private static final String KEY_ACTION = "Action";
  private static final String KEY_DX = "dx";
  private static final String KEY_DY = "dy";
  private static final String KEY_KEYCODE = "KeyCode";

  public static final String MOUSE = "MOUSE"; 
  public static final String KEYBOARD = "KEYBOARD"; 

  public static final String MOVE = "MOVE"; /* mouse move */
  public static final String LEFTCLICK = "LEFTCLICK"; /* left button down */
  public static final String LEFTDOWN = "LEFTDOWN"; /* left button down */
  public static final String LEFTUP = "LEFTUP"; /* left button up */
  public static final String RIGHTCLICK = "RIGHTCLICK"; /* left button down */
  public static final String RIGHTDOWN = "RIGHTDOWN"; /* right button down */
  public static final String RIGHTUP = "RIGHTUP"; /* right button up */
  public static final String MIDDLEDOWN = "MIDDLEDOWN"; /* middle button down */
  public static final String MIDDLEUP = "MIDDLEUP"; /* middle button up */
  public static final String XDOWN = "XDOWN"; /* x button down */
  public static final String XUP = "XUP"; /* x button down */
  public static final String WHEEL = "WHEEL"; /* wheel button rolled */

  public InputAlert( String string ) throws JSONException
  {
    super( string );
  }

  public InputAlert()
  {
    super( TYPE_INPUT_MSG, ALERT_NAME );
  }

  public void setType( String value )
  {
    try
    {
      put( KEY_TYPE, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  public void setAction( String value )
  {
    try
    {
      put( KEY_ACTION, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  public void setDeltaX( int value )
  {
    try
    {
      put( KEY_DX, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  public void setDeltaY( int value )
  {
    try
    {
      put( KEY_DY, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  public void setKeyCode( String value )
  {
    try
    {
      put( KEY_KEYCODE, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }
}
