using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ChasingState : State
{
    public ChasingState(AIController aiController) : base(aiController)
    {

    }
    public override void Enter()
    {
    
    }

    public override void Update()
    {
        if (Vector3.Distance(AIController.Enemy.transform.position, AIController.TankController.transform.position) <= 2)
        {
            AIController.TankController.photonView.RPC("Shoot", Photon.Pun.RpcTarget.All);
        }
        AIController.TankController.AimAt(AIController.Enemy.transform.position);
        AIController.TankController.MoveTo(FindMoveDirection());
    }

    public override void Exit()
    {
    }

    public override void TargetInRange()
    {
       
    }

    private Vector3 FindMoveTarget()
    {
        Vector2 min = DataManager.Instance.RoomConfiguration.StartingPositionMin;
        Vector2 max = DataManager.Instance.RoomConfiguration.StartingPositionMax;

        Vector3 target =
            new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0f);

        return target;
    }

    private Vector3 FindMoveDirection()
    {

        Vector3 dir = (AIController.Enemy.transform.position - AIController.TankController.transform.position).normalized;

        return dir;
    }
}
