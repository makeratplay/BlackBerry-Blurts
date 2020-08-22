using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Blurts
{
  class Contacts
  {
    private static Contacts instance;

    public static Contacts Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new Contacts();
        }
        return instance;
      }
    }

    private XmlDocument m_contactsXmlDoc;

    private Contacts()
    {
      try
      {
        m_contactsXmlDoc = new XmlDocument();
        m_contactsXmlDoc.Load(ApplicationSettings.Instance.LocalDataPath + @"\Contacts\data.xml");
      }
      catch (Exception)
      {
        m_contactsXmlDoc.LoadXml("<BlurtsData><Contacts/></BlurtsData>");
      }
    }

    public XmlDocument XMLDoc
    {
      get
      {
        return m_contactsXmlDoc;
      }
    }

    public void SaveContacts()
    {
      if (!Directory.Exists(ApplicationSettings.Instance.LocalDataPath + @"\Contacts"))
      {
        Directory.CreateDirectory(ApplicationSettings.Instance.LocalDataPath + @"\Contacts");
      }
      m_contactsXmlDoc.Save(ApplicationSettings.Instance.LocalDataPath + @"\Contacts\data.xml");
    }

    public void DeleteAllContacts()
    {
      if (Directory.Exists(ApplicationSettings.Instance.LocalDataPath + @"\Contacts"))
      {
        DirectoryInfo di = new DirectoryInfo(ApplicationSettings.Instance.LocalDataPath + @"\Contacts");
        FileInfo[] rgFiles = di.GetFiles("*.jpg");
        foreach (FileInfo fi in rgFiles)
        {
          try
          {
            fi.Delete();
          }
          catch (Exception)
          {
          }
        }
      }
      m_contactsXmlDoc.LoadXml("<BlurtsData><Contacts/></BlurtsData>");
      SaveContacts();
    }

    public void UpdateContact( string xml )
    {
      XmlDocument msgXmlDoc = new XmlDocument();
      msgXmlDoc.LoadXml(xml);
      XmlNode contactsNode = m_contactsXmlDoc.DocumentElement.FirstChild;
      XmlNode messagesNode = msgXmlDoc.DocumentElement.FirstChild;
      XmlNode newNode = m_contactsXmlDoc.ImportNode(messagesNode, true);

      newNode = contactsNode.AppendChild(newNode);
      string xPath = "PhoneNumbers/PhoneNumber/Number";
      XmlNodeList phoneNumberNodes = newNode.SelectNodes(xPath);
      for( int index = 0; index < phoneNumberNodes.Count; index++ )
      {
        XmlElement phoneNumberNode = (XmlElement)phoneNumberNodes.Item(index);
        phoneNumberNode.SetAttribute("raw", rawNumber(phoneNumberNode.InnerText));
      }

      
      SaveContacts();
    }

    public string getImageFile(string phoneNumber, string emailAddress)
    {
      
      string file = "";
      if (m_contactsXmlDoc != null && ApplicationSettings.Instance.ShowPhotos)
      {
        string xPath = "";
        if (phoneNumber != null)
        {
          string rawPhoneNumber = rawNumber(phoneNumber);
          xPath = string.Format("BlurtsData/Contacts/Contact[PhoneNumbers/PhoneNumber/Number/@raw = '{0}']/ImageFile", rawPhoneNumber);
        }
        else if (emailAddress != null)
        {
          xPath = string.Format("BlurtsData/Contacts/Contact[Emails/Email = '{0}']/ImageFile", emailAddress);
        }
        try
        {
          XmlNodeList contactNodes = m_contactsXmlDoc.SelectNodes(xPath);
          if (contactNodes.Count > 0)
          {
            XmlElement photoNode = (XmlElement)contactNodes.Item(0);
            file = photoNode.InnerText;
          }
        }
        catch (Exception)
        {
        }
      }
      return file;
    }

    public string getContactName(string phoneNumber)
    {
      string rawPhoneNumber = rawNumber(phoneNumber);
      string name = "";
      string xPath = string.Format("BlurtsData/Contacts/Contact[PhoneNumbers/PhoneNumber/Number/@raw = '{0}']/DisplayName", rawPhoneNumber);
      XmlNodeList contactNodes = m_contactsXmlDoc.SelectNodes(xPath);
      if (contactNodes.Count > 0)
      {
        XmlElement displayNameNode = (XmlElement)contactNodes.Item(0);
        name = displayNameNode.InnerText;
      }
      return name;
    }

    static public string rawNumber(string phoneNumber)
    {
      string retVal = "";
      if (phoneNumber != null)
      {
        for (int i = 0; i < phoneNumber.Length; i++)
        {
          if (phoneNumber[i] >= '0' && phoneNumber[i] <= '9')
          {
            retVal += phoneNumber[i];
          }
        }
        if (retVal.Length > 1 && retVal[0] == '1')
        {
          // remove the leading '1'
          retVal = retVal.Substring(1);
        }
      }
      return retVal;
    }

    static public string justNumber(string phoneNumber)
    {
      string retVal = "";
      if (phoneNumber != null)
      {
        for (int i = 0; i < phoneNumber.Length; i++)
        {
          if (phoneNumber[i] >= '0' && phoneNumber[i] <= '9' || phoneNumber[i] == '+')
          {
            retVal += phoneNumber[i];
          }
        }
      }
      return retVal;
    }
  }
}
