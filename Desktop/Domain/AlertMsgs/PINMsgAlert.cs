using System;
using System.Runtime.InteropServices;
using Blurts.Domain;
namespace Blurts
{
  [ComVisible(true)]
  public class PINMsgAlert : AlertBase
  {
    // Tag Names
    private static String ALERT_NAME = "PINMsg";
    private static String KEY_SENDER_NAME = "SenderName";
    private static String KEY_ACCOUNT = "ReceivingAccount";
    private static String KEY_ADDRESS = "SenderAddress";
    private static String KEY_SUBJECT = "Subject";
    private static String KEY_BODY = "BodyText";


    public PINMsgAlert(String s)
      : base(s)
    {
    }

    public PINMsgAlert()
      : base(DisplayItemType.PIN_MSG, ALERT_NAME)
    {
    }


    [ComVisible(true)]
    public String ReceivingAccount
    {
      get
      {
        return optString(KEY_ACCOUNT);
      }
      set
      {
        try
        {
          remove(KEY_ACCOUNT);
          put(KEY_ACCOUNT, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public String SenderName
    {
      get
      {
        return optString(KEY_SENDER_NAME);
      }
      set
      {
        try
        {
          remove(KEY_SENDER_NAME);
          put(KEY_SENDER_NAME, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }


    [ComVisible(true)]
    public String SenderAddress
    {
      get
      {
        return optString(KEY_ADDRESS);
      }
      set
      {
        try
        {
          remove(KEY_ADDRESS);
          put(KEY_ADDRESS, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public String Subject
    {
      get
      {
        return optString(KEY_SUBJECT);
      }
      set
      {
        try
        {
          remove(KEY_SUBJECT);
          put(KEY_SUBJECT, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }
    

    [ComVisible(true)]
    public String BodyText
    {
      get
      {
        return optString(KEY_BODY);
      }
      set
      {
        try
        {
          remove(KEY_BODY);
          put(KEY_BODY, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    public override Boolean ProcessMessage()
    {
      this.ImageFile = Contacts.Instance.getImageFile(null, this.SenderAddress);
      runScript( "OnPINMsgAlert" );
      AlertHistory.Instance.AddAlert(XML);
      return true;
    }
  }
}
