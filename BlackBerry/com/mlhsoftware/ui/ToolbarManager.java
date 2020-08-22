//#preprocess
/*
 * ToolbarManager.java
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
import net.rim.device.api.system.Bitmap;
import net.rim.device.api.ui.component.*;

/**
 * 
 */
public class ToolbarManager extends Manager
{
  public static int[] BACKGROUND_COLORS = { 0x006C6C6A, 0x006C6C6A, 0x00ACAEAB, 0x00ACAEAB };

  public ToolbarManager()
  {
    super( USE_ALL_WIDTH );
  }

  // This field in this demo has a fixed height.
  public int getPreferredHeight() 
  { 
    return 5;
  }
	
  // This field in this demo has a fixed width.
  public int getPreferredWidth() 
  {
    return Graphics.getScreenWidth();
  }

  public void sublayout( int width, int height )
  {
    int maxHeight = 0;
    int xPos = 0;
    int rWidth = getPreferredWidth();
    int numFields = getFieldCount();
    int maxWidth = rWidth / numFields;

    for ( int i = 0; i < numFields; i++ )
    {
      Field field = getField( i );
      if ( field != null )
      {
        layoutChild( field, maxWidth, height );
        maxHeight = Math.max( maxHeight, field.getHeight() );
        setPositionChild( field, xPos, 0 );
        xPos += field.getWidth();
      }
    }

    setExtent( getPreferredWidth(), maxHeight );
  }

  protected void paintBackground( Graphics graphics )
  {
    super.paintBackground( graphics );

    int[] xPts = new int[] { 0, getWidth(), getWidth(), 0 };
    int[] yPts = new int[] { 0, 0, getHeight(), getHeight() };
    graphics.drawShadedFilledPath( xPts, yPts, null, BACKGROUND_COLORS, null );

    // top line
    graphics.setColor( 0x6F6F6F );
    graphics.drawLine( 0, 0, getWidth(), 0 );

    // bottom line
    graphics.setColor( 0x444645 );
    graphics.drawLine( 0, getHeight()-1, getWidth(), getHeight()-1 );

    
  }
}
