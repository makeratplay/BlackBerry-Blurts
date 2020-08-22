using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Xml;
using System.Xml.Xsl;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Blurts
{
  [ComVisible(true)]
  public partial class DialDlg : Form
  {
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    private bool draggingToolbar;
    private Point draggedFrom;
    string m_contactFilter;
    private string m_phoneNumber;

    public DialDlg()
    {
      m_contactFilter = "";
      m_phoneNumber = "";
      InitializeComponent();
    }

    public String getPhoneNumber()
    {
      return m_phoneNumber;
    }

    private void DialDlg_Load(object sender, EventArgs e)
    {
      webBrowser1.AllowWebBrowserDrop = false;
      webBrowser1.IsWebBrowserContextMenuEnabled = false;
      webBrowser1.WebBrowserShortcutsEnabled = true;
      webBrowser1.ScriptErrorsSuppressed = true;

      webBrowser1.ObjectForScripting = this;

      Display();
    }

    private void Display()
    {
      XslCompiledTransform xsl = new XslCompiledTransform();
      xsl.Load(ApplicationSettings.Instance.AppPath + @"\dial.xslt");

      XsltArgumentList argList = new XsltArgumentList();
      argList.AddParam("systemPath", "", ApplicationSettings.Instance.AppPath);
      argList.AddParam("dataPath", "", ApplicationSettings.Instance.LocalDataPath);
      argList.AddParam("contactFilter", "", m_contactFilter);

      XmlDocument outputDocument = new XmlDocument();
      System.Xml.XPath.XPathNavigator outputNavigator = outputDocument.CreateNavigator();
      using (XmlWriter writer = outputNavigator.AppendChild())
      {
        xsl.Transform(Contacts.Instance.XMLDoc, argList, writer);
      }
      webBrowser1.DocumentText = outputDocument.OuterXml;
    }


    private void DialDlg_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (m_phoneNumber.Length == 0)
      {
        m_phoneNumber = Contacts.justNumber(m_contactFilter);
      }
    }


    /*
    private void OnKey(int keyCode)
    {
      KeysConverter kc2 = new KeysConverter();
      Console.WriteLine(keyCode + ": " + kc2.ConvertToString(keyCode));
      if (m_formActive)
      {
       switch (keyCode)
        {
          case 32: // space
          {
            m_contactFilter += " ";
            Display();
            break;
          }
          case 187: // plus
          {
            m_contactFilter += "+";
            Display();
            break;
          }
          case 8: // back space
          case 46: // delete
          {
            int len = m_contactFilter.Length;
            if (len > 0)
            {
              m_contactFilter = m_contactFilter.Substring(0, len - 1);
              Display();
            }
            break;
          }

          default:
          {
            if ( (keyCode >= 65 && keyCode <= 90 ) ||
                 (keyCode >= 48 && keyCode <= 57 ) )
            {
              KeysConverter kc = new KeysConverter();
              m_contactFilter += kc.ConvertToString(keyCode).ToLower();
              Display();
            }
            else if (keyCode >= 96 && keyCode <= 105)
            {
              KeysConverter kc = new KeysConverter();
              m_contactFilter += kc.ConvertToString(keyCode - 48).ToLower();
              Display();
            }

            break;
          }
        }
      }
    }
     * */


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

    private void DialDlg_MouseDown(object sender, MouseEventArgs e)
    {
      draggingToolbar = true;
      draggedFrom = new Point(e.X, e.Y);
      this.Capture = true;
    }

    private void DialDlg_MouseMove(object sender, MouseEventArgs e)
    {
      if (draggingToolbar)
      {
        this.Left = e.X + this.Left - draggedFrom.X;
        this.Top = e.Y + this.Top - draggedFrom.Y;
      }
    }

    private void DialDlg_MouseUp(object sender, MouseEventArgs e)
    {
      draggingToolbar = false;
      this.Capture = false;
    }
  }
}