using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TankController tank;

    Camera camera;
    Vector3 direction;

    void Awake()
    {
        if(camera == null)
            camera = Camera.main;

        if(tank != null)
        {
            tank.OnTankDestroyed += OnTankDestroyed;
        }
    }

    void Start()
    {
            
    }


    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if (tank == null) return;

        tank.AimAt(camera.ScreenToWorldPoint(Input.mousePosition));

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        tank.MoveTo(direction * Time.deltaTime);

        if(Input.GetButtonDown("Fire"))
        {
            //tank.Shoot();
            photonView.RPC("Shoot", RpcTarget.AllViaServer);
        }
    }

    void OnTankDestroyed()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        GameStateManager.Instance.OnPlayerDeath();
    }
}
