using System;
using System.Runtime.InteropServices;
using Blurts.Domain;

namespace Blurts
{
  [ComVisible(true)]
  public class ReadFileAlert : AlertBase
  {
    // Tag Names
    private static String ALERT_NAME = "ReadFile";
    private static String KEY_FILENAME = "FileName";


    public ReadFileAlert(String s)
      : base(s)
    {

    }

    public ReadFileAlert()
      : base(DisplayItemType.READFILE, ALERT_NAME)
    {

    }

    [ComVisible(true)]
    public String FileName
    {
      get
      {
        return optString(KEY_FILENAME);
      }
      set
      {
        try
        {
          remove(KEY_FILENAME);
          put(KEY_FILENAME, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    public override Boolean ProcessMessage()
    {
      /*
      alert.DisplayIcon = false;
      string fileName = ApplicationSettings.Instance.LocalDataPath + @"\screenshot.jpg";
      if (File.Exists(fileName))
      {
        File.Delete(fileName);
      }

      File.Move(alert.ImageFile, fileName);
      alert.ImageFile = fileName;
      m_form.m_screenCaptureImage = alert.ImageFile;

      publishMessage(alert);
       * */

      return true;
    }
  }
}
