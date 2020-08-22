package com.mlhsoftware.Blurts;

import java.util.*;
import java.io.*;
import net.rim.device.api.system.*;
import net.rim.device.api.util.*;
import net.rim.blackberry.api.pdap.*;
import javax.microedition.pim.*;

public final class ContactsList extends Thread
{
  private ContactList blackBerryContactList;
  private BlackBerryContact blackBerryContact;
  private Vector blackBerryContacts;



  BlurtsThread _dataConnection;

  //Constructor, sets up the screen.
  public ContactsList( BlurtsThread dataConnection )
  {
    _dataConnection = dataConnection;
  }

  public void run()
  {
    GetContactXML();
  }

  //Reloads the list of contacts from the
  //BlackBerry Contacts database.
  public void GetContactXML( )
  {
      
    String xml = "Action Failed";
    try 
    {
      blackBerryContactList =(ContactList)PIM.getInstance().openPIMList(PIM.CONTACT_LIST, PIM.READ_ONLY);

      int contactTotal = 0;
      _dataConnection.statusMessage( "Preparing contacts download" );
      Enumeration allContacts = blackBerryContactList.items();
      if ( allContacts != null )
      {
        int count = 0;
        while ( allContacts.hasMoreElements() )
        {
          contactTotal++;
          allContacts.nextElement();
        } // end of while
      }

      _dataConnection.statusMessage( "Downloading " + contactTotal + " contacts" );
      //_dataConnection.sendData();

      allContacts = blackBerryContactList.items();
      if ( allContacts != null )
      {
        int count = contactTotal;
        while ( allContacts.hasMoreElements() )
        {
          
          String progressMsg = "Processing Contact" + count;
          EventLogger.logEvent( Blurts.LOGGER_ID, progressMsg.getBytes(), EventLogger.DEBUG_INFO );

          BlackBerryContact bbContact = (BlackBerryContact)allContacts.nextElement();
          ContactAlert alert = new ContactAlert( bbContact );
          System.out.println( "Sending contact" + count );
          alert.setIndex( count-- );
          int size = _dataConnection.writeData( alert.toString(), null );
          
          // sleep until data sent
          while ( size > 0 )
          {
            Thread.sleep( 50 );
            size = _dataConnection.getSendBufferSize();
          }
        } // end of while
        _dataConnection.statusMessage( "Contact Download Complete" );
      }
    }
    catch (PIMException e)
    {
      String msg = "PIMException: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
      _dataConnection.statusMessage( msg );
    }
    catch ( Exception e)
    {
      String msg = "Contact Exception: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
      _dataConnection.statusMessage( msg );
    }    
  }

  

  static public void createContact( CreateContactCmd contactCmd )
  {
    try 
    {
      ContactList contactList = (ContactList)PIM.getInstance().openPIMList(PIM.CONTACT_LIST, PIM.WRITE_ONLY);
      Contact newContact = contactList.createContact();

      // Create string array for name.
      String[] name = new String[5]; // 5 name elements
      try 
      {
        name[Contact.NAME_PREFIX] = contactCmd.getPrefixName();
        name[Contact.NAME_FAMILY] = contactCmd.getLastName();
        name[Contact.NAME_GIVEN] = contactCmd.getFirstName();
        // Add name.
        if(contactList.isSupportedField(Contact.NAME)) 
        {
          newContact.addStringArray( Contact.NAME, Contact.ATTR_NONE, name );
        }
      } 
      catch (IllegalArgumentException iae) 
      {
        // handle exception
      }

      if ( contactList.isSupportedField( Contact.ORG ) )
      {
        String org = contactCmd.getOrg();
        if ( org.length() > 0 )
        {
          newContact.addString( Contact.ORG, Contact.ATTR_NONE, contactCmd.getOrg() );
        }
      }

      try
      {
        if ( contactList.isSupportedField( Contact.PHOTO ) )
        {
          String photoString = contactCmd.getPhoto();
          if ( photoString.length() > 0 )
          {
            byte[] photo = photoString.getBytes();
            newContact.addBinary( Contact.PHOTO, Contact.ATTR_NONE, photo, 0, photo.length );
          }
        }
      }
      catch ( Exception iae )
      {
        // handle exception
      }

      if ( contactList.isSupportedField( BlackBerryContact.PIN ) )
      {
        newContact.addString( BlackBerryContact.PIN, Contact.ATTR_NONE, contactCmd.getPin() );
      }

      // add loop to add all emails
      if ( contactList.isSupportedField( Contact.EMAIL ) )
      {
        newContact.addString( Contact.EMAIL, Contact.ATTR_NONE, contactCmd.getEmail() );
      }


      // Add home telephone number.
      if ( contactList.isSupportedField( Contact.TEL ) && contactList.isSupportedAttribute( Contact.TEL, Contact.ATTR_WORK ) )
      {
        newContact.addString( Contact.TEL, Contact.ATTR_WORK, contactCmd.getWorkNumber() );
      }
      // Add work telephone number.
      if ( contactList.isSupportedField( Contact.TEL ) && contactList.isSupportedAttribute( Contact.TEL, Contact.ATTR_MOBILE ) )
      {
        newContact.addString( Contact.TEL, Contact.ATTR_MOBILE, contactCmd.getMobileNumber() );
      }

      if ( newContact.isModified() )
      {
        newContact.commit();
      }

    } 
    catch (PIMException e) 
    {
    }


    /*
// Create string array for address.
String[] address = new String[7]; // 7 address elements
try
{
  address[Contact.ADDR_COUNTRY] = "United States";
  address[Contact.ADDR_LOCALITY] = "Los Angeles";
  address[Contact.ADDR_POSTALCODE] = "632300";
  address[Contact.ADDR_REGION] = "California";
  address[Contact.ADDR_STREET] = "323 Main Street";
  // Add address.
  newContact.addStringArray( Contact.ADDR, Contact.ATTR_NONE, address );
}
catch ( IllegalArgumentException iae )
{
  // Handle exception.
}
 * */
    /*

    

    // Add work internet messaging address.
    if ( contactList.isSupportedField( Contact.EMAIL ) )
    {
      newContact.addString( Contact.EMAIL, Contact.ATTR_WORK,
      "aisha.wahl@blackberry.com" );
    }




    String firstName = _first.getText();
    String lastName = _last.getText();
    String email = _email.getText();
    String phone = _phone.getText();
    String pin = _pin.getText();

    // Verify that a first or last name and email has been entered.
    if ( (firstName.equals("") && lastName.equals("") ) || email.equals("") ) 
    {
      Dialog.inform("You must enter a name and an email address!");
      return false;
    } 
    else 
    {
      try 
      {
        ContactList contactList = (ContactList)PIM.getInstance().openPIMList(PIM.CONTACT_LIST, PIM.WRITE_ONLY);
        Contact newContact = contactList.createContact();
        String[] name = new String[contactList.stringArraySize(Contact.NAME)];
        // Add values to PIM item.
        if (!firstName.equals("")) 
        {
          name[Contact.NAME_GIVEN] = firstName;
        }
        if (!lastName.equals("")) 
        {
          name[Contact.NAME_FAMILY] = lastName;
        }
        newContact.addStringArray(Contact.NAME, Contact.ATTR_NONE, name);
        newContact.addString(Contact.EMAIL, Contact.ATTR_HOME, email);
        newContact.addString(Contact.TEL, Contact.ATTR_WORK, phone);
        if ( contactList.isSupportedField(BlackBerryContact.PIN)) 
        {
          newContact.addString(BlackBerryContact.PIN, Contact.ATTR_NONE, pin);
        }
        
        // Save data to address book.
        newContact.commit();
        
        // Reset UI fields.
        _first.setText("");
        _last.setText("");
        _email.setText("");
        _phone.setText("");
        _pin.setText("");
        return true;
    } 
    catch (PIMException e) 
    {
      return false;
    }
     * */
  }

}

