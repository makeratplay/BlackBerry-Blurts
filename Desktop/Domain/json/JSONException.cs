using System;

namespace Blurts
{
  public class JSONException : System.Exception 
  {

    /**
     * Constructs a JSONException with an explanatory message.
     * @param message Detail about the reason for the exception.
     */
    public JSONException(String message)
    {
    }

    public JSONException(Exception e)
    {
    }
  }
}
