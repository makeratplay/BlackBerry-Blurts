//#preprocess
/*
 * SystemInfoScreen.java
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
import net.rim.device.api.system.RadioInfo;

import net.rim.device.api.system.EventLogger;

public class SystemInfoScreen extends MainScreen 
{
  public SystemInfoScreen( String appName, String  version ) 
  {
    super( DEFAULT_MENU | DEFAULT_CLOSE | Manager.NO_VERTICAL_SCROLL );

    // Build the titlebar with Cancel and Save buttons
    TitlebarManager titlebarMgr = new TitlebarManager( "System Info", "Back", null );
    titlebarMgr.handleLeftBtn( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        onDoneBtn();
      }
    } );
    add( titlebarMgr );

    ForegroundManager foreground = new ForegroundManager();

    ListStyleFieldSet infoSet = new ListStyleFieldSet();

    String deviceName = DeviceInfo.getDeviceName();
    String deviceOS = CodeModuleManager.getModuleVersion( CodeModuleManager.getModuleHandleForObject( "" ) );

    ListStyleLabelField info = new ListStyleLabelField( "BlackBerry Info" );
    infoSet.add( info );

    info = new ListStyleLabelField( null, "Model", deviceName );
    infoSet.add( info );

    info = new ListStyleLabelField( null, "Version", deviceOS );
    infoSet.add( info );

    int deviceId = DeviceInfo.getDeviceId();
    String deviceIdText = java.lang.Integer.toHexString( deviceId );
    String pin = deviceIdText.toUpperCase();
    info = new ListStyleLabelField( null, "PIN", pin );
    infoSet.add( info );

    String battery = DeviceInfo.getBatteryLevel() + "%";
    info = new ListStyleLabelField( null, "Battery", battery );
    infoSet.add( info );

    String signal = RadioInfo.getSignalLevel() + " dBm";
    info = new ListStyleLabelField( null, "Signal", signal );
    infoSet.add( info );


    foreground.add( infoSet );

    infoSet = new ListStyleFieldSet();
    info = new ListStyleLabelField( appName + " Info" );
    infoSet.add( info );

    info = new ListStyleLabelField( null, "Version", version );
    infoSet.add( info );

    String key = "";
    ActivationKeyStore keyStore = ActivationKeyStore.GetInstance();
    if ( keyStore != null )
    {
      key = keyStore.getKey();
    }

    info = new ListStyleLabelField( null, "Key", key );
    infoSet.add( info );

    foreground.add( infoSet );

    add( foreground );

    addMenuItem( _debug );
    addMenuItem( _showLog );
  }


  private void onDoneBtn()
  {
    close();
  }


  private MenuItem _close = new MenuItem( "Close", 1, 1 )
  {
    public void run()
    {
      close();
    }
  };

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
