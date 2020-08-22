using System;

using System.Xml;
using System.Xml.Xsl;
using System.Collections;
using System.Runtime.InteropServices;

namespace Blurts
{
  [ComVisible(true)]
  public class ReadFileCmd : CmdBase
  {
    // Tag Names
    private static String KEY_FILENAME = "FileName";



    public ReadFileCmd(String s)
      : base(s)
    {
    }

    public ReadFileCmd()
      : base(CMD_READFILE)
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
  }
}
