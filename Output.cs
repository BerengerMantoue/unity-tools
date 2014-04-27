using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum LogFlag
{
    Behaviour = 1 << 0,
    Tree = 1 << 1,
    AI = 1 << 2,
    Detection = 1 << 3,
    Gui = 1 << 4
}

/// <summary>
/// Our debug class.
/// </summary>
public static class Output
{
    public static bool enabled = true;

    [SerializeField]
    public static bool enableRegular = true;
    [SerializeField]
    public static bool enableWarning = true;
    [SerializeField]
    public static bool enableError = true;
    [SerializeField]
    public static bool displayTime = true;

    public const string PREF_OUTPTUT_KEY = "DEBUG_ENABLE";
    public const string PREF_REGULAR_KEY = "DEBUG_ENABLE_REGULAR";
    public const string PREF_WARNING_KEY = "DEBUG_ENABLE_WARNING";
    public const string PREF_ERROR_KEY = "DEBUG_ENABLE_ERROR";
    public const string PREF_MASK_KEY = "DEBUG_LOG_MASK";


    private const LogFlag LOGFLAG_EVERYTHING = LogFlag.Behaviour | LogFlag.Tree | LogFlag.AI | LogFlag.Detection | LogFlag.Gui;

    [SerializeField]
    public static int currentMask = (int)LOGFLAG_EVERYTHING;

    public static void Init()
    {
#if UNITY_EDITOR
        enabled = UnityEditor.EditorPrefs.GetBool(PREF_OUTPTUT_KEY);
        enableRegular = UnityEditor.EditorPrefs.GetBool(PREF_REGULAR_KEY);
        enableWarning = UnityEditor.EditorPrefs.GetBool(PREF_WARNING_KEY);
        enableError = UnityEditor.EditorPrefs.GetBool(PREF_ERROR_KEY);
        currentMask = UnityEditor.EditorPrefs.GetInt(PREF_MASK_KEY);
#endif
    }

    /// <summary>
    /// Regular log
    /// </summary>
    /// <param name="obj">Object to print</param>
    /// <param name="color">Font color</param>
    /// <param name="flag">Flag of the log</param>
    public static void Log(System.Object obj, Color color, LogFlag flag)
    {
        if (obj != null && enabled && enableRegular && HasFlag(flag))
            UnityEngine.Debug.Log("<color=#" + color.ToHex() + ">" + obj.ToString() + "</color>");
    }
    public static void Log(System.Object obj, LogFlag flag) { Log(obj, Color.black, flag); }

    /// <summary>
    /// Warning log
    /// </summary>
    /// <param name="obj">Object to print</param>
    /// <param name="color">Font color</param>
    /// <param name="flag">Flag of the log</param>
    public static void LogWarning(System.Object obj, Color color, LogFlag flag)
    {
        if (obj != null && enabled && enableWarning && HasFlag(flag))
            UnityEngine.Debug.LogWarning("<color=#" + color.ToHex() + ">" + obj.ToString() + "</color>");
    }
    public static void LogWarning(System.Object obj, LogFlag flag) { Log(obj, Color.black, flag); }

    /// <summary>
    /// Error log
    /// </summary>
    /// <param name="obj">Object to print</param>
    /// <param name="color">Font color</param>
    /// <param name="flag">Flag of the log</param>
    public static void LogError(System.Object obj, Color color, LogFlag flag)
    {
        if (obj != null && enabled && enableError && HasFlag(flag))
            UnityEngine.Debug.LogError("<color=#" + color.ToHex() + ">" + obj.ToString() + "</color>");
    }
    public static void LogError(System.Object obj, LogFlag flag) { Log(obj, Color.black, flag); }


    public static bool HasFlag(LogFlag flag)
    {
        //UnityEngine.Debug.Log("HasFlag("+flag+") => " + ((currentMask & (int)flag) == (int)flag));
        return (currentMask & (int)flag) == (int)flag; 
    }

    #region Data structures
    // Array
    public static void Log<T>(string arrayName, T[] array, LogFlag flag) { Log(arrayName, array, x => x, flag); }
    public static void Log<T, U>(string arrayName, T[] array, System.Func<T, U> func, LogFlag flag) { Log(arrayName + " :\n" + array.ToSmartString(func, "\n"), flag); }
    public static void LogWarning<T>(string arrayName, T[] array, LogFlag flag) { LogWarning(arrayName, array, x => x, flag); }
    public static void LogWarning<T, U>(string arrayName, T[] array, System.Func<T, U> func, LogFlag flag) { LogWarning(arrayName + " :\n" + array.ToSmartString(func, "\n"), flag); }
    public static void LogError<T>(string arrayName, T[] array, LogFlag flag) { LogError(arrayName, array, x => x, flag); }
    public static void LogError<T, U>(string arrayName, T[] array, System.Func<T, U> func, LogFlag flag) { LogError(arrayName + " :\n" + array.ToSmartString(func, "\n"), flag); }

    // List
    public static void Log<T>(string listName, List<T> list, LogFlag flag) { Log(listName, list, x => x, flag); }
    public static void Log<T, U>(string listName, List<T> list, System.Func<T, U> func, LogFlag flag) { Log(listName + " :\n" + (list == null ? "NULL" : list.ToSmartString(func, "\n")), flag); }
    public static void LogWarning<T>(string listName, List<T> list, LogFlag flag) { LogWarning(listName, list, x => x, flag); }
    public static void LogWarning<T, U>(string listName, List<T> list, System.Func<T, U> func, LogFlag flag) { LogWarning(listName + " :\n" + (list == null ? "NULL" : list.ToSmartString(func, "\n")), flag); }
    public static void LogError<T>(string listName, List<T> list, LogFlag flag) { LogError(listName, list, x => x, flag); }
    public static void LogError<T, U>(string listName, List<T> list, System.Func<T, U> func, LogFlag flag) { LogError(listName + " :\n" + (list == null ? "NULL" : list.ToSmartString(func, "\n")), flag); }

    // Dictionary
    public static void Log<K, V>(string dictionaryName, Dictionary<K, V> dico, LogFlag flag) { Log(dictionaryName + " :\n" + dico.ToSmartString("\n"), flag); }
    public static void Log<K, V>(string dictionaryName, Dictionary<K, V> dico, System.Func<K, string> funcKey, System.Func<V, string> funcValue, LogFlag flag) { Log(dictionaryName + " :\n" + dico.ToSmartString(funcKey, funcValue, "\n"), flag); }
    public static void LogWarning<K, V>(string dictionaryName, Dictionary<K, V> dico, LogFlag flag) { LogWarning(dictionaryName + " :\n" + dico.ToSmartString("\n"), flag); }
    public static void LogWarning<K, V>(string dictionaryName, Dictionary<K, V> dico, System.Func<K, string> funcKey, System.Func<V, string> funcValue, LogFlag flag) { LogWarning(dictionaryName + " :\n" + dico.ToSmartString(funcKey, funcValue, "\n"), flag); }
    public static void LogError<K, V>(string dictionaryName, Dictionary<K, V> dico, LogFlag flag) { LogError(dictionaryName + " :\n" + dico.ToSmartString("\n"), flag); }
    public static void LogError<K, V>(string dictionaryName, Dictionary<K, V> dico, System.Func<K, string> funcKey, System.Func<V, string> funcValue, LogFlag flag) { LogError(dictionaryName + " :\n" + dico.ToSmartString(funcKey, funcValue, "\n"), flag); }
    #endregion
}

public static class Output3D
{
    public static void Cube(Vector3 pos, Color color, float destroy = 10f)
    {
//#if UNITY_EDITOR
//        Box(pos, Vector3.one, color, destroy);
//#endif
    }

    public static void Box(Vector3 pos, Vector3 size, Color color, float destroy = 10f)
    {
//#if UNITY_EDITOR
//        GameObject box = MeshTools.CreateBox(size.x, size.y, size.z);
//        box.name = "DebugBox";
//        box.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));
//        box.transform.position = pos;
//        box.renderer.material.color = color;

//        if (destroy > 0f)
//            GameObject.Destroy(box, destroy);
//#endif
    }
}
