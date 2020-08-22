using System;

using System.Xml;
using System.Xml.Xsl;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;

using System.Windows.Forms;
using Blurts.Domain;

namespace Blurts
{
  [ComVisible(true)]
  public class AlertBase : JSONObject, Blurts.Domain.IDisplayItem
  {
    // Key Names
    protected static String KEY_VERSION = "Version";
    protected static String KEY_ALERT_NAME = "AlertName";
    protected static String KEY_ALERT_TYPE = "AlertType";
    protected static String KEY_ACTIVATED = "Activated";
    protected static String KEY_IMAGEFILE = "ImageFile";
    protected static String KEY_BATTERY = "Battery";
    protected static String KEY_SIGNAL = "Signal";
    protected static String KEY_CHANNEL = "Channel";



    public static String ROOT_NODE = "BlurtsData";


    // Display properties
    private Boolean m_playSound;
    private Boolean m_displayAlert;
    private Boolean m_displaySMSChat;
    private Boolean m_displayIcon;
    private int m_displayInterval;
    private string m_backgroundColor;
    private string m_backgroundColorTop;
    private string m_soundFile;
    private int m_opacity;
    private string m_smsAdress;
    private int m_priority;
    private string m_xslFile;


    static public AlertBase CreateAlert(string data)
    {
      AlertBase retVal = null;
      try
      {
        AlertBase msg = new AlertBase(data);
        DisplayItemType type = msg.ItemType;
        switch (type)
        {
          case DisplayItemType.UNKNOWN:
          {
            break;
          }
          case DisplayItemType.STATUS:
          {
            retVal = new StatusAlert(data);
            break;
          }
          case DisplayItemType.CONNECT:
          {
            retVal = new ConnectAlert(data);
            break;
          }
          case DisplayItemType.EMAIL:
          {
            retVal = new EmailAlert(data);
            break;
          }
          case DisplayItemType.CALL:
          {
            retVal = new CallAlert(data);
            break;
          }
          case DisplayItemType.LOCK:
          {
            retVal = new LockAlert(data);
            break;
          }
          case DisplayItemType.SMS:
          {
            retVal = new SMSAlert(data);
            break;
          }
          case DisplayItemType.SCREEN:
          {
            retVal = new ScreenCaptureAlert(data);
            break;
          }
          case DisplayItemType.CONTACTS:
          {
            retVal = new ContactAlert(data);
            break;
          }
          case DisplayItemType.CLIPBOARD:
          {
            retVal = new ClipboardAlert(data);
            break;
          }
          case DisplayItemType.DISCONNECT:
          {
            retVal = new DisconnectAlert(data);
            break;
          }
          case DisplayItemType.LEVEL:
          {
            retVal = new LevelAlert(data);
            break;
          }
          case DisplayItemType.MACRO:
          {
            retVal = new MacroAlert(data);
            break;
          }
          case DisplayItemType.PIN_MSG:
          {
            retVal = new PINMsgAlert(data);
            break;
          }
          case DisplayItemType.INPUT_MSG:
          {
            retVal = new InputAlert(data);
            break;
          }
          case DisplayItemType.READFILE:
          {
            retVal = new ReadFileAlert(data);
            break;
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("process Message Error:" + Environment.NewLine + ex.ToString());
        retVal = null;
      }
      return retVal;
    }


    private void commonInit()
    {
      m_displayIcon = true;
      m_playSound = true;
      m_displayAlert = true;
      m_displaySMSChat = false;
      m_backgroundColor = null;
      m_backgroundColorTop = null;
      m_opacity = ApplicationSettings.Instance.AlertOpacity;
      m_smsAdress = "";
      m_priority = LowPriority; // low
      m_xslFile = @"display.xslt";

      ApplicationSettings appSettings = ApplicationSettings.Instance;
      m_displayInterval = appSettings.DisplayTime * 1000;
      m_soundFile = appSettings.SoundFile;
      put("timestamp", DateTime.Now.ToString("yyyyMMddhhmm"));
    }

    public AlertBase(String s) : base(s)
    {
      commonInit();
    }

    public AlertBase(DisplayItemType ItemType, String alertName)
    {
      try
      {
        commonInit();
        put(KEY_ALERT_NAME, alertName);
        //put(KEY_VERSION, AboutDialog.APP_VERSION);
        put(KEY_ALERT_TYPE, (int)ItemType);
        //put(KEY_ACTIVATED, ActivationScreen.isKeyValid() ? "true" : "false");
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
      }
    }

    public virtual Boolean ProcessMessage()
    {
      return false;
    }

    [ComVisible(true)]
    public int BatteryLevel
    {
      get
      {
        return optInt(KEY_BATTERY);
      }
    }

    [ComVisible(true)]
    public int SignalLevel
    {
      get
      {
        return optInt(KEY_SIGNAL);
      }
    }

    [ComVisible(true)]
    public int Channel
    {
      get
      {
        return optInt(KEY_CHANNEL);
      }
    }

    [ComVisible(true)]
    public String ImageFile
    {
      get
      {
        return optString(KEY_IMAGEFILE);
      }
      set
      {
        try
        {
          remove(KEY_IMAGEFILE);
          put(KEY_IMAGEFILE, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    public DisplayItemType ItemType
    {
      get
      {
        return (DisplayItemType)optInt(KEY_ALERT_TYPE);
      }
    }

    [ComVisible(true)]
    public Boolean Activated
    {
      get
      {
        return optBoolean(KEY_ACTIVATED);
      }
    }

    public string getVersion()
    {
      return optString(KEY_VERSION);
    }

    public string getAlertName()
    {
      return optString(KEY_ALERT_NAME);
    }

    [ComVisible(true)]
    public int Opacity
    {
      get
      {
        return m_opacity;
      }
      set
      {
        m_opacity = value;
      }
    }

    [ComVisible(true)]
    public int Priority
    {
      get
      {
        return m_priority;
      }
      set
      {
        m_priority = value;
      }
    }

    [ComVisible(true)]
    public int LowPriority
    {
      get
      {
        return 10;
      }
    }

    [ComVisible(true)]
    public int HighPriority
    {
      get
      {
        return 1;
      }
    }

    [ComVisible(true)]
    public Boolean PlaySound
    {
      get
      {
        return m_playSound;
      }
      set
      {
        m_playSound = value;
      }
    }

    [ComVisible(true)]
    public Boolean DisplayAlert
    {
      get
      {
        return m_displayAlert;
      }
      set
      {
        m_displayAlert = value;
      }
    }

    [ComVisible(true)]
    public Boolean DisplaySMSChat
    {
      get
      {
        return m_displaySMSChat;
      }
      set
      {
        m_displaySMSChat = value;
      }
    }

    [ComVisible(true)]
    public Boolean DisplayIcon
    {
      get
      {
        return m_displayIcon;
      }
      set
      {
        m_displayIcon = value;
      }
    }

    [ComVisible(true)]
    public int DisplayInterval
    {
      get
      {
        return m_displayInterval;
      }
      set
      {
        m_displayInterval = value;
      }
    }

    [ComVisible(true)]
    public String SoundFile
    {
      get
      {
        return m_soundFile;
      }
      set
      {
        m_soundFile = value;
      }
    }

    [ComVisible(true)]
    public String BackgroundColor
    {
      get
      {
        return m_backgroundColor;
      }
      set
      {
        m_backgroundColor = value;
      }
    }

    [ComVisible(true)]
    public String BackgroundColorTop
    {
      get
      {
        return m_backgroundColorTop;
      }
      set
      {
        m_backgroundColorTop = value;
      }
    }

    [ComVisible(true)]
    public String SMSAdress
    {
      get
      {
        return m_smsAdress;
      }
      set
      {
        m_smsAdress = value;
      }
    }

    [ComVisible(true)]
    public String XML
    {
      get
      {
        return getXML();
      }
    }

    private string getXML()
    {
      string xmlOutput = "";
      try
      {
        // Create the xml document containe
        XmlDocument doc = new XmlDocument();// Create the XML Declaration, and append it to XML document
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
        doc.AppendChild(dec);// Create the root element
        XmlElement root = doc.CreateElement(ROOT_NODE);
        root.SetAttribute("version", getVersion());
        root.SetAttribute("alertType", ItemType.ToString());
        root.SetAttribute("alertName", getAlertName());
        root.SetAttribute("activated", this.Activated ? "true" : "false");
        doc.AppendChild(root);


        XmlElement alert = doc.CreateElement(getAlertName());
        root.AppendChild(alert);
        buildChildNodes( doc,alert, this );
 
        xmlOutput = doc.OuterXml;

      }
      catch (Exception ex)
      {
        Console.WriteLine("get XML Error:" + Environment.NewLine + ex.ToString());
      }
      return xmlOutput;
    }

    private void buildChildNodes( XmlDocument doc, XmlElement node, JSONObject obj )
    {
      try
      {
        IEnumerator keys = obj.getKeys();
        while (keys.MoveNext())
        {
          Object o = keys.Current;
          String keyName = o.ToString();
          Object value = obj.get(keyName);
          if (keyName != KEY_VERSION && keyName != KEY_ALERT_NAME && keyName != KEY_ALERT_TYPE && keyName != KEY_ACTIVATED)
          {
            XmlElement xmlNode = doc.CreateElement(keyName);
            //keyName.InnerText = valueToString(getString(o.ToString()));
            if (value is JSONObject)
            {
              buildChildNodes(doc, xmlNode, (JSONObject)value);
            }
            else if (value is JSONArray)
            {
              JSONArray objects = (JSONArray)value;
              int len = objects.Length();
              for (int index = 0; index < len; index++)
              {
                Object item = objects.opt(index);
                if (item is JSONObject)
                {
                  buildChildNodes(doc, xmlNode, (JSONObject)item);
                }
                else
                {
                  xmlNode.InnerText = item.ToString();
                }
              }
              
            }
            else
            {
              xmlNode.InnerText = obj.getString(o.ToString());
            }
            node.AppendChild(xmlNode);
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("IEnumerator Error:" + Environment.NewLine + ex.ToString());
      }
    }

    [ComVisible(true)]
    public String HTML
    {
      get
      {
        return buildHtml(getXML());
      }
    }

    public String XSLFile
    {
      set  
      {
        m_xslFile = value;
      }
    }

    private string buildHtml(string xml)
    {
      string html;
      try
      {
        if (Directory.Exists(ApplicationSettings.Instance.LocalDataPath + @"\debug"))
        {
          StreamWriter SW;
          SW = File.CreateText(ApplicationSettings.Instance.LocalDataPath + @"\debug\data.xml");
          SW.Write(xml);
          SW.Close();
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xml);

        XslCompiledTransform xsl = new XslCompiledTransform();
        if ( File.Exists(ApplicationSettings.Instance.LocalDataPath + @"\theme\" + m_xslFile ) &&
             ApplicationSettings.Instance.Active )
        {
          xsl.Load(ApplicationSettings.Instance.LocalDataPath + @"\theme\" + m_xslFile);
        }
        else
        {
          xsl.Load(ApplicationSettings.Instance.AppPath + @"\" + m_xslFile);
        }

        

        XsltArgumentList argList = new XsltArgumentList();
        if (ApplicationSettings.Instance.AppPath != null)
        {
          argList.AddParam("systemPath", "", ApplicationSettings.Instance.AppPath);
        }
        if (ApplicationSettings.Instance.LocalDataPath != null)
        {
          argList.AddParam("dataPath", "", ApplicationSettings.Instance.LocalDataPath);
        }
        if ( File.Exists(ApplicationSettings.Instance.LocalDataPath + @"\theme") )
        {
          argList.AddParam("themePath", "", ApplicationSettings.Instance.LocalDataPath + @"\theme");
        }

        if (BackgroundColor != null)
        {
          argList.AddParam("bgColor", "", BackgroundColor);
        }
        if (BackgroundColorTop != null)
        {
          argList.AddParam("bgColorStart", "", BackgroundColorTop);
        }
        if (ImageFile != null)
        {
          argList.AddParam("imageFile", "", ImageFile);
        }
        argList.AddParam("displayIcon", "", DisplayIcon ? "true" : "false");

        XmlDocument outputDocument = new XmlDocument();
        System.Xml.XPath.XPathNavigator outputNavigator = outputDocument.CreateNavigator();
        using (XmlWriter writer = outputNavigator.AppendChild())
        {
          xsl.Transform(xmlDoc, argList, writer);
        }

        html = outputDocument.OuterXml;
      }
      catch (Exception exc)
      {
        if (Directory.Exists(ApplicationSettings.Instance.LocalDataPath + @"\debug"))
        {
          string fileName = ApplicationSettings.Instance.LocalDataPath + @"\debug\error_" + Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".txt";
          StreamWriter SW;
          SW = File.CreateText(fileName);
          SW.Write(exc.ToString());
          SW.Write(xml);
          SW.Close();
        }
        html = exc.ToString();
      }
      return html;
    }

    protected void checkVersion(string bbVersionStr)
    {
      try
      {
        string pcVersionStr = AboutDlg.version;

        char[] token = { '.' };
        string[] bbVersionParts = bbVersionStr.Split(token);
        string[] pcVersionParts = pcVersionStr.Split(token);
        int size = Math.Min(bbVersionParts.Length, pcVersionParts.Length);
        for (int x = 0; x < size; x++)
        {
          int bbVersion = int.Parse(bbVersionParts[x]);
          int pcVersion = int.Parse(pcVersionParts[x]);
          if (pcVersion < bbVersion)
          {
            //TODO remove message box and fire event
            MessageBox.Show("Blackberry and PC software versions do not match. Please visit www.MLHSoftware.com to upgrade your PC software to version " + bbVersionStr, "Blurts - by MLH Software");
            break;
          }
          else if (bbVersion < pcVersion)
          {
            MessageBox.Show("Blackberry and PC software versions do not match. Please visit www.MLHSoftware.com to upgrade your BlackBerry software to version " + pcVersionStr, "Blurts - by MLH Software");
            break;
          }
        }
      }
      catch (Exception)
      {
      }
    }

    

    protected void runScript(String methodName)
    {
      try
      {
        if (methodName != null && methodName.Length > 0)
        {
          if (ApplicationSettings.Instance.EnableScript)
          {
            // FIX 
            /*Alert = alert;

            if (m_form.m_scriptControl != null)
            {
              if (File.Exists(ApplicationSettings.Instance.ScriptFile))
              {
                Boolean loadScript = false;
                DateTime chkFileDateTime = File.GetLastWriteTime(ApplicationSettings.Instance.ScriptFile);
                if (chkFileDateTime > m_scriptFileDateTime)
                {
                  loadScript = true;
                  m_scriptFileDateTime = chkFileDateTime;
                }

                if (loadScript)
                {
                  TextReader tr = new StreamReader(ApplicationSettings.Instance.ScriptFile);
                  String scriptCode = tr.ReadToEnd();
                  try
                  {
                    m_form.m_scriptControl.AddCode(scriptCode);
                    object[] args = new object[0];
                  }
                  catch (Exception ex)
                  {
                    if (ApplicationSettings.Instance.EnableScriptErrorMsg)
                    {

                      MessageBox.Show(ex.Message, "Blurts scripting error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                  }
                  tr.Close();
                }

                try
                {
                  object[] args = new object[0];
                  m_form.m_scriptControl.Run(methodName, ref args);
                }
                catch (Exception ex)
                {
                  string msg = "Error running script method " + methodName + "\n\r\n\r";
                  msg += ex.Message;

                  if (ApplicationSettings.Instance.EnableScriptErrorMsg)
                  {
                    MessageBox.Show(msg, "Blurts scripting error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
                }
              }
              else
              {
                string msg = "Script file not found.\n\r\n\r";
                msg += ApplicationSettings.Instance.ScriptFile;

                if (ApplicationSettings.Instance.EnableScriptErrorMsg)
                {
                  MessageBox.Show(msg, "Blurts scripting error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
              }
            }
            else
            {
              string msg = "Restart Blurts to enable scripting.";
              if (ApplicationSettings.Instance.EnableScriptErrorMsg)
              {
                MessageBox.Show(msg, "Blurts scripting error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
            }
             * */
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error " + methodName + " Script:" + Environment.NewLine + ex.ToString());
      }
    }
  }
}
