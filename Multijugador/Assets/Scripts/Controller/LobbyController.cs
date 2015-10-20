using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Model.Entity;

public class LobbyController : Photon.MonoBehaviour {

    PhotonPlayer[] players;
    List<Player> Players;
    private GameObject panelList;
    public GameObject buttonStar;


	void Start () {

        PhotonNetwork.automaticallySyncScene = true;
        panelList = GameObject.Find(Util.instance.PLAYER_LIST_PANEL);
        panelList.gameObject.AddComponent<VerticalLayoutGroup>();
        panelList.GetComponent<VerticalLayoutGroup>().childForceExpandHeight = false;
        panelList.GetComponent<VerticalLayoutGroup>().padding = new RectOffset(5, 5, 5, 0);
        panelList.GetComponent<VerticalLayoutGroup>().spacing = 5;
        Players = new List<Player>();
	}

    void OnJoinedRoom()
    {
        BubbleOrder(PhotonNetwork.playerList);
        FillListplayers();
        if (!GameObject.Find("ChatController").GetComponent<ChatController>().enabled)
        { 
            GameObject.Find("ChatController").GetComponent<ChatController>().enabled = true; 
        }
        if (PhotonNetwork.player.isMasterClient)
        {
            buttonStar.SetActive(true);
        }

    }
    void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        ClearListPlayers();
        BubbleOrder(PhotonNetwork.playerList);
        FillListplayers();
    }

    void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        ClearListPlayers();
        BubbleOrder(PhotonNetwork.playerList);
        FillListplayers();
    }

    void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        if (PhotonNetwork.player.isMasterClient)
        {
            buttonStar.SetActive(true);
        }
    }

    void FillListplayers()
    {

        foreach (Player p in Players)
        {
            GameObject go = new GameObject("go_Player_" + p.nombre);
            go.transform.parent = panelList.transform;
            go.AddComponent<RectTransform>();
            go.AddComponent<UEJugador>();
            go.AddComponent<Image>();
            go.AddComponent<HorizontalLayoutGroup>();
            go.GetComponent<HorizontalLayoutGroup>().childForceExpandHeight = false;
            go.GetComponent<UEJugador>().jugador = p;
            go.GetComponent<Image>().color = Color.gray;
            go.GetComponent<Transform>().localScale = Vector3.one;
            go.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }
    }

    void ClearListPlayers()
    {
        Players.Clear();
        int count = GameObject.Find(Util.instance.PLAYER_LIST_PANEL).transform.childCount;
        for (int i = 0; i < count; ++i)
        {
            Destroy(GameObject.Find(Util.instance.PLAYER_LIST_PANEL).transform.GetChild(i).gameObject);
        }
    }

    void BubbleOrder(PhotonPlayer[] playerArray)
    {
        PhotonPlayer auxiliar;
        Players = new List<Player>();

        for (int i = playerArray.Length - 1; i > 0; i--)
        {
            for (int j = 0; j < i; j++)
            {
                if (playerArray[j].ID > playerArray[j + 1].ID)
                {
                    auxiliar = playerArray[j];
                    playerArray[j] = playerArray[j + 1];
                    playerArray[j + 1] = auxiliar;
                }
            }
        }

        for (int i = 0; i < playerArray.Length; ++i)
        {
            Player player = new Player(i+1,playerArray[i].name);
            Players.Add(player);
        }
    }

    public void Disconnect()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(Util.instance.SCENE_1);
        
    }

    public void StartGame()
    {
        Util.instance.listPlayers = Players;
        PhotonNetwork.LoadLevel(Util.instance.SCENE_3);
    }

}
