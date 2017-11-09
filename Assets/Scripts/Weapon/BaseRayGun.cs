using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRayGun : Item
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

    [Header("GameObjects/Resources")]
    public Light gunLight;                          // Reference to the light component.
    public AudioClip[] gunShotSFX;
    public AudioClip reloadSFX;

    [Header("Fire Variance Attributes")]
    // Scale of the circle
    public float radius = 2.0f;
    // Used to set the distance of the circle
    public float z = 10f;

    GameObject player;
    GameObject gun;

    Ray shootRay;                                   // A ray from the gun end forwards.
    RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.

    ParticleSystem gunParticles;                    // Reference to the particle system.
    LineRenderer gunLine;                           // Reference to the line renderer.
    AudioSource gunAudio;                           // Reference to the audio source.
    AudioSource reloadAudio;                        // Reference to the reload audio source.

    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
    bool timerRunning = false;                      // timer begins at this value

    private int ammo;
    private bool reloading = false;
    private float nextTimeToFire;                   // Timer to keep track of fire rate

    // Use this for initialization
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

    public override void Using()
    {
        nextTimeToFire += Time.deltaTime;

        if (nextTimeToFire >= weaponInfo.timeBetweenBullets &&
            reloading == false &&
            ammo > 0)
        {
            Debug.Log("HAGJDJG");

            // Play weapon fire audio
            int hitSoundID = Mathf.CeilToInt(UnityEngine.Random.Range(0, gunShotSFX.Length));
            gunAudio.PlayOneShot(gunShotSFX[hitSoundID], 0.4f);

            ShootRay();
        } else if (ammo <= 0)
        {
            // Play empty sound
        }
    }

    public override void Reloading()
    {
        throw new NotImplementedException();
    }

    void ShootRay()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitCircle * radius;
        randomDirection.z = z;

        randomDirection = transform.TransformDirection(randomDirection.normalized);

        // Enable the light.
        gunLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = transform.position;
        shootRay.direction = randomDirection;

        Debug.DrawRay(transform.position, randomDirection);

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

        // Enable the light.
        gunLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        // Reset variables for next fire
        nextTimeToFire = 0f;
        ammo--;
    }
}
