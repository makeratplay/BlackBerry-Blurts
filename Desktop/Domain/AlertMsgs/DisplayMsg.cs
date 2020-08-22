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

namespace Blurts
{
  [ComVisible(true)]
  public class DisplayMsg
  {
    private Boolean m_playSound;
    private Boolean m_displayAlert;
    private Boolean m_displaySMSChat;
    private int m_displayInterval;
    private string m_backgroundColor;
    private string m_html;
    private string m_soundFile;
    private int m_opacity;
    private string m_smsAdress;


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

    public DisplayMsg()
    {
      m_playSound = true;
      m_displayAlert = true;
      m_displaySMSChat = false;
      m_displayInterval = ApplicationSettings.Instance.DisplayTime * 1000;
      m_backgroundColor = "#718AF4";
      m_html = "";
      m_soundFile = ApplicationSettings.Instance.SoundFile;
      m_opacity = 100;
      m_smsAdress = "";
    }

    public string getHTML()
    {
      return m_html;
    }

    public void setHTML( string value )
    {
      m_html = value;
    }
    
      
  }
}
