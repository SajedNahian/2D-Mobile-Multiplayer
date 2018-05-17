using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class photonHandler : MonoBehaviour {
    public InputField roomName;
    public GameObject mainPlayer;

    private void Awake()
    {
        DontDestroyOnLoad(this.transform);
        PhotonNetwork.sendRate = 100;
        PhotonNetwork.sendRateOnSerialize = 60;
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public void joinOrCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(roomName.text, roomOptions, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        moveScene();
        Debug.Log("Connected to the room...");
    }

    public void moveScene()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            spawnPlayer();
        }
    }

    private void spawnPlayer()
    {
        //PhotonNetwork.Instantiate(mainPlayer.name, mainPlayer.transform.position, mainPlayer.transform.rotation, 0);
        PhotonNetwork.Instantiate(mainPlayer.name, new Vector3(0, 4, 0), mainPlayer.transform.rotation, 0);

    }

    public static void DebugPrint ()
    {
        GameObject errorText = GameObject.FindGameObjectWithTag("ErrorText");
        errorText.GetComponent<Text>().text = "Too many players connected to room, try a different one";
       
    }
}
