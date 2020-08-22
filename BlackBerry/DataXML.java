package com.mlhsoftware.Blurts;

import java.lang.String;
import java.util.*;
import java.io.*;
import net.rim.device.api.xml.jaxp.XMLWriter;
import net.rim.device.api.xml.jaxp.DOMInternalRepresentation;
import net.rim.device.api.xml.parsers.*;
import org.w3c.dom.*;
import org.xml.sax.SAXException;
import net.rim.device.api.system.EventLogger;

class DataXML
{
  // XML Node Names
  public static final String END_TAG = "</BlurtsData>";
  public static final String ROOT_NODE = "BlurtsData";
  public static final String EMAIL_NODE = "Email";
  public static final String SENDER_NAME_NODE = "SenderName";
  public static final String SENDER_ADDRESS_NODE = "SenderAddress";
  public static final String SUBJECT_NODE = "Subject";
  public static final String BODY_NODE = "BodyText";
  public static final String STATUS_NODE = "Status";
  public static final String CALL_NODE = "Call";
  public static final String PHONE_NUMBER_NODE = "PhoneNumber";
  public static final String CALLER_NAME_NODE = "CallerName";
  public static final String MENUS_NODE = "Menus";
  public static final String MENU_NODE = "Menu";
  public static final String SMS_NODE = "SMS";
  public static final String SCREEN_NODE = "Screen";
  public static final String IMAGEDATA_NODE = "ImageData";
  public static final String CONTACTS_NODE = "Contacts";
  public static final String CONTACT_NODE = "Contact";
  public static final String CONTACT_LASTNAME_NODE = "LastName";
  public static final String CONTACT_FIRSTNAME_NODE = "FirstName";
  public static final String CONTACT_PHONENUMBER_NODE = "PhoneNumber";
  public static final String CONTACT_PHOTO_NODE = "Photo";
  public static final String CONTACT_PIN_NODE = "Pin";
  public static final String CONTACT_COMPANY_NODE = "Company";
  public static final String CONTACT_DISPLAYNAME_NODE = "DisplayName";
  public static final String CONTACT_EMAIL_NODE = "Email";
  public static final String CLIPBOARD_NODE = "Clipboard";


  // Alert Types
  public static final int TYPE_UNKNOWN = 0;
  public static final int TYPE_STATUS  = 1;
  public static final int TYPE_EMAIL   = 2;
  public static final int TYPE_CALL    = 3;
  public static final int TYPE_LOCK    = 4;
  public static final int TYPE_SMS     = 5;
  public static final int TYPE_SCREEN  = 6;
  public static final int TYPE_CONTACTS  = 7;
  public static final int TYPE_CLIPBOARD = 9;
  

  // Command IDs
  public static final int CMD_NOTPRO         = -2;
  public static final int CMD_UNKNOWN        = 0;
  public static final int CMD_PLACECALL      = 1;
  public static final int CMD_PRESSKEY       = 2;
  public static final int CMD_BUZZ           = 3;
  public static final int CMD_SENDSMS        = 4;
  public static final int CMD_CONTACTSEARCH  = 5;
  public static final int CMD_SCREENSHOT     = 6;
  public static final int CMD_CREATECONTACT  = 7;
  public static final int CMD_READCLIPBOARD  = 8;
  public static final int CMD_WRITECLIPBOARD = 9;

  private static DocumentBuilderFactory m_dBFactory = null;

  public Document m_document;
  private int m_type;
  private String m_statusText;
  private String m_senderAddress;
  private String m_senderName;
  private String m_subject;
  private String m_bodyText;
  private String m_phoneNumber;
  private String m_callerName;
  private String m_actionCode;
  private String m_callAction;
  private String m_imageData;

  private Vector m_menus;

  class MenuXML
  {
    public String name;
    public String ordinal;
  }

  public DataXML( String xml )
  {
    common();

    

    try 
    {
      //EventLogger.logEvent( Blurts.LOGGER_ID, xml.getBytes( "UTF-8" ), EventLogger.DEBUG_INFO );
      InputStream inputStream = new ByteArrayInputStream(xml.getBytes("UTF-8"));
      DocumentBuilder builder = m_dBFactory.newDocumentBuilder();
      m_document = builder.parse( inputStream );
    }
    catch (Exception e) 
    {
      String msg = "DataXML failed: " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
  }

  public DataXML( int type )
  {
    m_type = type;
    common();
  }

  public void common()
  {
    m_callAction = "";
    m_senderAddress = null;
    m_senderName = null;
    m_subject = null;
    m_bodyText = null;
    m_menus = null;
    m_document = null;

    if ( m_dBFactory == null )
    {
      m_dBFactory = DocumentBuilderFactory.newInstance();
    }

    switch ( m_type )
    {
      case TYPE_UNKNOWN:
      {
        m_actionCode = "";
        break;
      }
      case TYPE_STATUS:
      {
        m_actionCode = "statusMsg";
        break;
      }
      case TYPE_EMAIL:
      {
        m_actionCode = "emailMsg";
        break;
      }
      case TYPE_CALL:
      {
        m_actionCode = "callMsg";
        break;
      }
      case TYPE_LOCK:
      {
        m_statusText = "Locking workstation";
        m_actionCode = "lockWorkstation";
        break;
      }
      case TYPE_SMS:
      {
        m_actionCode = "smsMsg";
        break;
      }
      case TYPE_SCREEN:
      {
        m_actionCode = "screen";
        break;
      }
      case TYPE_CONTACTS:
      {
        m_actionCode = "contacts";
        break;
      }
      case TYPE_CLIPBOARD:
      {
        m_actionCode = "clipboard";
        break;
      }
      default:
      {
        m_actionCode = "";
      }
    }
  }

  public void addMenu( String name, int ordinal )
  {
    if ( m_menus == null )
    {
      m_menus = new Vector();
    }

    MenuXML menu = new MenuXML();
    menu.name = name;
    menu.ordinal = "" + ordinal ;

    m_menus.addElement( menu );
  }

  public void setPhoneNumber( String val )
  {
    m_phoneNumber = "";
    if ( val != null )
    {
      boolean add = false;
      int len = val.length();
      for ( int x = 0; x < len; x++ )
      {
        if ( val.charAt( x ) >= '0' && val.charAt( x ) <= '9' )
        {
          add = true;
        }
        else if ( val.charAt( x ) == '(' )
        {
          add = true;
        }

        if ( add )
        {
          m_phoneNumber += val.charAt( x );
        }
      }
    }
    //m_phoneNumber = val;
  }

  public void setCallAction( int action )
  {
    m_callAction = "" + action; // convert to string
  }

  public void setCallerName( String val )
  {
    m_callerName = val;
  }

  public void setStatusText( String val )
  {
    m_statusText = val;
  }

  public void setSenderName( String val )
  {
    m_senderName = val;
  }

  public void setSenderAddress( String val )
  {
    m_senderAddress = val;
  }

  public void setSubject( String val )
  {
    m_subject = val;
  }

  public void setBodyText( String val )
  {
    m_bodyText = val;
  }

  public void setData( String data )
  {
    m_imageData = data;
  }

  public String toString()
  {
    return CreateXMLString();
  }

  public int GetCommandId()
  {
    int retVal = 0;
    int tmpId = 0;
    if ( m_document != null )
    {
      try
      {
        NodeList nodes = m_document.getElementsByTagName( "Command" );
        if ( nodes.getLength() > 0 )
        {
          Element node = (Element)nodes.item( 0 );
          String val = node.getAttribute( "id" );
          tmpId = Integer.parseInt( val );
        }
      }
      catch ( Exception e )
      {
        String msg = "GetCommandId failed: " + e.toString();
        EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
        System.out.println( msg );
      }
    }

    if ( tmpId == CMD_BUZZ )
    {
      retVal = CMD_BUZZ;
    }
    else
    {
      if ( Blurts.m_optionProperties != null )
      {
        if ( Blurts.m_optionProperties.isKeyValid() )
        {
          retVal = tmpId;
        }
        else
        {
          retVal = -2; // Not Pro Version
        }
      }
    }
    return retVal;
  }

  public String GetParam1()
  {
    String retVal = "";
    if ( m_document != null )
    {
      try
      {
        NodeList nodes = m_document.getElementsByTagName( "Param1" );
        if ( nodes.getLength() > 0 )
        {
          Node node = nodes.item( 0 );
          Node text = node.getFirstChild();
          if ( text != null )
          {
            String tmp = text.getNodeValue();
            if ( tmp != null )
            {
              retVal = tmp;
            }
          }
        }
      }
      catch ( Exception e )
      {
        String msg = "GetParam1 failed: " + e.toString();
        EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
        System.out.println( msg );
      }
    }
    return retVal;
  }

  public String GetParam2()
  {
    String retVal = "";
    if ( m_document != null )
    {
      try
      {
        NodeList nodes = m_document.getElementsByTagName( "Param2" );
        if ( nodes.getLength() > 0 )
        {
          Node node = nodes.item( 0 );
          Node text = node.getFirstChild();
          if ( text != null )
          {
            String tmp = text.getNodeValue();
            if ( tmp != null )
            {
              retVal = tmp;
            }
          }
        }
      }
      catch ( Exception e )
      {
        String msg = "GetParam2 failed: " + e.toString();
        EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
        System.out.println( msg );
      }
    }
    return retVal;
  }

  public String GetParam3()
  {
    String retVal = "";
    if (m_document != null)
    {
      try
      {
        NodeList nodes = m_document.getElementsByTagName("Param3");
        if (nodes.getLength() > 0)
        {
          Node node = nodes.item(0);
          Node text = node.getFirstChild();
          if (text != null)
          {
            String tmp = text.getNodeValue();
            if (tmp != null)
            {
              retVal = tmp;
            }
          }
        }
      }
      catch (Exception e)
      {
        String msg = "GetParam3 failed: " + e.toString();
        EventLogger.logEvent(Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR);
        System.out.println(msg);
      }
    }
    return retVal;
  }

  public Element createContactDoc()
  {
    Element ContactsNode = null;
    if ( m_type == TYPE_CONTACTS )
    {
      try
      {
        DocumentBuilder dBuilder = m_dBFactory.newDocumentBuilder();
        m_document = dBuilder.newDocument();

        Element rootNode = m_document.createElement( ROOT_NODE );
        rootNode.setAttribute( "version", com.mlhsoftware.AboutDialog.APP_VERSION );
        rootNode.setAttribute( "xml_ver", "1" );
        rootNode.setAttribute( "actionCode", m_actionCode );

        if ( Blurts.m_optionProperties != null )
        {
          rootNode.setAttribute( "activated", Blurts.m_optionProperties.isKeyValid() ? "true" : "false" );
        }
        m_document.appendChild( rootNode );


        ContactsNode = m_document.createElement( CONTACTS_NODE );
        rootNode.appendChild( ContactsNode );

      }
      catch ( DOMException e )
      {
        String msg = "createContactDoc failed(1): " + e.toString();
        EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
        System.out.println( msg );
      }
      catch ( ParserConfigurationException e )
      {
        String msg = "createContactDoc failed(2): " + e.toString();
        EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
        System.out.println( msg );
      }
      catch ( Exception e )
      {
        String msg = "createContactDoc failed(4): " + e.toString();
        EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
        System.out.println( msg );
      }
    }
    return ContactsNode;
  }

  public String getContactXML()
  {
    String xml = "";  
    try
    {
      ByteArrayOutputStream os = new ByteArrayOutputStream();
      XMLWriter writer = new XMLWriter( os );
      writer.setPrintCompressedOutput();
      DOMInternalRepresentation.parse( m_document, writer );

      xml = os.toString();
    }
    catch ( DOMException e )
    {
      String msg = "getContactXML failed(1): " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
    catch ( SAXException e )
    {
      String msg = "getContactXML failed(3): " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg ); 
    }
    catch ( Exception e )
    {
      String msg = "getContactXML failed(4): " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }

    return xml;
  }

  public String CreateXMLString()
  {
    String xml = "";
    try
    {

      BlurtsThread._session.tagEvent( "AS_" + m_type );

      DocumentBuilder dBuilder = m_dBFactory.newDocumentBuilder();
      Document myDocument = dBuilder.newDocument();

      Element rootNode = myDocument.createElement( ROOT_NODE );
      rootNode.setAttribute( "version", com.mlhsoftware.AboutDialog.APP_VERSION );
      rootNode.setAttribute( "xml_ver", "1" );
      rootNode.setAttribute( "actionCode", m_actionCode );

      if ( Blurts.m_optionProperties != null )
      {
        rootNode.setAttribute( "activated", Blurts.m_optionProperties.isKeyValid() ? "true" : "false" );
      }
      myDocument.appendChild( rootNode );

      if ( m_type == TYPE_STATUS || m_type == TYPE_LOCK )
      {
        Element statusNode = myDocument.createElement( STATUS_NODE );
        Text textNode = myDocument.createTextNode( m_statusText );
        statusNode.appendChild( textNode );
        rootNode.appendChild( statusNode );
      }
      else if ( m_type == TYPE_CLIPBOARD )
      {
        Element statusNode = myDocument.createElement( CLIPBOARD_NODE );
        Text textNode = myDocument.createTextNode( m_statusText );
        statusNode.appendChild( textNode );
        rootNode.appendChild( statusNode );
      }
      else if ( m_type == TYPE_CALL )
      {
        Element callNode = myDocument.createElement( CALL_NODE );
        callNode.setAttribute( "actionCode", m_callAction );
        rootNode.appendChild( callNode );

        if ( m_phoneNumber != null )
        {
          Element node = myDocument.createElement( PHONE_NUMBER_NODE );
          Text textNode = myDocument.createTextNode( m_phoneNumber );
          node.appendChild( textNode );
          callNode.appendChild( node );
        }

        if ( m_callerName != null )
        {
          Element node = myDocument.createElement( CALLER_NAME_NODE );
          Text textNode = myDocument.createTextNode( m_callerName );
          node.appendChild( textNode );
          callNode.appendChild( node );
        }


        if ( m_menus != null )
        {
          Element MenusNode = myDocument.createElement( MENUS_NODE );
          callNode.appendChild( MenusNode );

          int count = m_menus.size();
          for ( int lcv = 0; lcv < count; lcv++ )
          {
            MenuXML menu = (MenuXML)m_menus.elementAt( lcv );

            Element node = myDocument.createElement( MENU_NODE );
            node.setAttribute( "name", menu.name );
            node.setAttribute( "ordinal", menu.ordinal );
            MenusNode.appendChild( node );
          }
        }
        
      }
      else if ( m_type == TYPE_EMAIL )
      {
        Element emailNode = myDocument.createElement( EMAIL_NODE );
        rootNode.appendChild( emailNode );

        if ( m_senderName != null )
        {
          Element node = myDocument.createElement( SENDER_NAME_NODE );
          Text textNode = myDocument.createTextNode( m_senderName );
          node.appendChild( textNode );
          emailNode.appendChild( node );
        }

        if ( m_senderAddress != null )
        {
          Element node = myDocument.createElement( SENDER_ADDRESS_NODE );
          Text textNode = myDocument.createTextNode( m_senderAddress );
          node.appendChild( textNode );
          emailNode.appendChild( node );
        }

        if ( m_subject != null )
        {
          Element node = myDocument.createElement( SUBJECT_NODE );
          Text textNode = myDocument.createTextNode( m_subject );
          node.appendChild( textNode );
          emailNode.appendChild( node );
        }

        if ( m_bodyText != null )
        {
          Element node = myDocument.createElement( BODY_NODE );
          Text textNode = myDocument.createTextNode( m_bodyText );
          node.appendChild( textNode );
          emailNode.appendChild( node );
        }
      }

      else if ( m_type == TYPE_SMS )
      {
        Element smsNode = myDocument.createElement( SMS_NODE );
        rootNode.appendChild( smsNode );

        if ( m_senderName != null )
        {
          Element node = myDocument.createElement( SENDER_NAME_NODE );
          Text textNode = myDocument.createTextNode( m_senderName );
          node.appendChild( textNode );
          smsNode.appendChild( node );
        }

        if ( m_senderAddress != null )
        {
          Element node = myDocument.createElement( SENDER_ADDRESS_NODE );
          Text textNode = myDocument.createTextNode( m_senderAddress );
          node.appendChild( textNode );
          smsNode.appendChild( node );
        }

        if ( m_bodyText != null )
        {
          Element node = myDocument.createElement( BODY_NODE );
          Text textNode = myDocument.createTextNode( m_bodyText );
          node.appendChild( textNode );
          smsNode.appendChild( node );
        }
      }

      else if ( m_type == TYPE_SCREEN )
      {
        Element screenNode = myDocument.createElement( SCREEN_NODE );
        rootNode.appendChild( screenNode );

        if ( m_imageData != null )
        {
          Element node = myDocument.createElement( IMAGEDATA_NODE );
          Text textNode = myDocument.createTextNode( m_imageData );
          node.appendChild( textNode );
          screenNode.appendChild( node );
        }
      }

      ByteArrayOutputStream os = new ByteArrayOutputStream();
      XMLWriter writer = new XMLWriter( os );
      writer.setPrintCompressedOutput();
      DOMInternalRepresentation.parse( myDocument, writer );

      xml = os.toString();
    }
    catch ( DOMException e )
    {
      String msg = "CreateXMLString failed(1): " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }
    catch ( ParserConfigurationException e )
    {
      String msg = "CreateXMLString failed(2): " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg ); 
    }
    catch ( SAXException e )
    {
      String msg = "CreateXMLString failed(3): " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg ); 
    }
    catch ( Exception e )
    {
      String msg = "CreateXMLString failed(4): " + e.toString();
      EventLogger.logEvent( Blurts.LOGGER_ID, msg.getBytes(), EventLogger.SEVERE_ERROR );
      System.out.println( msg );
    }

    return xml;
  }

}
