//#preprocess
/*
 * ListStyleField.java
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
import net.rim.device.api.ui.component.*;

public class StatusLabelField extends Field
{
  
  // Layout values
  private static final int CURVE_X = 12; // X-axis inset of curve
  private static final int CURVE_Y = 12; // Y-axis inset of curve
  private static final int VMARGIN = 4;   // Space within component boundary
  private static final int HMARGIN = 24;   // Space within component boundary
  private static final int FONT_SIZE = 16;   // Space within component boundary
	
  // Static colors
  private static final int BORDER_COLOR = 0x333333; 
	
  // Point types array for rounded rectangle. Each point type
  // corresponds to one of the colors in the colors array. The
  // space marks the division between points on the top half of
  // the rectangle and those on the bottom.
  private static final byte[] PATH_POINT_TYPES = {
    Graphics.CURVEDPATH_END_POINT, 
    Graphics.CURVEDPATH_QUADRATIC_BEZIER_CONTROL_POINT,
    Graphics.CURVEDPATH_END_POINT, 
    Graphics.CURVEDPATH_END_POINT, 
    Graphics.CURVEDPATH_QUADRATIC_BEZIER_CONTROL_POINT,
    Graphics.CURVEDPATH_END_POINT, 
		
    Graphics.CURVEDPATH_END_POINT, 
    Graphics.CURVEDPATH_QUADRATIC_BEZIER_CONTROL_POINT,
    Graphics.CURVEDPATH_END_POINT, 
    Graphics.CURVEDPATH_END_POINT, 
    Graphics.CURVEDPATH_QUADRATIC_BEZIER_CONTROL_POINT,
    Graphics.CURVEDPATH_END_POINT, 
  };
	
  // Colors array for rounded rectangle gradient. Each color corresponds
  // to one of the points in the point types array. Top light, bottom black.
  private static final int[] PATH_GRADIENT = {
    0xAAAAAA, 0xAAAAAA, 0xAAAAAA, 0xAAAAAA, 0xAAAAAA, 0xAAAAAA,
		
    0x000000, 0x000000, 0x000000, 0x000000, 0x000000, 0x000000
  };

  private String m_text;
  private int[] m_backColors = { 0x008E8E8E, 0x008E8E8E, 0x00F6FBFF, 0x00F6FBFF };
  private int m_fontColor = Color.BLACK;


  public StatusLabelField( String text )
  {
    super( FIELD_HCENTER | FIELD_VCENTER | READONLY );
    m_text = text;
  }

  public void setText( String text )
  {
    m_text = text;
  }

  public void setBackgroundColors( int[] cols )
  {
    m_backColors = cols;
  }

  public void setFontColor( int color )
  {
    m_fontColor = color;
  }

  // This field in this demo has a fixed height.
  public int getPreferredHeight() 
  { 
    Font font = Font.getDefault().derive( Font.BOLD, FONT_SIZE );
    return font.getHeight() + VMARGIN;
  }
	
  // This field in this demo has a fixed width.
  public int getPreferredWidth() 
  {
    Font font = Font.getDefault().derive( Font.BOLD, FONT_SIZE );
    return font.getAdvance( m_text ) + HMARGIN;
  }


  public void layout( int width, int height )
  {
    setExtent( getPreferredWidth(), getPreferredHeight() );
  }


  public void paint( Graphics g )
  {
    int oldColour = g.getColor();
    try
    {
      Font font = Font.getDefault().derive( Font.BOLD, 16 );
      g.setFont( font );
      g.setColor( m_fontColor );
      int strHeight = font.getHeight();
      int strWidth = font.getAdvance( m_text );
      g.drawText( m_text, ( getWidth() / 2 ) - ( strWidth / 2 ), ( getHeight() / 2 ) - ( strHeight / 2 ) );
    }
    finally
    {
      g.setColor( oldColour );
    }
  }

  protected void paintBackground( Graphics g )
  {
    super.paintBackground( g );

    // Drawing within our margin.
    int width = getWidth();
    int height = getHeight();
		
    // Compute paths for the rounded rectangle. The 1st point (0) is on the left
    // side, right where the curve in the top left corner starts. So the top left
    // corner is point 1. These points correspond to our static arrays.
    int[] xPts = { 0, width, width, 0 };
    int[] yPts = { 0, 0, height, height };
		
    // Draw the gradient fill.
    g.drawShadedFilledPath(xPts, yPts, null, m_backColors, null);


    // Draw a rounded rectangle for the outline.
    // I think that drawRoundRect looks better than drawPathOutline.
    g.setColor(BORDER_COLOR);
    //g.drawRoundRect(0, 0, width, height, CURVE_X * 2, CURVE_Y * 2);
    g.drawPathOutline(xPts, yPts, null, null, true );
  }
}



