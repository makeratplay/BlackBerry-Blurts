//#preprocess
/*
 * ActivationScreen.java
 *
 * MLH Software
 * Copyright 2010
 */

//#ifdef BLURTS
package com.mlhsoftware.ui.blurts;
import com.mlhsoftware.ui.blurts.*;
import com.mlhsoftware.webapi.blurts.*;
//#endif 

//#ifdef SIMPLYREMINDME
package com.mlhsoftware.ui.SimplyRemindMe;
import com.mlhsoftware.ui.SimplyRemindMe.*;
import com.mlhsoftware.webapi.SimplyRemindMe.*;
//#endif 

//#ifdef SIMPLYTASKS
package com.mlhsoftware.ui.SimplyTasks;
import com.mlhsoftware.ui.SimplyTasks.*;
import com.mlhsoftware.webapi.SimplyTasks.*;
//#endif 

//#ifdef MLHKEY
package com.mlhsoftware.ui.MLHKey;
import com.mlhsoftware.webapi.MLHKey.*;
//#endif 

import net.rim.device.api.ui.*;
import net.rim.device.api.ui.component.*;
import net.rim.device.api.ui.container.*;
import net.rim.device.api.ui.container.PopupScreen;
import net.rim.device.api.notification.NotificationsManager;
import net.rim.device.api.system.Characters;
import net.rim.device.api.system.ControlledAccessException;

import net.rim.device.api.system.DeviceInfo;
import net.rim.device.api.crypto.MD5Digest;
import net.rim.device.api.system.*;
import net.rim.device.api.util.*;

import net.rim.device.api.ui.MenuItem;

import java.util.*;

import net.rim.blackberry.api.phone.phonelogs.PhoneCallLogID;
import net.rim.blackberry.api.phone.phonelogs.PhoneCallLog;
import net.rim.blackberry.api.phone.phonelogs.PhoneCallLogID;
import net.rim.blackberry.api.phone.phonelogs.PhoneLogs;

import net.rim.device.api.system.EventLogger;
import net.rim.blackberry.api.browser.Browser;




public class ActivationScreen extends MainScreen implements WebApiBase.WebAPICallback
{

  public interface KeyDlgListener
  {
    // event dispatch methods
    public void DlgClosed();
    public void KeyActivated();
  }
  

  private EditFieldManager _keyField;
  private ListStyleButtonField _fetchBtn;
  KeyDlgListener _dlgListener;

  public ActivationScreen( KeyDlgListener dlgListener )
  {
    super( DEFAULT_MENU | DEFAULT_CLOSE | Manager.NO_VERTICAL_SCROLL );

    _dlgListener = dlgListener;

    // Build the titlebar with Cancel and Save buttons
    TitlebarManager titlebarMgr = new TitlebarManager( "Activate " + ActivationKeyStore.APP_NAME, "Cancel", "Done" );
    titlebarMgr.handleLeftBtn( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context ) { OnCancel(); }
    } );

    titlebarMgr.handleRightBtn( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context ) { OnAcceptCode(); }
    } );

    add( titlebarMgr );


    ForegroundManager foreground = new ForegroundManager();
    add( foreground );

    ListStyleFieldSet buttonSet = new ListStyleFieldSet();
    foreground.add( buttonSet );


    ListStyleLabelField info = new ListStyleLabelField( "Enter key to unlock full version" );
    buttonSet.add( info );

    _keyField = new EditFieldManager( "", "Enter key here", 6, 0 );
    buttonSet.add( _keyField );


    //info = new ListStyleLabelField( null, "Your PIN", ActivationKeyStore.getPin() );
    //buttonSet.add( info );


    // Action Buttons Section
    Bitmap caret = Bitmap.getBitmapResource( "greenArrow.png" );
    Bitmap buyIcon = Bitmap.getBitmapResource( "buyIcon.png" );
    Bitmap helpIcon = Bitmap.getBitmapResource( "helpIconBlack.png" );
    Bitmap downloadIcon = Bitmap.getBitmapResource( "download.png" );
    Bitmap spinnerIcon = Bitmap.getBitmapResource( "spinner.png" );

    buttonSet = new ListStyleFieldSet();

    info = new ListStyleLabelField( "Actions" );
    buttonSet.add( info );

    ListStyleButtonField link = new ListStyleButtonField( buyIcon, "Buy key", caret );
    link.setChangeListener( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        OnBuy();
      }
    } );
    buttonSet.add( link );

    _fetchBtn = new ListStyleButtonField( downloadIcon, "Fetch key for PIN: " + ActivationKeyStore.getPin(), caret );
    _fetchBtn.setProgressAnimationInfo( spinnerIcon, 6 );
    _fetchBtn.setChangeListener( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        onFetchKey();
      }
    } );
    buttonSet.add( _fetchBtn );

    link = new ListStyleButtonField( helpIcon, "Help", caret );
    link.setChangeListener( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        onHelp();
      }
    } );
    buttonSet.add( link );


    foreground.add( buttonSet );


  }

  public void makeMenu( Menu menu, int instance )
  {
    if ( instance == Menu.INSTANCE_DEFAULT )
    {
      menu.add( _accept );
      menu.add( _cancel );
      menu.add( _buy );
    }

    super.makeMenu( menu, instance );
  }

  private void OnAcceptCode()
  {
    if ( _keyField.toString().length() == 0 )
    {
      Dialog.alert( "Please enter your key" );
      _keyField.setFocus();
    }
    else
    {
      ActivationKeyStore keyStore = ActivationKeyStore.GetInstance();
      if ( keyStore != null )
      {
        keyStore.setKey( _keyField.toString() );
        if ( keyStore.isKeyValid() )
        {
          //SimplySolitaire._session.tagEvent( "Activated" );
          keyStore.save();
          _dlgListener.KeyActivated();
          Dialog.alert( "Thank you for your purchase. Your software has been activated." );
          close();
          _dlgListener.DlgClosed();
        }
        else
        {
          keyStore.setKey( "" );
          Dialog.alert( "The key you entered is not valid for BlackBerry PIN: " + ActivationKeyStore.getPin() );
        }
      }
      else
      {
        String msg = "Activation Key is null";
        EventLogger.logEvent( ActivationKeyStore.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      }
    }
  }

  private void OnCancel()
  {
    close();
  }

  private void OnBuy()
  {
    String url = ActivationKeyStore.getBuyUrl();
    Browser.getDefaultSession().displayPage( url );
  }

  private void onHelp()
  {
    String helpText;
    helpText = "To unlock the full version of " + ActivationKeyStore.APP_NAME + ", you need to purchase and enter your activation key.\n" +
               "You can either type in your 6 character activation key or press the Fetch key button to retrieve your purchased key from our web server.\n" +
               "The activation key is based on the PIN for your BlackBerry.\n" +
               "When purchasing an activation key you must provide your BlackBerry PIN or you will not receive a valid key.\n" +
               "If you replace your BlackBerry with a new BlackBerry, you will need to get a new activation key.\n" +
               "You can transfer your activation key for free as long as you are no longer using your old BlackBerry.\n" +
               "Visit www.MLHSoftware.com for more info.";
    HelpScreen dlg = new HelpScreen( "Activation Help", helpText );
    UiApplication.getUiApplication().pushScreen( dlg );
  }

  private void onFetchKey()
  {
    _fetchBtn.startAnimation();
    FetchKey fetchKey = new FetchKey();
    fetchKey.setAppName( ActivationKeyStore.APP_NAME );
    fetchKey.setDeviceId( ActivationKeyStore.getPin() );
    fetchKey.setCallback( this );
    fetchKey.process();
  }

  public void callComplete( boolean wasSuccess, Object obj )
  {
    _fetchBtn.stopAnimation();
    if ( obj instanceof FetchKey )
    {
      if ( wasSuccess )
      {
        FetchKey fetchKey = (FetchKey)obj;
        String key = fetchKey.getResultCode();
        if ( key != "" )
        {
          _keyField.setText( key );
          OnAcceptCode();
        }
        else
        {
          Dialog.alert( "Unable to locate key. Make sure you have purchased a key for this BlackBerry PIN: " + ActivationKeyStore.getPin() );
        }
      }
      else
      {
        Dialog.alert( "Unable to connect to server to fetch key." );
      }
    }
  }


  private MenuItem _accept = new MenuItem( "Done", 80, 80 )
  {
    public void run()
    {
      OnAcceptCode();
    }
  };

  private MenuItem _cancel = new MenuItem( "Cancel", 80, 80 )
  {
    public void run()
    {
      OnCancel();
    }
  };

  private MenuItem _buy = new MenuItem( "Buy", 80, 80 )
  {
    public void run()
    {
      OnBuy();
    }
  };

  public boolean keyChar( char key, int status, int time )
  {
    if ( key == net.rim.device.api.system.Characters.ENTER )
    {
      OnAcceptCode();
    }
    return super.keyChar( key, status, time );
  }
}
