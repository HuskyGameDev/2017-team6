using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    
    [System.Serializable]
    public class ImpactSounds
    {
        public AudioClip metal;
        public AudioClip wood;
        public AudioClip concrete;
    }

    public int Damage;
    public int Speed;
    public float Range;

    public LayerMask canDamageLMask;

    public ImpactSounds impactSoundClips;

    private Vector3 startPos;
    private AudioSource playAudio;

    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        playAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update()
    {
        float distanceTraveled = Vector3.Distance(startPos, transform.position);
        if (distanceTraveled <= Range)
        {
            transform.Translate(Vector3.forward * (Speed * Time.deltaTime));
        }
        else
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

            switch (col.gameObject.tag)
            {
                case "Player":
                    AudioSource.PlayClipAtPoint(impactSoundClips.wood, transform.position);
                    col.GetComponent<PlayerManager>().applyDamage(Damage);
                    break;
                case "Enemy":
                    AudioSource.PlayClipAtPoint(impactSoundClips.metal, transform.position);
                    col.GetComponentInParent<EnemyManager>().applyDamage(Damage);
                    break;
				case "RobotCart":
					AudioSource.PlayClipAtPoint(impactSoundClips.metal, transform.position);
					col.GetComponentInParent<EnemyManager>().applyDamage(Damage);
					break;

            }
            Destroy(gameObject);
        }
        else if (colLayer == LayerMask.NameToLayer("Obstacle"))
        {
            AudioSource.PlayClipAtPoint(impactSoundClips.concrete, transform.position);
            Destroy(gameObject);
        }
        // TODO: play hit sound
    }

    public void InheritWeaponValues(int damage, int speed, float range)
    {
        Damage = damage;
        Speed = speed;
        Range = range;
    }
}
