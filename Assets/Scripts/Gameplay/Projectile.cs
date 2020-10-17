using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    ProjectileManager pm;


    public void Setup(ProjectileManager manager)
    {
        pm = manager;
    }

    void ProjectileDestroy()
    {
        pm.OnProjectileDestroy(this);
    }

}
