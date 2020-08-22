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
public class ChoiceFieldManager extends ListStyleManager
{
  private ObjectChoiceField _objectChoiceField;
  private FieldChangeListener _listener;

  public ChoiceFieldManager( String labelText, Object[] appChoices, int initialValue  )
  {
    super( USE_ALL_WIDTH );

    _listener = null;

    _objectChoiceField = new ObjectChoiceField( labelText, appChoices, initialValue );
    add( _objectChoiceField );
  }

  public void setChangeListener( FieldChangeListener listener )
  {
    _listener = listener;
  }

  public String toString()
  {
    return _objectChoiceField.toString();
  }

  public int getSelectedIndex()
  {
    return _objectChoiceField.getSelectedIndex();
  }

  protected void sublayout( int width, int height )
  {
    int finalVPadding = getVPadding();
    int topPos = getTopPos();

    layoutChild( _objectChoiceField, width - ( HPADDING * 4 ), height - finalVPadding );
    setPositionChild( _objectChoiceField, ( width - _objectChoiceField.getWidth() ) / 2, topPos );

    setExtent( width, _objectChoiceField.getHeight() + finalVPadding );
  }

  private void onFieldChanged( Field field, int context )
  {
    if ( _listener != null )
    {
      _listener.fieldChanged( field, context );
    }

    fieldChangeNotify( 0 );
  }
}
