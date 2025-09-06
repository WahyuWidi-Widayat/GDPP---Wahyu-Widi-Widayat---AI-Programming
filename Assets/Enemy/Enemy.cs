using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;   

public class Enemy : MonoBehaviour
{
    [SerializeField] 
    public List<Transform> Waypoints = new List<Transform>();
    [SerializeField]
    public float ChaseDistance;
    [SerializeField]
    public  Player Player;

    // State saat ini
    private BaseState _currentState;

    // State instances
    public PatrolState PatrolState = new PatrolState();
    public ChaseState ChaseState = new ChaseState();
    public RetreatState RetreatState = new RetreatState();

    [HideInInspector] 
    public NavMeshAgent NavMeshAgent;



 
    private void Awake()
    {
        // Assign komponen NavMeshAgent
        NavMeshAgent = GetComponent<NavMeshAgent>();

        // Mulai dengan PatrolState
        SwitchState(PatrolState);
    }

    private void Update()
    {
        _currentState?.UpdateState(this);
    }
private void StartRetreating()
{
    SwitchState(RetreatState);
}

private void StopRetreating()
{
    SwitchState(PatrolState);
}
private void OnEnable()
{
    if (Player != null)
    {
        Player.OnPowerUpStart += StartRetreating;
        Player.OnPowerUpStop += StopRetreating;
    }
}

private void OnDisable()
{
    if (Player != null)
    {
        Player.OnPowerUpStart -= StartRetreating;
        Player.OnPowerUpStop -= StopRetreating;
    }
}


    /// <summary>
    /// Fungsi ganti state
    /// </summary>
    public void SwitchState(BaseState newState)
    {
        // Exit state lama
        _currentState?.ExitState(this);

        // Masuk ke state baru
        _currentState = newState;
        _currentState.EnterState(this);
    }
}
