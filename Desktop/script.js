/*
  Blurts Document Object Model (DOM)

    -AlertWindow
      - DataPath
      - AppPath
      - Clipboard
      - Sleep(int)
  
  
    -Alert  
      - Opacity  (Integer 0-100)
      - DisplayAlert (Boolean)
      - DisplayInterval (Integer milliseconds)
      - BackgroundColor  (String color name or RGB value in Hex)
      - BackgroundColorTop (String color name or RGB value in Hex)
      - DisplayIcon (Boolean)
      - Priority ( Set to Alert.LowPriority or Alert.HighPriority)
      - PlaySound (Boolean)
      - SoundFile (String full path to file)
      - BatteryLevel  (Integer)
      - ImageFile  (String)
      - SignalLevel (Integer)
      - XML (String)
      - HTML (String)
      
    
    -BlackBerry 
      - IsConnected()
      - Connect()
      - Disconnect()
      
      // Press keys
      - PressMenuKey()
      - PressEscKey()
      - PressSendKey()
      - PressEndKey()
      - PressSpeakerKey()
      - PressMuteKey()
      - PressVolumeUpKey()
      - PressVolumeDownKey()
      - PressKey( String key )
      - PressKeyEx( String key int status )

      // control Trackball / Trackpad
      - ClickTrackball()
      - MoveTrackballUp()
      - MoveTrackballDown()
      - MoveTrackballLeft()
      - MoveTrackballRight()
      
      // Clipboard control
      - WriteClipboard()
      - WriteClipboard( String text )
      - ReadClipboard()
     
      // Phone control
      - DialPhone( String phoneNumber )
      - SendDTMF()

      // SMS
      - SendSMS( String address, String text )

      - Buzz()
      - TakeScreenCapture()
      - GetLevelStatus()
      - DownloadContacts()
*/



/*
  
  BlackBerry.Alert has the additional properties
  
  - Action ( Incoming, Answered, Disconnected, Initiated, Connected, Waiting )
  - PhoneNumber
  - CallerName
  
  sample 
  
  if ( BlackBerry.Alert.Action == BlackBerry.Alert.Incoming )
  {
    if ( BlackBerry.Alert.PhoneNumber == "972-555-1212" )
    {
      BlackBerry.Alert.BackgroundColor = "red";
    }
  }
*/
function OnCallAlert()
{
}

/*
  BlackBerry.Alert has the additional properties
  
  - Text
*/
function OnLockAlert()
{
}

/*
  use BlackBerry.Alert.ImageFile to get image
*/
function OnScreenCaptureAlert()
{
}


/*
  BlackBerry.Alert has the additional properties
  
  - Text
  
  sample 
  
  BlackBerry.Alert.BackgroundColor = BlackBerry.Alert.Text;
*/
function OnClipBoardAlert()
{
}

/*
  BlackBerry.Alert has the additional properties
  
  - BodyText
  - ReceivingAccount
  - SenderName
  - SenderAddress
  - Subject

*/
function OnEmailAlert()
{
}

/*
  BlackBerry.Alert has the additional properties
  
  - Text
  - BluetoothName
  - DevicePIN
  - DeviceModel

*/
function OnConnectAlert()
{
}

/*
  BlackBerry.Alert has no additional properties
*/
function OnDisconnectAlert()
{
}

/*
  BlackBerry.Alert has the additional properties
  
  - Text
*/
function OnStatusAlert()
{
}

/*
  BlackBerry.Alert has the additional properties
  
  - BodyText
  - SenderAddress
  - SenderName
  - DisplaySMSChat (Boolean)
  
*/
function OnSMSAlert()
{
}

/*
  use BlackBerry.Alert.XML to get contact data
*/
function OnContactAlert()
{
}

/*
  BlackBerry.Alert has the additional properties
  
  - MacroName
*/
function OnMacro()
{
}

/*
  BlackBerry.Alert has no additional properties
*/
function OnLevelAlert()
{
}

