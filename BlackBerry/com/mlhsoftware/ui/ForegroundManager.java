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

//#ifdef MLHKEY
package com.mlhsoftware.ui.MLHKey;
//#endif 

import net.rim.device.api.ui.Graphics;

public class ForegroundManager extends NegativeMarginVerticalFieldManager
{
  public static final int BACKGROUND_COLOR = 0x00293439;
  //public static final int[] BACKGROUND_COLORS = { 0x0042454A, 0x0042454A, 0x00293439, 0x00293439 };  

  public ForegroundManager()
  {
    super( USE_ALL_HEIGHT | VERTICAL_SCROLL | VERTICAL_SCROLLBAR | USE_ALL_WIDTH );
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
