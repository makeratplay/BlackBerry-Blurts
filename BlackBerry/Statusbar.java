//#preprocess

package com.mlhsoftware.ui.blurts;

import net.rim.device.api.ui.*;
import net.rim.device.api.ui.component.*;

/**
 * 
 */
public class Statusbar extends Manager
{

  private static final int LEFT_MARGIN = 4;
  private static final int TOP_MARGIN = 4;
  private static final int BOTTOM_MARGIN = 4;
  private static final int RIGHT_MARGIN = 4;

  //public static final int[] cols = { 0x00ADBACB, 0x00ADBACB, 0x006F86A4, 0x006F86A4 };  
  public static final int[] cols = { 0x00484D54, 0x00484D54, 0x001F252D, 0x001F252D };

  private static final int[] PATH_GRADIENT = {
    0xAAAAAA, 0xAAAAAA, 0xAAAAAA, 0xAAAAAA, 0xAAAAAA, 0xAAAAAA,
		
    0x000000, 0x000000, 0x000000, 0x000000, 0x000000, 0x000000
  };

  public static final int[] colsConnected = { 0x00039AFF, 0x00039AFF, 0x00F6FBFF, 0x00F6FBFF };


  public static final int[] colsDisconnected = { 0x008C8E8D, 0x007A7A7A, 0x00BDBBBE, 0x00ADABAE };

  public static final Font BUTTON_FONT = Font.getDefault().derive( Font.BOLD, 7, Ui.UNITS_pt, Font.ANTIALIAS_STANDARD, 0 );
 

  private static final int SYSTEM_STYLE_SHIFT = 32;


  //private StatusLabelField m_label1;
  private StatusLabelField m_label2;

  private boolean m_connected = false;
  private int m_alertTotal = 0;
  private int m_cmdTotal = 0;

  public Statusbar()
  {
    super( 0 );
    //m_label1 = new StatusLabelField( "Alerts: 0000 Cmds: 0000" );
    m_label2 = new StatusLabelField( "Disconnected" );
    //add( m_label1 );
    add( m_label2 );
  }

  public void setConnected( boolean connected )
  {
    m_connected = connected;
    updateLabels();
  }

  public void incAlertCnt()
  {
    m_alertTotal++;
    updateLabels();
  }

  public void incCmdCnt()
  {
    m_cmdTotal++;
    updateLabels();
  }

  private void updateLabels()
  {
    
    if ( m_connected )
    {
      //m_label1.setText( "Alerts: " + m_alertTotal + " Cmds: " + m_cmdTotal );
      //m_label1.setBackgroundColors( colsConnected );
      //m_label1.setFontColor( Color.BLACK );

      m_label2.setText( "Connected" );
      m_label2.setBackgroundColors( colsConnected );
      m_label2.setFontColor( Color.BLACK );
    }
    else
    {
      //m_label1.setText( "" );
      //m_label1.setBackgroundColors( colsConnected );
      //m_label1.setFontColor( Color.BLACK );

      m_label2.setText( "Disconnected" );
      m_label2.setBackgroundColors( colsDisconnected );
      m_label2.setFontColor( Color.BLACK );
    }
    
  }

  public void resetCounts()
  {
    m_alertTotal = 0;
    m_cmdTotal = 0;
  }

  protected void paintBackground( Graphics graphics )
  {
    super.paintBackground( graphics );

    int[] yInds = new int[] { 0, 0, getHeight(), getHeight() };
    int[] xInds = new int[] { 0, getWidth(), getWidth(), 0 };
    graphics.drawShadedFilledPath( xInds, yInds, null, cols, null );
  }

  protected void sublayout( int width, int height )
  {
    int statusBarHeight = 30;
    int vPadding = Graphics.getScreenWidth() <= 320 ? 6 : 10;
    int hPadding = 5;
    int labelWidth = Graphics.getScreenWidth() / 3;

    //layoutChild( m_label1, labelWidth, statusBarHeight - (hPadding*2) );
    //setPositionChild( m_label1, vPadding, hPadding );

    layoutChild( m_label2, labelWidth, height );
    setPositionChild( m_label2, Graphics.getScreenWidth() - (hPadding + m_label2.getWidth()), vPadding / 2 );

    setExtent( width, m_label2.getHeight() + vPadding );
  }
}
