using System;
using System.Runtime.InteropServices;
using System.IO;
using Blurts.Domain;

namespace Blurts
{
  [ComVisible(true)]
  public class ScreenCaptureAlert : AlertBase
  {
    // Tag Names
    private static String ALERT_NAME = "Screen";
    private static String KEY_DATA = "Data";


    public ScreenCaptureAlert(String s)
      : base(s)
    {

    }

    public ScreenCaptureAlert()
      : base(DisplayItemType.SCREEN, ALERT_NAME)
    {

    }

    [ComVisible(true)]
    public String ImageData
    {
      get
      {
        return optString(KEY_DATA);
      }
      set
      {
        try
        {
          remove(KEY_DATA);
          put(KEY_DATA, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    public override Boolean ProcessMessage()
    {
      this.DisplayIcon = false;
      this.Priority = this.HighPriority;
      string fileName = ApplicationSettings.Instance.LocalDataPath + @"\screenshot.jpg";
      if (File.Exists(fileName))
      {
        File.Delete(fileName);
      }

      File.Move(this.ImageFile, fileName);
      this.ImageFile = fileName;

      runScript( "OnScreenCaptureAlert" );
      AlertHistory.Instance.AddAlert(XML);
      return true;
    }
  }
}
