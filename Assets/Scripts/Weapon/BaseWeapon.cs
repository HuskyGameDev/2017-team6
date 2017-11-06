using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base weapon class containing all the information for a gun
public class BaseWeapon : Item {

    [System.Serializable]
    public class WeaponInfo
    {
        public int damagePerShot = 20;                  // The damage inflicted by each bullet.
        public float timeBetweenBullets = 0.15f;        // The time between each shot.
        public float range = 20f;                       // The distance the gun can fire.
        public int clipSize = 30;
        public Light gunLight;                          // Reference to the light component.
        public AudioClip[] gunShotSFX;
        public AudioClip reloadSFX;
    }

    float nextTimeToFire;                           // A timer to determine when to fire.
    Ray shootRay;                                   // A ray from the gun end forwards.
    RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
    ParticleSystem gunParticles;                    // Reference to the particle system.
    LineRenderer gunLine;                           // Reference to the line renderer.
    AudioSource gunAudio;                           // Reference to the audio source.
    AudioSource reloadAudio;                        // Reference to the reload audio source.
    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
    private bool reloading = false;
    float reloadTimer = 0f;
    bool timerRunning = false;                      //timer begins at this value
    float reloadSpeed = 5.0f;                       //time reached to do something

    public WeaponInfo weaponInfo;

    private int ammo;

    void Awake()
    {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask("Shootable");

        // Set up the references.
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();

        ammo = weaponInfo.clipSize;
    }

    // Script for firing a weapon
    public override void Using() {
        nextTimeToFire += Time.deltaTime;

        if (nextTimeToFire >= weaponInfo.timeBetweenBullets &&
            reloading == false &&
            ammo > 0) {

            // Play weapon fire audio
            int hitSoundID = Mathf.CeilToInt(Random.Range(0, weaponInfo.gunShotSFX.Length));
            gunAudio.PlayOneShot(weaponInfo.gunShotSFX[hitSoundID], 0.4f);

            Shoot();
        } else {
            DisableEffects();
        }
    }


    public override void Reloading()
    {
        if (reloading)
        {
            return;
        }
        //do the reloading process
        gunAudio.PlayOneShot(weaponInfo.reloadSFX, 0.4f);
        reloading = true;

        timerRunning = true;

        if (timerRunning)
        {
            ReloadTimer();
        }

        ammo = weaponInfo.clipSize;
        reloading = false;

        //play reloading sound
        //reloadAudio.Play();
    }


    void ReloadTimer()
    {
        reloadTimer += Time.deltaTime;
        if(reloadTimer > reloadSpeed)
        {
            reloadTimer = 0f;
            timerRunning = false;
        }
    }


    public void DisableEffects()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
        weaponInfo.gunLight.enabled = false;
    }


    void Shoot()
    {
        // Reset the timer.
        nextTimeToFire = 0f;
        ammo--;

        // Play the gun shot audioclip.
        //gunAudio.Play();

        // Enable the light.
        weaponInfo.gunLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay, out shootHit, weaponInfo.range, shootableMask))
        {

            print("shot object\n");

            // Set the second position of the line renderer to the point the raycast hit.
            gunLine.SetPosition(1, shootHit.point);
        }
        // If the raycast didn't hit anything on the shootable layer...
        else
        {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * weaponInfo.range);
        }
    }

}
