using System;
using System.Runtime.InteropServices;
using Blurts.Domain;

namespace Blurts
{
  [ComVisible(true)]
  public class LevelAlert : AlertBase
  {
    // Tag Names
  private static String ALERT_NAME  = "Level";


   
    public LevelAlert(String s)
      : base(s)
    {
    }

    public LevelAlert()
      : base(DisplayItemType.LEVEL, ALERT_NAME)
    {
    }

    public override Boolean ProcessMessage()
    {
      this.DisplayAlert = false;
      this.PlaySound = false;
      runScript( "OnLevelAlert" );
      return true;
    }
  }
}
