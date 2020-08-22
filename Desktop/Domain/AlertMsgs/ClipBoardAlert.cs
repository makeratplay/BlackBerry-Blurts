using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Blurts.Domain;

namespace Blurts
{
  [ComVisible(true)]
  public class ClipboardAlert : AlertBase
  {
    // Tag Names
    private static String ALERT_NAME = "Clipboard";
    private static String KEY_TEXT = "Text";


    public ClipboardAlert(String s)
      : base(s)
    {
    }

    public ClipboardAlert()
      : base(DisplayItemType.CLIPBOARD, ALERT_NAME)
    {
    }

    [ComVisible(true)]
    public String Text
    {
      get
      {
        return optString(KEY_TEXT);
      }
      set
      {
        try
        {
          remove(KEY_TEXT);
          put(KEY_TEXT, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    public override Boolean ProcessMessage()
    {
      runScript("OnClipBoardAlert");
      if (this.Text.Length > 0)
      {
        // cannot call SetDataObject from here because we are on the bluetooth receiving thread
        // and must be on the UI thread. SetDataObject will be called from the MainScreen form handls this message 
        /*
        try
        {
          System.Windows.Forms.Clipboard.SetDataObject(this.Text, true);
        }
        catch (Exception ex)
        {
          Console.WriteLine("Error Writting to Clipboard:" + Environment.NewLine + ex.ToString());
        }
         * */
      }
      AlertHistory.Instance.AddAlert(XML);
      return true;
    }
  }
}
