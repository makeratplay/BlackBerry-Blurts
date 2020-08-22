using System;

using System.Xml;
using System.Xml.Xsl;
using System.Collections;
using System.Runtime.InteropServices;

namespace Blurts
{
  [ComVisible(true)]
  public class PressKeyCmd : CmdBase
  {
    // Tag Names
    private static String KEY_FUNC_KEY = "FunctionKey";
    private static String KEY_KEY_CODE = "KeyCode";
    private static String KEY_STATUS_CODE = "StatusCode";

    /*
          Keypad.KEY_ALT = 257
          Keypad.KEY_APPLICATION = 21
          Keypad.KEY_BACKLIGHT = 259
          Keypad.KEY_BACKSPACE = 8
          Keypad.KEY_CONVENIENCE_1 = 19
          Keypad.KEY_CONVENIENCE_2 = 21
          Keypad.KEY_DELETE = 127
          Keypad.KEY_END = 18
          Keypad.KEY_ENTER = 10
          Keypad.KEY_ESCAPE = 27
          Keypad.KEY_MENU = 4098
          Keypad.KEY_MIDDLE = 19
          Keypad.KEY_NEXT = 20
          Keypad.KEY_SEND = 17
          Keypad.KEY_SHIFT_LEFT = 258
          Keypad.KEY_SHIFT_RIGHT = 256
          Keypad.KEY_SHIFT_X = 261
          Keypad.KEY_SPACE = 32
          Keypad.KEY_SPEAKERPHONE = 273
          Keypad.KEY_VOLUME_DOWN = 4097
          Keypad.KEY_VOLUME_UP = 4096

        public class Characters
        {
          public const byte DIGIT_ZERO  = 48;
          public const byte DIGIT_ONE = 49;
          public const byte DIGIT_TWO = 50;
          public const byte DIGIT_THREE = 51;
          public const byte DIGIT_FOUR = 52;
          public const byte DIGIT_FIVE = 53;
          public const byte DIGIT_SIX = 54;
          public const byte DIGIT_SEVEN = 55;
          public const byte DIGIT_EIGHT = 56;
          public const byte DIGIT_NINE = 57;



          public const byte ACUTE_ACCENT = (byte)-76;
          public const byte AMPERSAND = 38;
          public const byte APOSTROPHE = 39;
          public const byte AQUARIUS = 82;
          public const byte ARIES = 72;
          public const byte ASTERISK = 42;
          public const byte BACKSPACE = 8;
          public const byte BALLOT_BOX = 16;
          public const byte BALLOT_BOX_WITH_CHECK = 17;
          public const byte BALLOT_X = 23;
          public const byte BLACK_CLUB_SUIT = 99;
          public const byte BLACK_DIAMOND_SUIT = 102;
          public const byte BLACK_DOWN_POINTING_SMALL_TRIANGLE = (byte)-66;
          public const byte BLACK_DOWN_POINTING_TRIANGLE = (byte)-68;
          public const byte BLACK_HEART_SUIT = 101;
          public const byte BLACK_LEFT_POINTING_POINTER = (byte)-60;
          public const byte BLACK_LEFT_POINTING_SMALL_TRIANGLE = (byte)-62;
          public const byte BLACK_LEFT_POINTING_TRIANGLE = (byte)-64;
          public const byte BLACK_RIGHT_POINTING_POINTER = (byte)-70;
          public const byte BLACK_RIGHT_POINTING_SMALL_TRIANGLE = (byte)-72;
          public const byte BLACK_RIGHT_POINTING_TRIANGLE = (byte)-74;
          public const byte BLACK_SMALL_SQUARE = (byte)-86;
          public const byte BLACK_SPADE_SUIT = 96;
          public const byte BLACK_SUN_WITH_RAYS = 0;
          public const byte BLACK_TELEPHONE = 14;
          public const byte BLACK_UP_POINTING_SMALL_TRIANGLE = (byte)-76;
          public const byte BLACK_UP_POINTING_TRIANGLE = (byte)-78;
          public const byte BOX_DRAWINGS_LIGHT_DOWN_AND_RIGHT = 12;
          public const byte BOX_DRAWINGS_LIGHT_UP_AND_RIGHT = 20;
          public const byte BOX_DRAWINGS_LIGHT_VERTICAL = 2;
          public const byte BROKEN_BAR = (byte)-90;
          public const byte BULLET = 34;
          public const byte CANCER = 75;
          public const byte CAPRICORN = 81;
          public const byte CEDILLA = (byte)-72;
          public const byte CENT_SIGN = (byte)-94;
          public const byte CHECK_MARK = 19;
          public const byte CIRCUMFLEX_ACCENT = 94;
          public const byte CLOUD = 1;
          public const byte COLON = 58;
          public const byte COMMA = 44;
          public const byte COMMERCIAL_AT = 64;
          public const byte CONTROL_DOWN = (byte)-126;
          public const byte CONTROL_LEFT = (byte)-125;
          public const byte CONTROL_MENU = (byte)-107;
          public const byte CONTROL_RIGHT = (byte)-124;
          public const byte CONTROL_SELECT = (byte)-112;
          public const byte CONTROL_SYMBOL = (byte)-128;
          public const byte CONTROL_UP = (byte)-127;
          public const byte CONTROL_VOLUME_DOWN = (byte)-105;
          public const byte CONTROL_VOLUME_UP = (byte)-106;
          public const byte COPYRIGHT_SIGN = (byte)-87;
          public const byte CURRENCY_SIGN = (byte)-92;
          public const byte DAGGER = 32;
          public const byte DEGREE_SIGN = -80;
          public const byte DELETE = 127;
          public const byte DIAERESIS = -88;
          public const byte DIVISION_SIGN = -9;
          public const byte DOLLAR_SIGN = 36;
          public const byte DOUBLE_DAGGER = 33;
          public const byte EM_DASH = 20;
          public const byte ENTER = 10;
          public const byte ENVELOPE = 9;
          public const byte EQUALS_SIGN = 61;
          public const byte ESCAPE = 27;
          public const byte EURO_SIGN = -84;
          public const byte EXCLAMATION_MARK = 33;
          public const byte FEMININE_ORDINAL_INDICATOR = -86;
          public const byte FULL_STOP = 46;
          public const byte GEMINI = 74;
          public const byte GRAVE_ACCENT = 96;
          public const byte GREATER_THAN_SIGN = 62;
          public const byte GREEK_CAPITAL_LETTER_DELTA = -108;
          public const byte GREEK_CAPITAL_LETTER_GAMMA = -109;
          public const byte GREEK_CAPITAL_LETTER_LAMDA = -101;
          public const byte GREEK_CAPITAL_LETTER_OMEGA = -87;
          public const byte GREEK_CAPITAL_LETTER_PHI = -90;
          public const byte GREEK_CAPITAL_LETTER_PI = -96;
          public const byte GREEK_CAPITAL_LETTER_PSI = -88;
          public const byte GREEK_CAPITAL_LETTER_SIGMA = -93;
          public const byte GREEK_CAPITAL_LETTER_THETA = -104;
          public const byte GREEK_CAPITAL_LETTER_XI = -98;
          public const byte HAIR_SPACE = 10;
          public const byte HORIZONTAL_ELLIPSIS = 38;
          public const byte HYPHEN_MINUS = 45;
          public const byte IDEOGRAPHIC_FULL_STOP = 2;
          public const byte INVERTED_EXCLAMATION_MARK = -95;
          public const byte INVERTED_QUESTION_MARK = -65;
          public const byte LATIN_CAPITAL_LETTER_A = 65;
          public const byte LATIN_CAPITAL_LETTER_A_WITH_ACUTE = -63;
          public const byte LATIN_CAPITAL_LETTER_A_WITH_CIRCUMFLEX = -62;
          public const byte LATIN_CAPITAL_LETTER_A_WITH_DIAERESIS = -60;
          public const byte LATIN_CAPITAL_LETTER_A_WITH_GRAVE = -64;
          public const byte LATIN_CAPITAL_LETTER_A_WITH_RING_ABOVE = -59;
          public const byte LATIN_CAPITAL_LETTER_A_WITH_TILDE = -61;
          public const byte LATIN_CAPITAL_LETTER_AE = -58;
          public const byte LATIN_CAPITAL_LETTER_B = 66;
          public const byte LATIN_CAPITAL_LETTER_C = 67;
          public const byte LATIN_CAPITAL_LETTER_C_WITH_CEDILLA = -57;
          public const byte LATIN_CAPITAL_LETTER_D = 68;
          public const byte LATIN_CAPITAL_LETTER_E = 69;
          public const byte LATIN_CAPITAL_LETTER_E_WITH_ACUTE = -55;
          public const byte LATIN_CAPITAL_LETTER_E_WITH_CIRCUMFLEX = -54;
          public const byte LATIN_CAPITAL_LETTER_E_WITH_DIAERESIS = -53;
          public const byte LATIN_CAPITAL_LETTER_E_WITH_GRAVE = -56;
          public const byte LATIN_CAPITAL_LETTER_ETH = -48;
          public const byte LATIN_CAPITAL_LETTER_F = 70;
          public const byte LATIN_CAPITAL_LETTER_G = 71;
          public const byte LATIN_CAPITAL_LETTER_H = 72;
          public const byte LATIN_CAPITAL_LETTER_I = 73;
          public const byte LATIN_CAPITAL_LETTER_I_WITH_ACUTE = -51;
          public const byte LATIN_CAPITAL_LETTER_I_WITH_CIRCUMFLEX = -50;
          public const byte LATIN_CAPITAL_LETTER_I_WITH_DIAERESIS = -49;
          public const byte LATIN_CAPITAL_LETTER_I_WITH_GRAVE = -52;
          public const byte LATIN_CAPITAL_LETTER_J = 74;
          public const byte LATIN_CAPITAL_LETTER_K = 75;
          public const byte LATIN_CAPITAL_LETTER_L = 76;
          public const byte LATIN_CAPITAL_LETTER_M = 77;
          public const byte LATIN_CAPITAL_LETTER_N = 78;
          public const byte LATIN_CAPITAL_LETTER_N_WITH_TILDE = -47;
          public const byte LATIN_CAPITAL_LETTER_O = 79;
          public const byte LATIN_CAPITAL_LETTER_O_WITH_ACUTE = -45;
          public const byte LATIN_CAPITAL_LETTER_O_WITH_CIRCUMFLEX = -44;
          public const byte LATIN_CAPITAL_LETTER_O_WITH_DIAERESIS = -42;
          public const byte LATIN_CAPITAL_LETTER_O_WITH_GRAVE = -46;
          public const byte LATIN_CAPITAL_LETTER_O_WITH_STROKE = -40;
          public const byte LATIN_CAPITAL_LETTER_O_WITH_TILDE = -43;
          public const byte LATIN_CAPITAL_LETTER_P = 80;
          public const byte LATIN_CAPITAL_LETTER_Q = 81;
          public const byte LATIN_CAPITAL_LETTER_R = 82;
          public const byte LATIN_CAPITAL_LETTER_S = 83;
          public const byte LATIN_CAPITAL_LETTER_T = 84;
          public const byte LATIN_CAPITAL_LETTER_THORN = -34;
          public const byte LATIN_CAPITAL_LETTER_U = 85;
          public const byte LATIN_CAPITAL_LETTER_U_WITH_ACUTE = -38;
          public const byte LATIN_CAPITAL_LETTER_U_WITH_CIRCUMFLEX = -37;
          public const byte LATIN_CAPITAL_LETTER_U_WITH_DIAERESIS = -36;
          public const byte LATIN_CAPITAL_LETTER_U_WITH_GRAVE = -39;
          public const byte LATIN_CAPITAL_LETTER_V = 86;
          public const byte LATIN_CAPITAL_LETTER_W = 87;
          public const byte LATIN_CAPITAL_LETTER_X = 88;
          public const byte LATIN_CAPITAL_LETTER_Y = 89;
          public const byte LATIN_CAPITAL_LETTER_Y_WITH_ACUTE = -35;
          public const byte LATIN_CAPITAL_LETTER_Z = 90;
          public const byte LATIN_CAPITAL_LIGATURE_OE = 82;
          public const byte LATIN_SMALL_LETTER_A = 97;
          public const byte LATIN_SMALL_LETTER_A_WITH_ACUTE = -31;
          public const byte LATIN_SMALL_LETTER_A_WITH_CIRCUMFLEX = -30;
          public const byte LATIN_SMALL_LETTER_A_WITH_DIAERESIS = -28;
          public const byte LATIN_SMALL_LETTER_A_WITH_GRAVE = -32;
          public const byte LATIN_SMALL_LETTER_A_WITH_RING_ABOVE = -27;
          public const byte LATIN_SMALL_LETTER_A_WITH_TILDE = -29;
          public const byte LATIN_SMALL_LETTER_AE = -26;
          public const byte LATIN_SMALL_LETTER_B = 98;
          public const byte LATIN_SMALL_LETTER_C = 99;
          public const byte LATIN_SMALL_LETTER_C_WITH_CEDILLA = -25;
          public const byte LATIN_SMALL_LETTER_D = 100;
          public const byte LATIN_SMALL_LETTER_E = 101;
          public const byte LATIN_SMALL_LETTER_E_WITH_ACUTE = -23;
          public const byte LATIN_SMALL_LETTER_E_WITH_CIRCUMFLEX = -22;
          public const byte LATIN_SMALL_LETTER_E_WITH_DIAERESIS = -21;
          public const byte LATIN_SMALL_LETTER_E_WITH_GRAVE = -24;
          public const byte LATIN_SMALL_LETTER_ETH = -16;
          public const byte LATIN_SMALL_LETTER_F = 102;
          public const byte LATIN_SMALL_LETTER_G = 103;
          public const byte LATIN_SMALL_LETTER_H = 104;
          public const byte LATIN_SMALL_LETTER_I = 105;
          public const byte LATIN_SMALL_LETTER_I_WITH_ACUTE = -19;
          public const byte LATIN_SMALL_LETTER_I_WITH_CIRCUMFLEX = -18;
          public const byte LATIN_SMALL_LETTER_I_WITH_DIAERESIS = -17;
          public const byte LATIN_SMALL_LETTER_I_WITH_GRAVE = -20;
          public const byte LATIN_SMALL_LETTER_J = 106;
          public const byte LATIN_SMALL_LETTER_K = 107;
          public const byte LATIN_SMALL_LETTER_L = 108;
          public const byte LATIN_SMALL_LETTER_M = 109;
          public const byte LATIN_SMALL_LETTER_N = 110;
          public const byte LATIN_SMALL_LETTER_N_WITH_TILDE = -15;
          public const byte LATIN_SMALL_LETTER_O = 111;
          public const byte LATIN_SMALL_LETTER_O_WITH_ACUTE = -13;
          public const byte LATIN_SMALL_LETTER_O_WITH_CIRCUMFLEX = -12;
          public const byte LATIN_SMALL_LETTER_O_WITH_DIAERESIS = -10;
          public const byte LATIN_SMALL_LETTER_O_WITH_GRAVE = -14;
          public const byte LATIN_SMALL_LETTER_O_WITH_STROKE = -8;
          public const byte LATIN_SMALL_LETTER_O_WITH_TILDE = -11;
          public const byte LATIN_SMALL_LETTER_P = 112;
          public const byte LATIN_SMALL_LETTER_Q = 113;
          public const byte LATIN_SMALL_LETTER_R = 114;
          public const byte LATIN_SMALL_LETTER_S = 115;
          public const byte LATIN_SMALL_LETTER_SHARP_S = -33;
          public const byte LATIN_SMALL_LETTER_T = 116;
          public const byte LATIN_SMALL_LETTER_THORN = -2;
          public const byte LATIN_SMALL_LETTER_U = 117;
          public const byte LATIN_SMALL_LETTER_U_WITH_ACUTE = -6;
          public const byte LATIN_SMALL_LETTER_U_WITH_CIRCUMFLEX = -5;
          public const byte LATIN_SMALL_LETTER_U_WITH_DIAERESIS = -4;
          public const byte LATIN_SMALL_LETTER_U_WITH_GRAVE = -7;
          public const byte LATIN_SMALL_LETTER_V = 118;
          public const byte LATIN_SMALL_LETTER_W = 119;
          public const byte LATIN_SMALL_LETTER_X = 120;
          public const byte LATIN_SMALL_LETTER_Y = 121;
          public const byte LATIN_SMALL_LETTER_Y_WITH_ACUTE = -3;
          public const byte LATIN_SMALL_LETTER_Y_WITH_DIAERESIS = -1;
          public const byte LATIN_SMALL_LETTER_Z = 122;
          public const byte LATIN_SMALL_LIGATURE_OE = 83;
          public const byte LEFT_CURLY_BRACKET = 123;
          public const byte LEFT_DOUBLE_QUOTATION_MARK = 28;
          public const byte LEFT_PARENTHESIS = 40;
          public const byte LEFT_POINTING_DOUBLE_ANGLE_QUOTATION_MARK = -85;
          public const byte LEFT_SINGLE_QUOTATION_MARK = 24;
          public const byte LEFT_SQUARE_BRACKET = 91;
          public const byte LEFT_TO_RIGHT_EMBEDDING = 42;
          public const byte LEFT_TO_RIGHT_OVERRIDE = 45;
          public const byte LEO = 76;
          public const byte LESS_THAN_SIGN = 60;
          public const byte LIBRA = 78;
          public const byte LIGHTNING = 7;
          public const byte LOW_LINE = 95;
          public const byte MACRON = -81;
          public const byte MASCULINE_ORDINAL_INDICATOR = -70;
          public const byte MICRO_SIGN = -75;
          public const byte MIDDLE_DOT = -73;
          public const byte MULTIPLICATION_SIGN = -41;
          public const byte NO_BREAK_SPACE = -96;
          public const byte NOT_SIGN = -84;
          public const byte NULL = 0;
          public const byte NUMBER_SIGN = 35;
          public const byte OBJECT_REPLACEMENT_CHARACTER = -4;
          public const byte PERCENT_SIGN = 37;
          public const byte PILCROW_SIGN = -74;
          public const byte PISCES = 83;
          public const byte PLUS_MINUS_SIGN = -79;
          public const byte PLUS_SIGN = 43;
          public const byte POP_DIRECTIONAL_FORMATTING = 44;
          public const byte POUND_SIGN = -93;
          public const byte QUESTION_MARK = 63;
          public const byte QUOTATION_MARK = 34;
          public const byte REGISTERED_SIGN = -82;
          public const byte REVERSE_SOLIDUS = 92;
          public const byte RIGHT_CURLY_BRACKET = 125;
          public const byte RIGHT_DOUBLE_QUOTATION_MARK = 29;
          public const byte RIGHT_PARENTHESIS = 41;
          public const byte RIGHT_POINTING_DOUBLE_ANGLE_QUOTATION_MARK = -69;
          public const byte RIGHT_SINGLE_QUOTATION_MARK = 25;
          public const byte RIGHT_SQUARE_BRACKET = 93;
          public const byte RIGHT_TO_LEFT_EMBEDDING = 43;
          public const byte RIGHT_TO_LEFT_OVERRIDE = 46;
          public const byte SAGITTARIUS = 80;
          public const byte SCORPIUS = 79;
          public const byte SECTION_SIGN = -89;
          public const byte SEMICOLON = 59;
          public const byte SMILE = 35;
          public const byte SNOWMAN = 3;
          public const byte SOFT_HYPHEN = -83;
          public const byte SOLIDUS = 47;
          public const byte SPACE = 32;
          public const byte SUPERSCRIPT_ONE = -71;
          public const byte SUPERSCRIPT_THREE = -77;
          public const byte SUPERSCRIPT_TWO = -78;
          public const byte TAB = 9;
          public const byte TAURUS = 73;
          public const byte TELEPHONE_LOCATION_SIGN = 6;
          public const byte TILDE = 126;
          public const byte TRADE_MARK_SIGN = 34;
          public const byte UMBRELLA = 2;
          public const byte VERTICAL_LINE = 124;
          public const byte VIRGO = 77;
          public const byte VULGAR_FRACTION_ONE_HALF = -67;
          public const byte VULGAR_FRACTION_ONE_QUARTER = -68;
          public const byte VULGAR_FRACTION_THREE_QUARTERS = -66;
          public const byte WHITE_BULLET = -26;
          public const byte WHITE_CLUB_SUIT = 103;
          public const byte WHITE_DIAMOND_SUIT = 98;
          public const byte WHITE_DOWN_POINTING_SMALL_TRIANGLE = -65;
          public const byte WHITE_DOWN_POINTING_TRIANGLE = -67;
          public const byte WHITE_HEART_SUIT = 97;
          public const byte WHITE_LEFT_POINTING_POINTER = -59;
          public const byte WHITE_LEFT_POINTING_SMALL_TRIANGLE = -61;
          public const byte WHITE_LEFT_POINTING_TRIANGLE = -63;
          public const byte WHITE_RIGHT_POINTING_POINTER = -69;
          public const byte WHITE_RIGHT_POINTING_SMALL_TRIANGLE = -71;
          public const byte WHITE_RIGHT_POINTING_TRIANGLE = -73;
          public const byte WHITE_SPADE_SUIT = 100;
          public const byte WHITE_UP_POINTING_SMALL_TRIANGLE = -75;
          public const byte WHITE_UP_POINTING_TRIANGLE = -77;
          public const byte YEN_SIGN = -91;
          public const byte ZERO_WIDTH_SPACE = 11;

        }
     * */
    /*
    public class KeypadSTATUS
    {
      public const int STATUS_ALT = 1;
      public const int STATUS_ALT_LOCK = 2;
      public const int STATUS_CAPS_LOCK = 3;
      public const int STATUS_SHIFT = 4;
      public const int STATUS_SHIFT_LEFT = 5;
      public const int STATUS_SHIFT_RIGHT = 6;
    }
     * */

    public class KeypadListener
    {
      public const int STATUS_ALT = 1;
      public const int STATUS_ALT_LOCK = 16;
      public const int STATUS_CAPS_LOCK = 4;
      public const int STATUS_FOUR_WAY = 536870912;
      public const int STATUS_KEY_HELD_WHILE_ROLLING = 8;
      public const int STATUS_NOT_FROM_KEYPAD = 32768;
      public const int STATUS_SHIFT = 2;
      public const int STATUS_SHIFT_LEFT = 32;
      public const int STATUS_SHIFT_RIGHT = 64;
      public const int STATUS_TRACKWHEEL = 1073741824;
    }

    private Hashtable requiresShift = new Hashtable();
    private Hashtable requiresAlt = new Hashtable();

    public PressKeyCmd(String s)
      : base(s)
    {
    }

    public PressKeyCmd()
      : base(CMD_PRESSKEY)
    {
      requiresShift = new Hashtable();
      requiresShift.Add('A', 1);
      requiresShift.Add('B', 1);
      requiresShift.Add('C', 1);
      requiresShift.Add('D', 1);
      requiresShift.Add('E', 1);
      requiresShift.Add('F', 1);
      requiresShift.Add('G', 1);
      requiresShift.Add('H', 1);
      requiresShift.Add('I', 1);
      requiresShift.Add('J', 1);
      requiresShift.Add('K', 1);
      requiresShift.Add('L', 1);
      requiresShift.Add('M', 1);
      requiresShift.Add('N', 1);
      requiresShift.Add('O', 1);
      requiresShift.Add('P', 1);
      requiresShift.Add('Q', 1);
      requiresShift.Add('R', 1);
      requiresShift.Add('S', 1);
      requiresShift.Add('T', 1);
      requiresShift.Add('U', 1);
      requiresShift.Add('V', 1);
      requiresShift.Add('W', 1);
      requiresShift.Add('X', 1);
      requiresShift.Add('Y', 1);
      requiresShift.Add('Z', 1);

      requiresAlt = new Hashtable();
      requiresAlt.Add('#', 'q');
      requiresAlt.Add('1', 'w');
      requiresAlt.Add('2', 'e');
      requiresAlt.Add('3', 'r');
      requiresAlt.Add('(', 't');
      requiresAlt.Add(')', 'y');
      requiresAlt.Add('_', 'u');
      requiresAlt.Add('-', 'i');
      requiresAlt.Add('+', 'o');
      requiresAlt.Add('@', 'p');
      requiresAlt.Add('*', 'a');
      requiresAlt.Add('4', 's');
      requiresAlt.Add('5', 'd');
      requiresAlt.Add('6', 'f');
      requiresAlt.Add('/', 'g');
      requiresAlt.Add(':', 'h');
      requiresAlt.Add(';', 'j');
      requiresAlt.Add('\'', 'k');
      requiresAlt.Add('"', 'l');
      requiresAlt.Add('7', 'z');
      requiresAlt.Add('8', 'x');
      requiresAlt.Add('9', 'c');
      requiresAlt.Add('$', 'v');
      requiresAlt.Add('!', 'b');
      requiresAlt.Add(',', 'n');
      requiresAlt.Add('=', 'm');

    }

    [ComVisible(true)]
    public int FunctionKey
    {
      get
      {
        return optInt(KEY_FUNC_KEY);
      }
      set
      {
        try
        {
          remove(KEY_FUNC_KEY);
          put(KEY_FUNC_KEY, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public int KeyCode
    {
      get
      {
        return optInt(KEY_KEY_CODE);
      }
      set
      {
        try
        {
          remove(KEY_KEY_CODE);
          put(KEY_KEY_CODE, value);
          /*
          StatusCode = KeypadListener.STATUS_NOT_FROM_KEYPAD;
          
          if (requiresShift.ContainsKey((char)value))
          {
            StatusCode = KeypadSTATUS.STATUS_SHIFT;
            put(KEY_KEY_CODE, value);
          }
          else if (requiresAlt.ContainsKey((char)value))
          {
            StatusCode = KeypadSTATUS.STATUS_ALT;
            put(KEY_KEY_CODE, requiresAlt[(char)value]);
          }
          else
          {
            put(KEY_KEY_CODE, value);
          }
           * */
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }

    [ComVisible(true)]
    public int StatusCode
    {
      get
      {
        return optInt(KEY_STATUS_CODE);
      }
      set
      {
        try
        {
          remove(KEY_STATUS_CODE);
          put(KEY_STATUS_CODE, value);
        }
        catch (JSONException e)
        {
          Console.WriteLine(e.ToString());
        }
      }
    }
  }
}
