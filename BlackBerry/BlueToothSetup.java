package com.mlhsoftware.Blurts;

import com.mlhsoftware.ui.blurts.*;

import com.mlhsoftware.ui.*;
import net.rim.device.api.ui.*;
import net.rim.device.api.ui.container.*;
import net.rim.device.api.ui.component.*;

import net.rim.device.api.ui.component.Dialog;
import net.rim.device.api.system.EventLogger;

import net.rim.device.api.bluetooth.BluetoothSerialPort;
import net.rim.device.api.bluetooth.BluetoothSerialPortInfo;

import java.util.Vector;

import javax.bluetooth.*;
import javax.bluetooth.DiscoveryAgent;
import javax.bluetooth.LocalDevice;
import javax.bluetooth.RemoteDevice;
import java.io.IOException;
import javax.microedition.io.*;

import javax.bluetooth.LocalDevice;
import javax.bluetooth.DiscoveryAgent;

import net.rim.blackberry.api.mail.*;
import net.rim.blackberry.api.mail.event.*;
import net.rim.device.api.system.*;
import net.rim.blackberry.api.mail.*;
import net.rim.device.api.servicebook.ServiceBook;
import net.rim.device.api.servicebook.ServiceRecord;


/*package*/
final class BlueToothSetup extends MainScreen 
{

  private LocalDevice m_localDevice;
  private int m_discoverableRaw;
  String m_BluetoothName;
  String m_BluetoothAddress;


  private DAOptionsProperties m_optionProperties;

  private LabeledSwitchManager m_autoStartSwitch;
  private LabeledSwitchManager m_buzzSwitch;
  private LabeledSwitchManager m_callAlertSwitch;
  private LabeledSwitchManager m_smsAlertSwitch;
  private LabeledSwitchManager m_levelAlertSwitch;
  private LabeledSwitchManager m_pinMsgAlertSwitch;
  private LabeledSwitchManager[] m_emailAlertSwitch;

  public BlueToothSetup()
  {
    super( DEFAULT_MENU | DEFAULT_CLOSE | Manager.NO_VERTICAL_SCROLL );

    initBluetooth();
    
    //Read in the properties from the persistent store.
    m_optionProperties = DAOptionsProperties.GetInstance();

    // Build the titlebar with Cancel and Save buttons
    TitlebarManager titlebarMgr = new TitlebarManager( "Blurts Setup ", "Cancel", "Done" );
    titlebarMgr.handleLeftBtn( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context ) { OnCancel(); }
    } );

    titlebarMgr.handleRightBtn( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context ) { OnDone(); }
    } );

    add( titlebarMgr );


    ForegroundManager foreground = new ForegroundManager();
    add( foreground );


    ListStyleFieldSet infoSet = new ListStyleFieldSet();

    ListStyleLabelField info = new ListStyleLabelField( "Bluetooth Info" );
    infoSet.add( info );

    info = new ListStyleLabelField( null, "Name", m_BluetoothName );
    infoSet.add( info );

    info = new ListStyleLabelField( null, "Address", m_BluetoothAddress );
    infoSet.add( info );

    foreground.add( infoSet );

    boolean editable = true;

    ListStyleFieldSet buttonSet = new ListStyleFieldSet();
    foreground.add( buttonSet );

    if ( !ActivationKeyStore.isKeyValid() )
    {
      editable = false;
      ListStyleRichTextlField label1 = new ListStyleRichTextlField( "Upgrade to Pro to enable settings" );

      //info = new ListStyleLabelField( "Enter key to unlock full version" );
      buttonSet.add( label1 );

      //foreground.add( new LabelField( "Upgrade to Pro to enable settings", LabelField.USE_ALL_WIDTH ) );
    }
    else
    {
      info = new ListStyleLabelField( "Settings" );
      buttonSet.add( info );
    }


    // Auto Start Setting
    m_autoStartSwitch = new LabeledSwitchManager( "Auto Start" );
    m_autoStartSwitch.setOn( m_optionProperties.autoStart() );
    m_autoStartSwitch.setEditable( editable );
    buttonSet.add( m_autoStartSwitch );

    // Buzz on connect Setting
    m_buzzSwitch = new LabeledSwitchManager( "Buzz on connect" );
    m_buzzSwitch.setOn( m_optionProperties.buzzOnConnection() );
    m_buzzSwitch.setEditable( editable );
    buttonSet.add( m_buzzSwitch );

    // Level Alert Setting
    m_levelAlertSwitch = new LabeledSwitchManager( "Bat/Sig Level Alert" );
    m_levelAlertSwitch.setOn( m_optionProperties.displayLevelAlert() );
    m_levelAlertSwitch.setEditable( true );
    buttonSet.add( m_levelAlertSwitch );

    // PIN Msg Alert Setting
    m_pinMsgAlertSwitch = new LabeledSwitchManager( "PIN Message Alert" );
    m_pinMsgAlertSwitch.setOn( m_optionProperties.displayPinMsgAlert() );
    m_pinMsgAlertSwitch.setEditable( editable );
    buttonSet.add( m_pinMsgAlertSwitch );

    

    // Call Alert Settings
    m_callAlertSwitch = new LabeledSwitchManager( "Call Alert" );
    m_callAlertSwitch.setOn( m_optionProperties.displayCallAlert() );
    m_callAlertSwitch.setEditable( editable );
    buttonSet.add( m_callAlertSwitch );

    // SMS Alert Setting
    m_smsAlertSwitch = new LabeledSwitchManager( "SMS Alert" );
    m_smsAlertSwitch.setOn( m_optionProperties.displaySMSAlert() );
    m_smsAlertSwitch.setEditable( editable );
    buttonSet.add( m_smsAlertSwitch );



    // Email Alerts  Setting
    ListStyleFieldSet emailSet = new ListStyleFieldSet();
    foreground.add( emailSet );
    info = new ListStyleLabelField( "Email Accounts" );
    emailSet.add( info );
    m_emailAlertSwitch = new LabeledSwitchManager[10];

    for ( int i = 0; i < 10; i++ )
    {
      m_emailAlertSwitch[i] = null;
    }

    try
    {
      int index = 0;
      ServiceBook sb = ServiceBook.getSB();
      ServiceRecord[] sr = sb.findRecordsByType( 0 );
      //ServiceRecord[] sr = sb.getRecords();
      for ( int i = 0; i < sr.length; i++ )
      {
        ServiceConfiguration sc = new ServiceConfiguration( sr[i] );
        if ( sc != null && sc.getEmailAddress() != null )
        {
          Session sn = Session.getInstance( sc );
          if ( sn != null )
          {
            Store store = sn.getStore();
            if ( store != null && index < 10 )
            {
              m_emailAlertSwitch[index] = new LabeledSwitchManager( sc.getEmailAddress() );
              m_emailAlertSwitch[index].setOn( m_optionProperties.displayEmailAlert( index ) );
              m_emailAlertSwitch[index].setEditable( editable );
              emailSet.add( m_emailAlertSwitch[index] );

              index++;
            }
          }
        }
      }
    }
    catch ( Exception e )
    {
      String msg = "addFolderListener failed: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }

    if ( AboutScreen.BETA )
    {
      addMenuItem( _reset );
    }
  }


  private void initBluetooth()
  {
    m_BluetoothName = "No Bluetooth";
    m_BluetoothAddress = "No Bluetooth";
    try
    {
      m_localDevice = LocalDevice.getLocalDevice();
      if ( m_localDevice != null )
      {
        m_discoverableRaw = m_localDevice.getDiscoverable();
        m_localDevice.setDiscoverable( DiscoveryAgent.GIAC );
        m_BluetoothName = m_localDevice.getFriendlyName();
        String tmp = m_localDevice.getBluetoothAddress();
        int len = tmp.length();
        m_BluetoothAddress = "";
        for ( int x = 0; x < len; x++ )
        {
          if ( x != 0 && x % 2 == 0 )
          {
            m_BluetoothAddress += ":";
          }
          m_BluetoothAddress += tmp.charAt( x );
        }
      }
    }
    catch ( javax.bluetooth.BluetoothStateException e )
    {
      String msg = "setDiscoverable failed: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
    catch ( Exception e )
    {
      String msg = "setDiscoverable failed: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
  }

  private void OnDone()
  {
    save();
    CleanUp();
    close();
  }

  public void OnCancel()
  {
    CleanUp();
    close();
  }

  // The keyChar method is called by the event handler when a key is pressed.
  public boolean keyChar( char key, int status, int time )
  {
    boolean retVal = false;

    switch ( key )
    {
      case Characters.ESCAPE:
      {
        OnDone();
        retVal = true;
        break;
      }
      default:
      {
        retVal = super.keyChar( key, status, time );
        break;
      }
    }
    return retVal;
  }

  public boolean CleanUp()
  { 
    try
    {
      if ( m_localDevice != null )
      {
        m_localDevice.setDiscoverable( m_discoverableRaw );
      }
    }
    catch (javax.bluetooth.BluetoothStateException e )
    {
      String msg = "addFolderListener failed: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }

    //int answer = Dialog.ask( Dialog.D_YES_NO, "Save Changes" );
    //if ( answer == Dialog.YES )
    //{
    //  save();
    //}
    //close();
    return true;
  }

  public void save()
  {

    m_optionProperties.setAutoStart( m_autoStartSwitch.getOnState() );
    m_optionProperties.setBuzzOnConnection( m_buzzSwitch.getOnState() );
    m_optionProperties.setCallAlert( m_callAlertSwitch.getOnState() );
    m_optionProperties.setSMSAlert( m_smsAlertSwitch.getOnState() );
    m_optionProperties.setLevelAlert( m_levelAlertSwitch.getOnState() );
    m_optionProperties.setPinMsgAlert( m_pinMsgAlertSwitch.getOnState() );
    
    

    for ( int i = 0; i < 10; i++ )
    {
      if ( m_emailAlertSwitch[i] != null )
      {
        m_optionProperties.setEmailAlert( m_emailAlertSwitch[i].getOnState(), i );
      }
    }

    //Write our changes back to the persistent store.
    m_optionProperties.save();
    m_optionProperties = null;
  }

  private MenuItem _reset = new MenuItem( "Reset All Settings", 80, 80 )
  {
    public void run()
    {
      DAOptionsProperties.reset();
    }
  }; 

}
