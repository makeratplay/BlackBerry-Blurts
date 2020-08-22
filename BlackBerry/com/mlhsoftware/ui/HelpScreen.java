//#preprocess
/*
 * HelpScreen.java
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

public class HelpScreen extends MainScreen 
{
  public HelpScreen( String title, String helpText ) 
  {
    super( DEFAULT_MENU | DEFAULT_CLOSE | Manager.NO_VERTICAL_SCROLL );

    // Build the titlebar with Cancel and Save buttons
    TitlebarManager titlebarMgr = new TitlebarManager( title, "Back", null );
    titlebarMgr.handleLeftBtn( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        onDoneBtn();
      }
    } );
    add( titlebarMgr );

    ForegroundManager foreground = new ForegroundManager();
    add( foreground );

    ListStyleFieldSet fieldSet = new ListStyleFieldSet();
    foreground.add( fieldSet );

    ListStyleRichTextlField info = new ListStyleRichTextlField( helpText );
    fieldSet.add( info );
  }


  private void onDoneBtn()
  {
    close();
  }

}
