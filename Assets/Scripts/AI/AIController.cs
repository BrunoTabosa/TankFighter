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

    public void Awake()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        string prefab = DataManager.Instance.GetRandomTank();
        GameObject tankGO = PhotonNetwork.Instantiate(prefab, transform.position, Quaternion.identity);

        Init(tankGO.GetComponent<TankController>());
        
    }
    public void Init(TankController tankController)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        
        TankController = tankController;
        transform.parent = TankController.transform;

        StateMachine = new StateMachine(this);
    }

    public void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        StateMachine.currentState?.Update();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if (collision.tag == Tags.Player)
        {
            //EnemyInSensorRange
            //Current State handles what to do
            StateMachine.currentState.TargetInRange(collision.GetComponent<TankController>());
        }
    }
}
