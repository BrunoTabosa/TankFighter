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

    int score;

    void Awake()
    {
        if (camera == null)
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
        tankController.OnEnemyDestroyed += OnEnemyDestroyed;
        tankController.OnDestructableDestroy += OnDestructableDestroy;
        tankController.OnShotFired += OnShotFired;

        PlayerCamera.Instance.SetTarget(tankController.transform);

        score = 0;
        UpdateScore();
        SetAmmo(tankController.stats.MaximumAmmo);
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

        tankController.MoveTo(direction);

        if(Input.GetButtonDown("Fire") && tankController.CanShoot())
        {
            tankController.photonView.RPC("Shoot", RpcTarget.AllViaServer);
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

    void OnEnemyDestroyed()
    {
        score += DataManager.Instance.RoomConfiguration.ScoreForEnemyDestroyed;
        UpdateScore();
    }

    void OnDestructableDestroy(Destructable destructable)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        score += destructable.ScoreReward;
        UpdateScore();

        UpdateAmmo();
    }

    void OnShotFired()
    {
        if (photonView.IsMine)
        {
            UpdateAmmo();
        }
    }

    void UpdateScore()
    {
        UIManager.Instance.UpdateScore(score);
    }

    void UpdateAmmo()
    {
        UIManager.Instance.UpdateAmmo(tankController.CurrentAmmo);
    }
    void SetAmmo(int value)
    {
        UIManager.Instance.UpdateAmmo(value);
    }
}
