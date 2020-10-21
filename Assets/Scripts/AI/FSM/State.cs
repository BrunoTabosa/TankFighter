public class State
{
    protected AIController AIController;
    public State(AIController AIController)
    {
        this.AIController = AIController;
    }
    public virtual void Enter()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void TargetInRange() { }
}
