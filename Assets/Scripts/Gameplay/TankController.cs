using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class TankController : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    private GameObject Tank;
    [SerializeField]
    private GameObject Gun;
    [SerializeField]
    private ProjectileManager ProjectileManager;


    //Actions
    public Action OnTankDestroyed;
    public Action<int, int> OnTankHealthChanged;
    public Action OnEnemyDestroyed;
    public Action<Destructable> OnDestructableDestroy;
    public Action OnShotFired;

    public TankStats stats;
    public int CurrentAmmo;

    int health;
    float curCooldown;


    // Start is called before the first frame update
    void Start()
    {
        ProjectileManager.Init(5);
        health = stats.MaximumHealth;
        CurrentAmmo = stats.MaximumAmmo;
        OnDestructableDestroy += OnDestructableDestroy_Handler;
        curCooldown = stats.ShotCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (curCooldown <= 0)
            {
                curCooldown = 0;
                return;
            }
            curCooldown -= Time.deltaTime;
        }
    }

    public void AimAt(Vector3 target)
    {
        float AngleRad = Mathf.Atan2(target.y - this.transform.position.y, target.x - this.transform.position.x);

        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        Tank.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }
    public void MoveTo(Vector3 direction)
    {
        transform.Translate(direction * stats.MovementSpeed * Time.deltaTime);
    }

    [PunRPC]
    public void Shoot()
    {
        //if (!photonView.IsMine) return;
        if (CurrentAmmo <= 0 || !CanShoot()) return;

        Projectile projectile = ProjectileManager.RequestProjectile();
        projectile.gameObject.SetActive(true);
        projectile.transform.rotation = Tank.transform.rotation;
        projectile.Shoot(this);
        CurrentAmmo--;
        curCooldown = stats.ShotCooldown;
        OnShotFired?.Invoke();

    }


    [PunRPC]
    public void SetStats(TankStats newStats)
    {
        stats = newStats;
        health = stats.MaximumHealth;
    }

    public void HitAndCheckDeath(int damage, TankController causer)
    {
        health = Mathf.Max(0, health - damage);

        OnTankHealthChanged(health, stats.MaximumHealth);

        if (health == 0)
        {
            //tank destroyed
            if (photonView.IsMine)
            {
                OnTankDestroyed?.Invoke();
                PhotonNetwork.Destroy(this.gameObject);
            }
            else
            {
                causer.OnEnemyDestroyed();
            }
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.Player)
        {
            //Hit another tank.
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.Bullet)
        {
            //hit by projectile            
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (int)stream.ReceiveNext();
        }
    }

    void OnDestructableDestroy_Handler(Destructable destructable)
    {
        if (photonView.IsMine)
        {
            //CurrentAmmo += destructable.AmmoReward;
        }
    }

    public bool CanShoot()
    {
        return curCooldown == 0;
    }
}
