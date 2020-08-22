using System;
using System.Runtime.InteropServices;
using Blurts.Domain;

namespace Blurts
{
  [ComVisible(true)]
  public class CallAlert : AlertBase
  {
    // Tag Names
    private static String ALERT_NAME = "Call";
    private static String KEY_ACTION = "Action";
    private static String KEY_NUMBER = "PhoneNumber";
    private static String KEY_NAME = "CallerName";

    private String rawNumber;

    [ComVisible(true)]
    public String Incoming
    {
      get
      {
        return "Incoming";
      }
    }

    [ComVisible(true)]
    public String Answered
    {
      get
      {
        return "Answered";
      }
    }

    [ComVisible(true)]
    public String Disconnected
    {
      get
      {
        return "Disconnected";
      }
    }

    [ComVisible(true)]
    public String Initiated
    {
      get
      {
        return "Initiated";
      }
    }

    [ComVisible(true)]
    public String Connected
    {
      get
      {
        return "Connected";
      }
    }

    [ComVisible(true)]
    public String Waiting
    {
      get
      {
        return "Waiting";
      }
    }

    public CallAlert(String s)
      : base(s)
    {
    }

    public CallAlert()
      : base(DisplayItemType.CALL, ALERT_NAME)
    {
    }

    [ComVisible(true)]
    public String PhoneNumber
    {
      get
      {
        if (rawNumber == null)
        {
          rawNumber = getRawNumber(FormattedNumber);
        }
        return rawNumber;
      }
      set
      {
        try
        {
          rawNumber = value;
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public String FormattedNumber
    {
      get
      {
        return optString(KEY_NUMBER);
      }
      set
      {
        try
        {
          remove(KEY_NUMBER);
          put(KEY_NUMBER, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public String CallerName
    {
      get
      {
        return optString(KEY_NAME);
      }
      set
      {
        try
        {
          remove(KEY_NAME);
          put(KEY_NAME, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public String Action
    {
      get
      {
        return optString(KEY_ACTION);
      }
      set
      {
        try
        {
          remove(KEY_ACTION);
          put(KEY_ACTION, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    static private string getRawNumber(string phoneNumber)
    {
      string retVal = "";
      if (phoneNumber != null)
      {
        for (int i = 0; i < phoneNumber.Length; i++)
        {
          if (phoneNumber[i] >= '0' && phoneNumber[i] <= '9')
          {
            retVal += phoneNumber[i];
          }
        }
        if (retVal.Length > 1 && retVal[0] == '1')
        {
          // remove the leading '1'
          retVal = retVal.Substring(1);
        }
      }
      return retVal;
    }

    public override Boolean ProcessMessage()
    {
      this.PhoneNumber = Contacts.rawNumber(this.FormattedNumber);
      this.ImageFile = Contacts.Instance.getImageFile(this.FormattedNumber, null);
      this.PlaySound = false;
      this.Priority = this.HighPriority;
      if (this.Action != this.Disconnected)
      {
        this.DisplayInterval = 120 * 1000;
      }
      runScript("OnCallAlert");
      AlertHistory.Instance.AddAlert(XML);
      return true;
    }
  }
}
