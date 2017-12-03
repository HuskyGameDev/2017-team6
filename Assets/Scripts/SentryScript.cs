using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryScript : MonoBehaviour {

    public float attackRange = 10f;
    public float RotationSpeed = 1f;
    public GameObject target;
    private Quaternion lookRotation;
    private Vector3 direction;

    // Use this for initialization
    void Start () {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSeePlayer())
        {
            return;
        }

        //transform.LookAt(target.transform);

        direction = (target.transform.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        lookRotation = Quaternion.LookRotation(direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);

    }

    //checks if the player is within range
    bool canSeePlayer()
    {
        if(Vector3.Distance(transform.position,target.transform.position) > attackRange)
        {
            //Debug.Log("false");
            return false;
        }
        
        RaycastHit hit;
        if (Physics.Linecast(transform.position, target.transform.position, out hit))
            return hit.transform == target.transform;

        Debug.Log("false");
        return false;
    }

}
