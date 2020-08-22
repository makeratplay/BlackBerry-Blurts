
package com.mlhsoftware.Blurts;

import com.mlhsoftware.ui.blurts.*;
import net.rim.device.api.system.Application;
import net.rim.device.api.ui.UiApplication;
import net.rim.device.api.ui.container.MainScreen;
import net.rim.device.api.ui.Graphics;
import net.rim.device.api.system.Bitmap;
import net.rim.device.api.ui.MenuItem;
import net.rim.device.api.ui.component.Menu;
import java.util.*;
import java.lang.*;

import net.rim.device.api.ui.*;
import net.rim.device.api.ui.component.*;
import net.rim.device.api.ui.container.*;

import net.rim.device.api.system.EventLogger;
import net.rim.device.api.bluetooth.BluetoothSerialPort;
import net.rim.device.api.system.Characters;

import net.rim.device.api.system.RuntimeStore;

import com.mlhsoftware.webapi.blurts.*;
import net.rim.blackberry.api.invoke.Invoke;
import net.rim.blackberry.api.invoke.MessageArguments;
import net.rim.device.api.system.DeviceInfo;

/**
 * 
 */
class ControlScreen extends MainScreen 
{
  private TitlebarManager m_titlebarMgr;
  private BlurtsThread m_BlurtsThread;

  private BlurtsScreenManager _screen;
  private ForegroundManager _foreground = new ForegroundManager();
  private Statusbar m_statusBar;


  private boolean m_connected;

  ControlScreen() 
  {
    super( DEFAULT_MENU | DEFAULT_CLOSE | Manager.NO_VERTICAL_SCROLL );
    m_BlurtsThread = null;
    m_connected = false;
    m_statusBar = null;

    _screen = new BlurtsScreenManager();
    add( _screen );

    // Build the titlebar with Cancel and Save buttons
    BatteryLevel battery = new BatteryLevel();
    SignalLevel signal = new SignalLevel();

    String title = "Control PC";

    m_titlebarMgr = new TitlebarManager( title, battery, signal );
    _screen.add( m_titlebarMgr );


    _foreground = new ForegroundManager();
    _screen.add( _foreground );

    ListStyleFieldSet fieldSet = new ListStyleFieldSet();
    _foreground.add( fieldSet );

    ListStyleRichTextlField info = new ListStyleRichTextlField( "Trackball = PC Mouse\nClick = Left Mouse Btn\nEsc = Right Mouse Btn\nKeyboard = PC Keyboard\nVolume Up = Next PowerPoint slide\nVolume Down = Prev PowerPoint slide\nUse menu to close this screen" );
    fieldSet.add( info );

   // m_statusBar = new Statusbar();
   // _screen.add( m_statusBar );


    Trackball.setFilter(Trackball.FILTER_ACCELERATION);
    Trackball.setSensitivityX(50);
    Trackball.setSensitivityY(50);   
  }

  public void setBlurtsThread( BlurtsThread BlurtsThread )
  {
    m_BlurtsThread = BlurtsThread;
  }

  // The navigationMovement method is called by the event handler when the trackball is used.
  protected boolean navigationMovement( int dx, int dy, int status, int time )
  {
    if ( m_BlurtsThread != null )
    {
      InputAlert alert = new InputAlert();
      alert.setType( InputAlert.MOUSE );
      alert.setAction( InputAlert.MOVE );
      alert.setDeltaX( dx );
      alert.setDeltaY( dy );
      m_BlurtsThread.writeData( alert.toString(), null );
    }
    
    System.out.println( "dx: " + dx + "; dy: " + dy );
    
    return true;
  }

  protected boolean navigationClick( int status, int time )
  {
    if ( m_BlurtsThread != null )
    {
      InputAlert alert = new InputAlert();
      alert.setType( InputAlert.MOUSE );
      alert.setAction( InputAlert.LEFTCLICK );
      alert.setDeltaX( 0 );
      alert.setDeltaY( 0 );
      m_BlurtsThread.writeData( alert.toString(), null );
    }

    return true;
  }

  protected boolean keyDown( int keycode, int time )
  {
    boolean retVal = false;
    switch ( Keypad.key(keycode) )
    {
      case Keypad.KEY_CONVENIENCE_1: // Right  convenience key
      {
        if ( m_BlurtsThread != null )
        {
          InputAlert alert = new InputAlert();
          alert.setType( InputAlert.MOUSE );
          alert.setAction( InputAlert.RIGHTCLICK );
          alert.setDeltaX( 0 );
          alert.setDeltaY( 0 );
          m_BlurtsThread.writeData( alert.toString(), null );
          retVal = true;
        }
        break;
      }
      case Keypad.KEY_CONVENIENCE_2: // Left convenience key
      {
        break;
      }
    }

    if ( !retVal )
    {
      retVal = super.keyDown( keycode, time );
    }

    return retVal;
  }

  public boolean keyChar( char key, int status, int time )
  {
    boolean retVal = true;
    if ( m_BlurtsThread != null )
    {
      switch (key)
      {
        case Characters.ESCAPE:
        {
          InputAlert alert = new InputAlert();
          alert.setType( InputAlert.MOUSE );
          alert.setAction( InputAlert.RIGHTCLICK );
          alert.setDeltaX( 0 );
          alert.setDeltaY( 0 );
          m_BlurtsThread.writeData( alert.toString(), null );            
          break;
        }
        default:
        {
          InputAlert alert = new InputAlert();
          alert.setType( InputAlert.KEYBOARD );
          Character code = new Character(key);
          alert.setKeyCode( code.toString() );
          m_BlurtsThread.writeData( alert.toString(), null );            
          break;
        }
      }
    }
    return retVal;
  }

  protected boolean keyControl( char key, int status, int time )
  {
    boolean retVal = false;
    switch ( key )
    {
      case Characters.CONTROL_VOLUME_UP:
      {
        if ( m_BlurtsThread != null )
        {
          InputAlert alert = new InputAlert();
          alert.setType( InputAlert.MOUSE );
          alert.setAction( InputAlert.LEFTCLICK );
          alert.setDeltaX( 0 );
          alert.setDeltaY( 0 );
          m_BlurtsThread.writeData( alert.toString(), null );
          retVal = true;
        }
        break;
      }

      case Characters.CONTROL_VOLUME_DOWN:
      {
        // Powerpoint Previous slide shortcut
        if ( m_BlurtsThread != null )
        {
          InputAlert alert = new InputAlert();
          alert.setType( InputAlert.KEYBOARD );
          alert.setKeyCode( "p" );
          m_BlurtsThread.writeData( alert.toString(), null );
          retVal = true;
        }
        break;
      }

      /*    case Characters.CONTROL_MENU:
          {
            break;
          }    
          */
    }

    if ( !retVal )
    {
      retVal = super.keyControl( key, status, time );
    }

    return retVal;
  }
} 
