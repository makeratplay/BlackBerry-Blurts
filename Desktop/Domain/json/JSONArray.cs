using System;
using System.Collections;
using System.Collections.Generic;

namespace Blurts
{
  public class JSONArray
  {


    /**
     * The Vector where the JSONArray's properties are kept.
     */
    private List<object> myArrayList;


    /**
     * Construct an empty JSONArray.
     */
    public JSONArray()
    {
      this.myArrayList = new List<object>();
    }

    /**
     * Construct a JSONArray from a JSONTokener.
     * @param x A JSONTokener
     * @throws JSONException If there is a syntax error.
     */
    public JSONArray(JSONTokener x)
      : this()
    {

      if (x.nextClean() != '[')
      {
        throw x.syntaxError("A JSONArray text must start with '['");
      }
      if (x.nextClean() == ']')
      {
        return;
      }
      x.back();
      for (; ; )
      {
        if (x.nextClean() == ',')
        {
          x.back();
          this.myArrayList.Add(null);
        }
        else
        {
          x.back();
          this.myArrayList.Add(x.nextValue());
        }
        switch (x.nextClean())
        {
          case ';':
          case ',':
            if (x.nextClean() == ']')
            {
              return;
            }
            x.back();
            break;
          case ']':
            return;
          default:
            throw x.syntaxError("Expected a ',' or ']'");
        }
      }
    }


    /**
     * Construct a JSONArray from a source sJSON text.
     * @param string     A string that begins with
     * <code>[</code>&nbsp;<small>(left bracket)</small>
     *  and ends with <code>]</code>&nbsp;<small>(right bracket)</small>.
     *  @throws JSONException If there is a syntax error.
     */
    public JSONArray(String s)
      : this(new JSONTokener(s))
    {
    }


    /**
     * Construct a JSONArray from a Collection.
     * @param collection     A Collection.
     */
    public JSONArray(List<object> collection)
    {
      if (collection == null)
      {
        this.myArrayList = new List<object>();
      }
      else
      {
        int size = collection.Count;
        this.myArrayList = new List<object>(size);
        for (int i = 0; i < size; i++)
        {
          this.myArrayList.Add(collection[i]);
        }
      }
    }

    /**
     * Get the number of elements in the JSONArray, included nulls.
     *
     * @return The length (or size).
     */
    public int Length()
    {
      return this.myArrayList.Count;
    }

    /**
       * Get the object value associated with an index.
       * @param index
       *  The index must be between 0 and length() - 1.
       * @return An object value.
       * @throws JSONException If there is no value for the index.
       */
    public Object get(int index)
    {
      Object o = opt(index);
      if (o == null)
      {
        throw new JSONException("JSONArray[" + index + "] not found.");
      }
      return o;
    }


    /**
     * Get the Boolean value associated with an index.
     * The string values "true" and "false" are converted to Boolean.
     *
     * @param index The index must be between 0 and length() - 1.
     * @return      The truth.
     * @throws JSONException If there is no value for the index or if the
     *  value is not convertable to Boolean.
     */
    public Boolean getBoolean(int index)
    {
      Object o = get(index);
      if (o.Equals(false) ||
              (o is String &&
              ((String)o).Equals( "false", StringComparison.CurrentCultureIgnoreCase)))
      {
        return false;
      }
      else if (o.Equals(true) ||
              (o is String &&
              ((String)o).Equals("true", StringComparison.CurrentCultureIgnoreCase)))
      {
        return true;
      }
      throw new JSONException("JSONArray[" + index + "] is not a Boolean.");
    }


    /**
     * Get the double value associated with an index.
     *
     * @param index The index must be between 0 and length() - 1.
     * @return      The value.
     * @throws   JSONException If the key is not found or if the value cannot
     *  be converted to a number.
     */
    public double getDouble(int index)
    {
      Object o = get(index);
      try
      {
        return Double.Parse(o.ToString());
      }
      catch (Exception)
      {
        throw new JSONException("JSONArray[" + index + "] is not a number.");
      }
    }


    /**
     * Get the int value associated with an index.
     *
     * @param index The index must be between 0 and length() - 1.
     * @return      The value.
     * @throws   JSONException If the key is not found or if the value cannot
     *  be converted to a number.
     *  if the value cannot be converted to a number.
     */
    public int getInt(int index)
    {
      Object o = get(index);
      return (int)getDouble(index);
    }


    /**
     * Get the JSONArray associated with an index.
     * @param index The index must be between 0 and length() - 1.
     * @return      A JSONArray value.
     * @throws JSONException If there is no value for the index. or if the
     * value is not a JSONArray
     */
    public JSONArray getJSONArray(int index)
    {
      Object o = get(index);
      if (o is JSONArray)
      {
        return (JSONArray)o;
      }
      throw new JSONException("JSONArray[" + index + "] is not a JSONArray.");
    }


    /**
     * Get the JSONObject associated with an index.
     * @param index subscript
     * @return      A JSONObject value.
     * @throws JSONException If there is no value for the index or if the
     * value is not a JSONObject
     */
    public JSONObject getJSONObject(int index)
    {
      Object o = get(index);
      if (o is JSONObject)
      {
        return (JSONObject)o;
      }
      throw new JSONException("JSONArray[" + index + "] is not a JSONObject.");
    }


    /**
     * Get the long value associated with an index.
     *
     * @param index The index must be between 0 and length() - 1.
     * @return      The value.
     * @throws   JSONException If the key is not found or if the value cannot
     *  be converted to a number.
     */
    public long getLong(int index)
    {
      Object o = get(index);
      return (long)getDouble(index);
    }


    /**
     * Get the string associated with an index.
     * @param index The index must be between 0 and length() - 1.
     * @return      A string value.
     * @throws JSONException If there is no value for the index.
     */
    public String getString(int index)
    {
      return get(index).ToString();
    }


    /**
     * Determine if the value is null.
     * @param index The index must be between 0 and length() - 1.
     * @return true if the value at the index is null, or if there is no value.
     */
    public Boolean isNull(int index)
    {
      return JSONObject.NULL.Equals(opt(index));
    }


    /**
     * Make a string from the contents of this JSONArray. The
     * <code>separator</code> string is inserted between each element.
     * Warning: This method assumes that the data structure is acyclical.
     * @param separator A string that will be inserted between the elements.
     * @return a string.
     * @throws JSONException If the array contains an invalid number.
     */
    public String join(String separator)
    {
      int len = Length();
      String sb = "";

      for (int i = 0; i < len; i += 1)
      {
        if (i > 0)
        {
          sb += separator;
        }
        sb += JSONObject.valueToString(this.myArrayList[i]);
      }
      return sb;
    }


    /**
     * Get the optional object value associated with an index.
     * @param index The index must be between 0 and length() - 1.
     * @return      An object value, or null if there is no
     *              object at that index.
     */
    public Object opt(int index)
    {
      return (index < 0 || index >= Length()) ?
          null : this.myArrayList[index];
    }


    /**
     * Get the optional Boolean value associated with an index.
     * It returns false if there is no value at that index,
     * or if the value is not Boolean.TRUE or the String "true".
     *
     * @param index The index must be between 0 and length() - 1.
     * @return      The truth.
     */
    public Boolean optBoolean(int index)
    {
      return optBoolean(index, false);
    }


    /**
     * Get the optional Boolean value associated with an index.
     * It returns the defaultValue if there is no value at that index or if
     * it is not a Boolean or the String "true" or "false" (case insensitive).
     *
     * @param index The index must be between 0 and length() - 1.
     * @param defaultValue     A Boolean default.
     * @return      The truth.
     */
    public Boolean optBoolean(int index, Boolean defaultValue)
    {
      try
      {
        return getBoolean(index);
      }
      catch (Exception)
      {
        return defaultValue;
      }
    }


    /**
     * Get the optional double value associated with an index.
     * NaN is returned if there is no value for the index,
     * or if the value is not a number and cannot be converted to a number.
     *
     * @param index The index must be between 0 and length() - 1.
     * @return      The value.
     */
    public double optDouble(int index)
    {
      return optDouble(index, Double.NaN);
    }


    /**
     * Get the optional double value associated with an index.
     * The defaultValue is returned if there is no value for the index,
     * or if the value is not a number and cannot be converted to a number.
     *
     * @param index subscript
     * @param defaultValue     The default value.
     * @return      The value.
     */
    public double optDouble(int index, double defaultValue)
    {
      try
      {
        return getDouble(index);
      }
      catch (Exception)
      {
        return defaultValue;
      }
    }


    /**
     * Get the optional int value associated with an index.
     * Zero is returned if there is no value for the index,
     * or if the value is not a number and cannot be converted to a number.
     *
     * @param index The index must be between 0 and length() - 1.
     * @return      The value.
     */
    public int optInt(int index)
    {
      return optInt(index, 0);
    }


    /**
     * Get the optional int value associated with an index.
     * The defaultValue is returned if there is no value for the index,
     * or if the value is not a number and cannot be converted to a number.
     * @param index The index must be between 0 and length() - 1.
     * @param defaultValue     The default value.
     * @return      The value.
     */
    public int optInt(int index, int defaultValue)
    {
      try
      {
        return getInt(index);
      }
      catch (Exception)
      {
        return defaultValue;
      }
    }


    /**
     * Get the optional JSONArray associated with an index.
     * @param index subscript
     * @return      A JSONArray value, or null if the index has no value,
     * or if the value is not a JSONArray.
     */
    public JSONArray optJSONArray(int index)
    {
      Object o = opt(index);
      return o is JSONArray ? (JSONArray)o : null;
    }


    /**
     * Get the optional JSONObject associated with an index.
     * Null is returned if the key is not found, or null if the index has
     * no value, or if the value is not a JSONObject.
     *
     * @param index The index must be between 0 and length() - 1.
     * @return      A JSONObject value.
     */
    public JSONObject optJSONObject(int index)
    {
      Object o = opt(index);
      return o is JSONObject ? (JSONObject)o : null;
    }


    /**
     * Get the optional long value associated with an index.
     * Zero is returned if there is no value for the index,
     * or if the value is not a number and cannot be converted to a number.
     *
     * @param index The index must be between 0 and length() - 1.
     * @return      The value.
     */
    public long optLong(int index)
    {
      return optLong(index, 0);
    }


    /**
     * Get the optional long value associated with an index.
     * The defaultValue is returned if there is no value for the index,
     * or if the value is not a number and cannot be converted to a number.
     * @param index The index must be between 0 and length() - 1.
     * @param defaultValue     The default value.
     * @return      The value.
     */
    public long optLong(int index, long defaultValue)
    {
      try
      {
        return getLong(index);
      }
      catch (Exception)
      {
        return defaultValue;
      }
    }


    /**
     * Get the optional string value associated with an index. It returns an
     * empty string if there is no value at that index. If the value
     * is not a string and is not null, then it is coverted to a string.
     *
     * @param index The index must be between 0 and length() - 1.
     * @return      A String value.
     */
    public String optString(int index)
    {
      return optString(index, "");
    }


    /**
     * Get the optional string associated with an index.
     * The defaultValue is returned if the key is not found.
     *
     * @param index The index must be between 0 and length() - 1.
     * @param defaultValue     The default value.
     * @return      A String value.
     */
    public String optString(int index, String defaultValue)
    {
      Object o = opt(index);
      return o != null ? o.ToString() : defaultValue;
    }


    /**
     * Append a Boolean value. This increases the array's length by one.
     *
     * @param value A Boolean value.
     * @return this.
     */
    public JSONArray put(Boolean value)
    {
      put(value ? true : false);
      return this;
    }


    /**
     * Put a value in the JSONArray, where the value will be a
     * JSONArray which is produced from a Collection.
     * @param value A Collection value.
     * @return      this.
     */
    public JSONArray put(List<object> value)
    {
      put(new JSONArray(value));
      return this;
    }


    /**
     * Append a double value. This increases the array's length by one.
     *
     * @param value A double value.
     * @throws JSONException if the value is not finite.
     * @return this.
     */
    public JSONArray put(double value)
    {
      Double d = value;
      JSONObject.testValidity(d);
      put(d);
      return this;
    }


    /**
     * Append an int value. This increases the array's length by one.
     *
     * @param value An int value.
     * @return this.
     */
    public JSONArray put(int value)
    {
      put(value);
      return this;
    }


    /**
     * Append an long value. This increases the array's length by one.
     *
     * @param value A long value.
     * @return this.
     */
    public JSONArray put(long value)
    {
      put(value);
      return this;
    }


    /**
     * Put a value in the JSONArray, where the value will be a
     * JSONObject which is produced from a Map.
     * @param value A Map value.
     * @return      this.
     */
    public JSONArray put(Hashtable value)
    {
      put(new JSONObject(value));
      return this;
    }


    /**
     * Append an object value. This increases the array's length by one.
     * @param value An object value.  The value should be a
     *  Boolean, Double, Integer, JSONArray, JSONObject, Long, or String, or the
     *  JSONObject.NULL object.
     * @return this.
     */
    public JSONArray put(Object value)
    {
      this.myArrayList.Add(value);
      return this;
    }


    /**
     * Put or replace a Boolean value in the JSONArray. If the index is greater
     * than the length of the JSONArray, then null elements will be added as
     * necessary to pad it out.
     * @param index The subscript.
     * @param value A Boolean value.
     * @return this.
     * @throws JSONException If the index is negative.
     */
    public JSONArray put(int index, Boolean value)
    {
      put(index, value ? true : false);
      return this;
    }


    /**
     * Put a value in the JSONArray, where the value will be a
     * JSONArray which is produced from a Collection.
     * @param index The subscript.
     * @param value A Collection value.
     * @return      this.
     * @throws JSONException If the index is negative or if the value is
     * not finite.
     */
    public JSONArray put(int index, List<object> value)
    {
      put(index, new JSONArray(value));
      return this;
    }


    /**
     * Put or replace a double value. If the index is greater than the length of
     *  the JSONArray, then null elements will be added as necessary to pad
     *  it out.
     * @param index The subscript.
     * @param value A double value.
     * @return this.
     * @throws JSONException If the index is negative or if the value is
     * not finite.
     */
    public JSONArray put(int index, double value)
    {
      put(index, value);
      return this;
    }


    /**
     * Put or replace an int value. If the index is greater than the length of
     *  the JSONArray, then null elements will be added as necessary to pad
     *  it out.
     * @param index The subscript.
     * @param value An int value.
     * @return this.
     * @throws JSONException If the index is negative.
     */
    public JSONArray put(int index, int value)
    {
      put(index, value);
      return this;
    }


    /**
     * Put or replace a long value. If the index is greater than the length of
     *  the JSONArray, then null elements will be added as necessary to pad
     *  it out.
     * @param index The subscript.
     * @param value A long value.
     * @return this.
     * @throws JSONException If the index is negative.
     */
    public JSONArray put(int index, long value)
    {
      put(index, value);
      return this;
    }


    /**
     * Put a value in the JSONArray, where the value will be a
     * JSONObject which is produced from a Map.
     * @param index The subscript.
     * @param value The Map value.
     * @return      this.
     * @throws JSONException If the index is negative or if the the value is
     *  an invalid number.
     */
    public JSONArray put(int index, Hashtable value)
    {
      put(index, new JSONObject(value));
      return this;
    }


    /**
     * Put or replace an object value in the JSONArray. If the index is greater
     *  than the length of the JSONArray, then null elements will be added as
     *  necessary to pad it out.
     * @param index The subscript.
     * @param value The value to put into the array. The value should be a
     *  Boolean, Double, Integer, JSONArray, JSONObject, Long, or String, or the
     *  JSONObject.NULL object.
     * @return this.
     * @throws JSONException If the index is negative or if the the value is
     *  an invalid number.
     */
    public JSONArray put(int index, Object value)
    {
      JSONObject.testValidity(value);
      if (index < 0)
      {
        throw new JSONException("JSONArray[" + index + "] not found.");
      }
      if (index < Length())
      {
        this.myArrayList.Insert(index, value);   // MLH 
      }
      else
      {
        while (index != Length())
        {
          put(JSONObject.NULL);
        }
        put(value);
      }
      return this;
    }


    /**
     * Produce a JSONObject by combining a JSONArray of names with the values
     * of this JSONArray.
     * @param names A JSONArray containing a list of key strings. These will be
     * paired with the values.
     * @return A JSONObject, or null if there are no names or if this JSONArray
     * has no values.
     * @throws JSONException If any of the names are null.
     */
    public JSONObject toJSONObject(JSONArray names)
    {
      if (names == null || names.Length() == 0 || Length() == 0)
      {
        return null;
      }
      JSONObject jo = new JSONObject();
      for (int i = 0; i < names.Length(); i += 1)
      {
        jo.put(names.getString(i), this.opt(i));
      }
      return jo;
    }


    /**
     * Make a JSON text of this JSONArray. For compactness, no
     * unnecessary whitespace is added. If it is not possible to produce a
     * syntactically correct JSON text then null will be returned instead. This
     * could occur if the array contains an invalid number.
     * <p>
     * Warning: This method assumes that the data structure is acyclical.
     *
     * @return a printable, displayable, transmittable
     *  representation of the array.
     */
    override public String ToString()
    {
      try
      {
        return '[' + join(",") + ']';
      }
      catch (Exception)
      {
        return null;
      }
    }


    /**
     * Make a prettyprinted JSON text of this JSONArray.
     * Warning: This method assumes that the data structure is acyclical.
     * @param indentFactor The number of spaces to add to each level of
     *  indentation.
     * @return a printable, displayable, transmittable
     *  representation of the object, beginning
     *  with <code>[</code>&nbsp;<small>(left bracket)</small> and ending
     *  with <code>]</code>&nbsp;<small>(right bracket)</small>.
     * @throws JSONException
     */
    public String ToString(int indentFactor)
    {
      return ToString(indentFactor, 0);
    }


    /**
     * Make a prettyprinted JSON text of this JSONArray.
     * Warning: This method assumes that the data structure is acyclical.
     * @param indentFactor The number of spaces to add to each level of
     *  indentation.
     * @param indent The indention of the top level.
     * @return a printable, displayable, transmittable
     *  representation of the array.
     * @throws JSONException
     */
    public String ToString(int indentFactor, int indent)
    {
      int len = Length();
      if (len == 0)
      {
        return "[]";
      }
      int i;
      String sb = "[";
      if (len == 1)
      {
        sb += JSONObject.valueToString(this.myArrayList[0], indentFactor, indent);
      }
      else
      {
        int newindent = indent + indentFactor;
        sb += '\n';
        for (i = 0; i < len; i += 1)
        {
          if (i > 0)
          {
            sb += ",\n";
          }
          for (int j = 0; j < newindent; j += 1)
          {
            sb += ' ';
          }
          sb += JSONObject.valueToString(this.myArrayList[i], indentFactor, newindent);
        }
        sb += '\n';
        for (i = 0; i < indent; i += 1)
        {
          sb += ' ';
        }
      }
      sb += ']';
      return sb;
    }
  }
}