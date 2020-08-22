
package com.mlhsoftware.Blurts;

import net.rim.blackberry.api.mail.event.*;

import net.rim.blackberry.api.mail.*;
import net.rim.device.api.system.*;
import java.util.*;

import net.rim.device.api.system.EventLogger;

class BlurtsEmailListener implements FolderListener
{
  private String _accountAddress;
  private BlurtsThread _dataConnection;
  private int _index;
  private Vector m_inboundMsg;


  public BlurtsEmailListener( BlurtsThread dataConnection, String accountAddress, int index )
  {
    _dataConnection = dataConnection;
    _accountAddress = accountAddress;
    _index = index;
    m_inboundMsg = new Vector();
  }

  public void processEmail()
  {
    FolderEvent e = null;
    synchronized ( m_inboundMsg )
    {
      if ( !m_inboundMsg.isEmpty() )
      {
        e = (FolderEvent)m_inboundMsg.elementAt( 0 );
        m_inboundMsg.removeElementAt( 0 );
      }
    }
    if ( e != null )
    {
      DAOptionsProperties optionProperties = DAOptionsProperties.GetInstance();
      try
      {
        //Get the message that fired the folder event.
        Message orginalMessage = e.getMessage();

        int type = Message.EMAIL_MESSAGE; //orginalMessage.getMessageType();
        Address from = orginalMessage.getFrom();
        if ( from != null )
        {
          String address =  from.getAddr();
          if ( address.indexOf( '@' ) == -1 )
          {
            type = Message.PIN_MESSAGE;
          }
        }

        if ( type == Message.PIN_MESSAGE )
        {
          processPINMsg( orginalMessage );
        }
        else
        {
          if ( optionProperties.displayEmailAlert( _index ) )
          {
            //Get the folder the message is in.
            Folder messageFolder = orginalMessage.getFolder();

            //Is the new message in the Inbox?
            if ( messageFolder.getType() == Folder.INBOX )
            {
              EmailAlert alert = new EmailAlert();
              alert.setAccount( _accountAddress );
              //try
              {
                //Address from = orginalMessage.getFrom();
                if ( from != null )
                {
                  alert.setName( from.getName() );
                  alert.setAddress( from.getAddr() );
                }
              }
              //catch ( net.rim.blackberry.api.mail.MessagingException exc )
              //{
              //}

              alert.setSubject( orginalMessage.getSubject() );
              alert.setBody( orginalMessage.getBodyText() );
              _dataConnection.writeData( alert.toString() );
            }
          }
        }
      }
      catch ( Exception ex )
      {
        String msg = "Process Email Error: " + ex.getMessage();
        EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
        System.out.println( msg );
      }
    }
  }

  private void processPINMsg( Message orginalMessage )
  {
    try
    {
      DAOptionsProperties optionProperties = DAOptionsProperties.GetInstance();
      if ( optionProperties.displayPinMsgAlert() )
      {
        //Get the folder the message is in.
        Folder messageFolder = orginalMessage.getFolder();

        //Is the new message in the Inbox?
        if ( messageFolder.getType() == Folder.INBOX )
        {
          PINMsgAlert alert = new PINMsgAlert();
          alert.setAccount( _accountAddress );
          try
          {
            Address from = orginalMessage.getFrom();
            if ( from != null )
            {
              alert.setName( from.getName() );
              alert.setAddress( from.getAddr() );
            }
          }
          catch ( net.rim.blackberry.api.mail.MessagingException exc )
          {
          }

          alert.setSubject( orginalMessage.getSubject() );
          alert.setBody( orginalMessage.getBodyText() );
          _dataConnection.writeData( alert.toString() );
        }
      }
    }
    catch ( Exception ex )
    {
      String msg = "PIN Messages Added Error: " + ex.getMessage();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
  }

  // Folder Listener Code

  //Called when a new message is created.
  public void messagesAdded( FolderEvent e )
  {
    synchronized ( m_inboundMsg )
    {
      m_inboundMsg.addElement( e );
    }
  }

  //Not used
  public void messagesRemoved( FolderEvent e )
  {
  }

  public static void addToInboxFolder( String addr, String name, String subject, String message )
  {
    try
    {
      Address fromAddress = new Address( addr, name );
      Session session = Session.getDefaultInstance(); //Session.waitForDefaultSession();
      if ( session != null )
      {
        Store store = session.getStore();
        Folder[] folders = store.list( Folder.INBOX );
        Folder inbox = folders[0];

        final Message msg = new Message( inbox );
        msg.setContent( message );
        msg.setFrom( fromAddress );
        msg.setStatus( Message.Status.RX_RECEIVED, Message.Status.RX_RECEIVED );
        msg.setSentDate( new Date( System.currentTimeMillis() ) );
        //msg.setFlag( Message.Flag.REPLY_ALLOWED, true );
        msg.setInbound( true );
        msg.setSubject( subject );
        inbox.appendMessage( msg );
      }
      else
      {
        String msg = "addToInboxFolder Error: No Default Session";
        EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
        System.out.println( msg );
      }
    }
    catch ( Exception ex )
    {
      String msg = "addToInboxFolder Error: " + ex.getMessage();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
  } 
}

