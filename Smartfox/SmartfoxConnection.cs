using UnityEngine;
using Sfs2X;

public class SmartfoxConnection : MonoBehaviour
{
    private static SmartfoxConnection mInstance;
    private static SmartFox smartFox;
    public static SmartFox Connection
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new GameObject("SmartFoxConnection").AddComponent<SmartfoxConnection>();
            }
            return smartFox;
        }
        set
        {
            if (mInstance == null)
            {
                mInstance = new GameObject("SmartFoxConnection").AddComponent<SmartfoxConnection>();
            }
            smartFox = value;
        }
    }

    public static bool IsInitialized
    {
        get
        {
            return (smartFox != null);
        }
    }

    // Handle disconnection automagically
    // ** Important for Windows users - can cause crashes otherwise
    void OnApplicationQuit()
    {
        if (smartFox.IsConnected)
        {
            smartFox.Disconnect();
        }
    }
}
