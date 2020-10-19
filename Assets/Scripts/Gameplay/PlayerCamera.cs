using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : SingletonLocal<PlayerCamera>
{
    public Transform target;

    void Awake()
    {
        SetInstance(this);
    }

    void Update()
    {
        if (target == null) return;
        transform.position = target.position + new Vector3(0, 0, -5);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
