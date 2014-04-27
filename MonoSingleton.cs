/********************************************************************************************
 * Author : Mantoue Bérenger
 * Version : 1.0
 * Date : February 2014
 * Description : Singleton design pattern for Unity.
 *  
 *  To use this, define the class as follow :
 *  => public class MyClass : MonoSingleton<MyClass>
 *  
 *  The point of the singleton design pattern is to access staticly an instance
 *  of the object. The first access also instantiate the object.
 * ******************************************************************************************/


using UnityEngine;

/// <summary>
/// Singleton design pattern for Unity.
/// 
/// To use this, define the class as follow :
/// => public class MyClass : MonoSingleton<MyClass>
/// 
/// The point of the singleton design pattern is to access staticly an instance
/// of the object. The first access also instantiate the object.
/// </summary>
/// <typeparam name="T">Class inheriting from MonoBehaviour</typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    /// <summary>
    /// Reference of the instance
    /// </summary>
    private static T m_Instance = null;

    /// <summary>
    /// Reference of the instance
    /// </summary>
    public static T instance
    {
        get
        {
            // Instance requiered for the first time, we look for it
            if (m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;

                // Object not found, we create a temporary one
                if (m_Instance == null)
                {
                    Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");
                    m_Instance = new GameObject("Temp Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();

                    // Problem during the creation, this should not happen
                    if (m_Instance == null)
                    {
                        Debug.LogError("Problem during the creation of " + typeof(T).ToString());
                    }
                }
                m_Instance.Init();
            }
            return m_Instance;
        }
    }
    // If no other monobehaviour request the instance in an awake function
    // executing before this one, no need to search the object.
    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this as T;
            m_Instance.Init();
        }
    }

    // This function is called when the instance is used the first time
    // Put all the initializations you need here, as you would do in Awake
    public virtual void Init() { }

    // Make sure the instance isn't referenced anymore when the user quit, just in case.
    private void OnApplicationQuit()
    {
        m_Instance = null;
    }
}