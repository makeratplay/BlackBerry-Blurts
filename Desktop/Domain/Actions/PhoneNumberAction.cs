using System;
using System.Runtime.InteropServices;
using Blurts.Domain;

namespace Blurts
{
  [ComVisible(true)]
  public class PhoneNumberAction : ActionBase
  {
    // Tag Names
    private static String ALERT_NAME = "ClipboardPhoneNumber";
    private static String KEY_NUMBER = "PhoneNumber";

    private String rawNumber;
    private String phoneNumber;



    public PhoneNumberAction( String phoneNumber )
      : base(DisplayItemType.CLIPBOARD_PHONE_NUMBER, ALERT_NAME)
    {
      FormattedNumber = phoneNumber;
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
      this.PlaySound = false;
      this.Priority = this.HighPriority;
      //runScript("OnCallAlert");
      //AlertHistory.Instance.AddAlert(XML);
      return true;
    }
  }
}
