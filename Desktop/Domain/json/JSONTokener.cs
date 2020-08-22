
using System;

namespace Blurts
{
  public class JSONTokener
  {

    /**
     * The index of the next character.
     */
    private int myIndex;


    /**
     * The source string being tokenized.
     */
    private String mySource;


    /**
     * Construct a JSONTokener from a string.
     *
     * @param s     A source string.
     */
    public JSONTokener(String s)
    {
      this.myIndex = 0;
      this.mySource = s;
    }


    /**
     * Back up one character. This provides a sort of lookahead capability,
     * so that you can test for a digit or letter before attempting to parse
     * the next number or identifier.
     */
    public void back()
    {
      if (this.myIndex > 0)
      {
        this.myIndex -= 1;
      }
    }



    /**
     * Get the hex value of a character (base16).
     * @param c A character between '0' and '9' or between 'A' and 'F' or
     * between 'a' and 'f'.
     * @return  An int between 0 and 15, or -1 if c was not a hex digit.
     */
    public static int dehexchar(char c)
    {
      if (c >= '0' && c <= '9')
      {
        return c - '0';
      }
      if (c >= 'A' && c <= 'F')
      {
        return c - ('A' - 10);
      }
      if (c >= 'a' && c <= 'f')
      {
        return c - ('a' - 10);
      }
      return -1;
    }


    /**
     * Determine if the source string still contains characters that next()
     * can consume.
     * @return true if not yet at the end of the source.
     */
    public Boolean more()
    {
      return this.myIndex < this.mySource.Length;
    }


    /**
     * Get the next character in the source string.
     *
     * @return The next character, or 0 if past the end of the source string.
     */
    public char next()
    {
      if (more())
      {
        char c = this.mySource[this.myIndex];  //MLH FIX THIS
        this.myIndex += 1;
        return c;
      }
      return (char)0;
    }


    /**
     * Consume the next character, and check that it matches a specified
     * character.
     * @param c The character to match.
     * @return The character.
     * @throws JSONException if the character does not match.
     */
    public char next(char c)
    {
      char n = next();
      if (n != c)
      {
        throw syntaxError("Expected '" + c + "' and instead saw '" +
                n + "'.");
      }
      return n;
    }


    /**
     * Get the next n characters.
     *
     * @param n     The number of characters to take.
     * @return      A string of n characters.
     * @throws JSONException
     *   Substring bounds error if there are not
     *   n characters remaining in the source string.
     */
    public String next(int n)
    {
      int i = this.myIndex;
      int j = i + n;
      if (j >= this.mySource.Length)
      {
        throw syntaxError("Substring bounds error");
      }
      this.myIndex += n;
      return this.mySource.Substring(i, (j-i) ); // MLH FIX THIS   .substring(i, j);
    }


    /**
     * Get the next char in the string, skipping whitespace
     * and comments (slashslash, slashstar, and hash).
     * @throws JSONException
     * @return  A character, or 0 if there are no more characters.
     */
    public char nextClean()
    {
      for (; ; )
      {
        char c = next();
        if (c == '/')
        {
          switch (next())
          {
            case '/':
              do
              {
                c = next();
              } while (c != '\n' && c != '\r' && c != 0);
              break;
            case '*':
              for (; ; )
              {
                c = next();
                if (c == 0)
                {
                  throw syntaxError("Unclosed comment.");
                }
                if (c == '*')
                {
                  if (next() == '/')
                  {
                    break;
                  }
                  back();
                }
              }
              break;
            default:
              back();
              return '/';
          }
        }
        else if (c == '#')
        {
          do
          {
            c = next();
          } while (c != '\n' && c != '\r' && c != 0);
        }
        else if (c == 0 || c > ' ')
        {
          return c;
        }
      }
    }


    /**
     * Return the characters up to the next close quote character.
     * Backslash processing is done. The formal JSON format does not
     * allow strings in single quotes, but an implementation is allowed to
     * accept them.
     * @param quote The quoting character, either
     *      <code>"</code>&nbsp;<small>(double quote)</small> or
     *      <code>'</code>&nbsp;<small>(single quote)</small>.
     * @return      A String.
     * @throws JSONException Unterminated string.
     */
    public String nextString(char quote)
    {
      char c;
      String sb = "";
      for (; ; )
      {
        c = next();
        switch (c)
        {
          case (char)0:
          case '\n':
          case '\r':
            throw syntaxError("Unterminated string");
          case '\\':
            c = next();
            switch (c)
            {
              case 'b':
                sb += '\b';
                break;
              case 't':
                sb += '\t';
                break;
              case 'n':
                sb += '\n';
                break;
              case 'f':
                sb += '\f';
                break;
              case 'r':
                sb += '\r';
                break;
              case 'u':
                sb += (char)int.Parse( next(4) ); // MLH base 16?
                break;
              case 'x':
                sb += (char)int.Parse(next(2)); // MLH base 16?
                break;
              default:
                sb += c;
                break;
            }
            break;
          default:
            if (c == quote)
            {
              return sb;
            }
            sb += c;
            break;
        }
      }
    }


    /**
     * Get the text up but not including the specified character or the
     * end of line, whichever comes first.
     * @param  d A delimiter character.
     * @return   A string.
     */
    public String nextTo(char d)
    {
      String sb = "";
      for (; ; )
      {
        char c = next();
        if (c == d || c == 0 || c == '\n' || c == '\r')
        {
          if (c != 0)
          {
            back();
          }
          return sb.Trim();
        }
        sb += c;
      }
    }


    /**
     * Get the text up but not including one of the specified delimeter
     * characters or the end of line, whichever comes first.
     * @param delimiters A set of delimiter characters.
     * @return A string, trimmed.
     */
    public String nextTo(String delimiters)
    {
      char c;
      String sb = "";
      for (; ; )
      {
        c = next();
        if (delimiters.IndexOf(c) >= 0 || c == 0 || c == '\n' || c == '\r')
        {
          if (c != 0)
          {
            back();
          }
          return sb.Trim();
        }
        sb += c;
      }
    }


    /**
     * Get the next value. The value can be a Boolean, Double, Integer,
     * JSONArray, JSONObject, Long, or String, or the JSONObject.NULL object.
     * @throws JSONException If syntax error.
     *
     * @return An object.
     */
    public Object nextValue()
    {
      char c = nextClean();
      String s;

      switch (c)
      {
        case '"':
        case '\'':
          return nextString(c);
        case '{':
          back();
          return new JSONObject(this);
        case '[':
          back();
          return new JSONArray(this);
      }

      /*
       * Handle unquoted text. This could be the values true, false, or
       * null, or it can be a number. An implementation (such as this one)
       * is allowed to also accept non-standard forms.
       *
       * Accumulate characters until we reach the end of the text or a
       * formatting character.
       */

      String sb = "";
      char b = c;
      while (c >= ' ' && ",:]}/\\\"[{;=#".IndexOf(c) < 0)
      {
        sb += c;
        c = next();
      }
      back();

      /*
       * If it is true, false, or null, return the proper value.
       */

      s = sb.Trim();
      if (s.Equals(""))
      {
        throw syntaxError("Missing value.");
      }
      if (s.Equals("true", StringComparison.CurrentCultureIgnoreCase ))
      {
        return true;
      }
      if (s.Equals("false", StringComparison.CurrentCultureIgnoreCase ))
      {
        return false;
      }
      if (s.Equals("null", StringComparison.CurrentCultureIgnoreCase ))
      {
        return JSONObject.NULL;
      }

      /*
       * If it might be a number, try converting it. We support the 0- and 0x-
       * conventions. If a number cannot be produced, then the value will just
       * be a string. Note that the 0-, 0x-, plus, and implied string
       * conventions are non-standard. A JSON parser is free to accept
       * non-JSON forms as long as it accepts all correct JSON forms.
       */

      if ((b >= '0' && b <= '9') || b == '.' || b == '-' || b == '+')
      {
        if (b == '0')
        {
          if (s.Length > 2 && (s[1] == 'x' || s[1] == 'X'))
          {
            try
            {
              return int.Parse(s.Substring(2) ); // MLH FIX?
            }
            catch (Exception)
            {
              /* Ignore the error */
            }
          }
          else
          {
            try
            {
              return int.Parse(s); // MLH FIX
            }
            catch (Exception)
            {
              /* Ignore the error */
            }
          }
        }
        try
        {
          return int.Parse(s);
        }
        catch (Exception)
        {
          try
          {
            return long.Parse(s);
          }
          catch (Exception)
          {
            try
            {
              return double.Parse(s);
            }
            catch (Exception)
            {
              return s;
            }
          }
        }
      }
      return s;
    }


    /**
     * Skip characters until the next character is the requested character.
     * If the requested character is not found, no characters are skipped.
     * @param to A character to skip to.
     * @return The requested character, or zero if the requested character
     * is not found.
     */
    public char skipTo(char to)
    {
      char c;
      int index = this.myIndex;
      do
      {
        c = next();
        if (c == 0)
        {
          this.myIndex = index;
          return c;
        }
      } while (c != to);
      back();
      return c;
    }


    /**
     * Skip characters until past the requested string.
     * If it is not found, we are left at the end of the source.
     * @param to A string to skip past.
     */
    public void skipPast(String to)
    {
      this.myIndex = this.mySource.IndexOf(to, this.myIndex);
      if (this.myIndex < 0)
      {
        this.myIndex = this.mySource.Length;
      }
      else
      {
        this.myIndex += to.Length;
      }
    }


    /**
     * Make a JSONException to signal a syntax error.
     *
     * @param message The error message.
     * @return  A JSONException object, suitable for throwing
     */
    public JSONException syntaxError(String message)
    {
      return new JSONException(message + toString());
    }


    /**
     * Make a printable string of this JSONTokener.
     *
     * @return " at character [this.myIndex] of [this.mySource]"
     */
    public String toString()
    {
      return " at character " + this.myIndex + " of " + this.mySource;
    }
  }
}