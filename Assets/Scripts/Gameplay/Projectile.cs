using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviourPun
{
    int damage;
    float timeToLive;
    float bulletSpeed;

    TankController owner;
    ProjectileManager pm;
    bool isActive = false;

    private void Update()
    {
        if (!isActive) return;

        transform.Translate(Vector3.right * Time.deltaTime * bulletSpeed);

        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            ProjectileDestroy();
        }
    }

    public void Setup(ProjectileManager manager)
    {
        pm = manager;
    }

    public void Shoot(TankController owner)
    {
        this.owner = owner;
        damage = owner.stats.BulletDamage;
        timeToLive = owner.stats.BulletLifeTime;
        bulletSpeed = owner.stats.BulletSpeed;
        isActive = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.Player && collision.gameObject != owner.gameObject)
        {
            print("Projectile Hit Enemy");
            //Hit Player
            TankController tankController = collision.GetComponent<TankController>();
            tankController.HitAndCheckDeath(damage, owner);

            ProjectileDestroy();
        }
        else if (collision.tag == Tags.Desctructable)
        {
            Destructable destructable = collision.gameObject.GetComponent<Destructable>();
            if (owner.photonView.IsMine)
            {
                owner.CurrentAmmo += destructable.AmmoReward;

                owner.OnDestructableDestroy(destructable);
                destructable.Destroy();

                ProjectileDestroy();
            }
        }
    }


    public void ProjectileDestroy()
    {
        isActive = false;
        pm?.OnProjectileDestroy(this);
    }

}
