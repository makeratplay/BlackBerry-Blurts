using System;

namespace Blurts
{
  public interface JSONString
  {
    /**
     * The <code>toJSONString</code> method allows a class to produce its own JSON 
     * serialization. 
     * 
     * @return A strictly syntactically correct JSON text.
     */
    String toJSONString();
  }
}
