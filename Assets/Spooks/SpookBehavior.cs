using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpookBehavior : MonoBehaviour
{
    private GameController gameController;
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private bool isChasing;
    [SerializeField]
    private Material GhostMaterial;
    private Transform Player;
    private Vector3 home;
    private Vector3 destination;
    private float distanceToTarget;
    private float patrolRadius = 10f;
    private List<Vector3> patrolPoints = new List<Vector3>();
    private int currentPatrolPointIndex;
    private float defaultSpeed;
    private bool isFleeing;

    public Shader Hologram;
    public Shader Toon;
    public bool IsFleeing
    {
        get { return isFleeing; } 
        set {  
            isFleeing = value;
            if (IsFleeing)
            {
                GhostMaterial.shader = Hologram;
                GhostMaterial.SetFloat("_RimPower", 8f);
            }
            else
                GhostMaterial.SetFloat("_RimPower", 0.5f);
        }
    }
    public bool IsChasing
    {
        get { return isChasing; }
        set { 
            isChasing = value;

            if (value)
            {
                navMeshAgent.speed = navMeshAgent.speed * 2;
                GhostMaterial.shader = Toon;
            }
            else
            {
                navMeshAgent.speed = defaultSpeed;
                GhostMaterial.shader = Hologram;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponents();

        isFleeing = false;

        switch (GameManager.Instance.Difficulty)
        {
            case GameManager.DifficultyLevel.Easy:
                patrolRadius = 15;
                navMeshAgent.speed = 3;
                break;
            case GameManager.DifficultyLevel.Normal:
                patrolRadius = 20;
                navMeshAgent.speed = 5;
                break;
            case GameManager.DifficultyLevel.Hard:
                patrolRadius = 25;
                navMeshAgent.speed = 8;
                break;
            default:
                break;

        }
        defaultSpeed = navMeshAgent.speed;

        // Generate a list of patrol points around the home position
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * (patrolRadius * 2);
            randomDirection += home;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1);
            patrolPoints.Add(hit.position);
        }
        Patrol();
    }

    private void GetComponents()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        home = GetComponent<Transform>().position;
        Renderer renderer = gameObject.GetComponentInChildren<Renderer>();
        GhostMaterial = renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGamePaused && !GameManager.Instance.IsDebuging)
        {
            if(navMeshAgent.isStopped)
                navMeshAgent.isStopped= false;

            //Check if player is close
            distanceToTarget = Vector3.Distance(transform.position, Player.position);

            if ((distanceToTarget < patrolRadius || isChasing) && !isFleeing)
                Chase();
            else if (navMeshAgent.remainingDistance < 1f && !IsChasing)
            {
                Patrol();
            }
        }
        else
            navMeshAgent.isStopped = true;
    }
    private void Chase()
    {
        if (IsChasing)
        {
            if (distanceToTarget < 50.0f)
            {
                destination = Player.position;
                navMeshAgent.SetDestination(destination);
            }
            else
            {
                navMeshAgent.SetDestination(home);
                IsChasing = false;
            }

        }
        else
        {
            destination = Player.position;
            navMeshAgent.SetDestination(destination);
            IsChasing = true;
            navMeshAgent.isStopped = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FlashLight")
        {
            isChasing = false;
            navMeshAgent.SetDestination(home);
            isFleeing = true;
        }
        else if (other.tag == "Player")
        {
            isChasing = false;
            other.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
            navMeshAgent.SetDestination(home);
        }
    }

    private void Patrol()
    {
        currentPatrolPointIndex = Random.Range(0, patrolPoints.Count);
        navMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex]);

        IsChasing = false;
        navMeshAgent.isStopped = false;
        isFleeing= false;
    }
}
