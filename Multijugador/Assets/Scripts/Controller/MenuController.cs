using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon;

public class MenuController : Photon.MonoBehaviour {

    private string url;
    private Grid grid;
    private string version;
    private RoomInfo[] rooms;
    private Text text;
    public GameObject errorPanel;

	void Start () {

        rooms = new RoomInfo[0];
        text = GameObject.Find("Message").GetComponent<Text>();
        version = "1.0";

        if (Util.instance.TestInternetConnection(errorPanel))
        {
            PhotonNetwork.ConnectUsingSettings(version);
        }

        if (Application.isPlaying)
        {
            GameObject.Find("EditionTime").SetActive(false);
        }
        
	}

    public void OnJoinedLobby()
    {
        print("OnJoinedLobby(). Use a GUI to show existing rooms available in PhotonNetwork.GetRoomList().");
        GameObject.Find(Util.instance.BUTTON_UNIRSE).GetComponent<Button>().interactable = true;
        GameObject.Find(Util.instance.BUTTON_CREAR).GetComponent<Button>().interactable = true;
    }

    void OnReceivedRoomListUpdate()
    {
        print("Se actuliza la lista");
        if (rooms.Length >0 )
        {
            rooms.Initialize();
            DeleteCurrentList();
        }
        rooms = PhotonNetwork.GetRoomList();
        GameObject.Find(Util.instance.CONTAINER).GetComponent<UEContainer>().grid = Util.instance.ParseRoomArrayToList(rooms);
        GameObject.Find(Util.instance.CONTAINER).gameObject.SendMessage("fillGridView");
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("Room " + Random.Range(1, 11));
    }

    public void JoinRoom()
    {

    }

    void DeleteCurrentList()
    {
        int count = GameObject.Find(Util.instance.CONTAINER_GRID_ROMMS).transform.childCount;
        print(count);
        for (int i = 0; i < count; ++i)
        {
            Destroy(GameObject.Find(Util.instance.CONTAINER_GRID_ROMMS).transform.GetChild(i).gameObject);
        }
    }
}
