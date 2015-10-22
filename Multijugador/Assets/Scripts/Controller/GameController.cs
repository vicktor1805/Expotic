using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Model.Entity;

public class GameController : Photon.MonoBehaviour {

    private List<Player> listPlayers;
    public GameObject []Cameras;

	void Start () {

        listPlayers = Util.instance.listPlayers;
        InstantiateCamera();

	}
	
	void Update () {
	
	}

    void InstantiateCamera()
    {
        string name = PhotonNetwork.playerName;
        print(name);
        Player myPlayer = SearchPlapyer(name);

        foreach (GameObject camera in Cameras)
        {
            string nameCamera = camera.name.Substring(7);
            string stdNamePlayer = "Player" + myPlayer.id;

            if (nameCamera.Equals(stdNamePlayer))
            {
                camera.SetActive(true);
                break;
            }

        }
    }

    Player SearchPlapyer(string name)
    {
        Player myPlayer = null;

        foreach(Player p in listPlayers)
        {
            if (p.nombre.Equals(name))
            {
                myPlayer = p;
                break;
            }
        }

        return myPlayer;
    }
}
