using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviourPun
{
    public int AmmoReward;
    public int ScoreReward;
    public void Destroy()
    {
        gameObject.SetActive(false); //Prevent multiple hits while RPC is running

        photonView.RPC("DestroyRPC", RpcTarget.AllViaServer);
    }

    [PunRPC]
    void DestroyRPC()
    {
        if(photonView.IsMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
