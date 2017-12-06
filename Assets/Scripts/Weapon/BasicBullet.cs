using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour {
    
    [System.Serializable]
    public class ImpactSounds
    {
        public AudioClip metal;
        public AudioClip wood;
        public AudioClip concrete;
    }

    public int damage;
    public LayerMask canDamageLMask;
    public float projectileSpeed;
    public float projectileRange;

    public ImpactSounds impactSoundClips;

    private Vector3 startPos;
    private AudioSource playAudio;

    // Use this for initialization
    void Start () {
        startPos = transform.position;
        playAudio = GetComponent<AudioSource>();
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

    void OnTriggerEnter(Collider col)
    {

        int colLayer = col.gameObject.layer;
        if (colLayer == LayerMask.NameToLayer("Shootable"))
        {
            Debug.Log("Damage Hit: " + col.gameObject.name);

            if (col.gameObject.tag == "Player")
            {

            }

            switch (col.gameObject.tag)
            {
                case "Player":
                    col.GetComponent<PlayerManager>().applyDamage(damage);
                    break;
                case "Enemy":
                    col.GetComponentInParent<EnemyManager>().applyDamage(damage);
                    break;
            }
            Destroy(gameObject);
        } else if (colLayer == LayerMask.NameToLayer("Obstacle"))
        {
            playAudio.PlayOneShot(impactSoundClips.concrete, 0.1f);
            Destroy(gameObject);
        }
        // TODO: play hit sound
    }
}
