using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Blurts
{
  class HotKeysManager
  {
    // singleton object
    private static HotKeysManager instance = null;

    public static HotKeysManager Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new HotKeysManager();
        }
        return instance;
      }
    }

    private List<Hotkey> m_hotKeys;

    private HotKeysManager()
    {
      m_hotKeys = new List<Hotkey>();
    }

    public void SetHotKeys()
    {
      //FIX
      /*
      ReleaseHotKeys();
      if (ApplicationSettings.Instance.HotKeys != null)
      {
        int len = ApplicationSettings.Instance.HotKeys.Length;
        for (int i = 0; i < len; i++)
        {
          ApplicationSettings.HotKeySettings hotKeySettings = ApplicationSettings.Instance.HotKeys[i];


          if (hotKeySettings != null && hotKeySettings.KeyCode != null && hotKeySettings.KeyCode.Length > 0)
          {
            Hotkey hotKey = new Hotkey();

            if (hotKeySettings.KeyCode == "F1")
            {
              hotKey.KeyCode = Keys.F1;
            }
            else if (hotKeySettings.KeyCode == "F2")
            {
              hotKey.KeyCode = Keys.F2;
            }
            else if (hotKeySettings.KeyCode == "F3")
            {
              hotKey.KeyCode = Keys.F3;
            }
            else if (hotKeySettings.KeyCode == "F4")
            {
              hotKey.KeyCode = Keys.F4;
            }
            else if (hotKeySettings.KeyCode == "F5")
            {
              hotKey.KeyCode = Keys.F5;
            }
            else if (hotKeySettings.KeyCode == "F6")
            {
              hotKey.KeyCode = Keys.F6;
            }
            else if (hotKeySettings.KeyCode == "F7")
            {
              hotKey.KeyCode = Keys.F7;
            }
            else if (hotKeySettings.KeyCode == "F8")
            {
              hotKey.KeyCode = Keys.F8;
            }
            else if (hotKeySettings.KeyCode == "F9")
            {
              hotKey.KeyCode = Keys.F9;
            }
            else if (hotKeySettings.KeyCode == "F10")
            {
              hotKey.KeyCode = Keys.F10;
            }
            else if (hotKeySettings.KeyCode == "F11")
            {
              hotKey.KeyCode = Keys.F11;
            }
            else if (hotKeySettings.KeyCode == "F12")
            {
              hotKey.KeyCode = Keys.F12;
            }


            if (hotKeySettings.Modifier == "Alt")
            {
              hotKey.Alt = true;
            }
            else if (hotKeySettings.Modifier == "Ctrl")
            {
              hotKey.Control = true;
            }
            else if (hotKeySettings.Modifier == "Win")
            {
              hotKey.Windows = true;
            }


            if (hotKeySettings.Command == "Send key")
            {
              hotKey.Pressed += delegate { this.m_BlackBerry.PressSendKey(); };
            }
            else if (hotKeySettings.Command == "End key")
            {
              hotKey.Pressed += delegate { this.m_BlackBerry.PressEndKey(); };
            }
            else if (hotKeySettings.Command == "Place call")
            {
              hotKey.Pressed += delegate { this.m_BlackBerry.DialPhone(""); };
            }
            else if (hotKeySettings.Command == "Send SMS")
            {
              hotKey.Pressed += delegate { this.m_BlackBerry.SendSMS("", ""); };
            }
            else if (hotKeySettings.Command == "Send Clipboard")
            {
              hotKey.Pressed += delegate { this.m_BlackBerry.WriteClipboard(); };
            }
            else if (hotKeySettings.Command == "Get Clipboard")
            {
              hotKey.Pressed += delegate { this.m_BlackBerry.ReadClipboard(); };
            }




            if (!hotKey.GetCanRegister(this))
            {
              Console.WriteLine("Whoops, looks like attempts to register will fail or throw an exception, show an error/visual user feedback");
            }
            else
            {
              hotKey.Register(this);
            }
            m_hotKeys.Add(hotKey);
          }
        }
      }
       * */
    }

    public void ReleaseHotKeys()
    {
      int count = m_hotKeys.Count;
      for (int i = 0; i < count; i++)
      {
        try
        {
          if (m_hotKeys[i].Registered)
          {
            m_hotKeys[i].Unregister();
          }
        }
        catch (Exception e)
        {
          Console.WriteLine("releaseHotKeys Exception: " + e.ToString());
        }
      }
      m_hotKeys.Clear();
    }
  }
}
