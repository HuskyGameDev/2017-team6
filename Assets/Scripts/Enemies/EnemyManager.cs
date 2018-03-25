using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : UnitManager {

	public int pointValue = 10;

    public float flashSpeed;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    AudioSource enemyAudio;
    EnemyMovement enemyMovement;
	EnemyAttack enemyAttack;
    Renderer enemyRenderer;

	public GameObject[] itemdrops = new GameObject[3];
	public GameObject[] resourcedrops = new GameObject[3];

    
    Color originalColor;

    // Use this for initialization
    void Awake()
    {
        currentHealth = maxHealth;

        enemyAudio = GetComponent<AudioSource>();
        enemyMovement = GetComponent<EnemyMovement>();
		enemyAttack = GetComponent<EnemyAttack>();
        enemyRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        originalColor = enemyRenderer.material.color;
    }

    // Update is called once per frame
    void Update() {
        if (damaged)
        {
            enemyRenderer.material.color = flashColor;
        }
        else
        {
            // Transition the color back to normal
            enemyRenderer.material.color = Color.Lerp(enemyRenderer.material.color, originalColor, flashSpeed * Time.deltaTime);
        }

        // Reset the damage
        damaged = false;
    }

    public override void applyDamage(int damage)
    {
        // Set damage to be true so we can show it
        damaged = true;

        // Apply the damage
        currentHealth = currentHealth - damage;

        // Check if the player is dead
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

	// Gives the point value of the enemy to the player
	void GivePoints()
	{
		GameObject.Find ("Player").GetComponent<PlayerManager>().AddPoints(pointValue);
	}

    // Kill the Enemy
    protected override void Death()
    {
        isDead = true;


		//create a randomly assigned number
		var droproll = Random.Range(1,10);
		//if the roll on death of the enemy matches the droproll value, drop scrap, energy, wires, and the gun of the enemy
		if (droproll == 2) {
			Vector3 position = transform.position;
			foreach (GameObject item in itemdrops ){
				if (item != null) {
					Vector3 spawnPos = new Vector3 (position.x, 0, position.y);
					Instantiate(item, spawnPos , Quaternion.identity);
				}
			}
		}
		if (droproll == 3) {
			Vector3 position = transform.position;
			foreach (GameObject item in resourcedrops ){
				if (item != null) {
					Vector3 spawnPos = new Vector3 (position.x, 0, position.y);
					Instantiate(item, spawnPos , Quaternion.identity);
				}
			}
		}

        // playerUse.DisableEffects();
        // anim.SetTrigger("Die");
		GivePoints();
        // playerAudio.clip = deathClip;
        // playerAudio.Play();

        Destroy(gameObject);

        enemyMovement.enabled = false;
    }

	public override void removeHeldItem()
	{
		enemyAttack.removeHeldItem ();
	}
}
