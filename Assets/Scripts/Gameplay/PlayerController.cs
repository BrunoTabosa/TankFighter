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
        tank.AimAt(camera.ScreenToWorldPoint(Input.mousePosition));

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        tank.MoveTo(direction * Time.deltaTime);

        if(Input.GetButtonDown("Fire"))
        {
            tank.Shoot();
        }
    }
}
