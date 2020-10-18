﻿using Photon.Pun;
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
        if(timeToLive <= 0)
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
        if(collision.tag == Tags.Player && collision.gameObject != owner.gameObject)
        {
            //Hit Player
            TankController tankController = collision.GetComponent<TankController>();
            tankController.BulletHit(damage);
            ProjectileDestroy();
        }
    }

    
    public void ProjectileDestroy()
    {
        isActive = false;
        pm.OnProjectileDestroy(this);
    }

}