
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Imagen : MonoBehaviour{

    private string nombre;
    private Sprite sprite;

    void Start()
    {
        nombre = this.name;
        sprite = Resources.Load<Sprite>(nombre);
        this.GetComponent<Image>().sprite = sprite;
        print(nombre);
    }

}
