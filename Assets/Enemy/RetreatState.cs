using UnityEngine;

public class RetreatState : BaseState
{
    public override void EnterState(Enemy enemy)
    {
        Debug.Log("Entering Retreat State");
    }

    public override void UpdateState(Enemy enemy)
    {
         if (enemy.Player != null)
    {
        enemy.NavMeshAgent.destination = enemy.transform.position - enemy.Player.transform.position;
    }
    }

    public override void ExitState(Enemy enemy)
    {
        Debug.Log("Exiting Retreat State");
    }
}
