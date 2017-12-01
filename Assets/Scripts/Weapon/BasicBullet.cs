using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour {

    public int damage;
    public LayerMask canDamageLMask;
    public float projectileSpeed;
    public float projectileRange;

    private Vector3 startPos;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
        Debug.Log(transform.forward);

    }
	
	// Update is called once per frame
	void Update () {
        float distanceTraveled = Vector3.Distance(startPos, transform.position);
        if (distanceTraveled <= projectileRange)
        {
            transform.Translate(Vector3.forward * (projectileSpeed * Time.deltaTime));
        } else
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        int colLayer = col.gameObject.layer;
        if (canDamageLMask == (canDamageLMask | (1 << colLayer)))
        {
            Debug.Log("Damage Hit: " + col.gameObject.name);
        } else {
            Debug.Log("No Damage Hit: " + col.gameObject.name);
        }
        // TODO: play hit sound
        Destroy(gameObject);
    }
}
