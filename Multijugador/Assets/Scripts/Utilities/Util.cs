using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using Newtonsoft.Json;

public class Util : MonoBehaviour {

    private static Util _instance;

    public readonly string LOGO_BIT = "Logo_BIT";
    public readonly string LOGO_EXPOTIC = "Logo_Expotic";
    public readonly string IMAGES_PARENT = "Image";
    public readonly string SCENE_0 = "Scene0";
    public readonly string SCENE_1 = "Scene1";
    private readonly string METHOD_GET = "GET";
    public readonly string CONTAINER = "Container";

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

    public void LoadScene(string name)
    {
        Application.LoadLevel(name);
    }

    public T GetJson<T>(string url)
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = METHOD_GET;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receive = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receive, System.Text.Encoding.UTF8);
            string res = readStream.ReadToEnd();

            T objeto = JsonConvert.DeserializeObject<T>(res);
            return objeto;
        }
        catch (HttpListenerException ex)
        {
            print("[ERROR]: Hubo un error recolectando la data: " + ex.Message);
            return default(T);
        }
    }
}
