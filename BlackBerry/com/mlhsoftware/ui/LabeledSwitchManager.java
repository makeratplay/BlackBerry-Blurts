//#preprocess
/*
 * LabeledSwitchManager.java
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

/**
 * 
 */
public class LabeledSwitchManager extends ListStyleManager
{
  public static final Bitmap switch_left = Bitmap.getBitmapResource( "switch_left.png" );
  public static final Bitmap switch_right = Bitmap.getBitmapResource( "switch_right.png" );
  public static final Bitmap switch_left_focus = Bitmap.getBitmapResource( "switch_left_focus.png" );
  public static final Bitmap switch_right_focus = Bitmap.getBitmapResource( "switch_right_focus.png" );


  private LabeledSwitch _switchField;

  public LabeledSwitchManager( String labelText )
  {
    super( USE_ALL_WIDTH );

    setFont( ListStyleField.FONT_LEFT_LABLE );
    _switchField = new LabeledSwitch( switch_left, switch_right, switch_left_focus, switch_right_focus, "on", "off", true );
    _switchField.setFont( ListStyleField.FONT_RIGHT_LABLE );

    LabelField label = new LabelField( labelText, DrawStyle.ELLIPSIS );
    label.setFont( ListStyleField.FONT_LEFT_LABLE );
    add( label );
    add( _switchField );
  }

  public void setOn( boolean on )
  {
    _switchField.setOn( on );
  }

  public boolean getOnState()
  {
    return _switchField.getOnState();
  }

  public void setEditable( boolean editable )
  {
    _switchField.setEditable( editable );
  }

  protected void sublayout( int width, int height )
  {
    int finalVPadding = getVPadding();
    int topPos = getTopPos();

    int maxHeight = 0;


    Field label = getField( 0 );
    Field swtch = getField( 1 );

    layoutChild( swtch, width - ( HPADDING * 4 ), height - finalVPadding );
    maxHeight = Math.max( maxHeight, swtch.getHeight() + finalVPadding );

    layoutChild( label, width - ( ( HPADDING * 4 ) + swtch.getWidth()), height - finalVPadding );
    maxHeight = Math.max( maxHeight, label.getHeight() + finalVPadding );



    setPositionChild( label, HPADDING * 2, ( maxHeight / 2 ) - (label.getHeight() / 2) );
    setPositionChild( swtch, width - ( swtch.getWidth() + ( HPADDING * 2 ) ), topPos );

    setExtent( width, maxHeight );
  }
}
