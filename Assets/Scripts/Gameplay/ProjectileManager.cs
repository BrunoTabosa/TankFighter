using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Implements a Projectile Pooling
public class ProjectileManager : MonoBehaviourPun
{
    [SerializeField]
    private GameObject projectilePrefab;

    private List<Projectile> projectilesPool;
    private List<Projectile> projectilesInUse;

    public void Init(int initialCount)
    {
        projectilesPool = new List<Projectile>();
        projectilesInUse = new List<Projectile>();
        for (int i = 0; i < initialCount; i++)
        {
            CreateProjectile();
        }
    }

    public Projectile RequestProjectile()
    {
        Projectile ret;
        if(projectilesPool.Count <= 0)
        {
            CreateProjectile();
        }

        ret = projectilesPool[0];
        
        projectilesPool.Remove(ret);
        projectilesInUse.Add(ret);
        ret.transform.position = this.transform.position;

        return ret;
    }

    void CreateProjectile()
    {
        GameObject go;
        //go = PhotonNetwork.Instantiate(projectilePrefab.name, this.transform.position, Quaternion.identity);
        go = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);
        go.SetActive(false);
        
        Projectile projectile = go.GetComponent<Projectile>();
        projectile.Setup(this);
        
        projectilesPool.Add(projectile);
    }


    public void OnProjectileDestroy(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.transform.position = this.transform.position;
        projectilesInUse.Remove(projectile);
        projectilesPool.Add(projectile);
    }
}
