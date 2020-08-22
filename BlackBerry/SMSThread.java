
package com.mlhsoftware.Blurts;

import net.rim.device.api.system.EventLogger;
import javax.microedition.io.file.*;
import javax.microedition.io.*;
import java.io.IOException;
import java.io.*;
import net.rim.device.api.io.DatagramBase;
import net.rim.device.api.io.SmsAddress;
import net.rim.device.api.system.SMSParameters;
       
class SMSThread extends Thread 
{
  public static final int THREAD_STATUS_RUNNING = 1;
  public static final int THREAD_STATUS_STOPPED = 0;

  // members
  private int m_threadStatus;
  private BlurtsThread m_blurtsTread;

  public SMSThread( BlurtsThread blurtsTread)
  {
    m_blurtsTread = blurtsTread;
    m_threadStatus = THREAD_STATUS_STOPPED;
    start();
  }

  /*
   * run() outputs either "running" or "paused" based on the state of the thread
   */
  public void run()
  {
    EventLogger.logEvent( Blurts.LOGGER_ID, "SMS Thread Running".getBytes(), EventLogger.DEBUG_INFO );
    try
    {
      DatagramConnection _dc = (DatagramConnection)Connector.open( "sms://" );
      m_threadStatus = THREAD_STATUS_RUNNING;
      while ( m_threadStatus == THREAD_STATUS_RUNNING )
      {
        Datagram smsMsg = _dc.newDatagram( _dc.getMaximumLength() );
        _dc.receive( smsMsg );
        //byte[] bytes = smsMsg.getData();
        String address = smsMsg.getAddress();

        //writeFile( bytes );

        //String msg = new String( bytes, 0, bytes.length, "UTF-8" );
        //String msg = new String( bytes );
        String msg = decodeText( smsMsg );

        m_blurtsTread.IncomingSMS( address, msg );

        //System.out.println( "Received SMS text from " + address + " : " + msg );
      }
    }
    catch ( Exception ie )
    {
      m_threadStatus = THREAD_STATUS_STOPPED;
      String errorMsg = "SMS Thread Run Exception: " + ie.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, errorMsg.getBytes(), EventLogger.SEVERE_ERROR );
    }

    EventLogger.logEvent( Blurts.LOGGER_ID, "SMS Thread Stopped".getBytes(), EventLogger.DEBUG_INFO );
  }

  public int getThreadStatus()
  {
    return m_threadStatus;
  }

  public void stopThread()
  {
    m_threadStatus = THREAD_STATUS_STOPPED;
  }

  private static String decodeText( Datagram message )
  {
    byte[] bytes = message.getData();
    try
    {
      // convert Binary Data to Text
      return getTextDecoder( getMessageCoding( message ) ).decode( bytes );
    }
    catch ( UnsupportedEncodingException e )
    {
      EventLogger.logEvent( Blurts.LOGGER_ID, "Decoder didn't work, using default platform decoding".getBytes(), EventLogger.DEBUG_INFO );

      // Try just anything
      return new String( bytes );
    }
  }

  /**
     * Determine the message encoding (one of
     * {@link net.rim.device.api.system.SMSParameters} MESSAGE_CODING_* values).
     */
  private static int getMessageCoding( Datagram message )
  {
    try
    {
      DatagramBase base = (DatagramBase)message;
      SmsAddress address = (SmsAddress)base.getAddressBase();
      int coding = address.getHeader().getMessageCoding();
      String msg1 = "SMS Message Coding: " + coding;
      EventLogger.logEvent( Blurts.LOGGER_ID, msg1.getBytes(), EventLogger.DEBUG_INFO );
      return coding;
    }
    catch ( NullPointerException e )
    {
      // I suspect a null pointer sometimes happens here
      String msg = "NullPointerException SMS Coding: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.DEBUG_INFO );
      return SMSParameters.MESSAGE_CODING_DEFAULT;
    }
  }

  /**
     * Get an appropriate decoder for the given message coding.
     */
  private static TextDecoder getTextDecoder( int messageCoding )
  {
    switch ( messageCoding )
    {
      case SMSParameters.MESSAGE_CODING_8_BIT:
      return new TextDecoder.EightBit();
      case SMSParameters.MESSAGE_CODING_ASCII:
      return new TextDecoder.Ascii();
      case SMSParameters.MESSAGE_CODING_DEFAULT:
      return new TextDecoder.Default();
      case SMSParameters.MESSAGE_CODING_ISO8859_1:
      return new TextDecoder.Iso8859();
      //case SMSParameters.MESSAGE_CODING_KOREAN_KSX1001:
      //return new TextDecoder.Korean();
      case SMSParameters.MESSAGE_CODING_UCS2:
      return new TextDecoder.Ucs2();
      default:
      return new TextDecoder.Default();
    }

  }

  public interface TextDecoder
  {

    /**
     * Decodes the bytes into a string.
     *
     * @returns UnsupportedEncodingException if that didn't work
     */
    public String decode( byte[] bytes ) throws UnsupportedEncodingException;

    // -------------------------------------------------------------------------------------------------------
    // The implementations
    // -------------------------------------------------------------------------------------------------------

    public class EightBit implements TextDecoder
    {
      public String decode( byte[] bytes ) throws UnsupportedEncodingException
      {
        // ???
        return new String( bytes, "UTF-8" );
      }
    }

    public class Ascii implements TextDecoder
    {
      public String decode( byte[] bytes ) throws UnsupportedEncodingException
      {
        return new String( bytes, "US-ASCII" );
      }
    }

    public class Default implements TextDecoder
    {
      public String decode( byte[] bytes ) throws UnsupportedEncodingException
      {
        return new String( bytes );
        /*
        try
        {
          return DefaultAlphabetEncoding.getInstance().decodeString( bytes );
        }
        catch ( SMPPRuntimeException e )
        {
          throw new UnsupportedEncodingException( "decoding using DefaultAlphabetEncoding didn't work" );
        }
         * */
      }
    }

    public class Iso8859 implements TextDecoder
    {
      public String decode( byte[] bytes ) throws UnsupportedEncodingException
      {
        return new String( bytes, "ISO-8859-1" );
      }
    }

    public class Korean implements TextDecoder
    {
      public String decode( byte[] bytes ) throws UnsupportedEncodingException
      {
        // ???
        return new String( bytes, "UTF-8" );
      }
    }

    public class Ucs2 implements TextDecoder
    {
      public String decode( byte[] bytes ) throws UnsupportedEncodingException
      {
        return new String( bytes, "UTF-16BE" );
      }
    }
  }


  public void writeFile(byte[] data)
  {
    String fullPath = "file:///SDCard/blurts.dat";
    try
    {
      FileConnection fconn = (FileConnection)Connector.open( fullPath, Connector.READ_WRITE );

      if ( fconn.exists() )
      {
        fconn.delete();
      }

      fconn.create();

      OutputStream os = fconn.openOutputStream();

      os.write( data );

      os.close();

      fconn.close();
    }
    catch ( IOException e )
    {
      EventLogger.logEvent( Blurts.LOGGER_ID, "writeFile failed".getBytes(), EventLogger.DEBUG_INFO );
    }
  }
}

