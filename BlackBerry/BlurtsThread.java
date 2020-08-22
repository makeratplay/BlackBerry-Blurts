
package com.mlhsoftware.Blurts;

import com.mlhsoftware.ui.blurts.*;

import java.lang.*;
import net.rim.device.api.system.*;
import net.rim.device.api.ui.*;
import net.rim.device.api.ui.component.*;
import net.rim.device.api.i18n.*;
import java.io.IOException;
import net.rim.device.api.util.DataBuffer;
import net.rim.device.api.system.EventLogger;

import javax.bluetooth.*;
import net.rim.device.api.bluetooth.BluetoothSerialPort;
import net.rim.device.api.bluetooth.BluetoothSerialPortInfo;
import net.rim.device.api.bluetooth.BluetoothSerialPortListener;


import net.rim.blackberry.api.mail.*;
import net.rim.blackberry.api.mail.event.*;
import net.rim.device.api.system.*;
import net.rim.blackberry.api.mail.*;
import net.rim.device.api.servicebook.ServiceBook;
import net.rim.device.api.servicebook.ServiceRecord;

import net.rim.blackberry.api.phone.*;
import net.rim.blackberry.api.phone.phonelogs.*;

import java.util.*;
//import java.util.Date;
//import java.util.Calendar;
import net.rim.device.api.i18n.SimpleDateFormat;
import net.rim.blackberry.api.invoke.Invoke;
import net.rim.blackberry.api.invoke.PhoneArguments;

import net.rim.device.api.system.EventInjector;
import net.rim.device.api.system.EventInjector.KeyCodeEvent;
import net.rim.device.api.system.EventInjector.NavigationEvent;

import net.rim.device.api.system.Alert;

import javax.wireless.messaging.MessageListener;
import javax.wireless.messaging.MessageConnection;
import javax.wireless.messaging.BinaryMessage;
import javax.wireless.messaging.TextMessage;
import javax.microedition.io.*;
//import javax.wireless.messaging.Message;
//import net.rim.blackberry.api.mail.Message;

import net.rim.blackberry.api.pdap.*;
//import javax.microedition.pim.*;
//import net.rim.blackberry.api.pim.Contact;
//import net.rim.blackberry.api.pim.BlackBerryContact;
//import net.rim.blackberry.api.pim.BlackBerryContactList;
import java.util.Enumeration;

import net.rim.device.api.io.Base64OutputStream;
import net.rim.device.api.system.PNGEncodedImage;
import net.rim.device.api.system.Clipboard;

import net.rim.device.api.system.DeviceInfo;
import net.rim.device.api.system.RadioInfo;

import net.rim.device.api.system.RadioStatusListener;
import net.rim.device.api.system.SystemListener;

import javax.microedition.io.file.FileConnection;
import java.io.*;

class BlurtsThread extends Thread implements BluetoothSerialPortListener
{
  // statics 

  public static interface BlurtsCallback
  {
    void BluetoothConnect();
    void BluetoothDisconnect();
    void MsgArrived();
    void AlertQueued();
    void LocateMeAlert();
    void OnTimer();
  }

  // A sequence of frequencies and durations (eg 1400hz for 15ms, 1350hz for 15ms, etc)
  short[] _locateMe = { 1400, 15, 1350, 15, 1320, 20, 1300, 20, 1250, 25, 1200, 35 };


  

  public static final String DATE_FORMAT_NOW = "HH:mm:ss";
  static SimpleDateFormat sdf = new SimpleDateFormat( DATE_FORMAT_NOW );

  public static final int THREAD_STATUS_RUNNING = 1;
  public static final int THREAD_STATUS_STOPPED = 0;

  private final static String APPLICATION_KEY = "";
  //public static LocalyticsSession _session = new LocalyticsSession( APPLICATION_KEY );

  // members
  
  private BlurtsCallListener _callListener;
  private Vector _emailListers;
  private String m_deviceName;
  private BluetoothSerialPortInfo m_BTSerialPortInfo;
  private BluetoothSerialPort m_BTSerialPort;

  private SystemListener m_systemListener;
  private RadioStatusListener m_radioStatusListener;
  private int m_batteryLevel;
  private int m_signalLevel;

  public DataBuffer m_sendBuffer;
  private DataBuffer m_receiveBuffer;
  private int m_messageSize;
  private int m_bytesReceived;
      

  private int m_bytesSent;
  private boolean m_dataSent;
  private String m_status;
  private int m_threadStatus;

  private boolean m_connected;
  private boolean m_locateMe;



  private String m_smsAddress;
  private String m_smsMsgText;
  private String m_incompleteMsg;
  Vector m_inboundMsg;

  private String m_bluetoothName;

  private SMSThread m_smsThread;

  private DAOptionsProperties m_optionProperties;

  private BlurtsCallback m_uiCallBack;

  public BlurtsThread()
  {
    m_optionProperties = DAOptionsProperties.GetInstance();
    //_session.open();
    //_session.tagEvent( ( ActivationKeyStore.isKeyValid() ? "PRO" : "FREE" ) );
    //_session.tagEvent( "V_" + AboutScreen.APP_VERSION );
    
    //_session.upload();

    m_locateMe = false;
    m_uiCallBack = null;
    m_batteryLevel = -1;
    m_signalLevel = -1;
    m_bluetoothName = null;
    m_smsThread = null;
    m_bytesSent = 0;
    m_status = "";
    m_deviceName = "";
    m_BTSerialPortInfo = null;
    m_connected = false;
    m_BTSerialPort = null;
    m_sendBuffer = new DataBuffer();
    m_sendBuffer.setBigEndian( false );
    m_receiveBuffer = new DataBuffer();
    m_receiveBuffer.setBigEndian( true );
    m_messageSize = 0;
    m_bytesReceived = 0;

    m_dataSent = true;
    m_threadStatus = THREAD_STATUS_STOPPED;
    m_smsAddress = "";
    m_smsMsgText = "";
    m_incompleteMsg = "";
    m_inboundMsg = new Vector();
    _emailListers = new Vector();


    // Add Email Listeners
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
            if ( store != null )
            {
              BlurtsEmailListener emailListener = new BlurtsEmailListener( this, sc.getEmailAddress(), index );
              _emailListers.addElement( emailListener );
              store.addFolderListener( emailListener );
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


    // add call listner
    try
    {
      _callListener = new BlurtsCallListener( this );
      Phone.addPhoneListener( _callListener );
      String msg = "Added Phone Listener";
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.DEBUG_INFO );
    }
    catch ( Exception e )
    {
      String msg = "Add Phone Listener failed: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }

    // add SMS listner
    //if ( m_optionProperties.displaySMSAlert() )
    //{
      m_smsThread = new SMSThread( this );
    //}
    //else
    //{
    //  String msg = "Skipped SMS Listener";
    //  EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.DEBUG_INFO );
    //}

    // add System Listener (for Battery Status Changed )
    m_systemListener = new SystemListener()
    {
      public void powerOff()
      {
      }
      public void powerUp()
      {
      }
      public void batteryLow()
      {
        onLevelStatusChanged();
      }
      public void batteryGood()
      {
        onLevelStatusChanged();
      }
      public void batteryStatusChange( int status )
      {
        onLevelStatusChanged();
      }
    };
    Application.getApplication().addSystemListener( m_systemListener );


    // add Radio Status Listener
    m_radioStatusListener = new RadioStatusListener()
    {
      public void signalLevel( int level )
      {
        onLevelStatusChanged();
      }
      public void networkStarted( int networkId, int service )
      {
        onLevelStatusChanged();
      }
      public void baseStationChange()
      {
        onLevelStatusChanged();
      }
      public void radioTurnedOff()
      {
        onLevelStatusChanged();
      }
      public void pdpStateChange( int apn, int state, int cause )
      {
        onLevelStatusChanged();
      }
      public void networkStateChange( int state )
      {
        onLevelStatusChanged();
      }
      public void networkScanComplete( boolean success )
      {
        onLevelStatusChanged();
      }
      public void mobilityManagementEvent( int eventCode, int cause )
      {
        onLevelStatusChanged();
      }
      public void networkServiceChange( int networkId, int service )
      {
        onLevelStatusChanged();
      }
    };
    Application.getApplication().addRadioListener( m_radioStatusListener );

    acceptConnect();
  }

  public void setCallBack( BlurtsCallback uiCallBack )
  {
    m_uiCallBack = uiCallBack;
  }


  /*
   * run() outputs either "running" or "paused" based on the state of the thread
   */
  public void run()
  {
    EventLogger.logEvent( Blurts.LOGGER_ID, "Thread Running".getBytes(), EventLogger.DEBUG_INFO );
    try
    {
      m_threadStatus = THREAD_STATUS_RUNNING;
      m_status = "running";
      while ( m_threadStatus == THREAD_STATUS_RUNNING )
      {
        if ( m_status.length() > 0 )
        {
          System.out.println( m_status );
        }

        try
        {
          // Process and pending Call Alerts
          _callListener.processCall();

          // Process and pending Email Alerts
          int numEmailListers = _emailListers.size();
          for ( int lcv = 0; lcv < numEmailListers; lcv++ )
          {
            ( (BlurtsEmailListener)_emailListers.elementAt( lcv ) ).processEmail();
          }

          // Process and pending SMS Alerts
          synchronized ( m_smsAddress )
          {
            if ( m_smsAddress.length() > 0 )
            {
              sendSMS( m_smsAddress, m_smsMsgText );
              m_smsAddress = "";
            }
          }

          // Process commands from PC
          String inboundMsg = "";
          synchronized ( m_inboundMsg )
          {
            if ( !m_inboundMsg.isEmpty() )
            {
              inboundMsg = (String)m_inboundMsg.elementAt( 0 );
              m_inboundMsg.removeElementAt( 0 );
            }
          }
          if ( inboundMsg.length() > 0 )
          {
            processMessage( inboundMsg );
          }
        }
        catch ( Exception e )
        {
          String msg = "In Run Exeption: " + e.toString();
          EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
          System.out.println( msg );
        }

        // Send any pending alerts to PC
        sendData();

        // check for new version
        if ( m_uiCallBack != null )
        {
          m_uiCallBack.OnTimer();
        }

        // Play bb locate sound
        if ( m_locateMe )
        {
          try
          {
            int volume = 100;
            Alert.startAudio( _locateMe, volume );
          }
          catch ( Exception e )
          {
            String msg = "Error playing locate me sound : " + e.toString();
            EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
            System.out.println( msg );
          }
        }

        sleep( 500 );
      }
    }
    catch ( Exception ie )
    {
      m_threadStatus = THREAD_STATUS_STOPPED;
      m_status = "Run Exception: " + ie.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, m_status.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( "An Exeption was thrown " + ie.toString() );
    }

    EventLogger.logEvent( Blurts.LOGGER_ID, "Thread Stopped".getBytes(), EventLogger.DEBUG_INFO );
  }

  public int getThreadStatus()
  {
    return m_threadStatus;
  }

  public void stopThread()
  {
    Application.getApplication().removeSystemListener( m_systemListener );
    Application.getApplication().removeRadioListener( m_radioStatusListener );

    //_session.close();
    m_threadStatus = THREAD_STATUS_STOPPED;

    if ( m_smsThread != null )
    {
      m_smsThread.stopThread();
    }
  }


  // Bluetooth Code
  private boolean acceptConnect()
  {
    String SERVICE_UUID_STRING = "0000110100001000800000805F9B34FB";
    byte[] uId = { (byte)0x00, (byte)0x00, (byte)0x11, (byte)0x01, (byte)0x00, (byte)0x00, (byte)0x10, (byte)0x00, (byte)0x80, (byte)0x00, (byte)0x00, (byte)0x80, (byte)0x5F, (byte)0x9B, (byte)0x34, (byte)0xFB };

    //String SERVICE_UUID_STRING = "C25D59738BA848EA986354FCD69FC6A4";
    //byte[] uId = { (byte)0xC2, (byte)0x5D, (byte)0x59, (byte)0x73, (byte)0x8B, (byte)0xA8, (byte)0x48, (byte)0xEA, (byte)0x98, (byte)0x63, (byte)0x54, (byte)0xFC, (byte)0xD6, (byte)0x9F, (byte)0xC6, (byte)0xA4 };
    UUID SERVICE_UUID = new UUID( SERVICE_UUID_STRING, false );
    String SERVICE_NAME = "Blurts"; // human-readable
    String SERVICE_URL = "btspp://localhost:" + SERVICE_UUID + ";name=" + SERVICE_NAME;


    boolean retVal = false;
    try
    {
      m_status = "Listening";
      m_BTSerialPort = new BluetoothSerialPort( uId, SERVICE_NAME, BluetoothSerialPort.BAUD_115200, BluetoothSerialPort.DATA_FORMAT_PARITY_NONE | BluetoothSerialPort.DATA_FORMAT_STOP_BITS_1 | BluetoothSerialPort.DATA_FORMAT_DATA_BITS_8, BluetoothSerialPort.FLOW_CONTROL_NONE, 4024, 4024, this );
      retVal = true;
    }
    catch ( Exception ex )
    {
      String msg = "Connect Error: " + ex.getMessage();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
    return retVal;
  }

  private boolean connect( BluetoothSerialPortInfo info, boolean firstTime )
  {
    boolean retVal = false;
    try
    {
      if ( info != null )
      {
        // Connect to the selected device.
        m_deviceName = info.getDeviceName();
        m_status = "Connecting to " + m_deviceName;
        m_BTSerialPort = new BluetoothSerialPort( info, BluetoothSerialPort.BAUD_115200, BluetoothSerialPort.DATA_FORMAT_PARITY_NONE | BluetoothSerialPort.DATA_FORMAT_STOP_BITS_1 | BluetoothSerialPort.DATA_FORMAT_DATA_BITS_8, BluetoothSerialPort.FLOW_CONTROL_NONE, 4024, 4024, this );
      }
    }
    catch ( IOException ex )
    {
      String msg = "Connect Error: " + ex.getMessage();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
    return retVal;
  }

  public void statusMessage( String text )
  {
    StatusAlert alert = new StatusAlert();
    alert.setStatus( text );
    writeData( alert.toString(), null );
  }

  public void lockMessage()
  {
    if ( ActivationKeyStore.isKeyValid() )
    {
      LockAlert alert = new LockAlert();
      writeData( alert.toString(), null );
    }
  }

  public int getSendBufferSize()
  {
    int bufferSize = 0;
    synchronized ( m_sendBuffer )
    {
      bufferSize = m_sendBuffer.getArrayLength();
    }
    return bufferSize;
  }

  public int writeData( String textData )
  {
    return writeData( textData, null );
  }

  public int writeData( String textData, byte[] binaryData )
  {
    int bufferSize = 0;
    if ( m_connected )
    {
      try
      {
        byte[] textBytes = textData.getBytes( "UTF-8" );
        //byte[] textBytes = textData.getBytes();
        synchronized ( m_sendBuffer )
        {
          m_sendBuffer.writeByte( 0xFF ); // Begining of message marker
          m_sendBuffer.writeByte( 0xFF );
          m_sendBuffer.writeInt( textBytes.length );  // JSON payload size
          if ( binaryData != null )
          {
            m_sendBuffer.writeInt( binaryData.length );  // binary payload size
          }
          else
          {
            m_sendBuffer.writeInt( 0 );  // binary payload size
          }

          m_sendBuffer.write( textBytes, 0, textBytes.length ); // JSON payload

          if ( binaryData != null )
          {
            m_sendBuffer.write( binaryData, 0, binaryData.length ); // binary payload
          }

          bufferSize = m_sendBuffer.getArrayLength();
          String msg;
          msg = "Write Data: text - " + textBytes.length;
          if ( binaryData != null )
          {
            msg += " bytes; binary - " + binaryData.length;
          }
          msg += " sendBuffer: " + bufferSize;
          if ( m_uiCallBack != null )
          {
            m_uiCallBack.AlertQueued();
          }
          System.out.println( msg );
          //EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.DEBUG_INFO );
        }
      }
      catch ( java.io.UnsupportedEncodingException ex )
      {
        String msg = "writeData Encoding Exception: " + ex.getMessage();
        EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
        System.out.println( msg );
      }
    }
    return bufferSize;
  }

  // Sends the data currently in the DataBuffer.
  private void sendData()
  {
    try
    {
      m_status = "";
      // Ensure we have data to send.
      int bufferSize = 0;
      synchronized ( m_sendBuffer )
      {
        bufferSize = m_sendBuffer.getArrayLength();
      }

      if ( bufferSize > 0 )
      {
        // Ensure the last write call has resulted in the sending of the data
        // prior to calling write again.  Calling write in sequence without waiting
        // for the data to be sent can overwrite existing requests and result in
        // data loss.

        if ( !m_connected )
        {
          //boolean retVal = connect( m_BTSerialPortInfo, false );
          //if ( !retVal )
          {
            // not able to connect to just clear the buffer
            synchronized ( m_sendBuffer )
            {
              m_sendBuffer.reset();
            }
          }
        }

        if ( m_dataSent )
        {
          if ( m_BTSerialPort != null && m_connected )
          {
            try
            {
              m_status = "Sending data...";
              // Set the _dataSent flag to false so we don't send any more
              // data until it has been verified that this data was sent.
              m_dataSent = false;

              synchronized ( m_sendBuffer )
              {
                // Write out the data in the DataBuffer and reset the DataBuffer.
                int bytesToSend = m_sendBuffer.getArrayLength() - m_bytesSent;
                if ( bytesToSend > 0 )
                {
                  int bytesSent = m_BTSerialPort.write( m_sendBuffer.getArray(), m_bytesSent, bytesToSend );
                  //String msg = "Send Data: " + bytesToSend + " bytes placed; " + bytesSent + "bytes actually sent";
                  //EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.DEBUG_INFO );

                  System.out.println( "bytesSent:" + bytesSent );

                  if ( bytesToSend == bytesSent )
                  {
                    System.out.println( "m_sendBuffer.reset: " + m_sendBuffer.getArrayLength() );
                    m_sendBuffer.reset();
                    m_bytesSent = 0;
                  }
                  else
                  {
                    m_bytesSent += bytesSent;
                  }
                }
                else
                {
                  m_sendBuffer.reset();
                  m_bytesSent = 0;
                }
              }
            }
            catch ( IOException ioex )
            {
              // Reset _dataSent to true so we can attempt another data write.
              m_dataSent = true;
              String msg = "Failed to send data. Exception: " + ioex.toString();
              EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
              System.out.println( msg );
            }
          }
        }
        else
        {
          System.out.println( "Can't send data right now, data will be sent after dataSent notify call." );
        }
      }
    }
    catch ( Exception ex )
    {
      // Reset _dataSent to true so we can attempt another data write.
      m_dataSent = true;
      String msg = "Outer send data Exception: " + ex.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
  }

  // Invoked after all data in the buffer has been sent.
  public void dataSent()
  {
    // Set the m_dataSent flag to true to allow more data to be written.
    m_dataSent = true;

    // Call sendData in case there is data waiting to be sent.
    sendData();
    m_status = "Data sent";
    //EventLogger.logEvent( Blurts.LOGGER_ID, m_status.getBytes(), EventLogger.DEBUG_INFO );
    //System.out.println( m_status );
  }

  // Invoked when a connection is established.
  public void deviceConnected( boolean success )
  {
    if ( success )
    {
      if ( m_BTSerialPort != null )
      {
        // Reset _dataSent to true so we can attempt another data write.
        m_dataSent = true;

        m_connected = true;
        //_session.tagEvent( "BC" );

        try
        {
          //retVal = true;
          if ( m_uiCallBack != null )
          {
            m_uiCallBack.BluetoothConnect();
          }

          if ( m_optionProperties.buzzOnConnection() )
          {
            Alert.startVibrate( 600 );
          }
        }
        catch ( Exception ex )
        {
        }

        if ( m_bluetoothName == null )
        {
          try
          {
            LocalDevice localDevice;
            localDevice = LocalDevice.getLocalDevice();
            m_bluetoothName = localDevice.getFriendlyName();
          }
          catch ( javax.bluetooth.BluetoothStateException e )
          {
            String msg = "getLocalDevice failed: " + e.toString();
            EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
            System.out.println( msg );
          }
        }

        ConnectAlert alert = new ConnectAlert();
        String statusText = "Connected";
        if ( m_bluetoothName != null )
        {
          statusText = m_bluetoothName + " connected";
          alert.setBTName( m_bluetoothName );
        }

        int deviceId = DeviceInfo.getDeviceId();
        String deviceIdText = java.lang.Integer.toHexString( deviceId );
        String pin = deviceIdText.toUpperCase();

        alert.setDevicePIN( pin );
        alert.setDeviceModel( DeviceInfo.getDeviceName() );
        alert.setText( statusText );
        writeData( alert.toString(), null );

      }

      m_status = "Bluetooth device connected to " + m_deviceName;
      EventLogger.logEvent( Blurts.LOGGER_ID, m_status.getBytes(), EventLogger.DEBUG_INFO );
      System.out.println( m_status );
    }
    else
    {
      if ( m_BTSerialPort != null )
      {
        m_BTSerialPort.close();
      }

      m_status = "Bluetooth device failed to connect " + m_deviceName;
      m_BTSerialPort = null;
      EventLogger.logEvent( Blurts.LOGGER_ID, m_status.getBytes(), EventLogger.DEBUG_INFO );
      System.out.println( m_status );
    }
  }

  // Invoked when a connection is closed.
  public void deviceDisconnected()
  {
    if ( m_BTSerialPort != null )
    {
      //m_BTSerialPort.close();
    }
    //m_BTSerialPort = null;
    m_status = "Device disconnected from " + m_deviceName;
    EventLogger.logEvent( Blurts.LOGGER_ID, m_status.getBytes(), EventLogger.DEBUG_INFO );
    System.out.println( m_status );

    m_connected = false;
    //_session.tagEvent( "BD" );
    //_session.upload();
    //m_BTSerialPortInfo = null;
    m_deviceName = "";

    clearReceiveBuffer();
    // Reset _dataSent to true so we can attempt another data write.
    m_dataSent = true;

    try
    {
      if ( m_uiCallBack != null )
      {
        m_uiCallBack.BluetoothDisconnect();
      }
      if ( m_optionProperties.buzzOnConnection() )
      {
        Alert.startVibrate( 600 );
      }
    }
    catch ( Exception ex )
    {
    }
  }

  // Invoked when the drt state changes.
  public void dtrStateChange( boolean high )
  {
    System.out.println( "dtrStateChange: " + high );
  }

  private void clearReceiveBuffer()
  {
    m_receiveBuffer.reset();
    m_messageSize = 0;
    m_bytesReceived = 0;
  }

  // Invoked when data has been received.
  public void dataReceived( int length )
  {
    try
    {
      byte[] tmpBuffer = new byte[4024];
      int bufferLen = tmpBuffer.length;
      int readLen = bufferLen;
      if ( length < bufferLen )
      {
        readLen = length;
      }
      int len = m_BTSerialPort.read( tmpBuffer, 0, readLen );
      m_receiveBuffer.write( tmpBuffer, 0, len );
     
      System.out.println( "len: " + len );
      System.out.println( "buffer len: " + m_receiveBuffer.getLength() );
      
      
      if ( m_messageSize == 0 )
      {
        int currentPos =  m_receiveBuffer.getPosition();
        m_receiveBuffer.rewind();
        m_messageSize = m_receiveBuffer.readInt();
        int binarySize = m_receiveBuffer.readInt();
        m_receiveBuffer.setPosition( currentPos );
        m_bytesReceived = 0;
        System.out.println( "Msg len: " + m_messageSize );
        len = len - 8;
      }

      m_bytesReceived += len;


      if ( m_bytesReceived == m_messageSize )
      {
        m_receiveBuffer.rewind();
        int len2 = m_receiveBuffer.getLength();
        byte[] tmpBytes = new byte[len2];
        m_receiveBuffer.readFully(tmpBytes);
        String msg = new String( tmpBytes, 8, len2 - 8, "UTF-8" );
        m_messageSize = 0; 
        synchronized ( m_inboundMsg )
        {
          m_inboundMsg.addElement( msg );
        }
        clearReceiveBuffer();
        if ( m_uiCallBack != null )
        {
          m_uiCallBack.MsgArrived();
        }
      }
    }
    catch ( IOException ioex )
    {
      String msg = "dataReceived failed: " + ioex.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
    catch ( Exception ioex )
    {
      String msg = "dataReceived failed(2): " + ioex.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
  }

  public void processMessage( String data )
  {
    try
    {
      if ( data != null && data.length() > 0 )
      {
        CmdBase msgBase = new CmdBase( data );
        int cmdType = msgBase.getCmdType();

        if ( msgBase.isProCmd() && !ActivationKeyStore.isKeyValid() )
        {
          statusMessage( "Upgrade to Blurts Pro to enable this feature." );
          return;
        }

        //_session.tagEvent( "PM_" + cmdType );
        switch ( cmdType )
        {
          case CmdBase.CMD_PLACECALL:
          {
            CallCmd msg = new CallCmd( data );
            String phoneNumber = msg.getPhoneNumber();
            if ( phoneNumber.length() > 0 )
            {
              _callListener.makePhoneCall( phoneNumber );
            }
            break;
          }
          case CmdBase.CMD_PRESSKEY: // call phone menu
          {
            PressKeyCmd msg = new PressKeyCmd( data );
            Backlight.enable( true );
            int functionKey = msg.getFunctionKey();
            int keyCode = msg.getKeyCode();
            int statusCode = msg.getStatusCode();
            if (pressKey(functionKey, keyCode, statusCode) == false)
            {
              statusMessage( "Action failed! Check BlackBerry permissions." );
            }
            break;
          }
          case CmdBase.CMD_BUZZ:
          {
            BuzzCmd msg = new BuzzCmd( data );
            Alert.startVibrate( msg.getBuzzLength() );
            break;
          }
          case CmdBase.CMD_SENDSMS:
          {
            SMSCmd msg = new SMSCmd( data );
            String address = msg.getAddress();
            String msgText = msg.getText();
            synchronized ( m_smsAddress )
            {
              m_smsAddress = address;
              m_smsMsgText = msgText;
            }
            break;
          }
          case CmdBase.CMD_CONTACTSEARCH:
          {
            
            //String match = dataXml.GetParam1();
            //String result = findContact( match );
            //statusMessage( result );

            ContactsList contacts = new ContactsList( this );
            contacts.start();

            //String contactXml = contacts.GetContactXML( this );

            //writeData( contactXml.getBytes(), 0, contactXml.length() );
            break;
          }
          case CmdBase.CMD_SCREENSHOT:
          {
            try
            {
              ScreenCaptureCmd msg = new ScreenCaptureCmd( data );
              Backlight.enable( true );

              Thread.sleep( 300 );
              int quality = msg.getQuality();
              int top = msg.getTop();
              int left = msg.getLeft();
              int width = msg.getWidth();
              int height = msg.getHeight();
              ScreenCapture(quality, top, left, width, height);
            }
            catch ( Exception ioex )
            {
              String msg = "CMD_SCREENSHOT failed: " + ioex.toString();
              EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
              statusMessage( "Capturing screen failed! " + ioex.getMessage());
            }
            break;
          }
          case CmdBase.CMD_CREATECONTACT:
          {
            CreateContactCmd msg = new CreateContactCmd( data );
            ContactsList.createContact( msg );
            /*
            ContactsList contacts = new ContactsList( this );
            String name = dataXml.GetParam1();
            if( name != null )
            {
              contacts.createContact( name );
            }
             * */
            break;
          }
          case CmdBase.CMD_READCLIPBOARD:
          {
            sendClipboardToPC();
            break;
          }
          case CmdBase.CMD_WRITECLIPBOARD:
          {
            WriteClipboardCmd msg = new WriteClipboardCmd( data );
            Clipboard clipboard = Clipboard.getClipboard();
            if ( clipboard != null )
            {
              String text = msg.getText();
              if ( text != null )
              {
                clipboard.put( text );
                statusMessage( "BlackBerry clipboard updated..." );
              }
            }
            break;
          }
          case CmdBase.CMD_LEVEL:
          {
            sendLevelStatus();
            break;
          }
          case CmdBase.CMD_DTMF:
          {
            DTMFCmd msg = new DTMFCmd( data );
            PhoneCall phoneCall = Phone.getActiveCall();
            if ( phoneCall != null )
            {
              boolean added = phoneCall.sendDTMFTones( msg.getDTMFTones() );
              if ( !added )
              {
                statusMessage( "Action failed!" );
              }
            }
            else
            {
              statusMessage( "Action failed! No active call." );
            }
            break;
          }
          case CmdBase.CMD_LOCATEME:
          {
            m_locateMe = true;
            Backlight.enable( true );
            break;
          }
          case CmdBase.CMD_READFILE:
          {
            ReadFileCmd msg = new ReadFileCmd( data );
            SendFile( msg.getFileName() );
            break;
          }
          default:
          {
            statusMessage( "Action failed! Invalid commmand." );
          }
        }
      }
    }
    catch ( Exception ioex )
    {
      String msg = "processMessage failed: " + ioex.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
  }

  protected void SendFile( String fileName )
  {
    if ( ActivationKeyStore.isKeyValid() )
    {
      try
      {
        FileConnection fconn = (FileConnection)Connector.open( fileName, Connector.READ_WRITE );
        // If no exception is thrown, then the URI is valid, but the file may or may not exist.
        if ( fconn.exists() )
        {
          ReadFileAlert alert = new ReadFileAlert();
          alert.setFileName( fileName );

          int length = (int)fconn.fileSize();
          byte[] data = new byte[length];
          DataInputStream dos = fconn.openDataInputStream();
          dos.readFully( data );
          dos.close();
          fconn.close();

          writeData( alert.toString(), data );
        }
        else
        {
          statusMessage( "File not found: " + fileName );
        }
      }
      catch ( Exception ex )
      {
        String msg = "SendFile Error: " + ex.getMessage();
        EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
        System.out.println( msg );
        statusMessage( "Failed to read file. " + ex.getMessage() );
      }
    }
  }

  protected void onLevelStatusChanged()
  {
    if ( m_optionProperties.displayLevelAlert() && m_connected )
    {
      int batteryLevel = 0;
      int battery = DeviceInfo.getBatteryLevel();
      int signalLevel = 0;
      int signal = RadioInfo.getSignalLevel();


      if ( battery >= 95 )
      {
        batteryLevel = 5;
      }
      else if (battery >= 70)
      {
        batteryLevel = 4;
      }
      else if (battery >= 55)
      {
        batteryLevel = 3;
      }
      else if (battery >= 35)
      {
        batteryLevel = 2;
      }
      else if (battery >= 15)
      {
        batteryLevel = 1;
      }


      if ( signal >= -77 )
      {
        //5 bands
        signalLevel = 5;
      }
      else if ( signal >= -86 )
      {
        //4 bands
        signalLevel = 4;
      }
      else if ( signal >= -92 )
      {
        //3 bands
        signalLevel = 3;
      }
      else if ( signal >= -101 )
      {
        //2 bands
        signalLevel = 2;
      }
      else if (signal >= -120)
      {
        //1 band
        signalLevel = 1;
      }

      if ( batteryLevel != m_batteryLevel || signalLevel != m_signalLevel )
      {
        sendLevelStatus();
      }
      m_batteryLevel = batteryLevel;
      m_signalLevel = signalLevel;
    }
  }


  private void sendLevelStatus()
  {
    LevelAlert alert = new LevelAlert();
    writeData( alert.toString(), null );
  }



  public void sendClipboardToPC()
  {
    if ( ActivationKeyStore.isKeyValid() )
    {
      Clipboard clipboard = Clipboard.getClipboard();
      if ( clipboard != null )
      {
        ClipBoardAlert alert = new ClipBoardAlert();
        alert.setText( clipboard.toString() );
        writeData( alert.toString(), null );
      }
    }
  }

  public void readClipboardFromPC()
  {
    if ( ActivationKeyStore.isKeyValid() )
    {
      ReadClipboardCmd cmd = new ReadClipboardCmd();
      writeData( cmd.toString(), null );
    }
  }

  public void ScreenCapture(int quality, int top, int left, int width, int height)
  {
    if ( ActivationKeyStore.isKeyValid() )
    {
      try
      {
        if ( width < 0 || width > Display.getWidth() )
        {
          width = Display.getWidth();
        }

        if ( height < 0 || height > Display.getHeight() )
        {
          height = Display.getHeight();
        }

        if ( top < 0 || top + height > Display.getHeight() )
        {
          top = 0;
        }

        if ( left < 0 || left + width > Display.getWidth() )
        {
          left = 0;
        }

        statusMessage( "Capturing screen image..." );
        sendData();

        Bitmap screenImage = new Bitmap( width, height );
        //Display.screenshot( screenImage, top, left, width, height ); // only in 4.3?
        Display.screenshot( screenImage );

        //statusMessage( "Compressing screen image.");
        //sendData();

        ByteArrayOutputStream byteOutput = new ByteArrayOutputStream();
        OutputStream output = new DataOutputStream( byteOutput );
        JpegEncoder jpeg = new JpegEncoder( screenImage, quality, output );
        byte[] data = byteOutput.toByteArray();

        ScreenCaptureAlert alert = new ScreenCaptureAlert();
        writeData( alert.toString(), data );
      }
      catch ( Exception ex )
      {
        String msg = "ScreenCapture Error3: " + ex.getMessage();
        EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
        System.out.println( msg );
        statusMessage( "Capturing screen Failed. " + ex.getMessage() );
      }
    }
  }

  /*
  public void controlCall( String menuText )
  {
    try
    {
      DataXML data = new DataXML( DataXML.TYPE_CALL );
      Menu menu = Ui.getUiEngine().getActiveScreen().getMenu( 0 );
      for ( int i = 0; i <= menu.getSize() - 1; i++ )
      {
        data.addMenu( menu.getItem( i ).toString(), menu.getItem( i ).getOrdinal() );
        EventLogger.logEvent( Blurts.LOGGER_ID, ( "MLH - callIncoming1: " + menu.getItem( i ).toString() ).getBytes("UTF-8"), EventLogger.DEBUG_INFO );
      }
      String xml = data.toString();
      writeData( xml.getBytes("UTF-8"), 0, xml.length() );
    }
    catch ( Exception exc )
    {
    }
  }
   * */

  public boolean pressKey( int functionKey, int keyCode, int statusCode )
  {
    boolean retVal = true;
    try
    {

      System.out.println( "STATUS_ALT " + KeypadListener.STATUS_ALT );
      System.out.println( "STATUS_ALT_LOCK " + KeypadListener.STATUS_ALT_LOCK );
      System.out.println( "STATUS_CAPS_LOCK " + KeypadListener.STATUS_CAPS_LOCK );
      System.out.println( "STATUS_SHIFT " + KeypadListener.STATUS_SHIFT );
      System.out.println( "STATUS_SHIFT_LEFT " + KeypadListener.STATUS_SHIFT_LEFT );
      System.out.println( "STATUS_SHIFT_RIGHT " + KeypadListener.STATUS_SHIFT_RIGHT );

      int status = statusCode;
      /*
      try
      {
        switch ( statusCode )
        {
          case 1:
          {
            status = KeypadListener.STATUS_ALT;
            break;
          }
          case 2:
          {
            status = KeypadListener.STATUS_ALT_LOCK;
            break;
          }
          case 3:
          {
            status = KeypadListener.STATUS_CAPS_LOCK;
            break;
          }
          case 4:
          {
            status = KeypadListener.STATUS_SHIFT;
            break;
          }
          case 5:
          {
            status = KeypadListener.STATUS_SHIFT_LEFT;
            break;
          }
          case 6:
          {
            status = KeypadListener.STATUS_SHIFT_RIGHT;
            break;
          }
        }
      }
      catch ( Exception e)
      {
        String msg = "Status Code failed: " + e.toString();
        EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
        System.out.println( msg );
      }
       * */
      switch ( functionKey )
      {
        case 1: // SEND Key
        {
          retVal = injectKey((char)Keypad.KEY_SEND, status);
          break;
        }
        case 2: // Menu Key
        {
          retVal = injectKey((char)Keypad.KEY_MENU, status);
          break;
        }
        case 3: // Escape Key
        {
          retVal = injectKey((char)Keypad.KEY_ESCAPE, status);
          break;
        }
        case 4: // END Key
        {
          retVal = injectKey((char)Keypad.KEY_END, status);
          break;
        }
        case 5: // Click
        {
          retVal = injectNavigation( NavigationEvent.NAVIGATION_CLICK, 0, 0 );
          Thread.sleep( 50 );
          retVal = injectNavigation( NavigationEvent.NAVIGATION_UNCLICK, 0, 0 );
          break;
        }
        case 6: // move Up
        {
          retVal = injectNavigation( NavigationEvent.NAVIGATION_MOVEMENT, 0, -1 );
          break;
        }
        case 7: // move Down
        {
          retVal = injectNavigation( NavigationEvent.NAVIGATION_MOVEMENT, 0, 1 );
          break;
        }
        case 8: // move Left
        {
          retVal = injectNavigation( NavigationEvent.NAVIGATION_MOVEMENT, -1, 0 );
          break;
        }
        case 9: // move Right
        {
          retVal = injectNavigation( NavigationEvent.NAVIGATION_MOVEMENT, 1, 0 );
          break;
        }
        case 10: // Middle Key
        {
          retVal = injectKey((char)Keypad.KEY_MIDDLE, status);
          break;
        }
        case 11: // Mute Key
        {
          retVal = injectKey((char)Keypad.KEY_SPEAKERPHONE, status);
          break;
        }
        case 12: // Volume up Key
        {
          retVal = injectKey((char)Keypad.KEY_VOLUME_UP, status);
          break;
        }
        case 13: // Volume down Key
        {
          retVal = injectKey((char)Keypad.KEY_VOLUME_DOWN, status);
          break;
        }
        case 14: // Convenience 1 Key
        {
          retVal = injectKey((char)Keypad.KEY_CONVENIENCE_1, status);
          break;
        }
        case 15: // Convenience 2 Key
        {
          retVal = injectKey((char)Keypad.KEY_CONVENIENCE_2, status);
          break;
        }
        case 16: // Speaker Phone Key
        {
          retVal = injectKey((char)36, status);
          break;
        }
        case 17: // Key Code
        {
          retVal = injectKey( (char)keyCode, status );
          break;
        }

      }
     }
    catch ( Exception e )
    {
      String msg = "pressKey failed: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
      retVal = false;
    }
    return retVal;
  }

  public boolean injectKey( char key, int status )
  {
    boolean retVal = true;
    try
    {
      EventInjector.KeyCodeEvent pressKey = new EventInjector.KeyCodeEvent(KeyCodeEvent.KEY_DOWN, key, status, (int)(new Date().getTime()));
      EventInjector.KeyCodeEvent releaseKey = new EventInjector.KeyCodeEvent(KeyCodeEvent.KEY_UP, key, status, (int)(new Date().getTime()));
      EventInjector.invokeEvent( pressKey );
      Thread.sleep( 50 );
      EventInjector.invokeEvent( releaseKey );
      Thread.sleep( 50 );
      //EventInjector.invokeEvent( pressKey );
      //Thread.sleep( 50 );
      //EventInjector.invokeEvent( releaseKey );
      //Thread.sleep( 50 );
    }
    catch ( Exception e )
    {
      String msg = "injectKey failed: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
      retVal = false;
    }
    return retVal;
  }

  public boolean injectNavigation( int event, int dx, int dy )
  {
    boolean retVal = true;
    try
    {
      EventInjector.invokeEvent( new EventInjector.NavigationEvent( event, dx, dy, 0 ) );
    }
    catch ( Exception e )
    {
      String msg = "injectRoll failed: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
      retVal = false;
    }
    return retVal;
  }

  // MessageListener for SMS
  public void IncomingSMS( String address, String msg )
  {
    String logMsg = "IncomingSMS: " + address;
    EventLogger.logEvent( Blurts.LOGGER_ID, logMsg.getBytes(), EventLogger.DEBUG_INFO );

    if ( m_optionProperties.displaySMSAlert() )
    {
      try
      {
        //String match = address.substring( address.length() - 5, address.length() - 1 );
        String formatedAddress = address;
        if ( address.length() > 6 && address.charAt(0) == 's' )
        {
          formatedAddress = address.substring( 6, address.length() );
        }
        else if ( address.length() > 2 && address.charAt(0) == '/' )
        {
          formatedAddress = address.substring( 2, address.length() );
        }

        String name = formatedAddress;
        PhoneCallLogID participant = new PhoneCallLogID( formatedAddress );
        if ( participant != null )
        {
          name = participant.getName();
        }

        SMSAlert alert = new SMSAlert();
        alert.setSenderName( name );
        alert.setSenderAddress( formatedAddress );
        alert.setBodyText( msg );
        writeData( alert.toString(), null );

        System.out.println( "Received SMS text from " + address + " : " + msg );
      }
      catch ( Exception e )
      {
        String errorMsg = "Received SMS failed: " + e.toString();
        EventLogger.logEvent( Blurts.LOGGER_ID, errorMsg.getBytes(), EventLogger.SEVERE_ERROR );
        System.out.println( errorMsg );
      }
    }
  }

  public void sendSMS( String address, String msgText )
  {
    String sendingAddress = "sms://" + address;
    try
    {
      if ( RadioInfo.getNetworkType() == RadioInfo.NETWORK_CDMA )
      {
        String logEventMsg = "Send SMS CDMA: (" + sendingAddress + ") ";
        EventLogger.logEvent( Blurts.LOGGER_ID, logEventMsg.getBytes(), EventLogger.DEBUG_INFO );
        System.out.println( logEventMsg );

        DatagramConnection connection = null;
        byte[] bytes = msgText.getBytes( "UTF-8" );
        connection = (DatagramConnection)Connector.open( sendingAddress );
        Datagram datagram = connection.newDatagram( bytes, bytes.length );
        connection.send( datagram );
        connection.close();
      }
      else
      {
        String logEventMsg = "Send SMS: (" + sendingAddress + ") ";
        EventLogger.logEvent( Blurts.LOGGER_ID, logEventMsg.getBytes(), EventLogger.DEBUG_INFO );
        System.out.println( logEventMsg );

        //"sms:// 15195555555"
        MessageConnection msgConn = (MessageConnection)Connector.open( sendingAddress );
        javax.wireless.messaging.Message msg = msgConn.newMessage( MessageConnection.TEXT_MESSAGE );
        TextMessage txtMsg = (TextMessage)msg;
        txtMsg.setPayloadText( msgText );
        msgConn.send( txtMsg );
        msgConn.close();
      }
    }
    catch ( IOException e )
    {
      String msg = "Send SMS IOException: (" + sendingAddress + ") " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
      statusMessage( msg );
    }
    catch ( Exception e )
    {
      String msg = "Send SMS failed: (" + sendingAddress + ") " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
      statusMessage( msg );
    }
  }

}

