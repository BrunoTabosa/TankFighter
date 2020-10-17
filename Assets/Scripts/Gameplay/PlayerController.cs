using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private TankController tank;

    Camera camera;
    Vector3 direction;

    private void Awake()
    {
        if(camera == null)
            camera = Camera.main;
    }

    
    // Update is called once per frame
    void Update()
    {        
        tank.AimAt(camera.ScreenToWorldPoint(Input.mousePosition));

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        tank.MoveTo(direction * Time.deltaTime);
    }
}
