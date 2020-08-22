using System;
using System.Runtime.InteropServices;
using Blurts.Domain;

namespace Blurts
{
  [ComVisible(true)]
  public class DisconnectAlert : AlertBase
  {
    // Tag Names
    private static String ALERT_NAME = "Disconnect";
    private static String KEY_STATUS = "Text";
    private static String KEY_BT_NAME  = "BluetoothName";
    private static String KEY_DEVICE_PIN   = "DevicePIN";
    private static String KEY_DEVICE_MODEL = "DeviceModel";

    public DisconnectAlert(String s)
      : base(s)
    {
    }

    public DisconnectAlert()
      : base(DisplayItemType.DISCONNECT, ALERT_NAME)
    {
      put(KEY_ACTIVATED, ApplicationSettings.Instance.Active ? "true" : "false");
    }

    [ComVisible(true)]
    public String Text
    {
      get
      {
        return optString(KEY_STATUS);
      }
      set
      {
        try
        {
          remove(KEY_STATUS);
          put(KEY_STATUS, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public String BluetoothName
    {
      get
      {
        return optString(KEY_BT_NAME);
      }
      set
      {
        try
        {
          remove(KEY_BT_NAME);
          put(KEY_BT_NAME, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public String DevicePIN
    {
      get
      {
        return optString(KEY_DEVICE_PIN);
      }
      set
      {
        try
        {
          remove(KEY_DEVICE_PIN);
          put(KEY_DEVICE_PIN, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public String DeviceModel
    {
      get
      {
        return optString(KEY_DEVICE_MODEL);
      }
      set
      {
        try
        {
          remove(KEY_DEVICE_MODEL);
          put(KEY_DEVICE_MODEL, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    public override Boolean ProcessMessage()
    {
      this.BackgroundColor = "red";
      this.Text = "BlackBerry Disconnected";
      runScript( "OnDisconnectAlert" );
      AlertHistory.Instance.AddAlert(XML);
      return true;
    }
  }
}