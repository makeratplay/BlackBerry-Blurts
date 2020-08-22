//#preprocess
/*
 * WebApiBase.java
 *
 * MLH Software
 * Copyright 2010
 */

//#ifdef BLURTS
package com.mlhsoftware.webapi.blurts;
//#endif 

//#ifdef SIMPLYREMINDME
package com.mlhsoftware.webapi.SimplyRemindMe;
//#endif 

//#ifdef SIMPLYTASKS
package com.mlhsoftware.webapi.SimplyTasks;
//#endif 

//#ifdef MLHKEY
package com.mlhsoftware.webapi.MLHKey;
//#endif 

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.Date;
import java.util.Enumeration;
import java.util.Hashtable;

import net.rim.blackberry.api.browser.Browser;
import net.rim.device.api.crypto.CryptoTokenException;
import net.rim.device.api.crypto.CryptoUnsupportedOperationException;
import net.rim.device.api.crypto.HMAC;
import net.rim.device.api.crypto.HMACKey;
import net.rim.device.api.crypto.RandomSource;
import net.rim.device.api.crypto.MD5Digest;
import net.rim.device.api.io.Base64OutputStream;
import net.rim.device.api.io.http.HttpProtocolConstants;
import net.rim.device.api.system.EventLogger;
import net.rim.device.api.util.Arrays;
import net.rim.device.api.util.Comparator;


import net.rim.device.api.util.StringUtilities;
import net.rim.device.api.system.DeviceInfo;
import net.rim.device.api.system.CoverageInfo;
import net.rim.device.api.servicebook.ServiceBook;
import net.rim.device.api.servicebook.ServiceRecord;


import javax.microedition.io.Connector;
import javax.microedition.io.HttpConnection;
import net.rim.device.api.system.EventLogger;

import net.rim.device.api.i18n.Locale;
import net.rim.device.api.system.RadioInfo;
import net.rim.device.api.system.CoverageInfo;



public class WebApiBase
{

  //#ifdef BLURTS
    public static String APP_NAME = "Blurts";
    public static long LOGGER_ID = 0xf0569edab4ce20faL;      //com.mlhsoftware.Blurts
  //#endif 

  private static final String MLH_API_METHOD = "json";
  private static final String MLH_API_SIGNATURE = "sig";
  private static final String TOKEN_SECRET = "**MLH0811210QWERRTT**";
  private static final String REQUEST_URL ="http://api.mlhsoftware.com/api.aspx";

  public static interface WebAPICallback
  {
    void callComplete( boolean wasSuccess, Object obj );
  }


  public WebApiBase()
  {
  }

  public static String api_header( String method )
  {
    String retVal = null;
    try
    {
      Hashtable params = new Hashtable();
      
      logMessage( method );
      String sig = signature( method );
      logMessage( sig );
      params.put( MLH_API_METHOD, escape( method ) );
      params.put( MLH_API_SIGNATURE, sig );
      //params.put( MLH_API_SIGNATURE, escape( sig ) );

      StringBuffer sb = new StringBuffer();
      Enumeration e = params.keys();
      while ( e.hasMoreElements() )
      {
        String k = (String)e.nextElement();
        sb.append(  k + "=" + (String)params.get( k ) ).append( '&' );
      }
      sb.deleteCharAt( sb.length() - 1 );

      retVal = sb.toString();
      System.out.println( retVal );
    }
    catch ( Exception e )
    {
      logMessage( "api_header failed: " + e.toString() );
    }
    return retVal;
  }


  public static void callMethod( String method, final CallerThread.CallerNotifier completeCallback )
  {
    try
    {
      CallerThread callerThread = new CallerThread( REQUEST_URL, api_header( method ), completeCallback );
      //callerThread.setPriority( Thread.MIN_PRIORITY );
      callerThread.start();
    }
    catch ( Exception e )
    {
      logMessage( "callMethod failed: " + e.toString() );
    }
  }

  public static String escape( String input )
  {
    final char[] reserved = { ':', '/', '?', '#', '[', ']', '@', '!', '$', '&', '\'', '(', ')', '*', '+', ',', ';', '=', ' ', '%' };
    StringBuffer o = new StringBuffer();

    try
    {
      if ( input != null )
      {
        int l = input.length();
        for ( int i = 0; i < l; i++ )
        {
          char ch = input.charAt( i );
          boolean escaped = false;
          for ( int j = 0; j < reserved.length; j++ )
          {
            if ( ch == reserved[j] )
            {
              o.append( '%' );
              String hex = Integer.toHexString( ch ).toUpperCase();
              o.append( hex );
              escaped = true;
              break;
            }
          }
          if ( !escaped )
          {
            o.append( ch );
          }
        }
      }
    }
    catch ( Exception e )
    {
      logMessage( "escape failed: " + e.toString() );
    }
    return o.toString();
  }

  public static String signature( String method )
  {
    String signature = new String();

    String strToHash = method + TOKEN_SECRET;
    try
    {
      byte[] bytes = strToHash.getBytes( "UTF-8" );
      MD5Digest digest = new MD5Digest();
      digest.update( bytes, 0, bytes.length );
      int length = digest.getDigestLength();
      byte[] md5 = new byte[length];
      digest.getDigest( md5, 0, true );

      signature = toHexString( md5 );
    }
    catch (Exception e )
    {
      logMessage( "signature failed: " + e.toString() );
    }
    return signature;
  }

  public static String toHexString( byte bytes[] )
  {
    if ( bytes == null )
    {
      return null;
    }

    StringBuffer sb = new StringBuffer();
    for ( int iter = 0; iter < bytes.length; iter++ )
    {
      byte high = (byte)( ( bytes[iter] & 0xf0 ) >> 4 );
      byte low = (byte)( bytes[iter] & 0x0f );
      sb.append( nibble2char( high ) );
      sb.append( nibble2char( low ) );
    }

    return sb.toString();
  }

  private static char nibble2char( byte b )
  {
    byte nibble = (byte)( b & 0x0f );
    if ( nibble < 10 )
    {
      return (char)( '0' + nibble );
    }
    return (char)( 'a' + nibble - 10 );
  }

  public static void logMessage( final String msg )
  {
    EventLogger.logEvent( LOGGER_ID, msg.getBytes(), EventLogger.DEBUG_INFO );
    System.out.println( APP_NAME + ": " + msg );
  }

}
