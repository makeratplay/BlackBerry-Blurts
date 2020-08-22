using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Blurts
{
  [ComVisible(true)]
  public class JSONObject
  {

    /**
     * JSONObject.NULL is equivalent to the value that JavaScript calls null,
     * whilst Java's null is equivalent to the value that JavaScript calls
     * undefined.
     */
    public class Null
    {


      /**
       * A Null object is equal to the null value and to itself.
       * @param object    An object to test for nullness.
       * @return true if the object parameter is the JSONObject.NULL object
       *  or null.
       
      public static Boolean operator ==(object x, object y)
      {
        return x == null || y == null || x == y;
      }
      */

      /**
       * Get the "null" string value.
       * @return The string "null".
       */
      override public String ToString()
      {
        return "null";
      }
    }


    /**
     * The hash map where the JSONObject's properties are kept.
     */
    private Hashtable myHashMap;


    /**
     * It is sometimes more convenient and less ambiguous to have a
     * <code>NULL</code> object than to use Java's <code>null</code> value.
     * <code>JSONObject.NULL.equals(null)</code> returns <code>true</code>.
     * <code>JSONObject.NULL.ToString()</code> returns <code>"null"</code>.
     */
    public static Object NULL = new Null();


    /**
     * Construct an empty JSONObject.
     */
    public JSONObject()
    {
      this.myHashMap = new Hashtable();
    }


    /**
     * Construct a JSONObject from a subset of another JSONObject.
     * An array of strings is used to identify the keys that should be copied.
     * Missing keys are ignored.
     * @param jo A JSONObject.
     * @param sa An array of strings.
     * @exception JSONException If a value is a non-finite number.
     */
    public JSONObject(JSONObject jo, String[] sa)
    {
      this.myHashMap = new Hashtable();
      for (int i = 0; i < sa.Length; i += 1)
      {
        putOpt(sa[i], jo.opt(sa[i]));
      }
    }


    /**
     * Construct a JSONObject from a JSONTokener.
     * @param x A JSONTokener object containing the source string.
     * @throws JSONException If there is a syntax error in the source string.
     */
    public JSONObject(JSONTokener x)
    {
      this.myHashMap = new Hashtable();
      char c;
      String key;

      if (x.nextClean() != '{')
      {
        throw x.syntaxError("A JSONObject text must begin with '{'");
      }
      for (; ; )
      {
        c = x.nextClean();
        switch (c)
        {
          case (char)0:
            throw x.syntaxError("A JSONObject text must end with '}'");
          case '}':
            return;
          default:
            x.back();
            key = x.nextValue().ToString();
            break;
        }

        /*
         * The key is followed by ':'. We will also tolerate '=' or '=>'.
         */

        c = x.nextClean();
        if (c == '=')
        {
          if (x.next() != '>')
          {
            x.back();
          }
        }
        else if (c != ':')
        {
          throw x.syntaxError("Expected a ':' after a key");
        }
        put(key, x.nextValue());

        /*
         * Pairs are separated by ','. We will also tolerate ';'.
         */

        switch (x.nextClean())
        {
          case ';':
          case ',':
            if (x.nextClean() == '}')
            {
              return;
            }
            x.back();
            break;
          case '}':
            return;
          default:
            throw x.syntaxError("Expected a ',' or '}'");
        }
      }
    }


    /**
     * Construct a JSONObject from a Map.
     * @param map A map object that can be used to initialize the contents of
     *  the JSONObject.
     */
    public JSONObject(Hashtable map)
    {
      if (map == null)
      {
        this.myHashMap = new Hashtable();
      }
      else
      {
        this.myHashMap = new Hashtable(map.Count);
        IDictionaryEnumerator _enumerator = map.GetEnumerator();
        while (_enumerator.MoveNext())
        {
          this.myHashMap.Add(_enumerator.Key, _enumerator.Value);
        }
      }
    }


    /**
     * Construct a JSONObject from a string.
     * This is the most commonly used JSONObject constructor.
     * @param string    A string beginning
     *  with <code>{</code>&nbsp;<small>(left brace)</small> and ending
     *  with <code>}</code>&nbsp;<small>(right brace)</small>.
     * @exception JSONException If there is a syntax error in the source string.
     */
    public JSONObject(String s)
      : this(new JSONTokener(s))
    {

    }


    /**
     * Accumulate values under a key. It is similar to the put method except
     * that if there is already an object stored under the key then a
     * JSONArray is stored under the key to hold all of the accumulated values.
     * If there is already a JSONArray, then the new value is appended to it.
     * In contrast, the put method replaces the previous value.
     * @param key   A key string.
     * @param value An object to be accumulated under the key.
     * @return this.
     * @throws JSONException If the value is an invalid number
     *  or if the key is null.
     */
    public JSONObject accumulate(String key, Object value)
    {
      testValidity(value);
      Object o = opt(key);
      if (o == null)
      {
        put(key, value);
      }
      else if (o is JSONArray)
      {
        //MLH       ((JSONArray)o).put(value);
      }
      else
      {
        //MLH       put(key, new JSONArray().put(o).put(value));
      }
      return this;
    }


    /**
     * Append values to the array under a key. If the key does not exist in the
     * JSONObject, then the key is put in the JSONObject with its value being a
     * JSONArray containing the value parameter. If the key was already
     * associated with a JSONArray, then the value parameter is appended to it.
     * @param key   A key string.
     * @param value An object to be accumulated under the key.
     * @return this.
     * @throws JSONException If the key is null or if the current value 
     * 	associated with the key is not a JSONArray.
     */
    public JSONObject append(String key, Object value)
    {
      testValidity(value);
      Object o = opt(key);
      if (o == null)
      {
        //MLH       put(key, new JSONArray().put(value));
      }
      else if (o is JSONArray)
      {
        throw new JSONException("JSONObject[" + key +
            "] is not a JSONArray.");
      }
      else
      {
        //MLH         put(key, new JSONArray().put(o).put(value));
      }
      return this;
    }


    /**
     * Produce a string from a double. The string "null" will be returned if
     * the number is not finite.
     * @param  d A double.
     * @return A String.
     */
    static public String doubleToString(double d)
    {
      if (Double.IsInfinity(d) || Double.IsNaN(d))
      {
        return "null";
      }

      // Shave off trailing zeros and decimal point, if possible.

      String s = d.ToString();
      if (s.IndexOf('.') > 0 && s.IndexOf('e') < 0 && s.IndexOf('E') < 0)
      {
        while (s.EndsWith("0"))
        {
          s = s.Substring(0, s.Length - 1);
        }
        if (s.EndsWith("."))
        {
          s = s.Substring(0, s.Length - 1);
        }
      }
      return s;
    }


    /**
     * Get the value object associated with a key.
     *
     * @param key   A key string.
     * @return      The object associated with the key.
     * @throws   JSONException if the key is not found.
     */
    public Object get(String key)
    {
      Object o = opt(key);
      if (o == null)
      {
        throw new JSONException("JSONObject[" + quote(key) +
                "] not found.");
      }
      return o;
    }


    /**
     * Get the boolean value associated with a key.
     *
     * @param key   A key string.
     * @return      The truth.
     * @throws   JSONException
     *  if the value is not a Boolean or the String "true" or "false".
     */
    public Boolean getBoolean(String key)
    {
      Object o = get(key);
      if (o.Equals(Boolean.FalseString) ||
              (o is String &&
              ((String)o).Equals("false", StringComparison.CurrentCultureIgnoreCase)))
      {
        return false;
      }
      else if (o.Equals(Boolean.TrueString) ||
              (o is String &&
              ((String)o).Equals("true", StringComparison.CurrentCultureIgnoreCase)))
      {
        return true;
      }
      throw new JSONException("JSONObject[" + quote(key) +
              "] is not a Boolean.");
    }


    /**
     * Get the double value associated with a key.
     * @param key   A key string.
     * @return      The numeric value.
     * @throws JSONException if the key is not found or
     *  if the value is not a Number object and cannot be converted to a number.
     */
    public double getDouble(String key)
    {
      Object o = get(key);
      if (o is Byte)
      {
        return (double)((Byte)o);
      }
      else if (o is short)
      {
        return (double)((short)o);
      }
      else if (o is Int32)
      {
        return (double)((int)o);
      }
      else if (o is long)
      {
        return (double)((long)o);
      }
      else if (o is float)
      {
        return (double)((float)o);
      }
      else if (o is Double)
      {
        return ((Double)o);
      }
      else if (o is String)
      {
        try
        {
          return Double.Parse((String)o);
        }
        catch (Exception)
        {
          throw new JSONException("JSONObject[" + quote(key) + "] is not a number.");
        }
      }
      throw new JSONException("JSONObject[" + quote(key) + "] is not a number.");
    }


    /**
     * Get the int value associated with a key. If the number value is too
     * large for an int, it will be clipped.
     *
     * @param key   A key string.
     * @return      The integer value.
     * @throws   JSONException if the key is not found or if the value cannot
     *  be converted to an integer.
     */
    public int getInt(String key)
    {
      Object o = get(key);
      if (o is Byte)
      {
        return ((Byte)o);
      }
      else if (o is short)
      {
        return ((short)o);
      }
      else if (o is int)
      {
        return ((int)o);
      }
      else if (o is long)
      {
        return (int)((long)o);
      }
      else if (o is float)
      {
        return (int)((float)o);
      }
      else if (o is Double)
      {
        return (int)((Double)o);
      }
      else if (o is String)
      {
        return (int)getDouble(key);
      }
      throw new JSONException("JSONObject[" + quote(key) + "] is not a number.");
    }


    /**
     * Get the JSONArray value associated with a key.
     *
     * @param key   A key string.
     * @return      A JSONArray which is the value.
     * @throws   JSONException if the key is not found or
     *  if the value is not a JSONArray.
     */
    public JSONArray getJSONArray(String key)
    {
      Object o = get(key);
      if (o is JSONArray)
      {
        return (JSONArray)o;
      }
      throw new JSONException("JSONObject[" + quote(key) + "] is not a JSONArray.");
    }


    /**
     * Get the JSONObject value associated with a key.
     *
     * @param key   A key string.
     * @return      A JSONObject which is the value.
     * @throws   JSONException if the key is not found or
     *  if the value is not a JSONObject.
     */
    public JSONObject getJSONObject(String key)
    {
      Object o = get(key);
      if (o is JSONObject)
      {
        return (JSONObject)o;
      }
      throw new JSONException("JSONObject[" + quote(key) + "] is not a JSONObject.");
    }


    /**
     * Get the long value associated with a key. If the number value is too
     * long for a long, it will be clipped.
     *
     * @param key   A key string.
     * @return      The long value.
     * @throws   JSONException if the key is not found or if the value cannot
     *  be converted to a long.
     */
    public long getLong(String key)
    {
      Object o = get(key);
      if (o is Byte)
      {
        return ((Byte)o);
      }
      else if (o is short)
      {
        return ((short)o);
      }
      else if (o is int)
      {
        return ((int)o);
      }
      else if (o is long)
      {
        return ((long)o);
      }
      else if (o is float)
      {
        return (long)((float)o);
      }
      else if (o is Double)
      {
        return (long)((Double)o);
      }
      else if (o is String)
      {
        return (long)getDouble(key);
      }
      throw new JSONException("JSONObject[" + quote(key) + "] is not a number.");
    }


    /**
     * Get the string associated with a key.
     *
     * @param key   A key string.
     * @return      A string which is the value.
     * @throws   JSONException if the key is not found.
     */
    public String getString(String key)
    {
      return get(key).ToString();
    }


    /**
     * Determine if the JSONObject contains a specific key.
     * @param key   A key string.
     * @return      true if the key exists in the JSONObject.
     */
    public Boolean has(String key)
    {
      return this.myHashMap.ContainsKey(key);
    }


    /**
     * Determine if the value associated with the key is null or if there is
     *  no value.
     * @param key   A key string.
     * @return      true if there is no value associated with the key or if
     *  the value is the JSONObject.NULL object.
     */
    public Boolean isNull(String key)
    {
      return JSONObject.NULL.Equals(opt(key));
    }


    /**
     * Get an enumeration of the keys of the JSONObject.
     *
     * @return An iterator of the keys.
    */
    public IEnumerator getKeys()
    {
      return this.myHashMap.Keys.GetEnumerator();
    }


    /**
     * Get the number of keys stored in the JSONObject.
     *
     * @return The number of keys in the JSONObject.
     */
    public int length()
    {
      return this.myHashMap.Count;
    }


    /**
     * Produce a JSONArray containing the names of the elements of this
     * JSONObject.
     * @return A JSONArray containing the key strings, or null if the JSONObject
     * is empty.
     */
    public JSONArray names()
    {
      JSONArray ja = new JSONArray();
      IEnumerator keys = getKeys();
      while (keys.MoveNext())
      {
//MLH        ja.put(keys.Current());
      }
      return ja.Length() == 0 ? null : ja;
    }


    /**
     * Shave off trailing zeros and decimal point, if possible.
     */
    static public String trimNumber(String s)
    {
      if (s.IndexOf('.') > 0 && s.IndexOf('e') < 0 && s.IndexOf('E') < 0)
      {
        while (s.EndsWith("0"))
        {
          s = s.Substring(0, s.Length - 1);
        }
        if (s.EndsWith("."))
        {
          s = s.Substring(0, s.Length - 1);
        }
      }
      return s;
    }

    /**
     * Produce a string from a Number.
     * @param  n A Number
     * @return A String.
     * @throws JSONException If n is a non-finite number.
     */
    static public String numberToString(Object n)
    {
      if (n == null)
      {
        throw new JSONException("Null pointer");
      }
      testValidity(n);
      return trimNumber(n.ToString());
    }

    /**
     * Get an optional value associated with a key.
     * @param key   A key string.
     * @return      An object which is the value, or null if there is no value.
     */
    public Object opt(String key)
    {
      return key == null ? null : this.myHashMap[key];
    }


    /**
     * Get an optional boolean associated with a key.
     * It returns false if there is no such key, or if the value is not
     * Boolean.TRUE or the String "true".
     *
     * @param key   A key string.
     * @return      The truth.
     */
    public Boolean optBoolean(String key)
    {
      return optBoolean(key, false);
    }


    /**
     * Get an optional boolean associated with a key.
     * It returns the defaultValue if there is no such key, or if it is not
     * a Boolean or the String "true" or "false" (case insensitive).
     *
     * @param key              A key string.
     * @param defaultValue     The default.
     * @return      The truth.
     */
    public Boolean optBoolean(String key, Boolean defaultValue)
    {
      try
      {
        return getBoolean(key);
      }
      catch (Exception)
      {
        return defaultValue;
      }
    }


    /**
     * Put a key/value pair in the JSONObject, where the value will be a
     * JSONArray which is produced from a Collection.
     * @param key 	A key string.
     * @param value	A Collection value.
     * @return		this.
     * @throws JSONException
     
    public JSONObject put(String key, Vector value)
    {
      put(key, new JSONArray(value));
      return this;
    }
    */

    /**
     * Get an optional double associated with a key,
     * or NaN if there is no such key or if its value is not a number.
     * If the value is a string, an attempt will be made to evaluate it as
     * a number.
     *
     * @param key   A string which is the key.
     * @return      An object which is the value.
     */
    public double optDouble(String key)
    {
      return optDouble(key, Double.NaN);
    }


    /**
     * Get an optional double associated with a key, or the
     * defaultValue if there is no such key or if its value is not a number.
     * If the value is a string, an attempt will be made to evaluate it as
     * a number.
     *
     * @param key   A key string.
     * @param defaultValue     The default.
     * @return      An object which is the value.
     */
    public double optDouble(String key, double defaultValue)
    {
      try
      {
        Object o = opt(key);
        return Double.Parse((String)o);
      }
      catch (Exception)
      {
        return defaultValue;
      }
    }


    /**
     * Get an optional int value associated with a key,
     * or zero if there is no such key or if the value is not a number.
     * If the value is a string, an attempt will be made to evaluate it as
     * a number.
     *
     * @param key   A key string.
     * @return      An object which is the value.
     */
    public int optInt(String key)
    {
      return optInt(key, 0);
    }


    /**
     * Get an optional int value associated with a key,
     * or the default if there is no such key or if the value is not a number.
     * If the value is a string, an attempt will be made to evaluate it as
     * a number.
     *
     * @param key   A key string.
     * @param defaultValue     The default.
     * @return      An object which is the value.
     */
    public int optInt(String key, int defaultValue)
    {
      try
      {
        return getInt(key);
      }
      catch (Exception)
      {
        return defaultValue;
      }
    }


    /**
     * Get an optional JSONArray associated with a key.
     * It returns null if there is no such key, or if its value is not a
     * JSONArray.
     *
     * @param key   A key string.
     * @return      A JSONArray which is the value.
     */
    public JSONArray optJSONArray(String key)
    {
      Object o = opt(key);
      return o is JSONArray ? (JSONArray)o : null;
    }


    /**
     * Get an optional JSONObject associated with a key.
     * It returns null if there is no such key, or if its value is not a
     * JSONObject.
     *
     * @param key   A key string.
     * @return      A JSONObject which is the value.
     */
    public JSONObject optJSONObject(String key)
    {
      Object o = opt(key);
      return o is JSONObject ? (JSONObject)o : null;
    }


    /**
     * Get an optional long value associated with a key,
     * or zero if there is no such key or if the value is not a number.
     * If the value is a string, an attempt will be made to evaluate it as
     * a number.
     *
     * @param key   A key string.
     * @return      An object which is the value.
     */
    public long optLong(String key)
    {
      return optLong(key, 0);
    }


    /**
     * Get an optional long value associated with a key,
     * or the default if there is no such key or if the value is not a number.
     * If the value is a string, an attempt will be made to evaluate it as
     * a number.
     *
     * @param key   A key string.
     * @param defaultValue     The default.
     * @return      An object which is the value.
     */
    public long optLong(String key, long defaultValue)
    {
      try
      {
        return getLong(key);
      }
      catch (Exception)
      {
        return defaultValue;
      }
    }


    /**
     * Get an optional string associated with a key.
     * It returns an empty string if there is no such key. If the value is not
     * a string and is not null, then it is coverted to a string.
     *
     * @param key   A key string.
     * @return      A string which is the value.
     */
    public String optString(String key)
    {
      return optString(key, "");
    }


    /**
     * Get an optional string associated with a key.
     * It returns the defaultValue if there is no such key.
     *
     * @param key   A key string.
     * @param defaultValue     The default.
     * @return      A string which is the value.
     */
    public String optString(String key, String defaultValue)
    {
      Object o = opt(key);
      return o != null ? o.ToString() : defaultValue;
    }


    /**
     * Put a key/boolean pair in the JSONObject.
     *
     * @param key   A key string.
     * @param value A boolean which is the value.
     * @return this.
     * @throws JSONException If the key is null.
     */
    public JSONObject put(String key, Boolean value)
    {
      put(key, (Object)value);
      return this;
    }


    /**
     * Put a key/double pair in the JSONObject.
     *
     * @param key   A key string.
     * @param value A double which is the value.
     * @return this.
     * @throws JSONException If the key is null or if the number is invalid.
     */
    public JSONObject put(String key, double value)
    {
      put(key, (Object)value);
      return this;
    }


    /**
     * Put a key/int pair in the JSONObject.
     *
     * @param key   A key string.
     * @param value An int which is the value.
     * @return this.
     * @throws JSONException If the key is null.
     */
    public JSONObject put(String key, int value)
    {
      put(key, (Object)value);
      return this;
    }


    /**
     * Put a key/long pair in the JSONObject.
     *
     * @param key   A key string.
     * @param value A long which is the value.
     * @return this.
     * @throws JSONException If the key is null.
     */
    public JSONObject put(String key, long value)
    {
      put(key, (Object)value);
      return this;
    }


    /**
     * Put a key/value pair in the JSONObject, where the value will be a
     * JSONObject which is produced from a Map.
     * @param key 	A key string.
     * @param value	A Map value.
     * @return		this.
     * @throws JSONException
     */
    public JSONObject put(String key, Hashtable value)
    {
      put(key, new JSONObject(value));
      return this;
    }


    /**
     * Put a key/value pair in the JSONObject. If the value is null,
     * then the key will be removed from the JSONObject if it is present.
     * @param key   A key string.
     * @param value An object which is the value. It should be of one of these
     *  types: Boolean, Double, Integer, JSONArray, JSONObject, Long, String,
     *  or the JSONObject.NULL object.
     * @return this.
     * @throws JSONException If the value is non-finite number
     *  or if the key is null.
     */
    public JSONObject put(String key, Object value)
    {
      if (key == null)
      {
        throw new JSONException("Null key.");
      }
      if (value != null)
      {
        testValidity(value);
        this.myHashMap.Add(key, value);
      }
      else
      {
        remove(key);
      }
      return this;
    }


    /**
     * Put a key/value pair in the JSONObject, but only if the
     * key and the value are both non-null.
     * @param key   A key string.
     * @param value An object which is the value. It should be of one of these
     *  types: Boolean, Double, Integer, JSONArray, JSONObject, Long, String,
     *  or the JSONObject.NULL object.
     * @return this.
     * @throws JSONException If the value is a non-finite number.
     */
    public JSONObject putOpt(String key, Object value)
    {
      if (key != null && value != null)
      {
        put(key, value);
      }
      return this;
    }


    /**
     * Produce a string in double quotes with backslash sequences in all the
     * right places. A backslash will be inserted within </, allowing JSON
     * text to be delivered in HTML. In JSON text, a string cannot contain a
     * control character or an unescaped quote or backslash.
     * @param string A String
     * @return  A String correctly formatted for insertion in a JSON text.
     */
    public static String quote(String s)
    {
      if (s == null || s.Length == 0)
      {
        return "\"\"";
      }

      char b;
      char c = (char)0;
      int i;
      int len = s.Length;
      String sb = "";
//MLH      String t = "";

      sb += '"';
      for (i = 0; i < len; i += 1)
      {
        b = c;
        c = s[i];
        switch (c)
        {
          case '\\':
          case '"':
            sb += '\\';
            sb += c;
            break;
          case '/':
            if (b == '<')
            {
              sb += '\\';
            }
            sb += c;
            break;
          case '\b':
            sb += "\\b";
            break;
          case '\t':
            sb += "\\t";
            break;
          case '\n':
            sb += "\\n";
            break;
          case '\f':
            sb += "\\f";
            break;
          case '\r':
            sb += "\\r";
            break;
          default:
            if (c < ' ')
            {
//MLH              t = "000" + Integer.toHexString(c);
//MLH               sb += "\\u" + t.Substring(t.Length - 4);
            }
            else
            {
              sb += c;
            }
            break;
        }
      }
      sb += '"';
      return sb;
    }

    /**
     * Remove a name and its value, if present.
     * @param key The name to be removed.
     * @return The value that was associated with the name,
     * or null if there was no value.
     */
    public Object remove(String key)
    {
      Object retVal = this.myHashMap[key];
      this.myHashMap.Remove(key);
      return retVal;
    }


    /**
     * Throw an exception if the object is an NaN or infinite number.
     * @param o The object to test.
     * @throws JSONException If o is a non-finite number.
     */
    public static void testValidity(Object o)
    {
      if (o != null)
      {
        if (o is Double)
        {
          if (Double.IsInfinity((Double)o) || Double.IsNaN((Double)o))
          {
            throw new JSONException("JSON does not allow non-finite numbers");
          }
        }
        else if (o is float)
        {
          if (float.IsInfinity((float)o) || float.IsNaN((float)o))
          {
            throw new JSONException("JSON does not allow non-finite numbers.");
          }
        }
      }
    }


    /**
     * Produce a JSONArray containing the values of the members of this
     * JSONObject.
     * @param names A JSONArray containing a list of key strings. This
     * determines the sequence of the values in the result.
     * @return A JSONArray of values.
     * @throws JSONException If any of the values are non-finite numbers.
     */
    public JSONArray toJSONArray(JSONArray names)
    {
      if (names == null || names.Length() == 0)
      {
        return null;
      }
      JSONArray ja = new JSONArray();
      for (int i = 0; i < names.Length(); i += 1)
      {
//MLH        ja.put(this.opt(names.getString(i)));
      }
      return ja;
    }

    /**
     * Make a JSON text of this JSONObject. For compactness, no whitespace
     * is added. If this would not result in a syntactically correct JSON text,
     * then null will be returned instead.
     * <p>
     * Warning: This method assumes that the data structure is acyclical.
     *
     * @return a printable, displayable, portable, transmittable
     *  representation of the object, beginning
     *  with <code>{</code>&nbsp;<small>(left brace)</small> and ending
     *  with <code>}</code>&nbsp;<small>(right brace)</small>.
     */
    override public String ToString()
    {
      try
      {
        IEnumerator keys = getKeys();
        String sb = "{";

        while (keys.MoveNext())
        {
          if (sb.Length > 1)
          {
            sb += ',';
          }
          Object o = keys.Current;
          sb += quote(o.ToString());
          sb += ':';
          sb += valueToString(this.myHashMap[o]);
        }
        sb += '}';
        return sb;
      }
      catch (Exception)
      {
        return null;
      }
    }


    /**
     * Make a prettyprinted JSON text of this JSONObject.
     * <p>
     * Warning: This method assumes that the data structure is acyclical.
     * @param indentFactor The number of spaces to add to each level of
     *  indentation.
     * @return a printable, displayable, portable, transmittable
     *  representation of the object, beginning
     *  with <code>{</code>&nbsp;<small>(left brace)</small> and ending
     *  with <code>}</code>&nbsp;<small>(right brace)</small>.
     * @throws JSONException If the object contains an invalid number.
     */
    public String ToString(int indentFactor)
    {
      return ToString(indentFactor, 0);
    }


    /**
     * Make a prettyprinted JSON text of this JSONObject.
     * <p>
     * Warning: This method assumes that the data structure is acyclical.
     * @param indentFactor The number of spaces to add to each level of
     *  indentation.
     * @param indent The indentation of the top level.
     * @return a printable, displayable, transmittable
     *  representation of the object, beginning
     *  with <code>{</code>&nbsp;<small>(left brace)</small> and ending
     *  with <code>}</code>&nbsp;<small>(right brace)</small>.
     * @throws JSONException If the object contains an invalid number.
     */
    String ToString(int indentFactor, int indent)
    {
      int i;
      int n = length();
      if (n == 0)
      {
        return "{}";
      }
      IEnumerator keys = getKeys();
      String sb = "{";
      int newindent = indent + indentFactor;
      Object o;
      if (n == 1)
      {
        keys.MoveNext();
        o = keys.Current;
        sb += quote(o.ToString());
        sb += ": ";
        sb += valueToString(this.myHashMap[o], indentFactor, indent);
      }
      else
      {
        while (keys.MoveNext())
        {
          o = keys.Current;
          if (sb.Length > 1)
          {
            sb += ",\n";
          }
          else
          {
            sb += '\n';
          }
          for (i = 0; i < newindent; i += 1)
          {
            sb += ' ';
          }
          sb += quote(o.ToString());
          sb += ": ";
          sb += valueToString(this.myHashMap[o], indentFactor, newindent);
        }
        if (sb.Length > 1)
        {
          sb += '\n';
          for (i = 0; i < indent; i += 1)
          {
            sb += ' ';
          }
        }
      }
      sb += '}';
      return sb;
    }


    /**
     * Make a JSON text of an Object value. If the object has an
     * value.toJSONString() method, then that method will be used to produce
     * the JSON text. The method is required to produce a strictly
     * conforming text. If the object does not contain a toJSONString
     * method (which is the most common case), then a text will be
     * produced by the rules.
     * <p>
     * Warning: This method assumes that the data structure is acyclical.
     * @param value The value to be serialized.
     * @return a printable, displayable, transmittable
     *  representation of the object, beginning
     *  with <code>{</code>&nbsp;<small>(left brace)</small> and ending
     *  with <code>}</code>&nbsp;<small>(right brace)</small>.
     * @throws JSONException If the value is or contains an invalid number.
     */
    public static String valueToString(Object value)
    {
      if (value == null || value.Equals(null))
      {
        return "null";
      }
      if (value is JSONString)
      {
        Object o;
        try
        {
          o = ((JSONString)value).toJSONString();
        }
        catch (Exception e)
        {
          throw new JSONException(e);
        }
        if (o is String)
        {
          return (String)o;
        }
        throw new JSONException("Bad value from toJSONString: " + o);
      }
      if (value is float || value is Double ||
          value is Byte || value is short ||
          value is int || value is long)
      {
        return numberToString(value);
      }
      if (value is Boolean || value is JSONObject ||
              value is JSONArray)
      {
        return value.ToString();
      }
      return quote(value.ToString());
    }


    /**
     * Make a prettyprinted JSON text of an object value.
     * <p>
     * Warning: This method assumes that the data structure is acyclical.
     * @param value The value to be serialized.
     * @param indentFactor The number of spaces to add to each level of
     *  indentation.
     * @param indent The indentation of the top level.
     * @return a printable, displayable, transmittable
     *  representation of the object, beginning
     *  with <code>{</code>&nbsp;<small>(left brace)</small> and ending
     *  with <code>}</code>&nbsp;<small>(right brace)</small>.
     * @throws JSONException If the object contains an invalid number.
     */
    public static String valueToString(Object value, int indentFactor, int indent)
    {
      if (value == null || value.Equals(null))
      {
        return "null";
      }
      try
      {
        if (value is JSONString)
        {
          Object o = ((JSONString)value).toJSONString();
          if (o is String)
          {
            return (String)o;
          }
        }
      }
      catch (Exception)
      {
        /* forget about it */
      }
      if (value is float || value is Double ||
          value is Byte || value is short ||
          value is int || value is long)
      {
        return numberToString(value);
      }
      if (value is Boolean)
      {
        return value.ToString();
      }
      if (value is JSONObject)
      {
        return ((JSONObject)value).ToString(indentFactor, indent);
      }
      if (value is JSONArray)
      {
        return ((JSONArray)value).ToString(indentFactor, indent);
      }
      return quote(value.ToString());
    }


    /**
     * Write the contents of the JSONObject as JSON text to a writer.
     * For compactness, no whitespace is added.
     * <p>
     * Warning: This method assumes that the data structure is acyclical.
     *
     * @return The writer.
     * @throws JSONException
     
    public Writer write(Writer writer)
    {
      try
      {
        Boolean b = false;
        Enumeration keys = keys();
        writer.write('{');

        while (keys.hasMoreElements())
        {
          if (b)
          {
            writer.write(',');
          }
          Object k = keys.nextElement();
          writer.write(quote(k.ToString()));
          writer.write(':');
          Object v = this.myHashMap.get(k);
          if (v is JSONObject)
          {
            ((JSONObject)v).write(writer);
          }
          else if (v is JSONArray)
          {
            ((JSONArray)v).write(writer);
          }
          else
          {
            writer.write(valueToString(v));
          }
          b = true;
        }
        writer.write('}');
        return writer;
      }
      catch (IOException e)
      {
        throw new JSONException(e);
      }
    }*/
  }
}