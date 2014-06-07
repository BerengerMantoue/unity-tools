using UnityEngine;
using Sfs2X;

public class SmartfoxSingleton : MonoSingleton<SmartfoxSingleton>
{
    /// <summary>
    /// Reference to the smartfox object
    /// </summary>
    private SmartFox _smartfox;
    public SmartFox smartfox
    {
        get { return _smartfox; }
        set { _smartfox = value; }
    }

    public override void Init()
    {
    }
}
