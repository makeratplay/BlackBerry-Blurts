package com.mlhsoftware.Blurts;

import net.rim.blackberry.api.phone.*;
import net.rim.blackberry.api.phone.phonelogs.*;
import net.rim.blackberry.api.invoke.*;
import net.rim.device.api.system.ControlledAccessException;
import net.rim.device.api.system.EventLogger;

class BlurtsCallListener implements PhoneListener
{
  private BlurtsThread _dataConnection;

  private String _syncToken;
  private String _callAction;
  private int _callId;

  public BlurtsCallListener( BlurtsThread dataConnection )
  {
    _dataConnection = dataConnection;
    _callId = 0;
    _callAction = null;
    _syncToken = "";
  }

  public void processCall()
  {
    boolean incomingCall = false;
    synchronized ( _syncToken )
    {
      if ( _callAction != null )
      {
        incomingCall = true;
      }
    }

    if ( incomingCall )
    {
      incomingCall();
    }
  }

  private void incomingCall()
  {
    DAOptionsProperties optionProperties = DAOptionsProperties.GetInstance();
    if ( optionProperties.displayCallAlert() )
    {
      try
      {
        String phoneNumber = "";
        String callAction = null;
        int callId = 0;
        synchronized ( _syncToken )
        {
          //phoneNumber = _incomingPhoneNumber;
          //_incomingPhoneNumber = "";
          callAction = _callAction;
          _callAction = null;
          callId = _callId;
        }

        try
        {
          PhoneCall phoneCall = Phone.getCall( callId );
          if ( phoneCall != null )
          {
            phoneNumber = phoneCall.getDisplayPhoneNumber();
          }
        }
        catch ( ControlledAccessException e )
        {
          String msg = "dataReceived failed: " + e.toString();
          EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
          System.out.println( msg );
        }

        String number = "";
        String name = "";
        PhoneCallLogID participant = new PhoneCallLogID( phoneNumber );
        if ( participant != null )
        {
          number = participant.getAddressBookFormattedNumber();
          if ( number == "" )
          {
            number = participant.getNumber();
          }
          name = participant.getName();
        }

        CallAlert alert = new CallAlert();
        alert.setAction( callAction );
        alert.setNumber( number );
        alert.setName( name );
        _dataConnection.writeData( alert.toString() );
      }
      catch ( Exception e )
      {
        String msg = "incomingCall failed: " + e.toString();
        EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
        System.out.println( msg );
      }
    }
  }


  




  /// Phone Listener Code
  public void callIncoming( int callId )
  {
    System.out.println( "MLH - callIncoming1: " + callId );
    EventLogger.logEvent( Blurts.LOGGER_ID, ( "MLH - callIncoming1: " + callId ).getBytes(), EventLogger.DEBUG_INFO );
    synchronized ( _syncToken )
    {
      _callId = callId;
      _callAction = "Incoming";
    }

    //System.out.println( "MLH - callIncoming" );
  }

  public void callAnswered( int callId )
  {
    synchronized ( _syncToken )
    {
      _callId = callId;
      _callAction = "Answered";
    }
    //System.out.println( "MLH - callAnswered" );
  }

  public void callDisconnected( int callId )
  {
    synchronized ( _syncToken )
    {
      _callId = callId;
      _callAction = "Disconnected";
    }
    //System.out.println( "MLH - callDisconnected" );
  }


  public void callInitiated( int callId )
  {
    synchronized ( _syncToken )
    {
      _callId = callId;
      _callAction = "Initiated";
    }

    //System.out.println( "MLH - callInitiated" );
  }

  public void callConnected( int callId )
  {
    synchronized ( _syncToken )
    {
      _callId = callId;
      _callAction = "Connected";
    }

    //System.out.println( "MLH - callConnected" );
  }

  public void callWaiting( int callId )
  {
    synchronized ( _syncToken )
    {
      _callId = callId;
      _callAction = "Waiting";
    }
  }

  public void callConferenceCallEstablished( int callId )
  {
    //System.out.println( "MLH - callConferenceCallEstablished" );
  }

  public void conferenceCallDisconnected( int callId )
  {
    //System.out.println( "MLH - conferenceCallDisconnected" );
  }

  public void callDirectConnectConnected( int callId )
  {
    //System.out.println( "MLH - callDirectConnectConnected" );
  }

  public void callDirectConnectDisconnected( int callId )
  {
    //System.out.println( "MLH - callDirectConnectDisconnected" );
  }

  public void callEndedByUser( int callId )
  {
    //System.out.println( "MLH - callEndedByUser" );
  }

  public void callFailed( int callId, int reason )
  {
    //System.out.println( "MLH - callFailed" );
  }

  public void callResumed( int callId )
  {
    //System.out.println( "MLH - callResumed" );
  }

  public void callHeld( int callId )
  {
    //System.out.println( "MLH - callHeld" );
  }

  public void callAdded( int callId )
  {
    //System.out.println( "MLH - callAdded" );
  }

  public void callRemoved( int callId )
  {
    //System.out.println( "MLH - callRemoved" );
  }


  public void makePhoneCall( String phoneNumber )
  {
    Invoke.invokeApplication( Invoke.APP_TYPE_PHONE, new PhoneArguments( PhoneArguments.ARG_CALL, phoneNumber ) );
  }
}

