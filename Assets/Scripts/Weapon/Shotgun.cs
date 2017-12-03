using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : ProjectileWeapon
{
	public int numberOfShots;

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

    // Override for the Using Script
    public override void Using()
    {
        if (Time.time > nextTimeToFire &&
            reloading == false &&
            ammo > 0)
        {
            // Play weapon fire audio
            int hitSoundID = Mathf.CeilToInt(UnityEngine.Random.Range(0, gunShotSFX.Length));
            gunAudio.PlayOneShot(gunShotSFX[hitSoundID], 0.2f);

            // Stop the particles from playing if they were, then start the particles.
            gunParticles.Stop();
            gunParticles.Play();

            gunLight.enabled = true;

            // Shoots the given number of pellets
            for (int i = 0; i < numberOfShots; ++i)
            {
                ShootProjectile();
            }

            nextTimeToFire = Time.time + weaponInfo.timeBetweenBullets;
            ammo--;
        }
        else if (Time.time > nextTimeToFire &&
                 reloading == false &&
                 ammo <= 0)
        {
            // Play empty sound
            gunAudio.PlayOneShot(gunEmpty, 0.4f);
            nextTimeToFire = Time.time + weaponInfo.timeBetweenBullets;
            DisableEffects();
        }
        else
        {
            DisableEffects();
        }
    }

    protected override void ShootProjectile()
    {
        Vector3 spread = new Vector3(
            UnityEngine.Random.Range(-1, 1),
            UnityEngine.Random.Range(-1, 1),
            UnityEngine.Random.Range(-1, 1)
        ).normalized * radius; 

        // Fire the Projectile
        GameObject tmpProj = Instantiate(projectile);
        tmpProj.transform.position = firepoint.position;
        tmpProj.transform.rotation = Quaternion.Euler(spread) * firepoint.rotation;
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
}
