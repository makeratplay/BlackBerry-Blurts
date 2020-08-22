'
'  Blurts Document Object Model (DOM)
'
'    -AlertWindow
'      - DataPath
'      - AppPath
'      - Clipboard
'      - Sleep(int)   
'    
'    -Alert  
'      - PlaySound
'      - Opacity
'      - DisplayAlert
'      - DisplayInterval
'      - DisplaySMSChat
'      - SoundFile
'      - BackgroundColor  
'      - BackgroundColorTop
'      - BatteryLevel
'      - DisplayIcon
'      - DisplaySMSChat
'      - ImageFile
'      - SignalLevel
'      - Priority ( Set to LowPriority or HighPriority)
'      - XML
'      
'    
'    -BlackBerry 
'      Buzz()
'      ClickTrackball()
'      Connect()
'      DialPhone(string phoneNumber)
'      Disconnect()
'      DownloadContacts()
'      GetLevelStatus()
'      IsConnected()
'      MoveTrackballDown()
'      MoveTrackballLeft()
'      MoveTrackballRight()
'      MoveTrackballUp()
'      PressEndKey()
'      PressEscKey()
'      PressMenuKey()
'      PressMuteKey()
'      PressSendKey()
'      PressSpeakerKey()
'      PressVolumeDownKey()
'      PressVolumeUpKey()
'      ReadClipboard()
'      SendSMS(string address, string text)
'      TakeScreenCapture()
'      WriteClipboard()
'      WriteClipboard( String text )
'      
'      
'      



'  
'  BlackBerry.Alert has the additional properties
'  
'  - Action ( Incoming, Answered, Disconnected, Initiated, Connected, Waiting )
'  - PhoneNumber
'  - CallerName
'  

Function  OnCallAlert()
End Function

'
'  BlackBerry.Alert has the additional properties
'  
'  - Text
'
Function OnLockAlert()
End Function

'
'  use BlackBerry.Alert.ImageFile to get image
'
Function OnScreenCaptureAlert()
End Function


'
'  BlackBerry.Alert has the additional properties
'  
'  - Text
'  
'  sample 
'  
'  BlackBerry.Alert.BackgroundColor = BlackBerry.Alert.Text;
'
Function OnClipBoardAlert()
End Function


'
'  BlackBerry.Alert has the additional properties
'  
'  - BodyText
'  - ReceivingAccount
'  - SenderName
'  - SenderAddress
'  - Subject
'
'
Function OnEmailAlert()
End Function

'
'  BlackBerry.Alert has the additional properties
'  
'  - Text
'  - BluetoothName
'  - DevicePIN
'  - DeviceModel
'
'
Function OnConnectAlert()
End Function

'
'  BlackBerry.Alert has no additional properties
'
Function OnDisconnectAlert()
End Function

'
'  BlackBerry.Alert has the additional properties
'  
'  - Text
'
Function OnStatusAlert()
End Function

'
'  BlackBerry.Alert has the additional properties
'  
'  - BodyText
'  - SenderAddress
'  - SenderName
'
Function OnSMSAlert()
End Function

'
'  use BlackBerry.Alert.XML to get contact data
'
Function OnContactAlert()
End Function

'
'  BlackBerry.Alert has the additional properties
'  
'  - MacroName
'
Function OnMacro()
End Function

'
' BlackBerry.Alert has no additional properties
'
Function OnLevelAlert()
End Function

