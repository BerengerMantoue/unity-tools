using UnityEngine;
using System.Collections.Generic;

public static class StringExtensions
{
    #region To Smart String
    public static string ToSmartString2(this Vector2 vector, string format = "f3")
    {
        return "(" + vector.x.ToString(format) + ", " + vector.y.ToString(format) + ")";
    }
    public static string ToSmartString3(this Vector3 vector, string format = "f3")
    {
        return "(" + vector.x.ToString(format) + ", " + vector.y.ToString(format) + ", " + vector.z.ToString(format) + ")";
    }


    public static string ToSmartString<T>(this List<T> list)
    {
        return list.ToSmartString(x => x, ", ");
    }

    public static string ToSmartString<T>(this List<T> list, string separator)
    {
        return list.ToSmartString(x => x, separator);
    }

    public static string ToSmartString<T, U>(this List<T> list, System.Func<T, U> func, string separator = ", ")
    {
        string str = "";

        foreach (T t in list)
        {
            if (t != null)
            {
                U funcResult = func(t);
                str += (funcResult != null ? funcResult.ToString() : "NULL") + separator;
            }
            else
                str += "NULL" + separator;
        }

        return str.Length > separator.Length ? str.Substring(0, str.Length - separator.Length) : str;
    }

    public static string ToSmartString<T>(this T[] array)
    {
        return array.ToSmartString(x => x, ", ");
    }
    public static string ToSmartString<T>(this T[] array, string separator)
    {
        return array.ToSmartString(x => x, separator);
    }
    public static string ToSmartString<T, U>(this T[] array, System.Func<T, U> func, string separator = ", ")
    {
        string str = "";

        foreach (T t in array)
            str += (t != null ? func(t).ToString() : "NULL") + separator;

        return str.Length > separator.Length ? str.Substring(0, str.Length - separator.Length) : str;
    }

    public static string ToSmartString<K, V>(this Dictionary<K, V> dico, string separator = ", ")
    {
        return dico.ToSmartString(k => (k != null ? k.ToString() : "NULL"), v => (v != null ? v.ToString() : "NULL"), separator);
    }
    public static string ToSmartString<K, V>(this Dictionary<K, V> dico, System.Func<V, string> funcValue, string separator = ", ")
    {
        return dico.ToSmartString(k => (k != null ? k.ToString() : "NULL"), funcValue, separator);
    }
    public static string ToSmartString<K, V>(this Dictionary<K, V> dico, System.Func<K, string> funcKey, System.Func<V, string> funcValue, string separator = ", ")
    {
        string str = "";

        foreach (KeyValuePair<K, V> kvp in dico)
        {
            str += "[" + (kvp.Key != null ? funcKey(kvp.Key).ToString() : "NULL") + "] = " + (kvp.Value != null ? funcValue(kvp.Value).ToString() : "NULL") + "\n";
        }

        return str.Length > separator.Length ? str.Substring(0, str.Length - separator.Length) : str;
    }
    #endregion


    public static string ToHex(this Color color)
    {
        return ((Color32)color).ToHex();
    }

    public static string ToHex(this Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }

    public static Color ToColor(this string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }
}
