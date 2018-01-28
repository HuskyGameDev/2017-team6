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

	// Gives the point value of the enemy
	int PointValue()
	{
		return 10;
	}

    // Kill the Enemy
    void Death()
    {
        isDead = true;

        // playerUse.DisableEffects();
        // anim.SetTrigger("Die");
		GameObject.Find ("Player").GetComponent<PlayerManager>().AddPoints(PointValue());
        // playerAudio.clip = deathClip;
        // playerAudio.Play();

        Destroy(gameObject);

        enemyMovement.enabled = false;
    }
}