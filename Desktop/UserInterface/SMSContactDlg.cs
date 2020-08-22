using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Blurts
{
  [ComVisible(true)]
  public partial class SMSContactDlg : Form
  {

    private Form parent;

    enum eTabs
    {
      eTab_History,
      eTab_Favorite,
      eTab_All,
      eTab_Keypad
    }

    enum eHistoryType
    {
      eHistoryType_SMS,
      eHistoryType_Phone
    }

    private eTabs m_tab;
    private eHistoryType m_historyType;

    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    private bool draggingToolbar;
    private Point draggedFrom;

    string m_contactFilter;

    XmlDocument m_HxXmlDoc;

    private string m_phoneNumber;

    public SMSContactDlg(bool smsDlg)
    {
      InitializeComponent();
      if (smsDlg)
      {
        m_historyType = eHistoryType.eHistoryType_SMS;
        m_tab = eTabs.eTab_History;
      }
      else
      {
        m_historyType = eHistoryType.eHistoryType_Phone;
        HistoryBtn.Visible = false;
        m_tab = eTabs.eTab_All;
      }
      
      m_contactFilter = "";
      m_phoneNumber = "";
      
      m_HxXmlDoc = new XmlDocument();
      showKeypad(false);
    }

    public string PhoneNumber
    {
      get
      {
        return m_phoneNumber;
      }
    }

    private void SMSContactDlg_Load(object sender, EventArgs e)
    {
      webBrowser1.AllowWebBrowserDrop = false;
      webBrowser1.IsWebBrowserContextMenuEnabled = false;
      webBrowser1.WebBrowserShortcutsEnabled = true;
      webBrowser1.ScriptErrorsSuppressed = true;

      webBrowser1.ObjectForScripting = this;

      m_HxXmlDoc.LoadXml("<HistoryRoot><History/></HistoryRoot>");

      int count = 0;

      if (m_historyType == eHistoryType.eHistoryType_SMS)
      {
        DirectoryInfo di = new DirectoryInfo(ApplicationSettings.Instance.LocalDataPath + @"\SMS");
        try
        {
          FileInfo[] rgFiles = di.GetFiles("*.xml");
          foreach (FileInfo fi in rgFiles)
          {
            try
            {
              string number = fi.Name.Substring(0, fi.Name.Length - 4);
              if (number.Length > 0)
              {
                XmlNode hxNode = m_HxXmlDoc.CreateNode(XmlNodeType.Element, "Hx", "");
                XmlElement hxElement = (XmlElement)hxNode;
                hxElement.SetAttribute("name", Contacts.Instance.getContactName(number));
                hxElement.SetAttribute("number", number);
                hxElement.SetAttribute("image", Contacts.Instance.getImageFile(number, ""));
                string ts = string.Format("{0:00}{1:00}{2:00}{3:00}", fi.LastWriteTime.Year, fi.LastWriteTime.Month, fi.LastWriteTime.Hour, fi.LastWriteTime.Minute);
                hxElement.SetAttribute("timestamp", ts);
                ts = string.Format("{0:00}/{1:00}", fi.LastWriteTime.Month, fi.LastWriteTime.Year.ToString().Substring(2, 2));
                hxElement.SetAttribute("date", ts);
                ts = string.Format("{0:00}:{1:00}", fi.LastWriteTime.Hour, fi.LastWriteTime.Minute);
                hxElement.SetAttribute("time", ts);
                m_HxXmlDoc.DocumentElement.FirstChild.AppendChild(hxNode);
                count++;
              }
            }
            catch (Exception)
            {
            }
          }
        }
        catch (Exception)
        {
        }
      }
      else
      {
      }

      if (count == 0)
      {
        m_tab = eTabs.eTab_All;
      }

      Display();
    }

    private void Display()
    {
      XslCompiledTransform xsl = new XslCompiledTransform();
      xsl.Load(ApplicationSettings.Instance.AppPath + @"\smscontact.xslt");
      // AppPath is startup and true app path


      XsltArgumentList argList = new XsltArgumentList();
      argList.AddParam("systemPath", "", ApplicationSettings.Instance.AppPath);
      argList.AddParam("dataPath", "", ApplicationSettings.Instance.LocalDataPath);
      argList.AddParam("contactFilter", "", m_contactFilter);


      XmlDocument outputDocument = new XmlDocument();
      System.Xml.XPath.XPathNavigator outputNavigator = outputDocument.CreateNavigator();
      using (XmlWriter writer = outputNavigator.AppendChild())
      {
        if (m_contactFilter.Length > 0 || m_tab == eTabs.eTab_All)
        {
          xsl.Transform(Contacts.Instance.XMLDoc, argList, writer);
        }
        else if (m_tab == eTabs.eTab_Favorite)
        {
          argList.AddParam("favoriteFilter", "", "true");
          xsl.Transform(Contacts.Instance.XMLDoc, argList, writer);
        }
        else
        {
          xsl.Transform(m_HxXmlDoc, argList, writer);
        }
      }

      webBrowser1.DocumentText = outputDocument.OuterXml;
    }


    private void SMSContactDlg_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (m_phoneNumber.Length == 0)
      {
        m_phoneNumber = Contacts.justNumber(m_contactFilter);
      }
    }

    [ComVisible(true)]
    public void selectPhoneNumber(string phoneNumber)
    {
      m_phoneNumber = phoneNumber;
      this.DialogResult = DialogResult.OK;
      Close();
    }

    private void searchTxtBx_TextChanged(object sender, EventArgs e)
    {
      m_contactFilter = searchTxtBx.Text.ToLower();
      Display();
    }

    private void closeBtn_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      Close();
    }

    private void SMSContactDlg_MouseDown(object sender, MouseEventArgs e)
    {
      draggingToolbar = true;
      draggedFrom = new Point(e.X, e.Y);
      this.Capture = true;
    }

    private void SMSContactDlg_MouseMove(object sender, MouseEventArgs e)
    {
      if (draggingToolbar)
      {
        this.Left = e.X + this.Left - draggedFrom.X;
        this.Top = e.Y + this.Top - draggedFrom.Y;
      }
    }

    private void SMSContactDlg_MouseUp(object sender, MouseEventArgs e)
    {
      draggingToolbar = false;
      this.Capture = false;
    }

    private void Ok_Click(object sender, EventArgs e)
    {
      selectPhoneNumber(searchTxtBx.Text);
    }


    private void closeBtn_MouseEnter(object sender, EventArgs e)
    {
      this.closeBtn.Image = global::Blurts.Properties.Resources.close_Btn_focus;
    }

    private void closeBtn_MouseLeave(object sender, EventArgs e)
    {
      this.closeBtn.Image = global::Blurts.Properties.Resources.close_Btn;
    }

    private void maximizeBtn_MouseEnter(object sender, EventArgs e)
    {
      this.maximizeBtn.Image = global::Blurts.Properties.Resources.maximize_Btn_focus;
    }

    private void maximizeBtn_MouseLeave(object sender, EventArgs e)
    {
      this.maximizeBtn.Image = global::Blurts.Properties.Resources.maximize_Btn;
    }

    private void minimizeBtn_MouseEnter(object sender, EventArgs e)
    {
      this.minimizeBtn.Image = global::Blurts.Properties.Resources.minimizeBtn_focus;
    }

    private void minimizeBtn_MouseLeave(object sender, EventArgs e)
    {
      this.minimizeBtn.Image = global::Blurts.Properties.Resources.minimizeBtn;
    }

    private void minimizeBtn_MouseClick(object sender, MouseEventArgs e)
    {

    }

    private void maximizeBtn_MouseClick(object sender, MouseEventArgs e)
    {
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
    }

    private void minimizeBtn_Click(object sender, EventArgs e)
    {
      if ( this.MdiParent != null )
      {
        this.parent = this.MdiParent;
        this.MdiParent = null;
      }
      else
      {
        this.MdiParent = this.parent;
      }
      //this.WindowState = FormWindowState.Minimized;
    }

    private void keypadBtn_Click(object sender, EventArgs e)
    {
      m_tab = eTabs.eTab_Keypad;
      showKeypad(true);
    }

    private void favoriteBtn_Click(object sender, EventArgs e)
    {
      m_tab = eTabs.eTab_Favorite;
      showKeypad(false);
      Display();
    }

    private void AllBtn_Click(object sender, EventArgs e)
    {
      m_tab = eTabs.eTab_All;
      showKeypad(false);
      Display();
    }

    private void HistoryBtn_Click(object sender, EventArgs e)
    {
      m_tab = eTabs.eTab_History;
      showKeypad(false);
      Display();
    }

    private void showKeypad( Boolean bShow )
    {
      key1.Visible = bShow;
      key2.Visible = bShow;
      key3.Visible = bShow;
      key4.Visible = bShow;
      key5.Visible = bShow;
      key6.Visible = bShow;
      key7.Visible = bShow;
      key8.Visible = bShow;
      key9.Visible = bShow;
      key0.Visible = bShow;
      keyPound.Visible = bShow;
      keyStar.Visible = bShow;
    }

    private void key1_Click(object sender, EventArgs e)
    {
      searchTxtBx.Text += "1";
    }

    private void key2_Click(object sender, EventArgs e)
    {
      searchTxtBx.Text += "2";
    }

    private void key3_Click(object sender, EventArgs e)
    {
      searchTxtBx.Text += "3";
    }

    private void key4_Click(object sender, EventArgs e)
    {
      searchTxtBx.Text += "4";
    }

    private void key5_Click(object sender, EventArgs e)
    {
      searchTxtBx.Text += "5";
    }

    private void key6_Click(object sender, EventArgs e)
    {
      searchTxtBx.Text += "6";
    }

    private void key7_Click(object sender, EventArgs e)
    {
      searchTxtBx.Text += "7";
    }

    private void key8_Click(object sender, EventArgs e)
    {
      searchTxtBx.Text += "8";
    }

    private void key9_Click(object sender, EventArgs e)
    {
      searchTxtBx.Text += "9";
    }

    private void keyStar_Click(object sender, EventArgs e)
    {
      searchTxtBx.Text += "*";
    }

    private void key0_Click(object sender, EventArgs e)
    {
      searchTxtBx.Text += "0";
    }

    private void keyPound_Click(object sender, EventArgs e)
    {
      searchTxtBx.Text += "#";
    }

    private void key1_MouseDown(object sender, MouseEventArgs e)
    {
      this.key1.Image = global::Blurts.Properties.Resources._1key_sel;
    }

    private void key1_MouseUp(object sender, MouseEventArgs e)
    {
      this.key1.Image = global::Blurts.Properties.Resources._1key;
    }

    private void key4_MouseDown(object sender, MouseEventArgs e)
    {
      this.key4.Image = global::Blurts.Properties.Resources._4key_sel;
    }

    private void key4_MouseUp(object sender, MouseEventArgs e)
    {
      this.key4.Image = global::Blurts.Properties.Resources._4key;
    }

    private void key7_MouseDown(object sender, MouseEventArgs e)
    {
      this.key7.Image = global::Blurts.Properties.Resources._7key_sel;
    }

    private void key7_MouseUp(object sender, MouseEventArgs e)
    {
      this.key7.Image = global::Blurts.Properties.Resources._7key;
    }

    private void keyStar_MouseDown(object sender, MouseEventArgs e)
    {
      this.keyStar.Image = global::Blurts.Properties.Resources.Starkey_sel;
    }

    private void keyStar_MouseUp(object sender, MouseEventArgs e)
    {
      this.keyStar.Image = global::Blurts.Properties.Resources.Starkey;
    }

    private void key2_MouseDown(object sender, MouseEventArgs e)
    {
      this.key2.Image = global::Blurts.Properties.Resources._2key_sel;
    }

    private void key2_MouseUp(object sender, MouseEventArgs e)
    {
      this.key2.Image = global::Blurts.Properties.Resources._2key;
    }

    private void key5_MouseDown(object sender, MouseEventArgs e)
    {
      this.key5.Image = global::Blurts.Properties.Resources._5key_sel;
    }

    private void key5_MouseUp(object sender, MouseEventArgs e)
    {
      this.key5.Image = global::Blurts.Properties.Resources._5key;
    }

    private void key8_MouseDown(object sender, MouseEventArgs e)
    {
      this.key8.Image = global::Blurts.Properties.Resources._8key_sel;
    }

    private void key8_MouseUp(object sender, MouseEventArgs e)
    {
      this.key8.Image = global::Blurts.Properties.Resources._8key;
    }

    private void key0_MouseDown(object sender, MouseEventArgs e)
    {
      this.key0.Image = global::Blurts.Properties.Resources._0key_sel;
    }

    private void key0_MouseUp(object sender, MouseEventArgs e)
    {
      this.key0.Image = global::Blurts.Properties.Resources._0key;
    }

    private void key3_MouseDown(object sender, MouseEventArgs e)
    {
      this.key3.Image = global::Blurts.Properties.Resources._3key_sel;
    }

    private void key3_MouseUp(object sender, MouseEventArgs e)
    {
      this.key3.Image = global::Blurts.Properties.Resources._3key;
    }

    private void key6_MouseDown(object sender, MouseEventArgs e)
    {
      this.key6.Image = global::Blurts.Properties.Resources._6key_sel;
    }

    private void key6_MouseUp(object sender, MouseEventArgs e)
    {
      this.key6.Image = global::Blurts.Properties.Resources._6key;
    }

    private void key9_MouseDown(object sender, MouseEventArgs e)
    {
      this.key9.Image = global::Blurts.Properties.Resources._9key_sel;
    }

    private void key9_MouseUp(object sender, MouseEventArgs e)
    {
      this.key9.Image = global::Blurts.Properties.Resources._9key;
    }

    private void keyPound_MouseDown(object sender, MouseEventArgs e)
    {
      this.keyPound.Image = global::Blurts.Properties.Resources.poundkey_sel;
    }

    private void keyPound_MouseUp(object sender, MouseEventArgs e)
    {
      this.keyPound.Image = global::Blurts.Properties.Resources.poundkey;
    }
  }
}