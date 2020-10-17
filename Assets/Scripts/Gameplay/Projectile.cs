using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    ProjectileManager pm;

    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * 2);
    }

    public void Setup(ProjectileManager manager)
    {
        pm = manager;
    }

    void ProjectileDestroy()
    {
        pm.OnProjectileDestroy(this);
    }

}
