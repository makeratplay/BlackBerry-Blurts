package com.mlhsoftware.Blurts;

import org.json.me.*;

import net.rim.blackberry.api.pdap.*;
import javax.microedition.pim.*;
import net.rim.device.api.system.*;
import java.util.Vector;

class CreateContactCmd extends CmdBase
{
  // Tag Names
  private static final String ALERT_NAME = "Contact";


  private static final String KEY_UID = "Uid";
  private static final String KEY_PREFIXNAME = "PrefixName";
  private static final String KEY_FIRSTNAME = "FirstName";
  private static final String KEY_LASTNAME = "LastName";
  private static final String KEY_DISPLAYNAME = "DisplayName";
  private static final String KEY_PHOTO = "Photo";
  private static final String KEY_ORG = "Org";
  private static final String KEY_PIN = "Pin";
  private static final String KEY_EMAIL = "Emails";
  private static final String KEY_TEL = "PhoneNumbers";
  private static final String KEY_WORK = "PhoneWork";
  private static final String KEY_MOBILE = "PhoneMobile";




  public CreateContactCmd( String string ) throws JSONException
  {
    super( string );
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

  String getWorkNumber()
  {
    return optString( KEY_WORK );
  }

  String getMobileNumber()
  {
    return optString( KEY_MOBILE );
  }

  String getEmail()
  {
    return optString( KEY_EMAIL );
  }
}