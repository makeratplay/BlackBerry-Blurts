using System;

using System.Xml;
using System.Xml.Xsl;
using System.Collections;
using System.Runtime.InteropServices;

namespace Blurts
{
  [ComVisible(true)]
  public class CreateContactCmd : CmdBase
  {
    // Tag Names
    private static String KEY_PREFIXNAME = "PrefixName";
    private static String KEY_FIRSTNAME = "FirstName";
    private static String KEY_LASTNAME = "LastName";
    private static String KEY_WORK = "PhoneWork";
    private static String KEY_MOBILE = "PhoneMobile";
    private static String KEY_PHOTO = "Photo";
    private static String KEY_ORG = "Org";
    private static String KEY_PIN = "Pin";
    private static String KEY_EMAIL = "Email";


    public CreateContactCmd(String s)
      : base(s)
    {
    }

    public CreateContactCmd()
      : base(CMD_CREATECONTACT)
    {
    }

    [ComVisible(true)]
    public String PrefixName
    {
      get
      {
        return optString(KEY_PREFIXNAME);
      }
      set
      {
        try
        {
          remove(KEY_PREFIXNAME);
          put(KEY_PREFIXNAME, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    public String FirstName
    {
      get
      {
        return optString(KEY_FIRSTNAME);
      }
      set
      {
        try
        {
          remove(KEY_FIRSTNAME);
          put(KEY_FIRSTNAME, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    public String LastName
    {
      get
      {
        return optString(KEY_LASTNAME);
      }
      set
      {
        try
        {
          remove(KEY_LASTNAME);
          put(KEY_LASTNAME, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    public String PhoneWork
    {
      get
      {
        return optString(KEY_WORK);
      }
      set
      {
        try
        {
          remove(KEY_WORK);
          put(KEY_WORK, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    public String PhoneMobile
    {
      get
      {
        return optString(KEY_MOBILE);
      }
      set
      {
        try
        {
          remove(KEY_MOBILE);
          put(KEY_MOBILE, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }


    [ComVisible(true)]
    public String Photo
    {
      get
      {
        return optString(KEY_PHOTO);
      }
      set
      {
        try
        {
          remove(KEY_PHOTO);
          put(KEY_PHOTO, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public String Org
    {
      get
      {
        return optString(KEY_ORG);
      }
      set
      {
        try
        {
          remove(KEY_ORG);
          put(KEY_ORG, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public String Pin
    {
      get
      {
        return optString(KEY_PIN);
      }
      set
      {
        try
        {
          remove(KEY_PIN);
          put(KEY_PIN, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public String Email
    {
      get
      {
        return optString(KEY_EMAIL);
      }
      set
      {
        try
        {
          remove(KEY_EMAIL);
          put(KEY_EMAIL, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }
  }
}
