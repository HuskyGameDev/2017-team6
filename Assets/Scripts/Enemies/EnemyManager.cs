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


		//create a randomly assigned number from 1-20, and if it matches the predetermined values, that enemy will drop loot.
		//health is twice as rare as regular scrap and such, to preserve its value
		var droproll = Random.Range(1, 20);
		if (droproll == 1) {
			Vector3 position = transform.position;
			foreach (GameObject item in itemdrops ){
				if (item != null) {
					Vector3 spawnPos = new Vector3 (position.x + Random.Range(-1,1), 1, position.z + Random.Range(-1,1));
					Instantiate(item, spawnPos , Quaternion.identity);
				}
			}
		}
		if (droproll == 2||droproll == 3) {
			Vector3 position = transform.position;
			foreach (GameObject item in resourcedrops ){
				if (item != null) {
					Vector3 spawnPos = new Vector3 (position.x + Random.Range(-1,1), .2f, position.z + Random.Range(-1,1));
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