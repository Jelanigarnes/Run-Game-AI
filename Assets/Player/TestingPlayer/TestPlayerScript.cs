using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestPlayerScript : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
        target = GameObject.Find("TestEnd").transform;
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
