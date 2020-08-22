//#preprocess
/*
 * ForegroundManager.java
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

import net.rim.device.api.ui.Graphics;
import net.rim.device.api.ui.*;

public class BlurtsScreenManager extends NegativeMarginVerticalFieldManager
{
  public static final int BACKGROUND_COLOR = 0x00293439;
  //public static final int[] BACKGROUND_COLORS = { 0x0042454A, 0x0042454A, 0x00293439, 0x00293439 };  

  public BlurtsScreenManager()
  {
    super( USE_ALL_HEIGHT | VERTICAL_SCROLL | VERTICAL_SCROLLBAR | USE_ALL_WIDTH );
  }

  protected void sublayout( int width, int height )
  {
    int numFields = getFieldCount();

    int remainingHeight = Graphics.getScreenHeight();
    int bodyTop = 0;

    // Titlebar
    if ( numFields > 0 )
    {
      Field titlebarField = getField( 0 );
      if ( titlebarField != null )
      {
        layoutChild( titlebarField, width, remainingHeight );
        setPositionChild( titlebarField, 0, 0 );
        bodyTop = titlebarField.getHeight();
        remainingHeight -= titlebarField.getHeight();
      }
    }

    /* 
    // Toolbar
    if ( numFields > 1 )
    {
      Field toolbarField = getField( 1 );
      if ( toolbarField != null )
      {
        layoutChild( toolbarField, width, remainingHeight );
        setPositionChild( toolbarField, 0, bodyTop );
        bodyTop += toolbarField.getHeight();
        remainingHeight -= toolbarField.getHeight();
      }
    }
     * */

    // Statusbar
    if ( numFields > 2 )
    {
      Field statusbarField = getField( 2 );
      if ( statusbarField != null )
      {
        layoutChild( statusbarField, width, remainingHeight );
        setPositionChild( statusbarField, 0, Graphics.getScreenHeight() - statusbarField.getHeight() );
        remainingHeight -= statusbarField.getHeight();
      }
    }

    // Main screen
    if ( numFields > 1 )
    {
      Field mainField = getField( 1 );
      if ( mainField != null )
      {
        layoutChild( mainField, width, remainingHeight );
        setPositionChild( mainField, 0, bodyTop );
      }
    }
    setExtent( width, Graphics.getScreenHeight() );
  }

  protected void paintBackground( Graphics g )
  {
    int oldColor = g.getColor();
    try
    {
      /*
      int[] yInds = new int[] { 0, 0, getHeight(), getHeight() };
      int[] xInds = new int[] { 0, getWidth(), getWidth(), 0 };
      g.drawShadedFilledPath( xInds, yInds, null, BACKGROUND_COLORS, null );
      */

      g.setColor( BACKGROUND_COLOR );
      g.fillRect( 0, getVerticalScroll(), getWidth(), getHeight() );
    }
    finally
    {
      g.setColor( oldColor );
    }
  }
}
