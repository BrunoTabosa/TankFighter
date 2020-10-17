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

    private List<GameObject> projectilesPool;
    private List<GameObject> projectilesInUse;

    

    public void Init(int initialCount)
    {
        projectilesPool = new List<GameObject>();
        projectilesInUse = new List<GameObject>();
        for (int i = 0; i < initialCount; i++)
        {
            CreateProjectile();
        }
    }

    public GameObject RequestProjectile()
    {
        GameObject ret;
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
        go = PhotonNetwork.Instantiate(projectilePrefab.name, this.transform.position, Quaternion.identity);
        go.SetActive(false);
        projectilesPool.Add(go);
    }

    public void OnProjectileDestroy(Projectile projectile)
    {
        GameObject go = projectile.gameObject;

        go.SetActive(false);
        go.transform.position = this.transform.position;

        projectilesInUse.Remove(go);
        projectilesPool.Add(go);
    }
}
