using System.Collections;
using UnityEngine;

public class HitscanWeapon : Weapon
{

    [Header("GameObjects/Resources")]
    public Light gunLight;                          // Reference to the light component.

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

        ammo = ClipSize;
    }

    // Inherited method for Using the weapon
    public override void Using()
    {

        if (Time.time > nextTimeToFire &&
            reloading == false &&
            ammo > 0)
        {
            // Play weapon fire audio
            int hitSoundID = Mathf.CeilToInt(UnityEngine.Random.Range(0, GunShotSFX.Length));
            gunAudio.PlayOneShot(GunShotSFX[hitSoundID], 0.4f);

            FireWeapon();

            nextTimeToFire = Time.time + TimeBetweenShots;
        }
        else if (ammo <= 0)
        {
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
        gunAudio.PlayOneShot(ReloadSFX, 0.3f);

        // TODO: Play reload animation

        yield return new WaitForSeconds(ReloadTime);

        ammo = ClipSize;
        reloading = false;
    }

    // Fire the Ray
    public override void FireWeapon()
    {
        // Adjust direction for weapon spread
        Vector3 randomDirection = UnityEngine.Random.insideUnitCircle * Radius;
        randomDirection.z = Z;

        randomDirection = gunLight.transform.TransformDirection(randomDirection.normalized);

        // Enable the light.
        gunLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = gunLight.transform.position;
        shootRay.direction = randomDirection;

        Debug.DrawRay(transform.position, randomDirection);

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay, out shootHit, Range, shootableMask))
        {
            print("Hit: " + shootHit.collider.name);

            switch(shootHit.collider.gameObject.tag)
            {
                case "Player":
                    shootHit.collider.gameObject.GetComponentInParent<PlayerManager>().applyDamage(Damage);
                    break;
                case "Enemy":
                    shootHit.collider.gameObject.GetComponentInParent<EnemyManager>().applyDamage(Damage);
                    break;
            }
            
            // Set the second position of the line renderer to the point the raycast hit.
            gunLine.SetPosition(1, shootHit.point);
        }
        // If the raycast didn't hit anything on the shootable layer...
        else
        {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * Range);
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

    public void DisableEffects()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}
