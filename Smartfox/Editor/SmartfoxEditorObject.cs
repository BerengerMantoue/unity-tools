using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor buttons under the GameObject/Create Other/Smartfox section.
/// Creates game objects related to smartfox.
/// </summary>
public static class SmartfoxEditorObject
{
    [MenuItem("GameObject/Create Other/Smartfox/SmartfoxSingleton")]
    public static void AddSmartfoxSingleton()
    {
        if (GameObject.FindObjectOfType<SmartfoxConnection>() != null)
        {
            Debug.LogError("There already is a SmartfoxSingleton in that scene");
            EditorGUIUtility.PingObject(GameObject.FindObjectOfType<SmartfoxConnection>());
        }
        else
        {
            GameObject go = new GameObject("SmartfoxSingleton");
            go.AddComponent<SmartfoxConnection>();
        }
    }
}
