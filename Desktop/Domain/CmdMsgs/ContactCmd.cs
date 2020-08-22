using System;

using System.Xml;
using System.Xml.Xsl;
using System.Collections;
using System.Runtime.InteropServices;

namespace Blurts
{
  [ComVisible(true)]
  public class ContactCmd : CmdBase
  {
    // Tag Names
    private static String KEY_PREFIXNAME = "PrefixName";
    private static String KEY_FIRSTNAME = "FirstName";
    private static String KEY_LASTNAME = "LastName";
    //private static String KEY_WORK = "PhoneWork";
    private static String KEY_MOBILE = "PhoneMobile";


    public ContactCmd(String s)
      : base(s)
    {
    }

    public ContactCmd()
      : base(CMD_CONTACTSEARCH)
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
  }
}
