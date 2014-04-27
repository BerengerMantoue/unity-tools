using UnityEngine;
using UnityEditor;

public class OutputEditor : EditorWindow
{
    private readonly string[] FLAG_NAMES = System.Enum.GetNames(typeof(LogFlag));

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Output window %#d")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        OutputEditor window = (OutputEditor)EditorWindow.GetWindow(typeof(OutputEditor));
        window.name = window.name;
    }

    void OnGUI()
    {
        GUILayout.Label("Display settings", EditorStyles.boldLabel);

        Output.enabled = SetBoolean("Log", Output.PREF_OUTPTUT_KEY);

        GUI.enabled = Output.enabled;

        Output.enableRegular = SetBoolean("\tRegular", Output.PREF_REGULAR_KEY);
        Output.enableWarning = SetBoolean("\tWarning", Output.PREF_WARNING_KEY);
        Output.enableError = SetBoolean("\tError", Output.PREF_ERROR_KEY);
        
        EditorGUILayout.Separator();

        int currMask = EditorPrefs.GetInt(Output.PREF_MASK_KEY);
        int newMask = EditorGUILayout.MaskField("Debug Mask", currMask, FLAG_NAMES);
        Output.currentMask = newMask;

        if (currMask != newMask)
        {
            EditorPrefs.SetInt(Output.PREF_MASK_KEY, newMask);
            //Debug.Log("Current Mask = " + currMask + ". New Mask = " + newMask
            //    + ". " + Output.HasFlag(LogFlag.Behaviour)
            //    + ". " + Output.HasFlag(LogFlag.Tree)
            //    + ". " + Output.HasFlag(LogFlag.Gui));
        }



        EditorGUILayout.Separator();

        GUILayout.Label("Mask : " + Output.currentMask);

        GUILayout.BeginHorizontal();

        string[] names = System.Enum.GetNames(typeof(LogFlag));
        LogFlag[] flags = (LogFlag[])System.Enum.GetValues(typeof(LogFlag));
        for (int i = 0; i < names.Length; i++)
        {
            if (GUILayout.Button(names[i]))
                Debug.Log(names[i] + " activated : " + Output.HasFlag(flags[i]));
        }
        GUILayout.EndHorizontal();
    }

    private bool SetBoolean(string label, string key)
    {
        bool currentValue = EditorPrefs.GetBool(key);
        bool newValue = EditorGUILayout.Toggle(label, currentValue);
        if (currentValue != newValue)
        {
            EditorPrefs.SetBool(key, newValue);
            return newValue;
        }

        return currentValue;
    }
}