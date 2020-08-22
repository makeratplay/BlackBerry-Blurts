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


public abstract class ListStyleManager extends Manager
{
  public static final int DRAWPOSITION_TOP = 0;
  public static final int DRAWPOSITION_BOTTOM = 1;
  public static final int DRAWPOSITION_MIDDLE = 2;
  public static final int DRAWPOSITION_SINGLE = 3;
  public static final int DRAWPOSITION_HEADER = 4;

  protected static final int CORNER_RADIUS = 18;

  public static final int VPADDING = Display.getWidth() <= 320 ? 6 : 10;
  protected static final int HPADDING = Display.getWidth() <= 320 ? 6 : 10;


  protected static final int COLOR_INNER_BACKGROUND       = 0xFFFFFF;
  protected static final int COLOR_INNER_BACKGROUND_FOCUS = 0x186DEF;
  protected static final int COLOR_BORDER                 = 0xBBBBBB;

  protected int _drawPosition = -1;

  public ListStyleManager( long style )
  {
    super( style );
  }

  /**
   * DRAWPOSITION_TOP | DRAWPOSITION_BOTTOM | DRAWPOSITION_MIDDLE
   * Determins how the field is drawn (borders)
   * If none is set, then no borders are drawn
   */
  public void setDrawPosition( int drawPosition )
  {
    _drawPosition = drawPosition;
  }


  protected void paintBackground( Graphics g )
  {
   int oldColor = g.getColor();

    if ( _drawPosition < 0 )
    {
      // it's like a list field, let the default background be drawn
      super.paintBackground( g );
      return;
    }

    int oldColour = g.getColor();

    // paint outer background color
    g.setColor( ForegroundManager.BACKGROUND_COLOR );
    g.fillRect( 0, getVerticalScroll(), getWidth(), getHeight() );


    int background = g.isDrawingStyleSet( Graphics.DRAWSTYLE_FOCUS ) ? COLOR_INNER_BACKGROUND_FOCUS : COLOR_INNER_BACKGROUND;
    try
    {
      switch ( _drawPosition )
      {
        case DRAWPOSITION_TOP:
        {
          g.setColor( background );
          g.fillRoundRect( HPADDING, VPADDING, getWidth() - ( HPADDING * 2 ), getHeight() + CORNER_RADIUS, CORNER_RADIUS, CORNER_RADIUS );
          g.setColor( COLOR_BORDER );
          g.drawRoundRect( HPADDING, VPADDING, getWidth() - ( HPADDING * 2 ), getHeight() + CORNER_RADIUS, CORNER_RADIUS, CORNER_RADIUS );
          g.drawLine( HPADDING, getHeight() - 1, getWidth() - HPADDING, getHeight() - 1 );
          break;
        }
        case DRAWPOSITION_BOTTOM:
        {
          g.setColor( background );
          g.fillRoundRect( HPADDING, -(CORNER_RADIUS + VPADDING), getWidth() - ( HPADDING * 2 ), getHeight() + CORNER_RADIUS, CORNER_RADIUS, CORNER_RADIUS );
          g.setColor( COLOR_BORDER );
          g.drawRoundRect( HPADDING, -( CORNER_RADIUS + VPADDING ), getWidth() - ( HPADDING * 2 ), getHeight() + CORNER_RADIUS, CORNER_RADIUS, CORNER_RADIUS );
          break;
        }
        case DRAWPOSITION_MIDDLE:
        {
          g.setColor( background );
          g.fillRoundRect( HPADDING, -CORNER_RADIUS, getWidth() - ( HPADDING * 2 ), getHeight() + 2 * CORNER_RADIUS, CORNER_RADIUS, CORNER_RADIUS );
          g.setColor( COLOR_BORDER );
          g.drawRoundRect( HPADDING, -CORNER_RADIUS, getWidth() - ( HPADDING * 2 ), getHeight() + 2 * CORNER_RADIUS, CORNER_RADIUS, CORNER_RADIUS );
          g.drawLine( HPADDING, getHeight() - 1, getWidth() - HPADDING, getHeight() - 1 );
          break;
        }
        case DRAWPOSITION_SINGLE:
        default:
        {
          g.setColor( background );
          g.fillRoundRect( HPADDING, VPADDING, getWidth() - ( HPADDING * 2 ), getHeight() - ( VPADDING * 2 ), CORNER_RADIUS, CORNER_RADIUS );
          g.setColor( COLOR_BORDER );
          g.drawRoundRect( HPADDING, VPADDING, getWidth() - ( HPADDING * 2 ), getHeight() - ( VPADDING * 2 ), CORNER_RADIUS, CORNER_RADIUS );
          break;
        }
      }
    }
    finally
    {
      g.setColor( oldColour );
    }
  }

  protected void drawFocus( Graphics g, boolean on )
  {
    if ( _drawPosition < 0 )
    {
      super.drawFocus( g, on );
    }
    else
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
  }

 protected int getVPadding()
  {
    int vPadding = 0;
    switch ( _drawPosition )
    {
      case DRAWPOSITION_TOP:
      {
        vPadding = VPADDING * 3;
        break;
      }
      case DRAWPOSITION_BOTTOM:
      {
        vPadding = VPADDING * 3;
        break;
      }
      case DRAWPOSITION_MIDDLE:
      {
        vPadding = VPADDING * 2;
        break;
      }
      case DRAWPOSITION_SINGLE:
      default:
      {
        vPadding = VPADDING * 4;
        break;
      }
    }
    return vPadding;
  }

  protected int getTopPos()
  {
    int topPos = 0;
    switch ( _drawPosition )
    {
      case DRAWPOSITION_TOP:
      {
        topPos = VPADDING * 2;
        break;
      }
      case DRAWPOSITION_BOTTOM:
      {
        topPos = VPADDING;
        break;
      }
      case DRAWPOSITION_MIDDLE:
      {
        topPos = VPADDING;
        break;
      }
      case DRAWPOSITION_SINGLE:
      default:
      {
        topPos = VPADDING * 2;
        break;
      }
    }
    return topPos;
  }

  public void setDirty( boolean dirty ) { }
  public void setMuddy( boolean muddy ) { }
}



