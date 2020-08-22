using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Blurts.Domain;

namespace Blurts
{
  [ComVisible(true)]
  public class SMSAlert : AlertBase
  {
    // Tag Names
    private static String ALERT_NAME = "SMS";
    private static String KEY_SENDER_NAME = "SenderName";
    private static String KEY_ADDRESS = "SenderAddress";
    private static String KEY_BODY = "BodyText";

    private String rawNumber;

    public SMSAlert(String s)
      : base(s)
    {
    }

    public SMSAlert()
      : base(DisplayItemType.SMS, ALERT_NAME)
    {
    }

    [ComVisible(true)]
    public String SenderName
    {
      get
      {
        return optString(KEY_SENDER_NAME);
      }
      set
      {
        try
        {
          remove(KEY_SENDER_NAME);
          put(KEY_SENDER_NAME, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }


    [ComVisible(true)]
    public String SenderAddress
    {
      get
      {
        return optString(KEY_ADDRESS);
      }
      set
      {
        try
        {
          remove(KEY_ADDRESS);
          put(KEY_ADDRESS, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public String PhoneNumber
    {
      get
      {
        return rawNumber;
      }
      set
      {
        try
        {
          rawNumber = value;
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public String BodyText
    {
      get
      {
        return optString(KEY_BODY);
      }
      set
      {
        try
        {
          remove(KEY_BODY);
          put(KEY_BODY, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    public override Boolean ProcessMessage()
    {
      Boolean retVal = true;

      this.SMSAdress = this.SenderAddress;
      this.ImageFile = Contacts.Instance.getImageFile(this.SenderAddress, null);
      this.PhoneNumber = Contacts.rawNumber(this.SenderAddress);

      runScript( "OnSMSAlert" );

      if (!Directory.Exists(ApplicationSettings.Instance.LocalDataPath + @"\SMS"))
      {
        Directory.CreateDirectory(ApplicationSettings.Instance.LocalDataPath + @"\SMS");
      }

      string rawPhoneNumber = Contacts.rawNumber(this.SenderAddress);
      string smsFile = string.Format(@"{0}\SMS\{1}.xml", ApplicationSettings.Instance.LocalDataPath, rawPhoneNumber);

      string xml = this.XML;
      XmlDocument smsXmlDoc = new XmlDocument();
      if (File.Exists(smsFile))
      {
        smsXmlDoc.Load(smsFile);
      }
      else
      {
        smsXmlDoc.LoadXml("<BlurtsData><Messages/></BlurtsData>");
      }


      XmlDocument msgXmlDoc = new XmlDocument();
      msgXmlDoc.LoadXml(xml);
      XmlNode smsNode = msgXmlDoc.DocumentElement.FirstChild;

      XmlNode messagesNode = smsXmlDoc.DocumentElement.FirstChild;
      XmlNode newNode = smsXmlDoc.ImportNode(smsNode, true);
      XmlElement newElement = (XmlElement)newNode;
      newElement.SetAttribute("timestamp", DateTime.Now.ToString());
      messagesNode.AppendChild(newNode);
      smsXmlDoc.Save(smsFile);

      AlertHistory.Instance.AddAlert(XML);
      //FIX m_form.Invoke(new EventHandler(m_form.FireSMSEvent));


      return retVal;
    }
  }
}
