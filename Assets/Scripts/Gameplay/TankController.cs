using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TankController : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    private GameObject Tank;
    [SerializeField]
    private GameObject Gun;
    [SerializeField]
    private ProjectileManager ProjectileManager;
  

    public TankStats stats;

    private int health;
    public int Health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ProjectileManager.Init(2);
        health = stats.MaximumHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AimAt(Vector3 target)
    {
        float AngleRad = Mathf.Atan2(target.y - this.transform.position.y, target.x - this.transform.position.x);

        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        Tank.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }
    public void MoveTo(Vector3 direction)
    {
        transform.Translate(direction * stats.MovementSpeed);
    }

    [PunRPC]
    public void Shoot()
    {
        Projectile projectile = ProjectileManager.RequestProjectile();
        projectile.gameObject.SetActive(true);
        projectile.transform.rotation = Tank.transform.rotation;
        projectile.Shoot(this);
    }

    [PunRPC]
    public void SetStats(TankStats newStats)
    {
        stats = newStats;
        health = stats.MaximumHealth;
    }



    public void BulletHit(int damage)
    {
        print($"{gameObject.name} received {damage} points of damage");
        health = Mathf.Max(0, health - damage);

        if (health == 0)
        {
            //tank destroyed
            if(photonView.IsMine)
            {
                PhotonNetwork.Destroy(this.gameObject);
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
        if(stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (int)stream.ReceiveNext();
        }
    }
}
