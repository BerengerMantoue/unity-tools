using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ScriptableClassWindow : EditorWindow
{
    /// <summary>
    /// List of objects
    /// </summary>
    private List<ClassA> _list;

    #region Unity Window stuff
    private static ScriptableClassWindow window;	
    [MenuItem ("My Window/Class")]
    static void Init () 
	{
        window = EditorWindow.GetWindow (typeof (ScriptableClassWindow)) as ScriptableClassWindow;
		Debug.Log("Init"); 
    }
    #endregion

    private void OnGUI()
    {
        // Make sure list isn't null
        if (_list == null)
            _list = new List<ClassA>();

        // Init if not done yet
        if (window == null)
            Init();

        // Buttons
        GUILayout.BeginArea(new Rect(10, 10, window.position.width - 20, 75));
        {
            GUILayout.BeginHorizontal();
            {
                // Add A
                if (GUILayout.Button("Add Class A", GUILayout.ExpandHeight(true)))
                    _list.Add(new ClassA());

                // Add B
                if (GUILayout.Button("Add Class B", GUILayout.ExpandHeight(true)))
                    _list.Add(new ClassB());

                if (GUILayout.Button("Clear", GUILayout.ExpandHeight(true)))
                    _list.Clear();

                // Save to file
                if (GUILayout.Button("Save", GUILayout.ExpandHeight(true)))
                {
                    // Create instance of holder
                    ClassHolder asset = new ClassHolder(_list);  //scriptable object

                    // Assign a COPY of the list. Not the list itself, or it would
                    // keep updating
                    _list = new List<ClassA>(_list);

                    // Save
                    AssetDatabase.CreateAsset(asset, "Assets/classes.asset");
                    AssetDatabase.SaveAssets();
                }

                // Load from file
                if (GUILayout.Button("Load", GUILayout.ExpandHeight(true)))
                {
                    // Load
                    Object o = AssetDatabase.LoadAssetAtPath("Assets/classes.asset", typeof(ClassHolder));

                    // Cast into class holder
                    ClassHolder classHolder = (ClassHolder)o;

                    // Copy the list
                    if (classHolder != null)
                        _list = _list = new List<ClassA>(classHolder.list);
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndArea();

        // Display the list
        GUILayout.BeginArea(new Rect(10, 110, 200, window.position.height - 80));
        {
            GUILayout.Space(20);
            GUILayout.Label("Childcount : " + _list.Count);
            GUILayout.Space(20);

            foreach (ClassA a in _list)
                if (a != null)
                    a.Draw();
        }
        GUILayout.EndArea();
    }
}