using System;
namespace Blurts.Domain
{
  public enum DisplayItemType
  {
    // Alert Types
    UNKNOWN = 0,
    STATUS = 1,
    EMAIL = 2,
    CALL = 3,
    LOCK = 4,
    SMS = 5,
    SCREEN = 6,
    CONTACTS = 7,
    CLIPBOARD = 9,
    CONNECT = 10,
    DISCONNECT = 11,
    LEVEL = 12,
    MACRO = 13,
    PIN_MSG = 14,
    INPUT_MSG = 15,
    READFILE = 16,
    STATUS_SCREEN_SHOT = 18,
    CLIPBOARD_PHONE_NUMBER = 19
  }

  public interface IDisplayItem
  {
    bool Activated { get; }
    string BackgroundColor { get; set; }
    string BackgroundColorTop { get; set; }
    int BatteryLevel { get; }
    int Channel { get; }
    bool DisplayAlert { get; set; }
    bool DisplayIcon { get; set; }
    int DisplayInterval { get; set; }
    bool DisplaySMSChat { get; set; }
    int HighPriority { get; }
    string HTML { get; }
    int Opacity { get; set; }
    bool PlaySound { get; set; }
    int Priority { get; set; }
    int SignalLevel { get; }
    string SoundFile { get; set; }
    DisplayItemType ItemType{ get; }

    string XML { get; }
  }
}
