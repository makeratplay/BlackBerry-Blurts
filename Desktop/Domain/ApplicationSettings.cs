using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Blurts
{
  public class ApplicationSettings
  {
    public const int HOTKEY_COUNT = 6;
    private static ApplicationSettings instance;
    static private string settingFileName = getLocalDataPath() + @"\Blurts.config";


    public static ApplicationSettings Instance
    {
      get
      {
        if (instance == null)
        {
          instance = LoadAppSettings();
        }
        return instance;
      }
    }

    public class HotKeySettings
    {
      private string _command;
      public string Command
      {
        get { return _command; }
        set { _command = value; }
      }

      private string _keyCode;
      public string KeyCode
      {
        get { return _keyCode; }
        set { _keyCode = value; }
      }

      private string _modifier;
      public string Modifier
      {
        get { return _modifier; }
        set { _modifier = value; }
      }
    }

    public class WindowSettings
    {
      private int _xPos = -1;
      private int _yPos = -1;
      private int _state = -1;
      private Boolean _visable = false;

      public int xPos
      {
        get { return _xPos; }
        set { _xPos = value; }
      }
      public int yPos
      {
        get { return _yPos; }
        set { _yPos = value; }
      }
      public int State
      {
        get { return _state; }
        set { _state = value; }
      }
      public Boolean Visable
      {
        get { return _visable; }
        set { _visable = value; }
      }
    }


    private bool m_appSettingsChanged;
    // Variables used to store the application settings.
    private Boolean m_active;
    private int m_displayTime;
    private int m_maxSMS;
    private string m_soundFile;
    private string m_deviceName;
    private string m_deviceAddress;
    private Boolean m_autoConnect;
    private Boolean m_autoLock;
    private Boolean m_showPhotos;
    private Boolean m_smsMultiMsg; 
    private Boolean m_smsShowImages;
    private int m_screenEnum;
    private int m_locationEnum;
    private Boolean m_enableScript;
    private Boolean m_enableScriptErrorMsg;
    private string m_scriptFile;
    private Boolean m_toolbarTopMost;
    private Boolean m_dbclickShowMsg;
    private int m_channel;
    private int m_toolbarOpacity;
    private int m_alertOpacity;
    private Boolean m_showToolbarOnConnect;

    private string m_smsTextColor;
    private string m_smsBgColor;

    private  HotKeySettings[] m_hotKeys;
    private WindowSettings m_toolbar;

    private ApplicationSettings()
    {
      m_appSettingsChanged = false;
      m_channel = 1;
      m_active = false;
      m_displayTime = 5;
      m_maxSMS = 160;
      m_soundFile = "";
      m_deviceName = "";
      m_deviceAddress = "";
      m_autoConnect = true;
      m_autoLock = false;
      m_showPhotos = true;
      m_smsMultiMsg = false;
      m_smsShowImages = true;
      m_screenEnum = -1;
      m_locationEnum = 3;
      m_enableScript = false;
      m_enableScriptErrorMsg = false;
      m_scriptFile = getLocalDataPath() + @"\script.js";
      m_toolbarTopMost = false;
      m_dbclickShowMsg = false;
      m_toolbar = new WindowSettings();
      m_smsTextColor = "black";
      m_smsBgColor = "white";
      m_toolbarOpacity = 100;
      m_alertOpacity = 100;
      m_showToolbarOnConnect = true;
      initHotKeys();
    }


    public void initHotKeys()
    {
      m_hotKeys = new HotKeySettings[HOTKEY_COUNT];
      m_hotKeys[0] = new HotKeySettings();
      m_hotKeys[0].Command = "Send key";

      m_hotKeys[1] = new HotKeySettings();
      m_hotKeys[1].Command = "End key";

      m_hotKeys[2] = new HotKeySettings();
      m_hotKeys[2].Command = "Place call";

      m_hotKeys[3] = new HotKeySettings();
      m_hotKeys[3].Command = "Send SMS";
      
      m_hotKeys[4] = new HotKeySettings();
      m_hotKeys[4].Command = "Write to BlackBerry Clipboard";

      m_hotKeys[5] = new HotKeySettings();
      m_hotKeys[5].Command = "Write to Windows Clipboard";
    }

    // Properties used to access the application settings variables.

    [System.Xml.Serialization.XmlElementAttribute("HotKey")]
    public HotKeySettings[] HotKeys
    {
      get { return m_hotKeys; }
      set
      {
        m_hotKeys = value;
        m_appSettingsChanged = true;
      }
    }

   // public HotKeySettings SendKeyHotKey
   // {
   //   get { return m_SendKeyHotKey; }
   // }

    public WindowSettings ToolBar
    {
      get { return m_toolbar; }
      set
      {
        m_toolbar = value;
        m_appSettingsChanged = true;
      }
    }

    public Boolean Active
    {
      get { return m_active; }
      set
      {
        if (value != m_active)
        {
          m_active = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public int Channel
    {
      get { return m_channel; }
      set
      {
        if (value != m_channel)
        {
          m_channel = value;
          m_appSettingsChanged = true;
        }
      }
    }
    
    public Boolean AutoConnect
    {
      get { return m_autoConnect; }
      set
      {
        if (value != m_autoConnect)
        {
          m_autoConnect = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public Boolean EnableScript
    {
      get { return m_enableScript; }
      set
      {
        if (value != m_enableScript)
        {
          m_enableScript = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public Boolean ToolbarTopMost
    {
      get { return m_toolbarTopMost; }
      set
      {
        if (value != m_toolbarTopMost)
        {
          m_toolbarTopMost = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public Boolean ShowLastMsg
    {
      get { return m_dbclickShowMsg; }
      set
      {
        if (value != m_dbclickShowMsg)
        {
          m_dbclickShowMsg = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public Boolean EnableScriptErrorMsg
    {
      get { return m_enableScriptErrorMsg; }
      set
      {
        if (value != m_enableScriptErrorMsg)
        {
          m_enableScriptErrorMsg = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public Boolean AutoLock
    {
      get { return m_autoLock; }
      set
      {
        if (value != m_autoLock)
        {
          m_autoLock = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public int DisplayTime
    {
      get { return m_displayTime; }
      set
      {
        if (value != m_displayTime)
        {
          m_displayTime = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public int MaxSMS
    {
      get { return m_maxSMS; }
      set
      {
        if (value != m_maxSMS)
        {
          m_maxSMS = value;
          if (m_maxSMS < 1)
          {
            m_maxSMS = 160;
          }
          m_appSettingsChanged = true;
        }
      }
    }

    public string SoundFile
    {
      get { return m_soundFile; }
      set
      {
        if (value != m_soundFile)
        {
          m_soundFile = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public string SmsTextColor
    {
      get { return m_smsTextColor; }
      set
      {
        if (value != m_smsTextColor)
        {
          m_smsTextColor = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public string SmsBackgroundColor
    {
      get { return m_smsBgColor; }
      set
      {
        if (value != m_smsBgColor)
        {
          m_smsBgColor = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public string ScriptFile
    {
      get { return m_scriptFile; }
      set
      {
        if (value != m_scriptFile)
        {
          m_scriptFile = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public string DeviceName
    {
      get { return m_deviceName; }
      set
      {
        if (value != m_deviceName)
        {
          m_deviceName = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public string DeviceAddress
    {
      get { return m_deviceAddress; }
      set
      {
        if (value != m_deviceAddress)
        {
          m_deviceAddress = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public Boolean ShowPhotos
    {
      get { return m_showPhotos; }
      set
      {
        if (value != m_showPhotos)
        {
          m_showPhotos = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public Boolean SmsMultiMsg
    {
      get { return m_smsMultiMsg; }
      set
      {
        if (value != m_smsMultiMsg)
        {
          m_smsMultiMsg = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public Boolean SmsShowImages
    {
      get { return m_smsShowImages; }
      set
      {
        if (value != m_smsShowImages)
        {
          m_smsShowImages = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public int ScreenEnum
    {
      get { return m_screenEnum; }
      set
      {
        if (value != m_screenEnum)
        {
          m_screenEnum = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public int LocationEnum
    {
      get { return m_locationEnum; }
      set
      {
        if (value != m_locationEnum)
        {
          m_locationEnum = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public int ToolbarOpacity
    {
      get 
      {
        if (m_toolbarOpacity < 10)
        {
          m_toolbarOpacity = 10;
        }
        else if (m_toolbarOpacity > 100)
        {
          m_toolbarOpacity = 100;
        }
        return m_toolbarOpacity; 
      }
      set
      {
        if (value != m_toolbarOpacity)
        {
          m_toolbarOpacity = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public int AlertOpacity
    {
      get
      {
        if (m_alertOpacity < 10)
        {
          m_alertOpacity = 10;
        }
        else if (m_alertOpacity > 100)
        {
          m_alertOpacity = 100;
        }
        return m_alertOpacity;
      }
      set
      {
        if (value != m_alertOpacity)
        {
          m_alertOpacity = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public Boolean ShowToolbarOnConnect
    {
      get
      {
        return m_showToolbarOnConnect;
      }
      set
      {
        if (value != m_showToolbarOnConnect)
        {
          m_showToolbarOnConnect = value;
          m_appSettingsChanged = true;
        }
      }
    }

    public void SetDirty()
    {
      m_appSettingsChanged = true;
    }

    // Serializes the class to the config file
    // if any of the settings have changed.
    public bool SaveAppSettings()
    {
      bool retVal = false;
      if (this.m_appSettingsChanged)
      {
        StreamWriter myWriter = null;
        XmlSerializer mySerializer = null;
        try
        {
          // Create an XmlSerializer for the 
          // ApplicationSettings type.
          mySerializer = new XmlSerializer( typeof(ApplicationSettings) );
          myWriter = new StreamWriter(settingFileName, false);
          // Serialize this instance of the ApplicationSettings 
          // class to the config file.
          mySerializer.Serialize(myWriter, this);
          m_appSettingsChanged = false;
          retVal = true;
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message);
        }
        finally
        {
          // If the FileStream is open, close it.
          if (myWriter != null)
          {
            myWriter.Close();
          }
        }
      }
      return retVal;
    }

    // Deserializes the class from the config file.
    static private ApplicationSettings LoadAppSettings()
    {
      XmlSerializer mySerializer = null;
      FileStream myFileStream = null;
      ApplicationSettings myAppSettings = null;

      try
      {
        // Create an XmlSerializer for the ApplicationSettings type.
        mySerializer = new XmlSerializer(typeof(ApplicationSettings));
        FileInfo fi = new FileInfo(settingFileName);
        // If the config file exists, open it.
        if (fi.Exists)
        {
          myFileStream = fi.OpenRead();
          // Create a new instance of the ApplicationSettings by
          // deserializing the config file.
          myAppSettings = (ApplicationSettings)mySerializer.Deserialize(myFileStream);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
      finally
      {
        // If the FileStream is open, close it.
        if (myFileStream != null)
        {
          myFileStream.Close();
        }
        else
        {
          myAppSettings = new ApplicationSettings();
        }
      }
      return myAppSettings;
    }

    public String ScriptLanguage
    {
      get 
      {
        if (Path.GetExtension(m_scriptFile).ToLower() == ".vbs")
        {
          return "VBScript";
        }
        else
        {
          return "JScript";
        }
      }
    }

    public String LocalDataPath
    {
      get
      {
        return getLocalDataPath();
      }
    }

    public String AppPath
    {
      get
      {
        //string executableName = Application.ExecutablePath;
        //FileInfo executableFileInfo = new FileInfo(executableName);
        //string executableDirectoryName = executableFileInfo.DirectoryName;
        return Application.StartupPath;
      }
    }

    static private String getLocalDataPath()
    {
      String path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      path += @"\MLHSoftware";

      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }

      path += @"\Blurts";
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }
      return path;
    }

  }
}
