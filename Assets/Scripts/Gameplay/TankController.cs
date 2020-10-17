using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TankController : MonoBehaviourPun
{
    [SerializeField]
    private GameObject Tank;
    [SerializeField]
    private GameObject Gun;

    [SerializeField]
    private ProjectileManager ProjectileManager;

    public float MoveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        ProjectileManager.Init(2);
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
        transform.Translate(direction * MoveSpeed);
    }

    public void Shoot()
    {
        GameObject go = ProjectileManager.RequestProjectile();
        go.SetActive(true);
        go.transform.rotation = Tank.transform.rotation;
    }
}
