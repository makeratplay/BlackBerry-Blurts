//#preprocess
/*
 * ListStyleLabelField.java
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


public class ListStyleLabelField extends ListStyleField
{

  private Bitmap _leftIcon;
  
  private int _targetHeight;
  private int _rightOffset;
  private int _leftOffset;
  private int _labelHeight;
  private int _topText;

  private String _leftLabel = null;
  private String _rightLabel = null;

  private int _leftColor = COLOR_LEFT_LABEL_FONT;

  private boolean _header = false;

  public ListStyleLabelField( String centerLabel )
  {
    super( USE_ALL_WIDTH | Field.NON_FOCUSABLE );

    _leftLabel = centerLabel;
    _rightLabel = null;
    _leftIcon = null;
    _header = true;
    _leftColor = COLOR_HEADER_FONT;
  }


  public ListStyleLabelField( Bitmap icon, String leftLabel, String rightLabel )
  {
    super( USE_ALL_WIDTH | Field.FOCUSABLE );

    _leftLabel = leftLabel;
    _rightLabel= rightLabel;
    _leftIcon = icon;
    _header = false;
    _leftColor = COLOR_LEFT_LABEL_FONT;
  }

  public void setRightText( String text )
  {
    _rightLabel= text;
  }

  //protected void sublayout( int width, int height )
  public void layout( int width, int height )
  {
    int labelHeight = ( FONT_LEFT_LABLE.getHeight() * 3 ) / 2 + ( 2 * VPADDING ); 
    if ( _header )
    {
      setDrawPosition( DRAWPOSITION_HEADER );
      labelHeight = FONT_LEFT_LABLE.getHeight() + ( 2 * VPADDING ); 
    }
    
    int iconHeight = 0;
    if ( _leftIcon != null )
    {
      iconHeight = _leftIcon.getHeight() + ( 2 * VPADDING ); 
    }

    _targetHeight = Math.max( labelHeight, iconHeight );

    _leftOffset = HPADDING * 2;
    if ( _leftIcon != null )
    {
      _leftOffset += _leftIcon.getWidth() + HPADDING;
    }

    if ( _header )
    {
      int labelWidth = FONT_LEFT_LABLE.getAdvance( _leftLabel );
      _leftOffset = ( width / 2 ) - ( labelWidth / 2 );
    }

    _rightOffset = HPADDING * 2;
    if ( _rightLabel != null )
    {
      _rightOffset += FONT_RIGHT_LABLE.getAdvance( _rightLabel );
    }
    _rightOffset = width - _rightOffset;

    _labelHeight = FONT_LEFT_LABLE.getHeight();
    

    _topText = ( ( _targetHeight / 2 ) - ( _labelHeight / 2 ) );

    setExtent( width, _targetHeight );
  }


  protected void paint( Graphics g )
  {
    int oldColor = g.getColor();
    Font oldFont = g.getFont();
    try
    {

      // Left Bitmap
      if ( _leftIcon != null )
      {
        g.drawBitmap( HPADDING * 2, 0, _leftIcon.getWidth(), _leftIcon.getHeight(), _leftIcon, 0, 0 );
      }

      // Left Label Text
      g.setColor( _leftColor );
      g.setFont( FONT_LEFT_LABLE );
      g.drawText( _leftLabel, _leftOffset, _topText );

      // Right Label Text
      if ( _rightLabel != null )
      {
        g.setColor( COLOR_RIGHT_LABEL_FONT );
        g.setFont( FONT_RIGHT_LABLE );

        // recompute each time as the label width could change
        _rightOffset = HPADDING * 2;
        _rightOffset += FONT_RIGHT_LABLE.getAdvance( _rightLabel );
        _rightOffset = getWidth() - _rightOffset;

        g.drawText( _rightLabel, _rightOffset, _topText );
      }
    }
    finally
    {
      g.setColor( oldColor );
      g.setFont( oldFont );
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

  public void clickButton()
  {
    fieldChangeNotify( 0 );
  }

}



