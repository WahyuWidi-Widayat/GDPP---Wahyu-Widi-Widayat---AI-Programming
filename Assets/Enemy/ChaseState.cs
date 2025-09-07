using UnityEngine;

public class ChaseState : BaseState
{
    public override void EnterState(Enemy enemy)
    {
        Debug.Log("Entering Chase State");
        // contoh: mulai kejar player
        enemy.Animator.SetTrigger("ChaseState");
    }

    public override void UpdateState(Enemy enemy)
    {
        if (enemy.Player == null)
        {
            Debug.LogWarning("Enemy has no player assigned!");
            return;
        }

        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.Player.transform.position);
        if (distanceToPlayer <= enemy.ChaseDistance)
        {
            // Kejar player
            enemy.NavMeshAgent.SetDestination(enemy.Player.transform.position);
        }
        else
        {
            // Jika terlalu jauh, kembali ke PatrolState
            enemy.SwitchState(enemy.PatrolState);
        }
    }
    public override void ExitState(Enemy enemy)
    {
        Debug.Log("Exiting Chase State");
    }
}
