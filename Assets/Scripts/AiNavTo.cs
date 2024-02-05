using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiNavTo : MonoBehaviour
{
    public Transform destination;
    public NavMeshAgent agent;

    private void Start()
    {
        agent.SetDestination(destination.position);
    }

}
