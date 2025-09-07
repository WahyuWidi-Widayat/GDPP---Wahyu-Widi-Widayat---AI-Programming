using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using TMPro;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    public List<Transform> Waypoints = new List<Transform>();
    [SerializeField]
    public float ChaseDistance;
    [SerializeField]
    public Player Player;

    // State saat ini
    private BaseState _currentState;

    // State instances
    public PatrolState PatrolState = new PatrolState();
    public ChaseState ChaseState = new ChaseState();
    public RetreatState RetreatState = new RetreatState();

    [HideInInspector]
    public NavMeshAgent NavMeshAgent;
    public Animator Animator;
    [HideInInspector]





    private void Awake()
    {
        Animator = GetComponent<Animator>();
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

    public void Dead()
    {
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        
        if(_currentState != RetreatState)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().Dead();
            }
        }
    }
}
