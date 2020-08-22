package com.mlhsoftware.Blurts;

import com.mlhsoftware.ui.blurts.*;

import net.rim.device.api.ui.*;
import net.rim.device.api.ui.component.*;
import net.rim.device.api.ui.container.*;
import net.rim.device.api.system.*;
import net.rim.device.api.system.EventLogger;

import net.rim.device.api.applicationcontrol.ApplicationPermissionsManager;
import net.rim.device.api.applicationcontrol.ApplicationPermissions;

import net.rim.device.api.system.RuntimeStore;
import java.util.*;
import net.rim.blackberry.api.homescreen.HomeScreen;

public class Blurts extends UiApplication implements ActivationScreen.KeyDlgListener
{
  
  public static final long LOGGER_ID = 0xf0569edab4ce20faL; //com.mlhsoftware.Blurts
  public static final long APP_ID = 0xf0569edab4ce20faL; //com.mlhsoftware.Blurts

  private StatusScreen m_mainScreen;
  private BlurtsThread m_BlurtsThread;
  static public DAOptionsProperties m_optionProperties = null;

  public String m_statusText;

  static Bitmap m_connectedIcon = null;
  static Bitmap m_disconnectedIcon = null;

  public static void main(String[] args)
  {
    boolean startup = false;
    EventLogger.register( LOGGER_ID, "Blurts", EventLogger.VIEWER_STRING );

    try
    {
      m_connectedIcon = Bitmap.getBitmapResource( "icon.png" );
    }
    catch ( Exception e )
    {
      String msg = "icon.png error: " + e.toString();
      EventLogger.logEvent( LOGGER_ID, msg.getBytes(), EventLogger.DEBUG_INFO );
    }

    try
    {
      m_disconnectedIcon = Bitmap.getBitmapResource( "icon_red.png" );
    }
    catch ( Exception e )
    {
      String msg = "icon_red.png error: " + e.toString();
      EventLogger.logEvent( LOGGER_ID, msg.getBytes(), EventLogger.DEBUG_INFO );
    }

    ApplicationManager appManager = ApplicationManager.getApplicationManager();
    if ( appManager != null )
    {
      while ( appManager.inStartup() )
      {
        try
        {
          Thread.sleep(15000);  // 15 seconds
        }
        catch( InterruptedException e )
        {
        }
      }
    }
    
    assertHasPermissions();

    //Check parameters to see if the application was entered
    //through the alternate application entry point.
    if ( args.length > 0 && args[0].equals( "autorun" ) )
    {
      EventLogger.logEvent( Blurts.LOGGER_ID, "autorun".getBytes(), EventLogger.DEBUG_INFO );
      DAOptionsProperties optionProperties = DAOptionsProperties.GetInstance();
      if ( optionProperties != null )
      {
        if ( optionProperties.autoStart() )
        {
          startup = true;
        }
      }
    }
    else
    {
      startup = true;
    }

    if ( startup )
    {
      //Entered by selecting the application icon on the ribbon.
      //Start the Blurts.
      Blurts app = null;
      RuntimeStore runtimeStore = RuntimeStore.getRuntimeStore();
      app = (Blurts)runtimeStore.get( APP_ID );
      if ( app == null )
      {
        app = new Blurts();
        synchronized ( runtimeStore )
        {
          runtimeStore.put( APP_ID, app );
        }
        app.enterEventDispatcher();
      }
      else
      {
        app.requestForeground(); 
      }
    }
    else
    {
      EventLogger.logEvent( Blurts.LOGGER_ID, "not pro".getBytes(), EventLogger.DEBUG_INFO );
    }
  }

  public Blurts()
  {
    m_BlurtsThread = null;
    m_optionProperties = DAOptionsProperties.GetInstance();

    //Initialize mainScreen.
    m_mainScreen = new StatusScreen( this );

    //Display the main screen
    pushScreen( m_mainScreen );

    start();

    invokeLater( new Runnable() { public void run() { checkHelpDlg(); } } );
  }

  public void promptForKey()
  {
    ActivationScreen activationScreen = new ActivationScreen( this );
    UiApplication.getUiApplication().pushScreen( activationScreen );
  }

  public void checkHelpDlg()
  {
    /*
    if ( !Blurts.m_optionProperties.everConnected() )
    {
      Dialog.ask( Dialog.D_OK, "Download Blurts desktop software from www.MLHSoftware.com" );
    }
     * */

    if ( betaExpired() )
    {
      Dialog.ask( Dialog.D_OK, "This beta version has expired. Go to www.MLHSoftware.com for the latest version." );
      try
      {
        RuntimeStore runtimeStore = RuntimeStore.getRuntimeStore();
        synchronized ( runtimeStore )
        {
          runtimeStore.remove( Blurts.APP_ID );
        }
      }
      catch ( Exception ie )
      {
        String msg = "RuntimeStore Error: " + ie.toString();
        EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      }
      System.exit( 0 );
    }

  }

  static public boolean betaExpired()
  {
    boolean retVal = false;
    /*
    long now = new Date().getTime();
    Calendar expiration = Calendar.getInstance();
    expiration.set( Calendar.YEAR, 2010 );
    expiration.set( Calendar.MONTH, Calendar.OCTOBER );
    expiration.set( Calendar.DAY_OF_MONTH, 1 );
    expiration.set( Calendar.HOUR, 11 );
    expiration.set( Calendar.MINUTE, 00 );
    if ( now > expiration.getTime().getTime() )
    {
      retVal = true;
    }
     * */
    return retVal;
  }

  public void KeyActivated()
  {
    m_mainScreen.removeUpgradeMenu();
  }

  public void DlgClosed()
  {
    /*
    m_optionProperties = DAOptionsProperties.fetch();
    if ( !ActivationKeyStore.isKeyValid() )
    {
      String key = m_optionProperties.getKey();
      if ( key != null && key.length() > 0 )
      {
        Dialog.ask( Dialog.D_OK, "Invalid key" );
      }
    }
    else
    {
      m_mainScreen.removeUpgradeMenu();
    }
     * */
  }

  public void setConnectedIcon()
  {
    try
    {
      HomeScreen.updateIcon( m_connectedIcon, 1 );
    }
    catch ( Exception ex )
    {
      String msg = "setConnectedIcon failed " + ex.toString();
      System.out.println( msg );
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.DEBUG_INFO );
    }
  }

  public void setDisconnectedIcon()
  {
    try
    {
      HomeScreen.updateIcon( m_disconnectedIcon, 1 );
    }
    catch ( Exception ex )
    {
      String msg = "setDisconnectedIcon failed " + ex.toString();
      System.out.println( msg );
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.DEBUG_INFO );
    }
  }

  public void start()
  {
    if ( m_BlurtsThread == null )
    {
      EventLogger.logEvent( Blurts.LOGGER_ID, "Started".getBytes(), EventLogger.DEBUG_INFO );
      m_statusText = "";
      m_BlurtsThread = new BlurtsThread(); // BlurtsThread.createInstance();
      if ( m_BlurtsThread != null )
      {
        m_mainScreen.setBlurtsThread( m_BlurtsThread );
        m_BlurtsThread.start();
      }
    }
  }

  // ASK FOR PERMISSIONS
  private static void assertHasPermissions()
  {
    if ( !DAOptionsProperties.isFirstRun() )
    {
      // only promt for permission on the first run.
      return;
    }

    // Capture the current state of permissions and check against the requirements.
    ApplicationPermissionsManager apm = ApplicationPermissionsManager.getInstance();
    ApplicationPermissions original = apm.getApplicationPermissions();
    ApplicationPermissions permRequest = new ApplicationPermissions();
    int[] permissions = original.getPermissionKeys();
    for ( int i = 0; i < permissions.length; i++ )
    {
      permRequest.addPermission( i );
    }
    apm.invokePermissionsRequest( permRequest );
  }
}

