//#preprocess
/*
 * AboutScreen.java
 *
 * MLH Software
 * Copyright 2010
 */

//#ifdef BLURTS
package com.mlhsoftware.ui.blurts;
//#endif 

//#ifdef SIMPLYREMINDME
package com.mlhsoftware.ui.SimplyRemindMe;
//#endif 

//#ifdef SIMPLYTASKS
package com.mlhsoftware.ui.SimplyTasks;
//#endif 

//#ifdef MLHKEY
package com.mlhsoftware.ui.MLHKey;
//#endif 

import com.mlhsoftware.ui.*;

import net.rim.device.api.ui.*;
import net.rim.device.api.ui.component.*;
import net.rim.device.api.system.Bitmap;

import net.rim.device.api.ui.container.*;
import net.rim.blackberry.api.browser.Browser;
import net.rim.blackberry.api.invoke.Invoke;
import net.rim.blackberry.api.invoke.MessageArguments;
import net.rim.device.api.system.CodeModuleManager;

import net.rim.device.api.system.DeviceInfo;

import net.rim.device.api.system.EventLogger;

public class AboutScreen extends MainScreen 
{
  private static final Bitmap ICON = Bitmap.getBitmapResource( "icon.png" );

//#ifdef MLHKEY
  static public boolean BETA = false;
  static public String APP_VERSION = "0.0.0.1";
  static protected String APP_NAME = "MLHKey";
  static protected String APP_URL = "http://m.mlhsoftware.com";
  static protected String HELP_URL = "http://www.mlhsoftware.com";
  static protected String TELL_A_FRIEND = "";
//#endif 


//#ifdef BLURTS
  static public boolean BETA = false;
  static public String APP_VERSION = "2.0.0.11";
  static protected String APP_NAME = "Blurts";
  static protected String APP_URL = "http://m.mlhsoftware.com";
  static protected String HELP_URL = "http://www.mlhsoftware.com/blurts";
  static protected String TELL_A_FRIEND = "Blurts lets you see who is calling without taking your phone out of your pocket and send SMS from your PC Desktop. Check it out at http://www.mlhsoftware.com";
//#endif 



  public AboutScreen() 
  {
    super( DEFAULT_MENU | DEFAULT_CLOSE | Manager.NO_VERTICAL_SCROLL );

    // Build the titlebar with Cancel and Save buttons
    TitlebarManager titlebarMgr = new TitlebarManager( "About " + APP_NAME, "Back", null );
    titlebarMgr.handleLeftBtn( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        onDoneBtn();
      }
    } );
    add( titlebarMgr );



    ForegroundManager foreground = new ForegroundManager();

    // About bar - App Icon, App Name, App Version, Vendor
    String appVersion = APP_VERSION;
    //#ifdef APPWORLD
    appVersion += " a";
    //#endif 
    if ( BETA == true )
    {
      appVersion += " Beta";
    }
    AboutbarManager aboutBar = new AboutbarManager( ICON, APP_NAME, appVersion );
    foreground.add( aboutBar );


    // Action buttons
    Bitmap caret = Bitmap.getBitmapResource( "greenArrow.png" );
    Bitmap tellIcon = Bitmap.getBitmapResource( "users_two_24.png" );
    Bitmap feedbackIcon = Bitmap.getBitmapResource( "comment_add_24.png" );
    Bitmap webIcon = Bitmap.getBitmapResource( "mlhIcon.png" );
    Bitmap twitterIcon = Bitmap.getBitmapResource( "twitter_24.png" );
    Bitmap systemIcon = Bitmap.getBitmapResource( "spanner_24.png" );
    Bitmap crackberryIcon = Bitmap.getBitmapResource( "crackberry_24.png" );
    Bitmap facebookIcon = Bitmap.getBitmapResource( "facebook_24.png" );
    Bitmap helpIcon = Bitmap.getBitmapResource( "helpIconBlack.png" );



    // Info Section
    ListStyleFieldSet buttonSet = new ListStyleFieldSet();

    ListStyleLabelField info = new ListStyleLabelField( "More Info" );
    buttonSet.add( info );

    ListStyleButtonField link = new ListStyleButtonField( systemIcon, "System info", caret );
    link.setChangeListener( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        onSystemInfo();
      }
    } );
    buttonSet.add( link );

    link = new ListStyleButtonField( helpIcon, "Help", caret );
    link.setChangeListener( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        onHelp();
      }
    } );
    buttonSet.add( link );

    link = new ListStyleButtonField( webIcon, "MLHSoftware.com", caret );
    link.setChangeListener( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        onMoreInfo();
      }
    } );
    buttonSet.add( link );
    foreground.add( buttonSet );

    // Share
    buttonSet = new ListStyleFieldSet();

    info = new ListStyleLabelField( "Share" );
    buttonSet.add( info );

    link = new ListStyleButtonField( tellIcon, "Tell a friend", caret );
    link.setChangeListener( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        onTellAFriend();
      }
    } );
    buttonSet.add( link );

    link = new ListStyleButtonField( twitterIcon, "Follow us on twitter", caret );
    link.setChangeListener( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        onTwitter();
      }
    } );
    buttonSet.add( link );

    link = new ListStyleButtonField( facebookIcon, "Fan us on Facebook", caret );
    link.setChangeListener( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        onFacebook();
      }
    } );
    buttonSet.add( link );
    foreground.add( buttonSet );

    // Feedback
    buttonSet = new ListStyleFieldSet();

    info = new ListStyleLabelField( "Feedback" );
    buttonSet.add( info );

    link = new ListStyleButtonField( feedbackIcon, "Email us", caret );
    link.setChangeListener( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        onFeedback();
      }
    } );
    buttonSet.add( link );

    link = new ListStyleButtonField( crackberryIcon, "User forum on CrackBerry", caret );
    link.setChangeListener( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        onCrackBerry();
      }
    } );
    buttonSet.add( link );


    foreground.add( buttonSet );


    add( foreground );

    addMenuItem( _debug );
    addMenuItem( _showLog );
  }

  private void onDoneBtn()
  {
    close();
  }

  private void onSystemInfo()
  {
    SystemInfoScreen dlg = new SystemInfoScreen( APP_NAME, APP_VERSION );
    UiApplication.getUiApplication().pushScreen( dlg );
  }

  private void onTellAFriend()
  {
    String arg = MessageArguments.ARG_NEW;
    String to = "";
    String subject = "Check out this Blackberry application: " + APP_NAME;
    String body = TELL_A_FRIEND;

    MessageArguments msgArg = new MessageArguments( arg, to, subject, body );
    Invoke.invokeApplication( Invoke.APP_TYPE_MESSAGES, msgArg );
  }

  private void onFeedback()
  {
    String key = "";
    ActivationKeyStore keyStore = ActivationKeyStore.GetInstance();
    if ( keyStore != null )
    {
      key = keyStore.getKey();
    }
    int deviceId = DeviceInfo.getDeviceId();
    String deviceIdText = java.lang.Integer.toHexString( deviceId );
    String pin = deviceIdText.toUpperCase();

    String arg = MessageArguments.ARG_NEW;
    String to = "feedback@mlhsoftware.com";
    String subject = "Feedback: " + APP_NAME;
    String body = "Tell us what you think about " + APP_NAME + ". \r\n" + 
                  "\r\n App: " + APP_NAME + 
                  "\r\n Key: " + key + 
                  "\r\n PIN: " + pin + 
                  "\r\n Version: " + APP_VERSION + 
                  "\r\n Device: " + DeviceInfo.getDeviceName() + 
                  "\r\n OS Version: " + CodeModuleManager.getModuleVersion( CodeModuleManager.getModuleHandleForObject( "" ) ) + 
                  "\r\n\r\n";

    MessageArguments msgArg = new MessageArguments( arg, to, subject, body );
    Invoke.invokeApplication( Invoke.APP_TYPE_MESSAGES, msgArg ); 
  }

  private void onMoreInfo()
  {
    Browser.getDefaultSession().displayPage( APP_URL );
  }

  private void onHelp()
  {
    String url = HELP_URL + "/?a=help&v=" + AboutScreen.APP_VERSION + "&b=" + DeviceInfo.getDeviceName();
    if ( AboutScreen.BETA )
    {
      url += "&beta=true";
    }
    Browser.getDefaultSession().displayPage( url );
  }

  private void onTwitter()
  {
    Browser.getDefaultSession().displayPage( "http://m.twitter.com/mlhsoftware" );
  }

  private void onFacebook()
  {
    Browser.getDefaultSession().displayPage( "http://m.facebook.com/pages/MLH-Software/137362215699" );
  }

  private void onCrackBerry()
  {
    Browser.getDefaultSession().displayPage( "http://forums.crackberry.com/f188/" );
  }

  private MenuItem _debug = new MenuItem( "Enable Debug Logging", 3001, 1003 )
  {
    public void run()
    {
      EventLogger.setMinimumLevel( EventLogger.DEBUG_INFO );
      Dialog.alert( "Debug Logging has been enabled" );
    }
  };

  private MenuItem _showLog = new MenuItem( "Open Event Logs", 3002, 1004 )
  {
    public void run()
    {
      EventLogger.startEventLogViewer();
    }
  };

}
