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


import net.rim.device.api.ui.*;
import net.rim.device.api.ui.component.*;
import net.rim.device.api.ui.container.*;
import net.rim.device.api.system.*;
import java.util.*;

//#ifdef V47
import net.rim.device.api.ui.VirtualKeyboard;
//#endif

public class SplashScreen extends MainScreen 
{
  private Screen next;
  private UiApplication application;
  private boolean bDismissed = false;

  private Bitmap SplashScreenImg = Bitmap.getBitmapResource("splashScreen.png");
  //private Bitmap SplashScreenTitle = null;
  //private Bitmap SplashScreenCopyRight = null;
   

  public SplashScreen(UiApplication ui, Screen next) 
  {
    super(Field.USE_ALL_HEIGHT | Field.FIELD_LEFT);
    this.application = ui;
    this.next = next;

    boolean bStorm = false;
    //#ifdef V47
    VirtualKeyboard vKeyboard = getVirtualKeyboard();
    if ( vKeyboard != null )
    {
      bStorm = true;
    }
    //#endif 

    /*
    int screenWidth = Graphics.getScreenWidth();
    if ( screenWidth < 320 && !bStorm )
    {
      SplashScreenImg = Bitmap.getBitmapResource( "splash_background_240.png" );
      SplashScreenTitle = Bitmap.getBitmapResource( "splash_title_240.png" );
      SplashScreenCopyRight = Bitmap.getBitmapResource( "splash_copyright_240.png" );
    }
    else if ( screenWidth < 480 && !bStorm )
    {
      SplashScreenImg = Bitmap.getBitmapResource( "splash_background_320.png" );
      SplashScreenTitle = Bitmap.getBitmapResource( "splash_title_320.png" );
      SplashScreenCopyRight = Bitmap.getBitmapResource( "splash_copyright_320.png" );
    }
    else
    {
      SplashScreenImg = Bitmap.getBitmapResource( "splash_background_480.png" );
      SplashScreenTitle = Bitmap.getBitmapResource( "splash_title_480.png" );
      SplashScreenCopyRight = Bitmap.getBitmapResource( "splash_copyright_480.png" );
    }
    */

    application.pushScreen(this);

    CountDown countDown = new CountDown();
    countDown.start();
  }
   
  protected void paint(Graphics graphics)
  {

    int screenWidth = Graphics.getScreenWidth();
    int screenHeight = Graphics.getScreenHeight();
    int bitmapWidth = SplashScreenImg.getWidth();
    int bitmapHeight = SplashScreenImg.getHeight();

    //int titleWidth = SplashScreenTitle.getWidth();
    //int titleHeight = SplashScreenTitle.getHeight();
    //int copyrightWidth = SplashScreenCopyRight.getWidth();
    //int copyrightHeight = SplashScreenCopyRight.getHeight();

    graphics.setColor( 0x000000 );
    graphics.fillRect( 0, 0, screenWidth, screenHeight );

    int x = (screenWidth - bitmapWidth) / 2;
    int y = (screenHeight - bitmapHeight) / 2;
    
    graphics.drawBitmap(x, y, bitmapWidth, bitmapHeight, SplashScreenImg, 0, 0);

    //graphics.drawBitmap( 0, 10, titleWidth, titleHeight, SplashScreenTitle, 0, 0 );
    //graphics.drawBitmap( screenWidth - copyrightWidth, screenHeight - copyrightHeight, copyrightWidth, copyrightHeight, SplashScreenCopyRight, 0, 0 );
  }   
   
   public void dismiss() 
   {
      if ( !bDismissed )
      {
        application.popScreen(this);
        application.pushScreen(next);
        bDismissed = true;
      }
   }
   
   private class CountDown extends Thread 
   {
      public void run() 
      {
        invalidate();
        setSleep( 3000 );

        application.invokeLater( new Runnable() { public void run() { dismiss(); } }  );
      }
      
      private void setSleep(int ms)
      {
        try
        {
          sleep(ms);
        }
        catch (InterruptedException e)
        {
        }         
      }
   }
   
   protected boolean navigationClick(int status, int time) 
   {
      dismiss();
      return true;
   }

  public boolean keyChar( char key, int status, int time )
  {
    //intercept the ESC and MENU key - exit the splash screen
    boolean retval = false;
    switch ( key )
    {
      case Characters.ENTER:
      case Characters.CONTROL_MENU:
      case Characters.ESCAPE:
      {
        dismiss();
        retval = true;
        break;
      }
    }
    return retval;
  }
} 


