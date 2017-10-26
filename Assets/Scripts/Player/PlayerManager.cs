using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class used to manage the player
public class PlayerManager : MonoBehaviour
{
    public int startHealth = 100;
    public int currentHealth;

    // public Slider healthSlider;

    public float flashSpeed;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerUse playerUse;
    PlayerSound playerSound;
    Renderer playerRenderer;

    bool isDead;
    bool damaged;

    // Use this for initialization
    void Awake()
    {
        currentHealth = startHealth;

        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerUse = GetComponent<PlayerUse>();
        playerSound = GetComponent<PlayerSound>();
        playerRenderer = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (damaged)
        {
            playerRenderer.material.color = flashColor;
        }
        else
        {
            // Transition the color back to normal
            playerRenderer.material.color = Color.Lerp(playerRenderer.material.color, Color.clear, flashSpeed * Time.deltaTime);
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

        // Play player hit sound
        playerSound.playHitSound();

        // Check if the player is dead
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        // playerUse.DisableEffects();
        // anim.SetTrigger("Die");

        // playerAudio.clip = deathClip;
        // playerAudio.Play();

        playerMovement.enabled = false;
        playerUse.enabled = false;
    }
}
