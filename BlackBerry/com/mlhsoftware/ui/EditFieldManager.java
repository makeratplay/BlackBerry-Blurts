//#preprocess
/*
 * EditFieldManager.java
 *
 * MLH Software
 * Copyright 2010
 */

//#ifdef BLURTS
package com.mlhsoftware.ui.blurts;
//#endif 

//#ifdef SIMPLYREMINDME
package com.mlhsoftware.ui.SimplyRemindMe;
//#endif 

//#ifdef SIMPLYTASKS
package com.mlhsoftware.ui.SimplyTasks;
//#endif 

//#ifdef MLHKEY
package com.mlhsoftware.ui.MLHKey;
//#endif 

import net.rim.device.api.system.*;
import net.rim.device.api.ui.*;
import net.rim.device.api.ui.component.*;

/**
 * 
 */
public class EditFieldManager extends ListStyleManager
{
  private AutoTextEditField _editField;
  private MyLabelField _labelField;
  private String _labelText;
  private FieldChangeListener _listener;

  public EditFieldManager( String initialValue, String labelText, int maxNumChars, long style  )
  {
    super( USE_ALL_WIDTH );

    _listener = null;
    _labelText = labelText;
    if ( initialValue.length() > 0 )
    {
      _labelField = new MyLabelField( "", 0 );
    }
    else
    {
      _labelField = new MyLabelField( labelText, 0 );
    }
    add( _labelField );


    _editField = new AutoTextEditField( null, initialValue, maxNumChars, style );
    _editField.setChangeListener( new FieldChangeListener()
    {
      public void fieldChanged( Field field, int context )
      {
        onFieldChanged( field, context );
      }
    } );


    add( _editField );
  }

  public void setChangeListener( FieldChangeListener listener )
  {
    _listener = listener;
  }

  public String toString()
  {
    return _editField.toString();
  }

  public void setText( String text )
  {
    _editField.setText( text );
  }

  protected void sublayout( int width, int height )
  {
    int finalVPadding = getVPadding();
    int topPos = getTopPos();

    layoutChild( _editField, width - ( HPADDING * 4 ), height - finalVPadding );
    setPositionChild( _editField, ( width - _editField.getWidth() ) / 2, topPos );

    layoutChild( _labelField, width - ( HPADDING * 4 ), height - finalVPadding );
    setPositionChild( _labelField, _editField.getLeft() + 5, topPos );

    setExtent( width, _editField.getHeight() + finalVPadding );
  }

  private void onFieldChanged( Field field, int context )
  {
    String currentText = _editField.getText();

    if ( currentText.length() == 0 )
    {
      _labelField.setText( _labelText );
    }
    else
    {
      _labelField.setText( "" );
    }

    if ( _listener != null )
    {
      _listener.fieldChanged( field, context );
    }

    fieldChangeNotify( 0 );
  }

  private static class MyLabelField extends LabelField
  {

    public MyLabelField( String text, long style )
    {
      super( text, style );
    }

    public void paint( Graphics g )
    {
      // change font to grey
      int currentColor = g.getColor();
      g.setColor( 0x00AAAAAA );
      super.paint( g );
      g.setColor( currentColor );
    }
  }
}
