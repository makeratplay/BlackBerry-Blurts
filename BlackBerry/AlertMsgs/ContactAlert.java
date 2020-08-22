package com.mlhsoftware.Blurts;

import org.json.me.*;

import net.rim.blackberry.api.pdap.*;
import javax.microedition.pim.*;
import net.rim.device.api.system.*;
import java.util.Vector;

class ContactAlert extends AlertBase 
{
  // Tag Names
  private static final String ALERT_NAME = "Contact";
  private static final String KEY_STATUS = "Text";


  private static final String KEY_UID         = "Uid";
  private static final String KEY_INDEX       = "Index";
  private static final String KEY_PREFIXNAME = "PrefixName";
  private static final String KEY_FIRSTNAME   = "FirstName";
  private static final String KEY_LASTNAME    = "LastName";
  private static final String KEY_DISPLAYNAME = "DisplayName";
  private static final String KEY_PHOTO       = "Photo";
  private static final String KEY_ORG         = "Org";
  private static final String KEY_PIN         = "Pin";
  private static final String KEY_EMAIL       = "Emails";
  private static final String KEY_TEL         = "PhoneNumbers";
  


  public ContactAlert( String string ) throws JSONException
  {
    super( string );
  }

  public ContactAlert( BlackBerryContact bbContact )
  {
    super( TYPE_CONTACTS, ALERT_NAME );
    getUID( bbContact );
    getName( bbContact );
    getOrg( bbContact );
    getPIN( bbContact );
    getEmail( bbContact );
    getTel( bbContact );
    getPhoto( bbContact );
  }

  public void setUID( String value )
  {
    try
    {
      put( KEY_UID, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  public void setIndex( int value )
  {
    try
    {
      put( KEY_INDEX, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }
  

  String getUID()
  {
    return optString( KEY_UID );
  }

  public void setPrefixName( String value )
  {
    try
    {
      put( KEY_PREFIXNAME, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  String getPrefixName()
  {
    return optString( KEY_PREFIXNAME );
  }

  public void setFirstName( String value )
  {
    try
    {
      put( KEY_FIRSTNAME, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  String getFirstName()
  {
    return optString( KEY_FIRSTNAME );
  }

  public void setLastName( String value )
  {
    try
    {
      put( KEY_LASTNAME, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  String getLastName()
  {
    return optString( KEY_LASTNAME );
  }

  public void setDisplayName( String value )
  {
    try
    {
      put( KEY_DISPLAYNAME, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  String getDisplayName()
  {
    return optString( KEY_DISPLAYNAME );
  }


  public void setPhoto( String value )
  {
    try
    {
      put( KEY_PHOTO, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  String getPhoto()
  {
    return optString( KEY_PHOTO );
  }


  public void setPin( String value )
  {
    try
    {
      put( KEY_PIN, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  String getPin()
  {
    return optString( KEY_PIN );
  }


  public void setOrg( String value )
  {
    try
    {
      put( KEY_ORG, value );
    }
    catch ( JSONException e )
    {
      System.out.println( e.toString() );
    }
  }

  String getOrg()
  {
    return optString( KEY_ORG );
  }

  /////////  Helper methods ///////////

  private void getName( BlackBerryContact bbContact )
  {
    try
    {
      //Verify that the contact has a name.
      if ( bbContact.countValues( BlackBerryContact.NAME ) > 0 )
      {
        String displayName = "";
        final String[] name = bbContact.getStringArray( BlackBerryContact.NAME, 0 );
        final String prefixName = name[BlackBerryContact.NAME_PREFIX];
        final String firstName = name[BlackBerryContact.NAME_GIVEN];
        final String lastName = name[BlackBerryContact.NAME_FAMILY];
        if ( prefixName != null && (firstName != null || lastName != null) )
        {
          displayName += prefixName;
          displayName += " ";
        }
        if ( firstName != null )
        {
          displayName += firstName;
          displayName += " ";
        }
        if ( lastName != null )
        {
          displayName += lastName;
        }

        if ( displayName == "" )
        {
          // If not, use the company name.
          if ( bbContact.countValues( Contact.ORG ) > 0 )
          {
            final String companyName = bbContact.getString( Contact.ORG, 0 );
            if ( companyName != null )
            {
              displayName = companyName;
            }
          }
        }

        setPrefixName( prefixName );
        setFirstName( firstName );
        setLastName( lastName );
        setDisplayName( displayName );
      }
    }
    catch ( Exception e )
    {
      String msg = "BlackBerryContact.NAME failed: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
  }


  private void getPhoto( BlackBerryContact bbContact )
  {
    try
    {
      if ( bbContact.countValues( BlackBerryContact.PHOTO ) > 0 )
      {
        byte[] photoEncoded = bbContact.getBinary( Contact.PHOTO, 0 );
        String base64Data = new String( photoEncoded, 0, photoEncoded.length );
        setPhoto( base64Data );
      }
    }
    catch ( Exception e )
    {
      String msg = "BlackBerryContact.PHOTO failed: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
  }

  private void getUID( BlackBerryContact bbContact )
  {
    try
    {
      if ( bbContact.countValues( BlackBerryContact.UID ) > 0 )
      {
        String id = bbContact.getString( BlackBerryContact.UID, 0 );
        setUID( id );
      }
    }
    catch ( Exception e )
    {
      String msg = "BlackBerryContact.UID failed: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
  }

  private void getPIN( BlackBerryContact bbContact )
  {
    try
    {
      if ( bbContact.countValues( BlackBerryContact.PIN ) > 0 )
      {
        String pin = bbContact.getString( BlackBerryContact.PIN, 0 );
        setPin( pin );
      }
    }
    catch ( Exception e )
    {
      String msg = "BlackBerryContact.PIN failed: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
  }

  private void getOrg( BlackBerryContact bbContact )
  {
    try
    {
      if ( bbContact.countValues( BlackBerryContact.ORG ) > 0 )
      {
        String org = bbContact.getString( BlackBerryContact.ORG, 0 );
        setOrg( org );
      }
    }
    catch ( Exception e )
    {
      String msg = "BlackBerryContact.ORG failed: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }        
  }

  private void getEmail( BlackBerryContact bbContact )
  {
    try
    {
      Vector collection = new Vector();
      for ( int index = 0; index < bbContact.countValues( BlackBerryContact.EMAIL ); ++index )
      {
        String email = bbContact.getString( BlackBerryContact.EMAIL, index );
        JSONObject obj = new JSONObject();
        obj.put( "Email", email );
        collection.addElement( obj );
      }
      if ( collection.size() > 0 )
      {
        JSONArray emailArray = new JSONArray( collection );
        put( KEY_EMAIL, emailArray );
      }
    }
    catch ( Exception e )
    {
      String msg = "BlackBerryContact.EMAIL failed: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
  }

  private void getTel( BlackBerryContact bbContact )
  {
    try
    {
      Vector collection = new Vector();
      for ( int index = 0; index < bbContact.countValues( BlackBerryContact.TEL ); ++index )
      {
        String phoneNumber = bbContact.getString( BlackBerryContact.TEL, index );

        String telType = "";
        switch ( bbContact.getAttributes( BlackBerryContact.TEL, index ) )
        {
          case BlackBerryContact.ATTR_WORK:
          {
            telType = "Work";
            break;
          }
          case BlackBerryContact.ATTR_HOME:
          {
            telType = "Home";
            break;
          }
          case BlackBerryContact.ATTR_MOBILE:
          {
            telType = "Mobile";
            break;
          }
          case BlackBerryContact.ATTR_PAGER:
          {
            telType = "Pager";
            break;
          }
          case BlackBerryContact.ATTR_FAX:
          {
            telType = "Fax";
            break;
          }
          default:
          {
            telType = "Other";
          }
        }

        JSONObject obj = new JSONObject();
        obj.put( "Number", phoneNumber );
        obj.put( "Type", telType );

        JSONObject parent = new JSONObject();
        parent.put( "PhoneNumber", obj );


        collection.addElement( parent );
      }
      if ( collection.size() > 0 )
      {
        JSONArray array = new JSONArray( collection );
        put( KEY_TEL, array );
      }
    }
    catch ( Exception e )
    {
      String msg = "BlackBerryContact.TEL failed: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
  }
}
