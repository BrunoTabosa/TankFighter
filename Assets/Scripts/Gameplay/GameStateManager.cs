﻿using Photon.Chat.UtilityScripts;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameStateManager : SingletonPUN<GameStateManager>
{
    [SerializeField]
    private GameObject playerPrefab;
    public override void Awake()
    {
        SetInstance(this);
        PhotonNetwork.AutomaticallySyncScene = true;
        base.Awake();
    }

    public void Start()
    {
        Connect();
    }

    public void Play()
    {
        print("play click");
    }

    public void Connect()
    {
        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.

            PhotonNetwork.JoinRandomRoom();

        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = "1";
        }
    }

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");

        PhotonNetwork.JoinRandomRoom();

    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions()
        {
            MaxPlayers = 10
        });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");

        UIManager.Instance.ShowSelectTank();
        
    }

    public void SpawnPlayerTank(string prefabName)
    {
        Vector2 min = DataManager.Instance.RoomConfiguration.StartingPositionMin;
        Vector2 max = DataManager.Instance.RoomConfiguration.StartingPositionMax;

        PhotonNetwork.Instantiate(prefabName,
            new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0f),
            Quaternion.identity, 0);
    }

    #endregion
}
