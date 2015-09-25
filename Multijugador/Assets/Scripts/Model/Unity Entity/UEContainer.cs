using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UEContainer : MonoBehaviour {

    public Grid grid { get; set; }

	void Start () {

        fillGridView();
	}

    public void fillGridView()
    {
        if (grid!=null)
        {
            print("Cantidad en el UEContainer: " + grid.filas.Count);
            GameObject childHeader = gameObject.transform.FindChild(Util.instance.CONTAINER_HEADER).gameObject;
            childHeader.AddComponent<HorizontalLayoutGroup>();

            Columna colHeader = grid.filas[0].columna;
            
            foreach (var prop in colHeader.GetType().GetProperties())
            {
                GameObject go = new GameObject("go_" + prop.Name);
                go.transform.parent = childHeader.transform;
                go.AddComponent<Text>();
                go.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                go.GetComponent<Text>().color = Color.black;
                go.GetComponent<Text>().font = Resources.Load<Font>(Util.instance.FONT_SOLANOGOTHIC_MVB_MDCAP);
                go.GetComponent<Text>().fontStyle = FontStyle.Bold;
                go.GetComponent<Text>().text = prop.GetValue(colHeader,null).ToString();
                go.GetComponent<RectTransform>().localScale = Vector3.one;
                go.GetComponent<RectTransform>().localPosition = Vector3.zero;
                
            }

            GameObject childRows = gameObject.transform.FindChild(Util.instance.CONTAINER_GRID_ROMMS).gameObject;
            childRows.gameObject.AddComponent<VerticalLayoutGroup>();
            childRows.gameObject.GetComponent<VerticalLayoutGroup>().childForceExpandHeight = false;
            childRows.gameObject.GetComponent<VerticalLayoutGroup>().padding.top = 10;
            childRows.gameObject.GetComponent<VerticalLayoutGroup>().padding.left = 5;
            childRows.gameObject.GetComponent<VerticalLayoutGroup>().padding.right = 5;

            for (int i = 1; i < grid.filas.Count; ++i)
            {
                
                GameObject go = new GameObject("go_Row_" + i);
                go.transform.parent = childRows.transform;
                go.AddComponent<UERow>();
                go.AddComponent<RectTransform>();
                go.AddComponent<Image>();
                go.AddComponent<Button>();
                go.GetComponent<Button>().onClick.AddListener(() => { someFunction();});
                go.GetComponent<Image>().color = Color.gray;
                go.GetComponent<UERow>().columna = grid.filas[i].columna;
                go.GetComponent<Transform>().localScale = Vector3.one;
                go.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }
        }
    }

    void someFunction()
    {
        print("Button clicked");
    }
}
