using UnityEngine;

public class PatrolState : BaseState
{
    private bool _isMoving;
    private Vector3 _destination;

    public override void EnterState(Enemy enemy)
    {
        _isMoving = false;
        enemy.Animator.SetTrigger("PatrolState");
    }

    public override void UpdateState(Enemy enemy)
    {
        if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) <= enemy.ChaseDistance)
        {
            enemy.SwitchState(enemy.ChaseState);
            return;
        }
        
        if (enemy.Waypoints == null || enemy.Waypoints.Count == 0)
        {
            Debug.LogWarning("Enemy has no waypoints assigned!");
            return;
        }

        if (!_isMoving)
        {
            _isMoving = true;

            int index = UnityEngine.Random.Range(0, enemy.Waypoints.Count);
            _destination = enemy.Waypoints[index].position;

            enemy.NavMeshAgent.SetDestination(_destination);
        }
        else
        {
            if (Vector3.Distance(enemy.transform.position, _destination) < 0.5f)
            {
                _isMoving = false;
            }
        }
    }

    public override void ExitState(Enemy enemy)
    {
        Debug.Log("Exiting Patrol State");
    }
}
