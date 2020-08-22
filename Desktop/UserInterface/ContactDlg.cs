using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Xml;
using System.Xml.Xsl;

using System.Runtime.InteropServices;
using Blurts.Domain;

namespace Blurts
{
  [ComVisible(true)]
  public partial class ContactDlg : Form
  {
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]


    private bool draggingToolbar;
    private Point draggedFrom;
    private bool downloading;

    public string m_number;
    public string m_formatedNumber;
    public string m_displayName;
    private string m_contactFilter;
    private int m_scrollTop;
    private int m_scrollLeft;

    public ContactDlg()
    {
      downloading = false;
      m_scrollTop = 0;
      m_scrollLeft = 0;
      m_number = "";
      m_formatedNumber = "";
      m_displayName = "";
      m_contactFilter = "";
      InitializeComponent();
    }

    private void ContactDlg_Load(object sender, EventArgs e)
    {
      webBrowser1.AllowWebBrowserDrop = false;
      webBrowser1.IsWebBrowserContextMenuEnabled = false;
      webBrowser1.WebBrowserShortcutsEnabled = true;
      webBrowser1.ScriptErrorsSuppressed = true;

      webBrowser1.ObjectForScripting = this;


      Screen desktop = Screen.PrimaryScreen;
      if ( Location.X > desktop.WorkingArea.Width - this.Width - 10)
      {
        Location = new Point(desktop.WorkingArea.Width - this.Width - 10, Location.Y);
      }

      if (Location.Y > desktop.WorkingArea.Height - this.Height - 10)
      {
        Location = new Point(Location.X, desktop.WorkingArea.Height - this.Height - 10);
      }

      Device.Instance.MsgArrivedEvent += new MsgArrivedEventHandler(BlackBerry_MsgArrivedEvent);

      Display();
    }

    void BlackBerry_MsgArrivedEvent(object msg)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new EventHandler(OnMsgArrivedEvent), new Object[] { msg, null });
      }
      else
      {
        OnMsgArrivedEvent(msg, null);
      }
    }

    void OnMsgArrivedEvent(object msg, EventArgs e)
    {
      try
      {
        AlertBase alert = (AlertBase)msg;
        DisplayItemType type = alert.ItemType;
        switch (type)
        {
          case DisplayItemType.STATUS:
          {
            //processStatusAlert(new StatusAlert(data));
            break;
          }
          case DisplayItemType.CONTACTS:
          {
            string data = msg.ToString();
            ContactAlert contactAlert = new ContactAlert(data);
            this.downloadBtn.Text = "Download: " + contactAlert.Index + " left";
            this.downloadBtn.Invalidate();

            if (int.Parse(contactAlert.Index) % 10 == 0)
            {
              Display();
            }

            if ( int.Parse(contactAlert.Index) == 1 )
            {
              this.downloadBtn.Text = "Download";
              this.downloading = false;
              this.downloadBtn.Invalidate();
              Display();
            }
            break;
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("BlackBerry_MsgArrivedEvent:" + Environment.NewLine + ex.ToString());
      }
    }

    public void DisconnectEventFired()
    {
      Close();
    }

    private void Display()
    {
      this.Cursor = Cursors.WaitCursor;
      XslCompiledTransform xsl = new XslCompiledTransform();
      xsl.Load(ApplicationSettings.Instance.AppPath + @"\contacts.xslt");
      // AppPath is startup and true app path

      if (webBrowser1.Document != null )
      {
        m_scrollTop = webBrowser1.Document.Body.ScrollTop;
        m_scrollLeft = webBrowser1.Document.Body.ScrollLeft;
      }

      XsltArgumentList argList = new XsltArgumentList();
      argList.AddParam("systemPath", "", ApplicationSettings.Instance.AppPath);
      argList.AddParam("dataPath", "", ApplicationSettings.Instance.LocalDataPath);
      argList.AddParam("contactFilter", "", m_contactFilter);
      argList.AddParam("scrollTop", "", m_scrollTop.ToString());
      

      XmlDocument outputDocument = new XmlDocument();
      System.Xml.XPath.XPathNavigator outputNavigator = outputDocument.CreateNavigator();
      using (XmlWriter writer = outputNavigator.AppendChild())
      {
        xsl.Transform(Contacts.Instance.XMLDoc, argList, writer);
      }
      webBrowser1.DocumentText = outputDocument.OuterXml;
      
    }

    private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
      this.Cursor = Cursors.Default;

      //webBrowser1.Document.Window.ScrollTo(m_scrollLeft, m_scrollTop);
    }

    [ComVisible(true)]
    public void selectPhoneNumber(string number, string formatedNumber, string displayName)
    {
      m_number = number;
      m_formatedNumber = formatedNumber;
      m_displayName = displayName;

      this.DialogResult = DialogResult.OK;
      Close();
    }

    [ComVisible(true)]
    public void searchContacts(string contactFilter)
    {
      if (contactFilter == null)
      {
        m_contactFilter = "";
      }
      else
      {
        m_contactFilter = contactFilter.ToLower();
      }
      Display();
    }



    [ComVisible(true)]
    public void favoriteContact(string contactID, string number)
    {
      string xPath = string.Format("BlurtsData/Contacts/Contact[Uid = '{0}']/PhoneNumbers/PhoneNumber[Number/@raw = '{1}']", contactID, number);
      XmlNodeList contactNodes = Contacts.Instance.XMLDoc.SelectNodes(xPath);
      if (contactNodes.Count > 0)
      {
        XmlElement contactNode = (XmlElement)contactNodes.Item(0);
        contactNode.SetAttribute("favorite", "true");
      }
      Contacts.Instance.SaveContacts();
      Display();
    }

    [ComVisible(true)]
    public void clearFavoriteContact(string contactID, string rawNumber)
    {
      string xPath = string.Format("BlurtsData/Contacts/Contact[Uid = '{0}']/PhoneNumbers/PhoneNumber[Number/@raw = '{1}']", contactID, rawNumber);
      XmlNodeList contactNodes = Contacts.Instance.XMLDoc.SelectNodes(xPath);
      if (contactNodes.Count > 0)
      {
        XmlElement contactNode = (XmlElement)contactNodes.Item(0);
        contactNode.SetAttribute("favorite", "false");
      }
      Contacts.Instance.SaveContacts();
      Display();
    }

    [ComVisible(true)]
    public void showContact(string contactID)
    {
      string xPath = string.Format("BlurtsData/Contacts/Contact[Uid = '{0}']", contactID);
      XmlNodeList contactNodes = Contacts.Instance.XMLDoc.SelectNodes(xPath);
      if (contactNodes.Count > 0)
      {
        XmlElement contactNode = (XmlElement)contactNodes.Item(0);
        contactNode.SetAttribute("status", "visible");
      }
      Contacts.Instance.SaveContacts();
      Display();
    }

    [ComVisible(true)]
    public void hideContact(string contactID)
    {
      string xPath = string.Format("BlurtsData/Contacts/Contact[Uid = '{0}']", contactID);
      XmlNodeList contactNodes = Contacts.Instance.XMLDoc.SelectNodes(xPath);
      if (contactNodes.Count > 0)
      {
        XmlElement contactNode = (XmlElement)contactNodes.Item(0);
        contactNode.SetAttribute("status", "hidden");
      }
      Contacts.Instance.SaveContacts();
      Display();
    }


 

    private void ContactDlg_FormClosing(object sender, FormClosingEventArgs e)
    {
      Device.Instance.MsgArrivedEvent -= new MsgArrivedEventHandler(BlackBerry_MsgArrivedEvent);
    }



    private void downloadBtn_Click(object sender, EventArgs e)
    {
      if (!this.downloading)
      {
        if (Device.Instance.IsConnected())
        {
          if (ApplicationSettings.Instance.Active)
          {
            Device.Instance.DownloadContacts();
            this.downloadBtn.Text = "Downloading...";
            this.downloading = true;
            this.downloadBtn.Invalidate();
          }
          else
          {
            BuyDlg dlg = new BuyDlg();
            dlg.ShowDialog();
            //MessageBox.Show("Upgrade to Blurts Pro to enable this feature.", "Blurts - by MLH Software");
          }
        }
        else
        {
          MessageBox.Show("BlackBerry not connected.", "Blurts - by MLH Software");
        }
      }
      //this.downloadBtn.Enabled = false;
    }

    private void closeBtn_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      Close();
    }

    private void minimizeBtn_Click(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Minimized;
    }

    private void deleteAllContactsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Are you sure?", "Delete All Contacts", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
      {
        Contacts.Instance.DeleteAllContacts();
        Display();
      }
    }



    private void ContactDlg_MouseDown(object sender, MouseEventArgs e)
    {
      draggingToolbar = true;
      draggedFrom = new Point(e.X, e.Y);
      this.Capture = true;
    }

    private void ContactDlg_MouseMove(object sender, MouseEventArgs e)
    {
      if (draggingToolbar)
      {
        this.Left = e.X + this.Left - draggedFrom.X;
        this.Top = e.Y + this.Top - draggedFrom.Y;
      }
    }

    private void ContactDlg_MouseUp(object sender, MouseEventArgs e)
    {
      draggingToolbar = false;
      this.Capture = false;
    }

    private void minimizeBtn_MouseEnter(object sender, EventArgs e)
    {
      this.minimizeBtn.Image = global::Blurts.Properties.Resources.minimizeBtn_focus;
    }

    private void minimizeBtn_MouseLeave(object sender, EventArgs e)
    {
      this.minimizeBtn.Image = global::Blurts.Properties.Resources.minimizeBtn;
    }

    private void closeBtn_MouseEnter(object sender, EventArgs e)
    {
      this.closeBtn.Image = global::Blurts.Properties.Resources.close_Btn_focus;
    }

    private void closeBtn_MouseLeave(object sender, EventArgs e)
    {
      this.closeBtn.Image = global::Blurts.Properties.Resources.close_Btn;
    }
  }
}