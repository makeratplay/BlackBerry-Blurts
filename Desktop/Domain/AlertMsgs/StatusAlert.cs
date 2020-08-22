using System;
using System.Runtime.InteropServices;
using Blurts.Domain;
namespace Blurts
{
  [ComVisible(true)]
  public class StatusAlert : AlertBase
  {
    // Tag Names
    private static String ALERT_NAME = "Status";
    private static String KEY_STATUS = "Text";



    public StatusAlert(String s)
      : base(s)
    {
      
    }

    public StatusAlert()
      : base(DisplayItemType.STATUS, ALERT_NAME)
    {
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

    public override Boolean ProcessMessage()
    {
      if (this.Text.StartsWith("Capturing screen image"))
      {
        remove(KEY_ALERT_TYPE);
        put(KEY_ALERT_TYPE, (int)DisplayItemType.STATUS_SCREEN_SHOT);
      }
      else if (this.Text.StartsWith("Contact Download Complete"))
      {
        this.Priority = this.HighPriority;
      }
      runScript( "OnStatusAlert" );
      AlertHistory.Instance.AddAlert(XML);
      return true;
    }

  }
}