
package com.mlhsoftware.Blurts;

import net.rim.device.api.ui.*;
import net.rim.device.api.ui.component.*;
import net.rim.device.api.ui.container.*;
import net.rim.device.api.ui.container.PopupScreen;
import net.rim.device.api.notification.NotificationsManager;
import net.rim.device.api.system.Characters;
import net.rim.device.api.system.ControlledAccessException;

import net.rim.device.api.ui.MenuItem;

import java.util.*;

import net.rim.blackberry.api.phone.phonelogs.PhoneCallLogID;
import net.rim.blackberry.api.phone.phonelogs.PhoneCallLog;
import net.rim.blackberry.api.phone.phonelogs.PhoneCallLogID;
import net.rim.blackberry.api.phone.phonelogs.PhoneLogs;

import net.rim.device.api.system.EventLogger;

class KeyDlg extends PopupScreen
{
  private EditField m_keyField;
  private DAOptionsProperties m_optionProperties;
  Blurts m_app;

  KeyDlg( Manager manager, Blurts app )
  {
    super( manager, DEFAULT_MENU | DEFAULT_CLOSE );

    m_app = app;

    add( new LabelField( "Enter activation code", LabelField.FIELD_HCENTER ) );
    add( new LabelField( "to upgrade to Blurts Pro.", LabelField.FIELD_HCENTER ) );
    add( new LabelField( "Your PIN: " + DAOptionsProperties.getPin(), LabelField.FIELD_HCENTER ) );
    m_keyField = new EditField( "", "", 50, EditField.NO_NEWLINE );
    add( m_keyField );


    HorizontalFieldManager hfm = new HorizontalFieldManager( Field.FIELD_HCENTER | Field.FIELD_BOTTOM );
    hfm.add( new OkButton() );
    hfm.add( new CancelButton() );
    add( hfm );

  }

  public void makeMenu( Menu menu, int instance )
  {
    if ( instance == Menu.INSTANCE_DEFAULT )
    {
      menu.add( _accept );
      menu.add( _cancel );
    }

    super.makeMenu( menu, instance );
  }

  private void OnAcceptCode()
  {
    if ( m_keyField.getText().length() == 0 )
    {
      Dialog.alert( "Please enter your key" );
      m_keyField.setFocus();
    }
    else
    {
      m_optionProperties = DAOptionsProperties.fetch();
      m_optionProperties.setKey( m_keyField.getText() );
      m_optionProperties.save();
      close();
      m_app.onDlgClose();
    }
  }

  private void OnCancel()
  {
    close();
  }

  private final class OkButton extends ButtonField
  {
    private OkButton()
    {
      super( "OK", ButtonField.CONSUME_CLICK );
    }

    protected void fieldChangeNotify( int context )
    {
      if ( ( context & FieldChangeListener.PROGRAMMATIC ) == 0 )
      {
        OnAcceptCode();
      }
    }
  }

  private final class CancelButton extends ButtonField
  {
    private CancelButton()
    {
      super( "Cancel", ButtonField.CONSUME_CLICK );
    }

    protected void fieldChangeNotify( int context )
    {
      if ( ( context & FieldChangeListener.PROGRAMMATIC ) == 0 )
      {
        OnCancel();
      }
    }
  }

  private MenuItem _accept = new MenuItem( "Accept", 80, 80 )
  {
    public void run()
    {
      OnAcceptCode();
    }
  };

  private MenuItem _cancel = new MenuItem( "Cancel", 80, 80 )
  {
    public void run()
    {
      OnCancel();
    }
  };

  public boolean keyChar( char key, int status, int time )
  {
    if ( key == net.rim.device.api.system.Characters.ENTER )
    {
      OnAcceptCode();
    }
    return super.keyChar( key, status, time );
  }
}
