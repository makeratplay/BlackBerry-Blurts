//#preprocess
/*
 * SignalLevel.java
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

import net.rim.device.api.system.Application;
import net.rim.device.api.system.Display;
import net.rim.device.api.system.RadioInfo;
import net.rim.device.api.system.RadioStatusListener;
import net.rim.device.api.ui.Color;
import net.rim.device.api.ui.DrawStyle;
import net.rim.device.api.ui.Field;
import net.rim.device.api.ui.Font;
import net.rim.device.api.ui.Graphics;

/**
 * General purpose header field for application screens.
 * Based on the sample code provided here:
 * http://www.northcubed.com/site/?p=15
 */
public class SignalLevel extends Field
{
  private int m_backgroundColor;
  private int m_signalBarColor;
  private RadioStatusListener m_radioStatusListener;
  private boolean m_listenersActive;
  private int m_signalLevel;

  public SignalLevel()
  {
    super( Field.NON_FOCUSABLE );
    this.m_backgroundColor = 0;
    this.m_signalBarColor = Color.WHITE;
    m_signalLevel = RadioInfo.getSignalLevel();

    this.m_listenersActive = false;

    this.m_radioStatusListener = new RadioStatusListener()
    {
      public void signalLevel( int level )
      {
        onRadioStatusChanged();
      }
      public void networkStarted( int networkId, int service )
      {
        onRadioStatusChanged();
      }
      public void baseStationChange()
      {
        onRadioStatusChanged();
      }
      public void radioTurnedOff()
      {
        onRadioStatusChanged();
      }
      public void pdpStateChange( int apn, int state, int cause )
      {
        onRadioStatusChanged();
      }
      public void networkStateChange( int state )
      {
        onRadioStatusChanged();
      }
      public void networkScanComplete( boolean success )
      {
        onRadioStatusChanged();
      }
      public void mobilityManagementEvent( int eventCode, int cause )
      {
        onRadioStatusChanged();
      }
      public void networkServiceChange( int networkId, int service )
      {
        onRadioStatusChanged();
      }
    };
  }

  protected void onRadioStatusChanged()
  {
    m_signalLevel = RadioInfo.getSignalLevel();
    invalidate();
  }

  protected void onDisplay()
  {
    checkAddListeners();
    super.onExposed();
  }

  protected void onExposed()
  {
    checkAddListeners();
    super.onExposed();
  }

  protected void onObscured()
  {
    checkRemoveListeners();
    super.onObscured();
  }

  protected void onUndisplay()
  {
    checkRemoveListeners();
    super.onUndisplay();
  }

  private void checkAddListeners()
  {
    if ( !m_listenersActive )
    {
      Application.getApplication().addRadioListener( m_radioStatusListener );
      onRadioStatusChanged();
      m_listenersActive = true;
    }
  }

  private void checkRemoveListeners()
  {
    if ( m_listenersActive )
    {
      Application.getApplication().removeRadioListener( m_radioStatusListener );
      m_listenersActive = false;
    }
  }

  /**
     * Remove any global event listeners.  Intended to be called on shutdown,
     * where the active screen may not get popped off the stack prior to
     * System.exit() being called.
     */
  public void removeListeners()
  {
    if ( m_listenersActive )
    {
      Application.getApplication().removeRadioListener( m_radioStatusListener );
      m_listenersActive = false;
    }
  }

  public void setBackgroundColor( int backgroundColor )
  {
    this.m_backgroundColor = backgroundColor;
    invalidate();
  }


  protected void layout( int width, int height )
  {
    setExtent( getPreferredWidth(), getPreferredHeight() );
  }

  public int getPreferredWidth()
  {
    return 35;
  }

  public int getPreferredHeight()
  {
    return 14;
  }

  protected void paint( Graphics graphics )
  {
    int preferredWidth = this.getPreferredWidth();
    int preferredHeight = this.getPreferredHeight();
    int midPoint = preferredHeight / 2;

    if ( m_backgroundColor != 0 )
    {
      graphics.setColor( m_backgroundColor );
      graphics.fillRect( 0, 0, preferredWidth, preferredHeight );
    }

    graphics.setColor( 0x002A323D );
    graphics.fillRect( 7, 12, 4, 2 );
    graphics.fillRect( 13, 9, 4, 5 );
    graphics.fillRect( 19, 6, 4, 8 );
    graphics.fillRect( 25, 3, 4, 11 );
    graphics.fillRect( 31, 0, 4, 14 );

    graphics.setColor( m_signalBarColor );
    graphics.drawLine( 0, 0, 8, 0 );
    graphics.drawLine( 0, 0, 4, 4 );
    graphics.drawLine( 8, 0, 4, 4 );
    graphics.drawLine( 4, 4, 4, 13 );

    if ( m_signalLevel >= -120 )
    {
      //1 band
      graphics.fillRect( 7, 12, 4, 2 );
    }
    if ( m_signalLevel >= -101 )
    {
      //2 bands
      graphics.fillRect( 13, 9, 4, 5 );
    }
    if ( m_signalLevel >= -92 )
    {
      //3 bands
      graphics.fillRect( 19, 6, 4, 8 );
    }
    if ( m_signalLevel >= -86 )
    {
      //4 bands
      graphics.fillRect( 25, 3, 4, 11 );
    }
    if ( m_signalLevel >= -77 )
    {
      //5 bands
      graphics.fillRect( 31, 0, 4, 14 );
    }
  }

}