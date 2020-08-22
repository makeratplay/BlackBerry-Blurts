//#preprocess
/*
 * ChkVersionScreen.java
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
//#endif 

//#ifdef SIMPLYTASKS
package com.mlhsoftware.ui.SimplyTasks;
//#endif 

//#ifdef MLHKEY
package com.mlhsoftware.ui.MLHKey;
import com.mlhsoftware.ui.MLHKey.*;
import com.mlhsoftware.webapi.MLHKey.*;
//#endif 


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

public class ChkVersionScreen extends MainScreen implements WebApiBase.WebAPICallback 
{
  public static interface DlgCallback
  {
    void ChkVersionScreenClosed();
  }

  private static final Bitmap ICON = Bitmap.getBitmapResource( "icon.png" );

  private String _udpateUrl;
  private String _newVersion;
  private ForegroundManager _foreground;
  private DlgCallback _uiCallBack;



  public ChkVersionScreen( String version, String url, DlgCallback uiCallBack ) 
  {
    super( DEFAULT_MENU | DEFAULT_CLOSE | Manager.NO_VERTICAL_SCROLL );

    _uiCallBack = uiCallBack;
    // Build the titlebar with Cancel and Save buttons
    TitlebarManager titlebarMgr = new TitlebarManager( ActivationKeyStore.APP_NAME + " Update", "Back", null );
    titlebarMgr.handleLeftBtn( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        onDoneBtn();
      }
    } );
    add( titlebarMgr );

    _foreground = new ForegroundManager();
    add( _foreground );

    if ( version == null )
    {
      Bitmap spinnerIcon = Bitmap.getBitmapResource( "spinner.png" );
      ListStyleButtonField checkBtn = new ListStyleButtonField( "Checking for updates...", null );
      checkBtn.setProgressAnimationInfo( spinnerIcon, 6 );
      checkBtn.startAnimation();
      
      ListStyleFieldSet fieldSet = new ListStyleFieldSet();
      fieldSet.add( checkBtn );
      _foreground.add( fieldSet );
      checkForUpdate();
    }
    else
    {
      _newVersion = version;
      _udpateUrl = url;
      addGetUpdateCtrl();
    }
  }

  public void close()
  {
    
    if ( _uiCallBack != null )
    {
      _uiCallBack.ChkVersionScreenClosed();
    }
    super.close();
  }

  private void addGetUpdateCtrl()
  {
    ListStyleFieldSet fieldSet = new ListStyleFieldSet();
    _foreground.add( fieldSet );
    
    ListStyleRichTextlField info = new ListStyleRichTextlField( "A new version of " + ActivationKeyStore.APP_NAME + " (" + _newVersion + ") is available. Press Upgrade to download new version."  );
    fieldSet.add( info );


    // Action buttons
    Bitmap caret = Bitmap.getBitmapResource( "greenArrow.png" );
    Bitmap downloadIcon = Bitmap.getBitmapResource( "download.png" );


    ListStyleButtonField link = new ListStyleButtonField( downloadIcon, "Upgrade", caret );
    link.setChangeListener( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        OnGetUpdate();
      }
    } );
    fieldSet.add( link );
  }

  private void onDoneBtn()
  {
    close();
  }
  
  public void checkForUpdate()
  {
    VersionCheck versionCheck = new VersionCheck();
    versionCheck.setAppName( ActivationKeyStore.APP_NAME );
    versionCheck.setVersion( AboutScreen.APP_VERSION );
    versionCheck.setBeta( AboutScreen.BETA );
    versionCheck.setCallback( this );
    versionCheck.process();
  }

  public void callComplete( boolean wasSuccess, Object obj )
  {

    _foreground.deleteAll();
    if ( obj instanceof VersionCheck )
    {
      if ( wasSuccess )
      {
        VersionCheck versionCheck = (VersionCheck)obj;
        if ( versionCheck.getNew() )
        {
          _udpateUrl = versionCheck.getUrl();
          _newVersion = versionCheck.getNewVersion();
          addGetUpdateCtrl();
        }
        else
        {
          ListStyleFieldSet fieldSet = new ListStyleFieldSet();
          ListStyleRichTextlField info = new ListStyleRichTextlField( "You have the most current version."  );
          fieldSet.add( info );
          _foreground.add( fieldSet );
        }
      }
      else
      {
        Dialog.alert( "Unable to connect to server to check for updates." );
      }
    }
  }

  private void OnGetUpdate()
  {
    Browser.getDefaultSession().displayPage( _udpateUrl );
  }
}
