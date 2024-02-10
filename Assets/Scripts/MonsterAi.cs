using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAi : MonoBehaviour
{
    [Range(0.5f,50)] public float detecDistance = 3; // Distance where the monster will detect the player
    public Transform[] points; // Positions where the enemies will patrol
    NavMeshAgent agent;
    int destinationIndex = 0;
    Transform player;
    [SerializeField] float speed; // Speed of the enemy when it spots the player

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        if(agent != null)
        {
            agent.destination = points[destinationIndex].position; // Begin the patrol
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
        // When the player approches the enemy, it scales
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
            // The player is detected by the ennemy, which then attacks and pursues the player
            agent.destination = player.position;
            agent.speed = speed;
        }
        else
        {
            // The enemy doesn't see the player, so it continues its patrol
            agent.destination = points[destinationIndex].position;
            agent.speed = 1.5f;
        }
    }

    public void Walk()
    {
        // Patrol between the given points
        float dist = agent.remainingDistance;
        if (dist <= 0.05f)
        {            
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
