using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField]
    private GameObject Tank;
    [SerializeField]
    private GameObject Gun;

    public float MoveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
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


}
