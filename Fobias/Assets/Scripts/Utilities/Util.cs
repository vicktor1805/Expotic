using UnityEngine;
using System.Collections;

public class Util : MonoBehaviour {

    private static Util _instance;

    public readonly string LOGO_BIT = "Logo_BIT";
    public readonly string LOGO_EXPOTIC = "Logo_Expotic";

    public static Util instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Util>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

}
