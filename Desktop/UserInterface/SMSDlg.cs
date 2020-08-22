using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;

namespace Blurts
{

  [ComVisible(true)]
  public partial class SMSDlg : Form
  {

    public enum FLASHWINFOFLAGS
    {
      FLASHW_STOP = 0,
      FLASHW_CAPTION = 0x00000001,
      FLASHW_TRAY = 0x00000002,
      FLASHW_ALL = (FLASHW_CAPTION | FLASHW_TRAY),
      FLASHW_TIMER = 0x00000004,
      FLASHW_TIMERNOFG = 0x0000000C
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FLASHWINFO
    {
      public UInt32 cbSize;
      public IntPtr hwnd;
      public Int32 dwFlags;
      public UInt32 uCount;
      public Int32 dwTimeout;
    }

    [DllImport("user32.dll")]
    public static extern int FlashWindowEx(ref FLASHWINFO pfwi);
    
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    public string m_address;
    private string m_ident;
    public string m_text;
    private string m_contactName;

    string m_xmlFilepath;
    int m_smsMax = 160;

    Boolean m_pickedAddress;
    XmlDocument m_smsXmlDoc;

    static private Dictionary<string, SMSDlg> m_SMSDailogs = new Dictionary<string, SMSDlg>();

    static public Boolean AddSMSDialog(string number, SMSDlg dlg)
    {
      Boolean retVal = false;
      if (m_SMSDailogs.ContainsKey(number))
      {
        SMSDlg smsDlg = m_SMSDailogs[number];
        smsDlg.BringToFront();
      }
      else
      {
        m_SMSDailogs.Add(number, dlg);
        retVal = true;
      }
      return retVal;
    }

    static public void RemoveSMSDialog(string number)
    {
      m_SMSDailogs.Remove(number);
    }


    public SMSDlg()
    {
      m_smsMax = ApplicationSettings.Instance.MaxSMS;
      m_smsXmlDoc = null;
      InitializeComponent();

      if (!ApplicationSettings.Instance.SmsMultiMsg)
      {
        msgTxtBox.MaxLength = m_smsMax;
      }
      m_pickedAddress = false;
    }

    public void setAddress( string address )
    {
      m_address = Contacts.justNumber(address);
      setFileName(address);
    }

    public void setFileName(string address)
    {
      m_ident = Contacts.rawNumber(address);
      m_xmlFilepath = string.Format(@"{0}\SMS\{1}.xml", ApplicationSettings.Instance.LocalDataPath, m_ident);
    }


    public string getAddress()
    {
      return m_address;
    }

    public void setText(string text)
    {
      m_text = text;
    }

    public string getText()
    {
      return m_text;
    }



    private void sendBtn_Click(object sender, EventArgs e)
    {
      UpdateState();

      if ( m_text.Length > 0 )
      {
        saveEntry(m_text);

        if (ApplicationSettings.Instance.SmsMultiMsg && m_text.Length > m_smsMax)
        {
          ArrayList msgList = new ArrayList();
          while( m_text.Length > 0 )
          {
            string msg = "";
            int i = m_smsMax - 6;

            if (i > m_text.Length - 1)
            {
              msgList.Add(m_text);
              m_text = "";
            }
            else
            {
              for (; i > 0; i--)
              {
                if (m_text[i] == ' ')
                {
                  msg = m_text.Substring(0, i);
                  msgList.Add(msg);
                  m_text = m_text.Substring(i + 1);
                  break;
                }
              }
            }
          }

          int count = msgList.Count;
          for (int x = 0; x < count; x++)
          {
            string msg = string.Format("({0}/{1}) {2}", x+1, count, msgList[x]);
    //FIX        m_form.m_BlackBerry.SendSMS( m_address, msg);
          }

        }
        else
        {
    //FIX      m_form.m_BlackBerry.SendSMS(m_address, m_text);
        }
        
        m_text = "";
        msgTxtBox.Text = "";
      }
      Display();
    }

    private void saveEntry(string messageText)
    {
      try
      {
        XmlNode smsNode = m_smsXmlDoc.CreateNode(XmlNodeType.Element, "SMS", "");
        XmlNode senderNode = m_smsXmlDoc.CreateNode(XmlNodeType.Element, "Sender", "");
        XmlNode bodyTextNode = m_smsXmlDoc.CreateNode(XmlNodeType.Element, "BodyText", "");

        XmlElement smsElement = (XmlElement)smsNode;
        smsElement.SetAttribute("timestamp", DateTime.Now.ToString());


        senderNode.InnerText = "Me";
        bodyTextNode.InnerText = messageText;
        smsNode.AppendChild(senderNode);
        smsNode.AppendChild(bodyTextNode);

        XmlNode messagesNode = m_smsXmlDoc.DocumentElement.FirstChild;
        messagesNode.AppendChild(smsNode);


        m_smsXmlDoc.Save(m_xmlFilepath);
      }
      catch (Exception e)
      {
        MessageBox.Show("Failed to save message. Error: " + e.ToString());
      }
    }

    private void addressTxtBox_TextChanged(object sender, EventArgs e)
    {
      if (m_pickedAddress)
      {
        m_pickedAddress = false;
        addressTxtBox.Text = "";
      }
      UpdateState();
    }

    private void msgTxtBox_TextChanged(object sender, EventArgs e)
    {
      UpdateState();
    }

    private void UpdateState()
    {
      m_text = msgTxtBox.Text;

      if (ApplicationSettings.Instance.SmsMultiMsg)
      {
        int count = (int)Math.Ceiling(((double)m_text.Length / (double)m_smsMax));
        string pural = "";
        if ( count > 1 )
        {
          pural = "s";
        }
        textCnt.Text = string.Format("{0} message{1} will be sent ({2} characters)", count, pural, m_text.Length);
      }
      else
      {
        textCnt.Text = string.Format( "{0} characters left", (m_smsMax - m_text.Length).ToString() );
      }
      

      if (m_contactName != null && m_contactName.Length > 0)
      {
        this.Text = string.Format("{0} ({1}) - Blurts", m_contactName, m_address);
      }
      else
      {
        this.Text = string.Format("{0} - Blurts", m_address);
      }

      if (m_address.Length != 0 && m_text.Length != 0)
      {
        sendBtn.Enabled = true;
      }
      else
      {
        sendBtn.Enabled = false;
      }
    }

    void watcher_Changed(object sender, EventArgs e)
    {
      try
      {
        m_smsXmlDoc.Load(m_xmlFilepath);
      }
      catch (Exception )
      {
      }
      Display();
    }

    private void SMSDlg_Load(object sender, EventArgs e)
    {
      if (m_address.Length == 0)
      {
        if (selectContact() == false)
        {
          Close();
          return;
        }
      }
      else
      {
        m_contactName = Contacts.Instance.getContactName(m_ident);
      }

      if (!SMSDlg.AddSMSDialog(m_ident, this))
      {
        m_ident = "";
        // dialog with this number already esists so close this dialog
        Close();
        return;
      }


      try
      {
        m_smsXmlDoc = new XmlDocument();
        if (File.Exists(m_xmlFilepath))
        {
          m_smsXmlDoc.Load(m_xmlFilepath);
        }
        else
        {
          m_smsXmlDoc.LoadXml("<BlurtsData><Messages/></BlurtsData>");
          m_smsXmlDoc.Save(m_xmlFilepath);
        }

    //FIX    m_form.SMSMessageEvent += new SMSMessageEventHandler(SMSMessageArrived);

   //FIX     m_form.DisconnectEvent += new DisconnectEventHandler(DisconnectEventFired);
      }
      catch (Exception)
      {
      }

      webBrowser.AllowWebBrowserDrop = false;
      webBrowser.IsWebBrowserContextMenuEnabled = false;
      webBrowser.WebBrowserShortcutsEnabled = true;
      webBrowser.ScriptErrorsSuppressed = true;
      // Add an event handler that prints the document after it loads.
      //webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(DocumentComlpete);

      webBrowser.ObjectForScripting = this;

      
      addressTxtBox.Text = m_address;
      msgTxtBox.Text = m_text;
      UpdateState();
      Display();
    }

    public void SMSMessageArrived()
    {
      try
      {
        m_smsXmlDoc.Load(m_xmlFilepath);
      }
      catch (Exception)
      {
      }
      Display();

      //flashWindow();

      this.Visible = true;
      this.TopMost = true;
      //this.Focus();
      this.BringToFront();
      this.TopMost = false;
    }

    public void DisconnectEventFired()
    {
      Close();
    }

    private void Display()
    {
      try
      {
        XslCompiledTransform xsl = new XslCompiledTransform();
        if ( File.Exists(ApplicationSettings.Instance.LocalDataPath + @"\theme\sms.xslt") &&
            ApplicationSettings.Instance.Active )
        {
          xsl.Load(ApplicationSettings.Instance.LocalDataPath + @"\theme\sms.xslt");
        }
        else
        {
          xsl.Load(ApplicationSettings.Instance.AppPath + @"\sms.xslt");
        }

        XsltArgumentList argList = new XsltArgumentList();

        argList.AddParam("systemPath", "", ApplicationSettings.Instance.AppPath);
        argList.AddParam("dataPath", "", ApplicationSettings.Instance.LocalDataPath);
        if (File.Exists(ApplicationSettings.Instance.LocalDataPath + @"\theme"))
        {
          argList.AddParam("themePath", "", ApplicationSettings.Instance.LocalDataPath + @"\theme");
        }
        if (ApplicationSettings.Instance.SmsShowImages)
        {
          argList.AddParam("showImages", "", "true");
        }
        else
        {
          argList.AddParam("showImages", "", "false");
        }

        if (File.Exists(ApplicationSettings.Instance.LocalDataPath + @"\me.png"))
        {
          argList.AddParam("meImage", "", ApplicationSettings.Instance.LocalDataPath + @"\me.png");
        }
        else if (File.Exists(ApplicationSettings.Instance.LocalDataPath + @"\me.gif"))
        {
          argList.AddParam("meImage", "", ApplicationSettings.Instance.LocalDataPath + @"\me.gif");
        }
        else if (File.Exists(ApplicationSettings.Instance.LocalDataPath + @"\me.jpg"))
        {
          argList.AddParam("meImage", "", ApplicationSettings.Instance.LocalDataPath + @"\me.jpg");
        }
        else if (File.Exists(ApplicationSettings.Instance.LocalDataPath + @"\me.jpeg"))
        {
          argList.AddParam("meImage", "", ApplicationSettings.Instance.LocalDataPath + @"\me.jpeg");
        }
        else
        {
          argList.AddParam("meImage", "", ApplicationSettings.Instance.AppPath + @"\me.gif");
        }

        argList.AddParam("bgColor", "", ApplicationSettings.Instance.SmsBackgroundColor);
        argList.AddParam("fontColor", "", ApplicationSettings.Instance.SmsTextColor);

        XmlDocument outputDocument = new XmlDocument();
        System.Xml.XPath.XPathNavigator outputNavigator = outputDocument.CreateNavigator();
        using (XmlWriter writer = outputNavigator.AppendChild())
        {
          xsl.Transform(m_smsXmlDoc, argList, writer);
        }

        webBrowser.DocumentText = outputDocument.OuterXml;
      }
      catch (Exception)
      {
      }

    }


    private void addressTxtBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      try
      {
        if (e.KeyChar != 8)
        {
          int isNumber = 0;
          e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }
      }
      catch ( Exception )
      {
      }
    }

    private void contactBtn_Click(object sender, EventArgs e)
    {
      selectContact();
    }

    private Boolean selectContact()
    {
      Boolean retVal = false;
      SMSContactDlg dlg = new SMSContactDlg(true);

      //dlg.Location = new Point(this.Location.X + 20, this.Location.Y + 35);
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        if (dlg.PhoneNumber != null && dlg.PhoneNumber.Length > 0)
        {
          m_pickedAddress = false;
          //addressTxtBox.Text = dlg.m_displayName + " (" + dlg.m_number + ")";
          m_contactName = Contacts.Instance.getContactName(dlg.PhoneNumber);
          setAddress(dlg.PhoneNumber);
          m_pickedAddress = true;
          retVal = true;
        }
      }
      return retVal;
    }

    [ComVisible(true)]
    public void setClientSize(int width, int height)
    {
      //ClientSize = new System.Drawing.Size(width, height);
    }

    [ComVisible(true)]
    public void selectPhoneNumber(string number, string formatedNumber, string displayName)
    {
    }

    private void SMSDlg_KeyPress(object sender, KeyPressEventArgs e)
    {
      Display();
    }

    private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
      string bgColor = webBrowser.Document.Body.GetAttribute("bgcolor");
      if (bgColor != null)
      {
        if (bgColor.Length > 0)
        {
          if (bgColor[0] == '#')
          {
            if (bgColor.Length > 6)
            {
              int r = (int)Convert.ToByte(bgColor.Substring(1, 2), 16);
              int g = (int)Convert.ToByte(bgColor.Substring(3, 2), 16);
              int b = (int)Convert.ToByte(bgColor.Substring(5, 2), 16);
              this.BackColor = System.Drawing.Color.FromArgb(r, g, b);
              //sendBtn.BackColor = System.Drawing.Color.FromArgb(r, g, b);
            }
          }
          else
          {
            //sendBtn.BackColor = System.Drawing.Color.FromName(bgColor);
            this.BackColor = System.Drawing.Color.FromName(bgColor);
          }
        }
      }

      bgColor = webBrowser.Document.Body.GetAttribute("color");
      if (bgColor != null)
      {
        if (bgColor.Length > 0)
        {
          if (bgColor[0] == '#')
          {
            if (bgColor.Length > 6)
            {
              int r = (int)Convert.ToByte(bgColor.Substring(1, 2), 16);
              int g = (int)Convert.ToByte(bgColor.Substring(3, 2), 16);
              int b = (int)Convert.ToByte(bgColor.Substring(5, 2), 16);
              textCnt.ForeColor = System.Drawing.Color.FromArgb(r, g, b);
              //sendBtn.ForeColor = System.Drawing.Color.FromArgb(r, g, b);
            }
          }
          else
          {
            textCnt.ForeColor = System.Drawing.Color.FromName(bgColor);
            //sendBtn.ForeColor = System.Drawing.Color.FromName(bgColor);
          }
        }
      }

      bgColor = webBrowser.Document.Body.GetAttribute("link");
      if (bgColor != null)
      {
        if (bgColor.Length > 0)
        {
          if (bgColor[0] == '#')
          {
            if (bgColor.Length > 6)
            {
              int r = (int)Convert.ToByte(bgColor.Substring(1, 2), 16);
              int g = (int)Convert.ToByte(bgColor.Substring(3, 2), 16);
              int b = (int)Convert.ToByte(bgColor.Substring(5, 2), 16);
              clearHxBtn.LinkColor = System.Drawing.Color.FromArgb(r, g, b);
            }
          }
          else
          {
            clearHxBtn.LinkColor = System.Drawing.Color.FromName(bgColor);
          }
        }
      }

       

      

      webBrowser.Document.Click += delegate
      {
        OnClick();
      };
    }

    private void OnClick()
    {
      Display();
    }

    private void SMSDlg_FormClosing(object sender, FormClosingEventArgs e)
    {
      SMSDlg.RemoveSMSDialog(m_ident);
  //FIX    m_form.SMSMessageEvent -= new SMSMessageEventHandler(SMSMessageArrived);

  //FIX    m_form.DisconnectEvent -= new DisconnectEventHandler(DisconnectEventFired);
      
    }

    private void clearHxBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      XmlNode messagesNode = m_smsXmlDoc.DocumentElement.FirstChild;
      messagesNode.RemoveAll();
      //m_smsXmlDoc.Save(m_xmlFilepath);
      try
      {
        File.Delete(m_xmlFilepath);
      }
      catch (Exception)
      {
      }
      Display();
    }

    private void flashWindow()
    {
      FLASHWINFO fw = new FLASHWINFO();
      fw.cbSize = Convert.ToUInt32(Marshal.SizeOf(typeof(FLASHWINFO)));
      fw.hwnd = this.Handle;
      fw.dwFlags = (Int32)(FLASHWINFOFLAGS.FLASHW_ALL | FLASHWINFOFLAGS.FLASHW_TIMERNOFG);
      fw.dwTimeout = 0;

      FlashWindowEx(ref fw);
    }

    private void msgTxtBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\x1')
      {
        ((TextBox)sender).SelectAll();
        e.Handled = true;
      }
    }
  }
}