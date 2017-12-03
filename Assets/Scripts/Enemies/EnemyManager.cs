using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    int scrapValue = 10;
    public AudioClip deathClip;

    public float flashSpeed;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    
    AudioSource enemyAudio;
    EnemyMovement enemyMovement;
    Renderer enemyRenderer;
    CapsuleCollider capsuleCollider;

    bool isDead;
    bool isSinking;
    bool damaged;

    // Use this for initialization
    void Awake()
    {
        currentHealth = startingHealth;

        enemyAudio = GetComponent<AudioSource>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyRenderer = GetComponent<Renderer>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update() {
        if (isSinking)
        {
            //move the enemy down by the sinkSpeed per second.
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
        
        else
        {
            // Transition the color back to normal
            enemyRenderer.material.color = Color.Lerp(enemyRenderer.material.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damage
        damaged = false;
    }

    public void TakeDamage (int ammount)
    {
        if (isDead)
        {
            return;
        }

        enemyAudio.Play();

        currentHealth -= ammount;
        
        enemyRenderer.material.color = flashColor;

        // Check if the player is dead
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void applyDamage(int damage)
    {
        // Set damage to be true so we can show it
        damaged = true;

        // Apply the damage
        currentHealth = currentHealth - damage;

        
    }

    void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        // playerUse.DisableEffects();

        //enemyMovement.enabled = false;
    }

    public void StartSinking()
    {
        // Find and disable the Nav Mesh Agent.
        GetComponent<NavMeshAgent>().enabled = false;

        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        GetComponent<Rigidbody>().isKinematic = true;

        // The enemy should no sink.
        isSinking = true;

        //Drop Scrap
        //implement here

        // After 2 seconds destory the enemy.
        Destroy(gameObject, 2f);
    }
}