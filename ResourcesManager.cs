using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void OnGetResourceAsyncComplete<T>(T resource) where T : UnityEngine.Object;

public static class ResourcesManager
{
    private static Dictionary<string, UnityEngine.Object> _dictionary = null;

    private const string MATERIAL_PATH = "Materials/";
    private const string SHADER_PATH = "Shaders/";
    private const string GUI_PATH = "Gui/";
    private const string GUI3D_PATH = "Gui3D/";
    private const string FONTS_PATH = "Fonts/";
    private const string SKINS_PATH = "Skins/";
    private const string PREFABS_PATH = "Prefabs/";

    public static Material  LoadMaterial(string matName) { return GetResource<Material>(MATERIAL_PATH, matName); }
    public static Shader LoadShader(string shaName) { return GetResource<Shader>(SHADER_PATH, shaName); }
    public static Texture2D LoadTextureGui2D(string texName) { return GetResource<Texture2D>(GUI_PATH, texName); }
    public static Texture2D LoadTextureGui3D(string texName) { return GetResource<Texture2D>(GUI3D_PATH, texName); }
    public static Font LoadFont(string fontName) { return GetResource<Font>(FONTS_PATH, fontName); }
    public static GUISkin LoadSkin(string skinName) { return GetResource<GUISkin>(SKINS_PATH, skinName); }
    public static GameObject LoadPrefab(string prefabName) { return GetResource<GameObject>(PREFABS_PATH, prefabName); }

    public static Material LoadMaterialAsync(string matName, OnGetResourceAsyncComplete<Material> onComplete){ return GetResourceAsync<Material>(MATERIAL_PATH, matName, onComplete); }
    public static Shader LoadShaderAsync(string shaName, OnGetResourceAsyncComplete<Shader> onComplete) { return GetResourceAsync<Shader>(SHADER_PATH, shaName, onComplete); }
    public static Texture2D LoadTextureGui2DAsync(string texName, OnGetResourceAsyncComplete<Texture2D> onComplete) { return GetResourceAsync<Texture2D>(GUI_PATH, texName, onComplete); }
    public static Texture2D LoadTextureGui3DAsync(string texName, OnGetResourceAsyncComplete<Texture2D> onComplete) { return GetResourceAsync<Texture2D>(GUI3D_PATH, texName, onComplete); }
    public static Font LoadFontAsync(string fontName, OnGetResourceAsyncComplete<Font> onComplete) { return GetResourceAsync<Font>(FONTS_PATH, fontName, onComplete); }
    public static GUISkin LoadSkinAsync(string skinName, OnGetResourceAsyncComplete<GUISkin> onComplete) { return GetResourceAsync<GUISkin>(SKINS_PATH, skinName, onComplete); }
    public static GameObject LoadPrefabAsync(string prefabName, OnGetResourceAsyncComplete<GameObject> onComplete) { return GetResourceAsync<GameObject>(PREFABS_PATH, prefabName, onComplete); }

    private static T GetResource<T>(string path, string name) where T : UnityEngine.Object
    {
        if (_dictionary == null)
            _dictionary = new Dictionary<string, Object>();

        UnityEngine.Object result;
        if (_dictionary.TryGetValue(name, out result))
            return result as T;
        else
        {
            T resource = Resources.Load(path + name) as T;
            if (resource == null)
                Debug.LogError("Resource " + path + name + " not found.");

            _dictionary.Add(name, resource);
            return resource;
        }
    }
    
    private static T GetResourceAsync<T>(string path, string name, OnGetResourceAsyncComplete<T> onComplete) where T : UnityEngine.Object
    {
        if (_dictionary == null)
            _dictionary = new Dictionary<string, Object>();

        UnityEngine.Object result;
        if (_dictionary.TryGetValue(name, out result))
            return result as T;
        else
        {
            // Create a temporary monobehaviour to execute the coroutine
            GameObject go = new GameObject();
            go.hideFlags = HideFlags.HideAndDontSave;
            ResourceLoader rl = go.AddComponent<ResourceLoader>();
            rl.StartCoroutine(rl.GetResourceAsync(path, name, onComplete));

            return null;
        }
    }
}

public class ResourceLoader : MonoBehaviour
{
    public IEnumerator GetResourceAsync<T>(string path, string name, OnGetResourceAsyncComplete<T> onComplete) where T : UnityEngine.Object
    {
        WWW www = new WWW("file://" + Application.dataPath + "/" + path + name);

        yield return www;

        onComplete(null); // Not sure what should be in there ...

        // Job done, this guy can get lost
        GameObject.Destroy(gameObject);
    }
}
