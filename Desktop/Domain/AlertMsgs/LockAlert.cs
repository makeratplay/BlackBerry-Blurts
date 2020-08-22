using System;
using System.Runtime.InteropServices;
using Blurts.Domain;

namespace Blurts
{
  [ComVisible(true)]
  public class LockAlert : AlertBase
  {
    // Tag Names
    private static String ALERT_NAME = "Lock";
    private static String KEY_STATUS = "Text";


    public LockAlert(String s)
      : base(s)
    {
    }

    public LockAlert()
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
      if (ApplicationSettings.Instance.Active)
      {
        MainScreen.LockWorkStation();  //FIX move this call out of MainScreen
      }
      runScript( "OnLockAlert" );
      AlertHistory.Instance.AddAlert(XML);
      return true;
    }
  }
}