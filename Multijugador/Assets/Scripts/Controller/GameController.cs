using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Model.Entity;

public class GameController : Photon.MonoBehaviour {

    private List<Player> listPlayers;
    

	void Start () {

        listPlayers = Util.instance.listPlayers;
        print(listPlayers.Count);

	}
	
	void Update () {
	
	}

    void RelocateCameras()
    {
        
    }
}
