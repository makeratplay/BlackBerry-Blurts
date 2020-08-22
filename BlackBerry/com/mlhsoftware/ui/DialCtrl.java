//#preprocess
/*
 * DialCtrl.java
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

import net.rim.device.api.system.Bitmap;
import net.rim.device.api.ui.Graphics;
import net.rim.device.api.ui.Field;
import java.util.Vector;

public class DialCtrl extends Field
{
  Vector m_strings;

  public DialCtrl( int width )
  {
    super( FOCUSABLE | EDITABLE );

    m_strings = new Vector();
  }

  public void addString( String text )
  {
    m_strings.addElement( text );
  }

  protected boolean navigationClick( int status, int time ) 
  {
    return super.navigationClick( status, time );
  } 

  protected boolean navigationMovement( int dx, int dy, int status, int time )
  {
    return super.navigationMovement( dx, dy, status, time );
  }
  

  protected boolean keyChar( char key, int status, int time )
  {
    return super.keyChar( key, status, time );
  }

  protected void paintBackground( Graphics g )
  {
  }

  public void paint( Graphics graphics )
  {
  }


  public boolean isFocusable()
  {
    return true;
  }

  public void layout( int width, int height )
  {
    // Calculate height.
    height = this.getFont().getHeight() * 5;

    setExtent( width, height );
  }
} 


