using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    Transform player;
    public NavMeshAgent nav;
    float speed = 3f;
    public bool isAttacking = true;
    public float rotationSpeed = 1f;

    private Quaternion lookRotation;
    private Vector3 direction;

    private void Awake()
    {
        Transform target = GameObject.Find("Player").transform;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        nav.speed = speed;
    }

    private void Update()
    {
        if (isAttacking)
        {
            direction = (player.position - transform.position).normalized;

            direction.y = 0.0f;

            //create the rotation we need to be in to look at the target
            lookRotation = Quaternion.LookRotation(direction);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            nav.isStopped = true;
        } else
        {
            nav.isStopped = false;
            nav.SetDestination(player.position);
        }
    }

    
}
