//#preprocess
/*
 * LabeledSwitch.java
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

import net.rim.device.api.system.*;
import net.rim.device.api.ui.*;

public class ToolbarButton extends Field
{
  public static final int[] FOCUS_COLORS = { 0x009A9C6B, 0x009A9C6B, 0x00D3D1AD, 0x00D3D1AD };
  
  private static final int VMARGIN = 4;   // Space within component boundary
  private Bitmap _image;

  public ToolbarButton( Bitmap image )
  {
    super( Field.FOCUSABLE );
    _image = image;
  }

  public boolean isFocusable()
  {
    return true;
  }

  public int getPreferredWidth()
  {
    return _image.getWidth();
  }

  public int getPreferredHeight()
  {
    return _image.getHeight() + VMARGIN;
  }

  protected void layout( int width, int height )
  {
    setExtent( width, getPreferredHeight() );
  }

  public void paint( Graphics g )
  {
    // Image
    if ( g.isDrawingStyleSet( Graphics.DRAWSTYLE_FOCUS ) )
    {
     // g.drawBitmap( ( getWidth() / 2 ) - ( _imageFocus.getWidth() / 2 ), ( getHeight() / 2 ) - ( _imageFocus.getHeight() / 2 ), _imageFocus.getWidth(), _imageFocus.getHeight(), _imageFocus, 0, 0 );
    }

    g.drawBitmap( ( getWidth() / 2 ) - ( _image.getWidth() / 2 ), ( getHeight() / 2 ) - ( _image.getHeight() / 2 ), _image.getWidth(), _image.getHeight(), _image, 0, 0 );
  }

  public void paintBackground( Graphics g )
  {
    g.clear();
    super.paintBackground( g );

    int[] xPts = new int[] { 0, getWidth(), getWidth(), 0 };
    int[] yPts = new int[] { 0, 0, getHeight(), getHeight() };
    
    if ( g.isDrawingStyleSet( Graphics.DRAWSTYLE_FOCUS ) )
    {
      g.drawShadedFilledPath( xPts, yPts, null, FOCUS_COLORS, null );
    }
    else
    {
      g.drawShadedFilledPath( xPts, yPts, null, ToolbarManager.BACKGROUND_COLORS, null );
    }

    // top line
    g.setColor( 0x757776 );
    g.drawLine( 0, 0, getWidth(), 0 );

    // Right line
    g.setColor( 0x9EA09D );
    g.drawLine( getWidth()-2, 0, getWidth()-2, getHeight() );
    g.setColor( 0x626262 );
    g.drawLine( getWidth()-1, 0, getWidth()-1, getHeight() );

    // bottom line
    g.setColor( 0x444645 );
    g.drawLine( 0, getHeight()-1, getWidth(), getHeight()-1 );

  }

  protected void drawFocus( Graphics g, boolean on )
  {
    boolean oldDrawStyleFocus = g.isDrawingStyleSet( Graphics.DRAWSTYLE_FOCUS );
    try
    {
      if ( on )
      {
        g.setDrawingStyle( Graphics.DRAWSTYLE_FOCUS, true );
      }
      paintBackground( g );
      paint( g );
    }
    finally
    {
      g.setDrawingStyle( Graphics.DRAWSTYLE_FOCUS, oldDrawStyleFocus );
    }
  }

  protected boolean keyChar( char character, int status, int time )
  {
    if ( character == Characters.ENTER )
    {
      clickButton();
      return true;
    }
    return super.keyChar( character, status, time );
  }

  protected boolean navigationClick( int status, int time )
  {
    clickButton();
    return true;
  }

  protected boolean trackwheelClick( int status, int time )
  {
    clickButton();
    return true;
  }

  //#ifndef VER_4.1.0 | 4.0.0
  protected boolean invokeAction( int action )
  {
    switch ( action )
    {
      case ACTION_INVOKE:
      {
        clickButton();
        return true;
      }
    }
    return super.invokeAction( action );
  }
  //#endif        

  /**
     * A public way to click this button
     */
  public void clickButton()
  {
    fieldChangeNotify( 0 );
  }

  public void setDirty( boolean dirty )
  {
    // We never want to be dirty or muddy
  }

  public void setMuddy( boolean muddy )
  {
    // We never want to be dirty or muddy
  }
}
