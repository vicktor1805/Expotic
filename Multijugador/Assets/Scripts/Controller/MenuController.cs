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
    public Text roomName;
    public GameObject messagePanel;
    public GameObject playerNameError;

	void Start () {

        
        roomName = GameObject.Find(Util.instance.ROOM_NAME).GetComponent<Text>();
        rooms = new RoomInfo[0];
        version = "1.0";

        if (Util.instance.TestInternetConnection(errorPanel))
        {
            if (!Util.instance.isConnected)
            {
                PhotonNetwork.ConnectUsingSettings(version);
            }
        }

        if (Application.isPlaying)
        {
            GameObject.Find("EditionTime").SetActive(false);
        }
        
	}

    public void OnJoinedLobby()
    {
        SetNamePlayer();
        GameObject.Find(Util.instance.BUTTON_UNIRSE).GetComponent<Button>().interactable = true;
        GameObject.Find(Util.instance.BUTTON_CREAR).GetComponent<Button>().interactable = true;
        GameObject.Find(Util.instance.ROOM_NAME_CONTAINER).GetComponent<InputField>().interactable = true;
        Util.instance.isConnected = true;
    }

    void OnReceivedRoomListUpdate()
    {
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
        Util.instance.HidePanelHandleUIExeption(errorPanel);
    }

    public void CreateRoom()
    {
        string temp = roomName.text.Trim();
        if(temp.Equals(""))
        {
            Util.instance.OPERATION = 1;
            Util.instance.ShowPanelHandleUIExeption(Util.instance.MESSAGE_NO_NAME_ROOM, errorPanel);
        }
        else
        {
            PhotonNetwork.CreateRoom(temp);
            Util.instance.LoadScene(Util.instance.SCENE_2);
        }
        
    }

    public void JoinRoom()
    {
        if (Util.instance.currentID != -1)
        {
            string nameRoom = getRoom(Util.instance.currentID);
            PhotonNetwork.JoinRoom(nameRoom);
            PhotonNetwork.LoadLevel(Util.instance.SCENE_2);
        }
        else
        {

        }
    }

    void DeleteCurrentList()
    {
        int count = GameObject.Find(Util.instance.CONTAINER_GRID_ROMMS).transform.childCount;
        for (int i = 0; i < count; ++i)
        {
            Destroy(GameObject.Find(Util.instance.CONTAINER_GRID_ROMMS).transform.GetChild(i).gameObject);
        }
    }

    string getRoom(int id)
    {
        for (int i = 0; i < rooms.Length; ++i)
        {
            if (i + 1 == id)
                return rooms[i].name;
        }
        return "";
    }

    void SetNamePlayer()
    {
        if (!Util.instance.isLoggedIn)
        {
            messagePanel.SetActive(true);
        }
    }

    public void SubmitNamePlayer()
    {
        string playerName = GameObject.Find(Util.instance.PLAYER_NAME).GetComponent<Text>().text;
        if(!playerName.Trim().Equals(""))
        {
            PhotonNetwork.player.name = GameObject.Find(Util.instance.PLAYER_NAME).GetComponent<Text>().text;
            messagePanel.SetActive(false);
            Util.instance.isLoggedIn = true;
        }
        else
        {
            playerNameError.SetActive(true);
        }
        
    }
}
