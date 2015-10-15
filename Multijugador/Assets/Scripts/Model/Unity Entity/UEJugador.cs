using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.Model.Entity;

public class UEJugador : MonoBehaviour {

    public Player jugador { get; set; }
	void Start () {

        FillColumns();
	}

    void FillColumns()
    {
        foreach (var prop in jugador.GetType().GetProperties())
        {
            GameObject go = new GameObject("go_" + prop.Name);
            go.transform.parent = gameObject.transform;
            go.AddComponent<Text>();
            go.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            go.GetComponent<Text>().color = Color.black;
            go.GetComponent<Text>().font = Resources.Load<Font>(Util.instance.FONT_SOLANOGOTHIC_MVB_MDCAP);
            go.GetComponent<Text>().fontStyle = FontStyle.Bold;
            go.GetComponent<Text>().text = prop.GetValue(jugador, null).ToString();
            go.GetComponent<Text>().fontSize = 25;
            go.GetComponent<RectTransform>().localScale = Vector3.one;
            go.GetComponent<RectTransform>().localPosition = Vector3.zero;

        }
    }
}
