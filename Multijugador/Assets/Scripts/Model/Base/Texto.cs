﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Texto : MonoBehaviour {

    private string nombre;
    private Text text;
	void Start () {

        nombre = this.transform.parent.name;
        text = this.GetComponent<Text>();
        text.text = nombre;
        text.text = text.text.ToString().Substring(3);
	}
	
}
