using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageController : MonoBehaviour {

    private CanvasGroup Images;
    public float speed = 5f;

	void Start () {

        Images = GameObject.Find(Util.instance.IMAGES_PARENT).GetComponent<CanvasGroup>();
        Images.alpha = 0;
	}

    void Update()
    {
        Images.alpha = Mathf.Lerp(0f, 1f, Time.time/speed);
        if (Time.time >= 4)
        {
            Util.instance.LoadScene(Util.instance.SCENE_1);
        }
    }

}
