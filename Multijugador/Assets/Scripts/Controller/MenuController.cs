using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    private string url;
    public Grid dataGrid { get; set; }

	void Start () {

        url = "http://upcmoviles.somee.com/PruebaCubos/JSONTest.txt";
        dataGrid = Util.instance.GetJson<Grid>(url);
        print("Data was loaded succesfully");
	}
}
