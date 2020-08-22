//#preprocess
/*
 * ListStyleButtonField.java
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


public class ListStyleButtonField extends ListStyleField implements Runnable
{

  private Bitmap _leftIcon;
  private Bitmap _actionIcon;

  private int _targetHeight;
  private int _rightOffset;
  private int _leftOffset;
  private int _labelHeight;
  private int _topText;
  private String _labelText;
  private Font lableFont = null;

  // Progress Animation info
  private Bitmap _progressBitmap;
  private int _numFrames;
  private int _frameWidth;
  private int _frameHeight;
  private int _currentFrame;
  private int _timerID = -1;
  private Application _application;
  private boolean _animate = false;
  private boolean _visible = true;

  public ListStyleButtonField( String label, Bitmap actionIcon )
  {
    this( null, label, actionIcon, 0 );
  }

  public ListStyleButtonField( Bitmap icon, String label, Bitmap actionIcon )
  {
    this( icon, label, actionIcon, 0 );
  }

  public ListStyleButtonField( Bitmap icon, String label, Bitmap actionIcon, long style )
  {
    super( USE_ALL_WIDTH | Field.FOCUSABLE );

    _labelText = label;
    _actionIcon = actionIcon;
    _leftIcon = icon;
    _animate = false;
    _timerID = -1;
  }

  public void setProgressAnimationInfo( Bitmap bitmap, int numFrames )
  {
    _progressBitmap = bitmap;
    _numFrames = numFrames;
    _frameWidth = _progressBitmap.getWidth() / _numFrames;
    _frameHeight = _progressBitmap.getHeight();
    _application = Application.getApplication();
    _animate = false;
    _visible = true;
    _timerID = -1;
  }

  public void startAnimation()
  {
    _animate = true;
    startTimer();
  }

  public void stopAnimation()
  {
    _animate = false;
    stopTimer();
  }

  private void startTimer()
  {
    if ( _timerID == -1 && _animate == true )
    {
      _timerID = _application.invokeLater( this, 200, true );
    }
  }

  private void stopTimer()
  {
    if ( _timerID != -1 )
    {
      _application.cancelInvokeLater( _timerID );
      _timerID = -1;
    }
  }

  public void run()
  {
    if ( _visible )
    {
      invalidate();
    }
  }

  protected void onDisplay()
  {
    super.onDisplay();
    _visible = true;
    startTimer();
  }

  protected void onUndisplay()
  {
    super.onUndisplay();
    _visible = false;
    stopTimer();
  }

  public boolean isFocusable()
  {
    return true;
  }

  //protected void sublayout( int width, int height )
  public void layout( int width, int height )
  {
    int finalVPadding = getVPadding();
    int topPos = getTopPos();

    int labelHeight = ( FONT_LEFT_LABLE.getHeight() * 3 ) / 2 + finalVPadding; 
    int iconHeight = 0;
    if ( _leftIcon != null )
    {
      iconHeight = _leftIcon.getHeight() + finalVPadding; 
    }

    _targetHeight = Math.max( labelHeight, iconHeight );
    _topText = getTopPos() + (( _targetHeight - labelHeight) / 2 );

    _leftOffset = HPADDING * 2;
    if ( _leftIcon != null )
    {
      _leftOffset += _leftIcon.getWidth() + HPADDING;
    }

    _rightOffset = HPADDING * 2;
    if ( _actionIcon != null )
    {
      _rightOffset += _actionIcon.getWidth() + HPADDING;
    }

    int fontSize = 7;
    lableFont = FONT_LEFT_LABLE.derive( FONT_LEFT_LABLE.isBold() ? Font.BOLD : Font.PLAIN, fontSize, Ui.UNITS_pt );
    int labelWidth = lableFont.getAdvance( _labelText );

    int availableCenterWidth = width - (_rightOffset + _leftOffset);
    while ( labelWidth > availableCenterWidth && fontSize > 2 )
    {
      fontSize--;
      lableFont = FONT_LEFT_LABLE.derive( FONT_LEFT_LABLE.isBold() ? Font.BOLD : Font.PLAIN, fontSize, Ui.UNITS_pt );
      labelWidth = lableFont.getAdvance( _labelText );
    }

    _labelHeight = lableFont.getHeight();
    

    if ( isStyle( DrawStyle.HCENTER ) )
    {
      _leftOffset = ( width - labelWidth ) / 2;
    }
    else if ( isStyle( DrawStyle.RIGHT ) )
    {
      _leftOffset = width - labelWidth - HPADDING - _rightOffset;
    }

    

    setExtent( width, _targetHeight );
  }

  protected void paint( Graphics g )
  {
    int oldColor = g.getColor();
    Font oldFont = g.getFont();
    try
    {
      int topPos = getTopPos();
      int topText = getTopPos();
      // Left Bitmap
      if ( _leftIcon != null )
      {
        g.drawBitmap( HPADDING * 2, topPos, _leftIcon.getWidth(), _leftIcon.getHeight(), _leftIcon, 0, 0 );
        topText = topPos + ((_leftIcon.getHeight() - _labelHeight) / 2);
      }



      if ( _animate == true && _progressBitmap != null )
      {
        g.drawBitmap( getWidth() - ( HPADDING * 2 ) - _frameWidth, topPos, _frameWidth, _frameHeight, _progressBitmap, _frameWidth * _currentFrame, 0 );
        _currentFrame++;
        if ( _currentFrame >= _numFrames )
        {
          _currentFrame = 0;
        }
        if ( _leftIcon == null )
        {
          topText = topPos + ((_frameHeight - _labelHeight) / 2);
        }
      }

      // Right (Action) Bitmap
      else if ( _actionIcon != null )
      {
        g.drawBitmap( getWidth() - ( HPADDING * 2 ) - _actionIcon.getWidth(),  topPos, _actionIcon.getWidth(), _actionIcon.getHeight(), _actionIcon, 0, 0 );
        if ( _leftIcon == null )
        {
          topText = topPos + ((_actionIcon.getHeight() - _labelHeight) / 2);
        }
      }


      // Left Label Text
      g.setColor( COLOR_LEFT_LABEL_FONT );
      g.setFont( lableFont );
      g.drawText( _labelText, _leftOffset, topText );

      //g.setColor( 0xFF0000 );
      //g.drawRect(0, 0, getWidth(), getHeight());
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

  /*       
//#ifndef VER_4.6.1 | VER_4.6.0 | VER_4.5.0 | VER_4.2.1 | VER_4.2.0
    protected boolean touchEvent( TouchEvent message )
    {
        int x = message.getX( 1 );
        int y = message.getY( 1 );
        if( x < 0 || y < 0 || x > getExtent().width || y > getExtent().height ) {
            // Outside the field
            return false;
        }
        switch( message.getEvent() ) {
       
            case TouchEvent.UNCLICK:
                clickButton();
                return true;
        }
        return super.touchEvent( message );
    }
//#endif 
*/

}



