using System;
using System.Runtime.InteropServices;
using Blurts.Domain;

namespace Blurts
{
  [ComVisible(true)]
  public class MacroAlert : AlertBase
  {
    // Tag Names
    private static String ALERT_NAME = "Macro";
    private static String KEY_STATUS = "Text";
    private static String KEY_MACRO_NAME = "MacroName";


    public MacroAlert(String s)
      : base(s)
    {
    }

    public MacroAlert()
      : base(DisplayItemType.MACRO, ALERT_NAME)
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

    public String MacroName
    {
      get
      {
        return optString(KEY_MACRO_NAME);
      }
      set
      {
        try
        {
          remove(KEY_MACRO_NAME);
          put(KEY_MACRO_NAME, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    public override Boolean ProcessMessage()
    {
      this.Text = "Running " + this.MacroName;
      runScript( this.MacroName );
      return true;
    }
  }
}