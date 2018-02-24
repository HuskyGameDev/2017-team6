using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public Weapon[] Weapons;
    public float enemyFov;

    Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    PlayerManager playerHealth;                  // Reference to the player's health.
    EnemyManager enemyHealth;                    // Reference to this enemy's health.
    EnemyMovement enemyMovement;
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer = 0f;                                // Timer for counting up to the next attack.

    private Transform weaponHolder;
    private Item currentEquipped;

    void Awake()
    {
        // Find the WeaponHolder object within the GameObject. fam.
        Transform[] transforms = this.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in transforms)
        {
            if (t.name == "WeaponHolder")
            {
                weaponHolder = t;
            }
        }

        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerManager>();
        enemyHealth = GetComponent<EnemyManager>();
        enemyMovement = GetComponent<EnemyMovement>();
        anim = GetComponent<Animator>();
        
        attachWeapon(Weapons[Random.Range(0, Weapons.Length)]);
    }

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            Debug.Log("ShouldAttack");
            // TODO: Disable movement in enemy movement
            enemyMovement.isAttacking = true;

            // ... the player is in range.
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == player)
        {
            // TODO: Enable movement in enemy movement
            enemyMovement.isAttacking = false;
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }

    void Update()
    {
        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (playerInRange && 
            enemyHealth.currentHealth > 0 && 
            IsLookingAtObject(transform, player.transform.position, enemyFov))
        {

            // TODO: Rotate enemy to follow player like in sentry
            Debug.Log("IM SHOOTNG");
            currentEquipped.Using();
        }

        // If the player has zero or less health...
        if (enemyHealth.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
            anim.SetTrigger("PlayerDead");
        }
    }


    void Attack()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if (playerHealth.currentHealth > 0)
        {
            currentEquipped.Using();
        }
    }

    // Attach the weapon to the enemy
    // NOTE: Kind of different from the player version xD
    void attachWeapon(Item weapon)
    {
        currentEquipped = Instantiate(
          weapon,
          weaponHolder.position,
          weaponHolder.rotation
        );

        currentEquipped.transform.parent = weaponHolder;
    }

    // Check to see if looker is looking at the targetPos
    // Shamelessly stolen from: https://answers.unity.com/questions/503934/chow-to-check-if-an-object-is-facing-another.html
    bool IsLookingAtObject(Transform looker, Vector3 targetPos, float FOVAngle)
    {
        // FOVAngle has to be less than 180
        float checkAngle = Mathf.Min(FOVAngle, 359.9999f) / 2; // divide by 2 isn't necessary, just a bit easier to understand when looking at the angles.

        float dot = Vector3.Dot(looker.forward, (targetPos - looker.position).normalized); // credit to fafase for this

        float viewAngle = (1 - dot) * 90; // convert the dot product value into a 180 degree representation (or *180 if you don't divide by 2 earlier)

        if (viewAngle <= checkAngle)
            return true;
        else
            return false;
    }
}
