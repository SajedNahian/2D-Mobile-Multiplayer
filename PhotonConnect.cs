using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonConnect : MonoBehaviour {
    public string versionName = "0.1";
    public GameObject connectingPane, connectedPane, disconnectedPane;
	void Awake () {
        PhotonNetwork.ConnectUsingSettings(versionName);
        Debug.Log("Connecting...");
	}

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("We are connected to photon master");
    }

    private void OnJoinedLobby()
    {
        changePaneView(false, true, false);
        Debug.Log("On Joined Lobby");
    }

    private void OnDisconnectedFromPhoton()
    {
        changePaneView(false, false, true);
        Debug.Log("...Disconnected");
    }

    private void changePaneView (bool pane1, bool pane2, bool pane3)
    {
        connectingPane.SetActive(pane1);
        connectedPane.SetActive(pane2);
        disconnectedPane.SetActive(pane3);
    }
}
