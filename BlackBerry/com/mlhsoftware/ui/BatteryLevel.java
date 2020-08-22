//#preprocess
/*
 * BatteryLevel.java
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
import net.rim.device.api.system.DeviceInfo;
import net.rim.device.api.system.Display;
import net.rim.device.api.system.SystemListener;
import net.rim.device.api.ui.Color;
import net.rim.device.api.ui.DrawStyle;
import net.rim.device.api.ui.Field;
import net.rim.device.api.ui.Font;
import net.rim.device.api.ui.Graphics;


public class BatteryLevel extends Field
{
  private int m_backgroundColor;
  private int m_batteryBackground;
  private SystemListener m_systemListener;
  private boolean m_listenersActive;
  private int m_batteryLevel;

  public BatteryLevel()
  {
    super( Field.NON_FOCUSABLE );
    this.m_backgroundColor = 0;
    this.m_batteryBackground = 0x999999;
    m_batteryLevel = DeviceInfo.getBatteryLevel();

    this.m_listenersActive = false;

    this.m_systemListener = new SystemListener()
    {
      public void powerOff()
      {
      }
      public void powerUp()
      {
      }
      public void batteryLow()
      {
        onBatteryStatusChanged();
      }
      public void batteryGood()
      {
        onBatteryStatusChanged();
      }
      public void batteryStatusChange( int status )
      {
        onBatteryStatusChanged();
      }
    };
  }

  protected void onBatteryStatusChanged()
  {
    m_batteryLevel = DeviceInfo.getBatteryLevel();
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
      Application.getApplication().addSystemListener( m_systemListener );
      onBatteryStatusChanged();
      m_listenersActive = true;
    }
  }

  private void checkRemoveListeners()
  {
    if ( m_listenersActive )
    {
      Application.getApplication().removeSystemListener( m_systemListener );
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
      Application.getApplication().removeSystemListener( m_systemListener );
      m_listenersActive = false;
    }
  }

  public void setBatteryBackground( int batteryBackground )
  {
    this.m_batteryBackground = batteryBackground;
    invalidate();
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
    return 44;
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

    int backgroundColor = graphics.getBackgroundColor();

    graphics.setColor( m_batteryBackground );
    graphics.drawRect( 1, 0, 40, 14 );
    graphics.drawRect( 2, 1, 38, 12 );
    graphics.drawLine( 0, 2, 0, 12 );
    graphics.fillRect( 41, 3, 3, 8 );

    graphics.setColor( backgroundColor );
    graphics.fillRect( 3, 2, 36, 10 );

    // Pick the battery color
    if ( m_batteryLevel > 75 ) { graphics.setColor( 0x28f300 ); }
    else if ( m_batteryLevel > 50 ) { graphics.setColor( 0x91dc00 ); }
    else if ( m_batteryLevel > 25 ) { graphics.setColor( 0xefec00 ); }
    else { graphics.setColor( 0xff2200 ); }

    // Paint the battery level indicator
    graphics.fillRect( 4, 3, 6, 8 );
    graphics.fillRect( 11, 3, 6, 8 );
    graphics.fillRect( 18, 3, 6, 8 );
    graphics.fillRect( 25, 3, 6, 8 );
    graphics.fillRect( 32, 3, 6, 8 );

    graphics.setColor( backgroundColor );
    int power = (int)( ( 34.00 / 100 ) * m_batteryLevel );
    power = Math.max( power, 0 );
    power = Math.min( power, 34 );
    graphics.fillRect( 38 - ( 34 - power ), 3, 34 - power, 8 );
  }
}