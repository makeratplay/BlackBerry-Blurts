using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Blurts
{
  class AlertHistory
  {

    private static AlertHistory instance;

    public static AlertHistory Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new AlertHistory();
        }
        return instance;
      }
    }

    private XmlDocument xmlDoc;

    private AlertHistory()
    {
      try
      {
        this.xmlDoc = new XmlDocument();
        this.xmlDoc.Load(ApplicationSettings.Instance.LocalDataPath + @"\AlertHistory.xml");
      }
      catch (Exception)
      {
        this.xmlDoc.LoadXml("<BlurtsData><AlertHistory/></BlurtsData>");
      }
    }

    public XmlDocument XMLDoc
    {
      get
      {
        return xmlDoc;
      }
    }

    public void Save()
    {
      this.xmlDoc.Save(ApplicationSettings.Instance.LocalDataPath + @"\AlertHistory.xml");
    }

    public void DeleteAllHistory()
    {
      this.xmlDoc.LoadXml("<BlurtsData><AlertHistory/></BlurtsData>");
      Save();
    }

    public void AddAlert(string xml)
    {
      XmlDocument msgXmlDoc = new XmlDocument();
      msgXmlDoc.LoadXml(xml);
      XmlNode hxRootNode = this.xmlDoc.DocumentElement.FirstChild;
      XmlNode alertNode = msgXmlDoc.DocumentElement.FirstChild;
      XmlNode newNode = this.xmlDoc.ImportNode(alertNode, true);
      newNode = hxRootNode.AppendChild(newNode);
      
      Save();
    }

  }
}
