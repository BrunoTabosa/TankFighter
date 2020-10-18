using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    [SerializeField]
    TankController tankController;

    Camera camera;
    Vector3 direction;

    void Awake()
    {
        if(camera == null)
            camera = Camera.main;
    }

    public void Init(string tank)
    {
        //SpawnTank
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        GameObject go = PhotonNetwork.Instantiate(tank, this.transform.position, Quaternion.identity);
        tankController = go.GetComponent<TankController>();
        tankController.OnTankDestroyed += OnTankDestroyed;
    }


    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if (tankController == null) return;

        tankController.AimAt(camera.ScreenToWorldPoint(Input.mousePosition));

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        tankController.MoveTo(direction * Time.deltaTime);

        if(Input.GetButtonDown("Fire"))
        {
            //tank.Shoot();
            tankController.photonView.RPC("Shoot", RpcTarget.AllViaServer);
        }
    }

    public void Shoot()
    {
        tankController.Shoot();
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
