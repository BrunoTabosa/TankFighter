using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MovingState : State
{
    public MovingState(AIController aiController) : base(aiController)
    {

    }
    public override void Enter()
    {
        AIController.moveTarget = FindMoveTarget();
        AIController.TankController.AimAt(AIController.moveTarget);
    }

    public override void Update()
    {
        AIController.TankController.MoveTo(FindMoveDirection());
        if (Vector3.Distance(AIController.moveTarget, AIController.TankController.transform.position) <= 1)
        {
            AIController.StateMachine.SetState(new MovingState(AIController));
        }
        
    }

    public override void Exit()
    {
    }

    public override void TargetInRange(TankController enemy)
    {
        AIController.Enemy = enemy;
        AIController.StateMachine.SetState(new ChasingState(AIController));
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

        Vector3 dir = (AIController.moveTarget - AIController.TankController.transform.position).normalized;

        return dir;
    }
}
