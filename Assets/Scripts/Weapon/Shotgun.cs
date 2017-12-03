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

	// Inherited method for Using the weapon
	public override void Using()
	{
		nextTimeToFire += Time.deltaTime;

		if (nextTimeToFire >= weaponInfo.timeBetweenBullets &&
			reloading == false &&
			ammo > 0)
		{
			// Play weapon fire audio
			int hitSoundID = Mathf.CeilToInt(UnityEngine.Random.Range(0, gunShotSFX.Length));
			gunAudio.PlayOneShot(gunShotSFX[hitSoundID], 0.4f);
			for (int i = 0; i < numberOfShots; ++i) {
				ShootProjectile();
			}

			nextTimeToFire = 0f;
			ammo--;
		}
		else if (nextTimeToFire >= weaponInfo.timeBetweenBullets && 
			reloading == false && 
			ammo <= 0)
		{
			gunAudio.PlayOneShot(gunEmpty, 0.4f);
			nextTimeToFire = 0f;
			// Play empty sound
		}
		else
		{
			DisableEffects();
		}
	}
}
