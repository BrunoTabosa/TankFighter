using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviourPun
{
    public TankController TankController;
    public StateMachine StateMachine;

    public TankController Enemy;
    public Vector3 moveTarget;

    public void Start()
    {
        StateMachine = new StateMachine(this);
        transform.parent = TankController.transform;
    }

    public void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        StateMachine.currentState?.Update();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if(collision.tag == Tags.Player)
        {
            //EnemyInSensorRange
            Enemy = collision.GetComponent<TankController>();
            StateMachine.currentState.TargetInRange();
        }
    }
}
