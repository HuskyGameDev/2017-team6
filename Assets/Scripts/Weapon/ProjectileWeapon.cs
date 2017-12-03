using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Item
{

    [System.Serializable]
    public class WeaponInfo
    {
        public int damagePerShot = 20;                  // The damage inflicted by each bullet.
        public float timeBetweenBullets = 0.15f;        // The time between each shot.
        public float range = 20f;                       // The distance the gun can fire.
        public int clipSize = 30;
        public float reloadSpeed = 5.0f;                // time reached to do something
    }

    // Weapon Info Class
    public WeaponInfo weaponInfo;

    // Information about the projectiles
    [Header("Projectile")]
    public GameObject projectile;

    [Header("GameObjects/Resources")]
    public Light gunLight;                          // Reference to the light component.
    public AudioClip[] gunShotSFX;
    public AudioClip reloadSFX;
	public AudioClip gunEmpty;

    [Header("Fire Variance Attributes")]
    // Scale of the circle
    public float radius = 2.0f;
    // Used to set the distance of the circle
    public float z = 10f;

    protected Transform firepoint;                            // Point where bullets come out of in our gun

    protected int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.

    protected ParticleSystem gunParticles;                    // Reference to the particle system.
    protected AudioSource gunAudio;                           // Reference to the audio source.

    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
    bool timerRunning = false;                      // timer begins at this value

	protected int ammo;
    protected bool reloading = false;
    protected float nextTimeToFire = 0.0f;                   // Timer to keep track of fire rate

    // Use this for initialization
    void Awake()
    {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask("Shootable");

        // Set up the references.
        gunParticles = GetComponent<ParticleSystem>();
        gunAudio = GetComponent<AudioSource>();

        // Find the fire point
        Transform[] transforms = this.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in transforms)
        {
            if (t.name == "FirePoint")
            {
                firepoint = t;
            }
        }

        // Initialize the ammo within our weapon
        ammo = weaponInfo.clipSize;
    }

    // Inherited method for Using the weapon
    public override void Using()
    {
        if (Time.time > nextTimeToFire &&
            reloading == false &&
            ammo > 0)
        {
            // Play weapon fire audio
            int hitSoundID = Mathf.CeilToInt(UnityEngine.Random.Range(0, gunShotSFX.Length));
            gunAudio.PlayOneShot(gunShotSFX[hitSoundID], 0.4f);

            ShootProjectile();

            nextTimeToFire = Time.time + weaponInfo.timeBetweenBullets;
        }
		else if (nextTimeToFire >= weaponInfo.timeBetweenBullets && 
				 reloading == false && 
				 ammo <= 0)
        {
			gunAudio.PlayOneShot(gunEmpty, 0.4f);
			nextTimeToFire = 0f;
            DisableEffects();
            // Play empty sound
        }
        else
        {
            DisableEffects();
        }
    }

    // Inherited method for reloading
    public override void Reloading()
    {
        if (!reloading)
        {
            StartCoroutine(Reload());
        }

        // TODO: Update the GUI
    }

    // IEnumerator for handling reloads
    IEnumerator Reload()
    {
        reloading = true;

        // Play reload sound
        gunAudio.PlayOneShot(reloadSFX, 0.3f);

        // TODO: Play reload animation

        yield return new WaitForSeconds(weaponInfo.reloadSpeed);

        ammo = weaponInfo.clipSize;
        reloading = false;
    }

    // Fire the Ray
    protected virtual void ShootProjectile()
    {
        Vector3 spread = new Vector3(
            UnityEngine.Random.Range(-1, 1), 
            UnityEngine.Random.Range(-1, 1), 
            UnityEngine.Random.Range(-1, 1)
        ).normalized * radius;

        // Enable the light.
        gunLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        // Fire the Projectile
		Debug.Log("WHTKSFJ");
        GameObject tmpProj = Instantiate(projectile);
        tmpProj.transform.position = firepoint.position;
        tmpProj.transform.rotation = Quaternion.Euler(spread) * firepoint.rotation;

        // Enable the light.
        gunLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        // Reset variables for next fire
        nextTimeToFire = 0f;
        ammo--;
    }

    public void DisableEffects()
    {
        gunLight.enabled = false;
    }
}
