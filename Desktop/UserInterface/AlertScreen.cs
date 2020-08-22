using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using System.Configuration;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Blurts.Domain;

namespace Blurts
{
  
  [ComVisible(true)]
  public partial class AlertScreen : Form
  {
    // [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    
    
    
    private SoundPlayer m_player;
    private Boolean m_displaying;
    private Boolean m_playSound;
    private int m_Opacity;

    // singleton object
    private static AlertScreen instance = null;


    static public void DisplayAlert( IDisplayItem msg)
    {
      if (instance == null)
      {
        instance = new AlertScreen();
      }
      instance.Display(msg);
    }


    private AlertScreen()
    {
      m_player = null;
      m_playSound = true;
      m_displaying = false;
      m_Opacity = 100;
      

      InitializeComponent();

      Opacity = 0;
      webBrowser.AllowWebBrowserDrop = false;
      webBrowser.IsWebBrowserContextMenuEnabled = false;
      webBrowser.WebBrowserShortcutsEnabled = true;
      webBrowser.ScriptErrorsSuppressed = true;
      // Add an event handler that prints the document after it loads.
      webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(DocumentComlpete);
      webBrowser.ObjectForScripting = this;
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
    }

    private void Display( IDisplayItem msg)
    {
      if (msg != null)
      {
        m_displaying = true;
        timer.Interval = msg.DisplayInterval;
        m_Opacity = msg.Opacity;
        m_playSound = msg.PlaySound;
        if (m_playSound)
        {
          setSoundFile(msg.SoundFile);
        }

        string html = msg.HTML;

        if (html != null && html.Length > 0)
        {
          //FIX m_form.setMsgIcon();
          ClientSize = new System.Drawing.Size(320, 68);
          webBrowser.DocumentText = html;

          if (Directory.Exists(ApplicationSettings.Instance.LocalDataPath + @"\debug"))
          {
            StreamWriter SW;
            SW = File.CreateText(ApplicationSettings.Instance.LocalDataPath + @"\debug\display.html");
            SW.Write(html);
            SW.Close();
          }
        }
      }
      else
      {
        // display last message
        ShowForm();
      }
    }

    private void setSoundFile(string soundFile)
    {
      try
      {
        if (soundFile != null && soundFile.Length > 0)
        {
          if (m_player == null)
          {
            m_player = new SoundPlayer();
          }
          m_player.SoundLocation = soundFile;
          m_player.Load();
        }
        else
        {
          m_player = null;
        }
      }
      catch (Exception)
      {
      }
    }

    private void DocumentComlpete(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
//      webBrowser.Document.MouseOver += delegate
//      {
//        OnMouseOver();
//      };

//      webBrowser.Document.MouseLeave += delegate
//      {
//        OnMouseLeave();
//      };

//      webBrowser.Document.Click += delegate
//      {
//        OnClick();
//      };

      Refresh();
      ShowForm();

      if (m_player != null && ApplicationSettings.Instance.Active && m_playSound)
      {
        try
        {
          m_player.Play();
        }
        catch (Exception)
        {
        }
      }
    }

    private void setDlgPosition()
    {
      Screen desktop = Screen.PrimaryScreen;

      Screen[] screens = Screen.AllScreens;
      if (ApplicationSettings.Instance.ScreenEnum >= 0 && ApplicationSettings.Instance.ScreenEnum < screens.Length)
      {
        desktop = screens[ApplicationSettings.Instance.ScreenEnum];
      }

      switch (ApplicationSettings.Instance.LocationEnum)
      {
        case 0: // top left
        {
          Location = new Point(desktop.WorkingArea.Left + 10, desktop.WorkingArea.Top + 10);
          break;
        }
        case 1: // top right
        {
          Location = new Point(desktop.WorkingArea.Right - Width - 10, desktop.WorkingArea.Top + 10);
          break;
        }
        case 2: // bottom left
        {
          Location = new Point(desktop.WorkingArea.Left + 10, desktop.WorkingArea.Bottom - Height - 10);
          break;
        }
        case 3: // bottom right
        {
          Location = new Point(desktop.WorkingArea.Right - Width - 10, desktop.WorkingArea.Bottom - Height - 10);
          break;
        }
      }
    }

    private void ShowForm()
    {
      // Set location every time in case they change resolution.
      setDlgPosition();
      Visible = true;
      TopMost = true;
      TopLevel = true;
      FadeUpTo(m_Opacity);
      timer.Start();
    }

    private void HideForm()
    {
      FadeDownTo(0);
      m_displaying = false;
      //FIX m_form.setConnectedIcon();
      //FIX DisplayAlert(null, null); // show next alert
    }

    private void FadeUpTo(int max)
    {
      m_displaying = true;
      for (int i = Int32Opacity; i <= max; i += 5)
      {
        SetOpacityAndWait(i);
      }
    }

    private void FadeDownTo(int min)
    {
      for (int i = Int32Opacity; i >= min; i -= 5)
      {
        SetOpacityAndWait(i);
      }
    }

    private void SetOpacityAndWait(int opacity)
    {
      Opacity = opacity / 100d;
      Refresh();
      Thread.Sleep(20);
    }

    private int Int32Opacity
    {
      get { return (int)(Opacity * 100); }
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      timer.Stop();
      HideForm();
    }


    // Exposed methods and properties

    [ComVisible(true)]
    public String DataPath
    {
      get
      {
        return ApplicationSettings.Instance.LocalDataPath;
      }
    }

    [ComVisible(true)]
    public String AppPath
    {
      get
      {
        return ApplicationSettings.Instance.AppPath;
      }
    }

    [ComVisible(true)]
    public String Clipboard
    {
      get
      {
        string text = "";
        if (System.Windows.Forms.Clipboard.GetDataObject().GetDataPresent(DataFormats.UnicodeText))
        {
          text = System.Windows.Forms.Clipboard.GetDataObject().GetData(DataFormats.UnicodeText).ToString();
        }
        return text;
      }
      set
      {
        try
        {
          System.Windows.Forms.Clipboard.SetDataObject(value, true);
        }
        catch (Exception ex)
        {
          Console.WriteLine("Error Writting to Clipboard:" + Environment.NewLine + ex.ToString());
        }
      }
    }

    [ComVisible(true)]
    public void Sleep(int time)
    {
      Thread.Sleep(time);
    }

    [ComVisible(true)]
    public void OnMouseOver()
    {
      if (m_displaying)
      {
        timer.Stop();
        FadeUpTo(100);
      }
    }

    [ComVisible(true)]
    public void OnMouseLeave()
    {
      //Point pt = Cursor.Position;
      //Rectangle formBounds = Bounds;
      //Console.WriteLine( "OnMouseLeave pt(" + pt.X + "," + pt.Y + ") " + formBounds.ToString() );

      if (!Bounds.Contains(Cursor.Position))
      {
        //Console.WriteLine("Mouse Left the alert");
        FadeDownTo(m_Opacity);
        timer.Start();
      }
    }

    [ComVisible(true)]
    public void OnCloseBtn()
    {
      HideForm();

    }

    [ComVisible(true)]
    public void setClientSize(int width, int height)
    {
      ClientSize = new System.Drawing.Size(width, height);
      setDlgPosition();
    }



    [ComVisible(true)]
    public Device BlackBerry
    {
      get
      {
        return Device.Instance;
      }
    }

  }
}