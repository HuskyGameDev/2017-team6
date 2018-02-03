using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;

    public float flashSpeed;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    AudioSource enemyAudio;
    EnemyMovement enemyMovement;
    Renderer enemyRenderer;

	public GameObject[] itemdrops = new GameObject[3];
	public GameObject[] resourcedrops = new GameObject[3];

    bool isDead;
    bool damaged;
    Color originalColor;

    // Use this for initialization
    void Awake()
    {
        currentHealth = startingHealth;

        enemyAudio = GetComponent<AudioSource>();
        enemyMovement = GetComponent<EnemyMovement>();
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

    public void applyDamage(int damage)
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

    // Kill the Enemy
    void Death()
    {
        isDead = true;


		//create a randomly assigned number
		var droproll = Random.Range(2,3);
		//if the roll on death of the enemy matches the droproll value, drop scrap, energy, wires, and the gun of the enemy
		if (droproll == 2) {
			Vector3 position = transform.position;
			foreach (GameObject item in itemdrops ){
				if (item != null) {
					Vector3 spawnPos = new Vector3 (position.x, 0, position.z);
					Instantiate(item, spawnPos , Quaternion.identity);
				}
			}
		}
		if (droproll == 3) {
			Vector3 position = transform.position;
			foreach (GameObject item in resourcedrops ){
				if (item != null) {
					Vector3 spawnPos = new Vector3 (position.x, 0, position.z);
					Instantiate(item, spawnPos , Quaternion.identity);
				}
			}
		}
        // playerUse.DisableEffects();
        // anim.SetTrigger("Die");

        // playerAudio.clip = deathClip;
        // playerAudio.Play();

        Destroy(gameObject);

        enemyMovement.enabled = false;
    }
}