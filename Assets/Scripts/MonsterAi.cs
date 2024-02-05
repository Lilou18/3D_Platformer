using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAi : MonoBehaviour
{
    [Range(0.5f,50)]
    public float detecDistance = 3; // Distance where the monster will detect the player
    public Transform[] points;
    NavMeshAgent agent;
    int destinationIndex = 0;
    Transform player;
    float runSpped = 2;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        if(agent != null)
        {
            agent.destination = points[destinationIndex].position;
        }       
    }

    private void Update()
    {
        Walk();
        SearchPlayer();
        SetMobSize();
    }

    public void SetMobSize()
    {
        if(Vector3.Distance(transform.position, player.position) <= detecDistance + 2)
        {
            iTween.ScaleTo(gameObject, Vector3.one, 0.5f);
        }
    }

    public void SearchPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if(distanceToPlayer <= detecDistance)
        {
            // The player is detected by the ennemy
            agent.destination = player.position;
            agent.speed = runSpped;
        }
        else
        {
            agent.destination = points[destinationIndex].position;
            agent.speed = 1.5f;
        }
    }

    public void Walk()
    {
        float dist = agent.remainingDistance;
        if (dist <= 0.05f)
        {
            // Potentiellement utiliser Random.Range pour qu,il ne puisse pas prédire le déplacement
            destinationIndex++;
            if (destinationIndex > points.Length - 1)
            {
                destinationIndex = 0;
            }
            agent.destination = points[destinationIndex].position;
        }
    }

    // Draw a sphere around the ennemy and this represent his vision
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detecDistance);
    }

}
