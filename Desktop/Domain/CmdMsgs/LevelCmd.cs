using System;

using System.Xml;
using System.Xml.Xsl;
using System.Collections;
using System.Runtime.InteropServices;

namespace Blurts
{
  [ComVisible(true)]
  public class LevelCmd : CmdBase
  {
    // Tag Names

    public LevelCmd(String s)
      : base(s)
    {
    }

    public LevelCmd()
      : base(CMD_LEVEL)
    {
    }
  }
}
