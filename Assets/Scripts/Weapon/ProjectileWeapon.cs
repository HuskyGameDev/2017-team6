using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
	[Header("ItemStat Constants")]
	protected List<ItemStat> stats_Projectile = new List<ItemStat> {
		new ItemStat {name="Bullets/Shot",field="NumberOfProjectiles",baseVal=1,canUpgrade=true,increaseOnLv=true,increment=1,limit=10},
		new ItemStat {name="Bullet Speed",field="ProjectileSpeed",baseVal=20,canUpgrade=true,increaseOnLv=true,increment=2,limit=50}
	};

    [Header("Projectile Info")]
    public int NumberOfProjectiles = 1;
    public int ProjectileSpeed = 20;

    public GameObject Projectile;               // Reference to projectile game object

    [Header("Game Objects/Resources")]
    public Light GunLight;                      // Reference to the light component

    private Transform _firepoint;               // Point where bullets come out of in our gun

    private int _shootableMask;                 // A layer mask so the raycast only hits things on the shootable layer

	private int _ammo;
    private ParticleSystem _gunParticles;       // Reference to the particle system
    private AudioSource _gunAudio;              // Reference to the audio source

    //float effectsDisplayTime = 0.2f;            // The proportion of the timeBetweenBullets that the effects will display for
    //bool timerRunning = false;                  // timer begins at this value

	private float _reloadStart;
    private bool _reloading = false;
    private float _nextTimeToFire = 0.0f;       // Timer to keep track of fire rate

    // Use this for initialization
    void Awake()
    {
        // Create a layer mask for the Shootable layer.
        _shootableMask = LayerMask.GetMask("Shootable");

        // Set up the references.
        _gunParticles = GetComponent<ParticleSystem>();
        _gunAudio = GetComponent<AudioSource>();

        // Find the fire point
        _firepoint = transform.GetComponentsInChildren<Transform>()
            .FirstOrDefault(t => t.name == "FirePoint");

        // Initialize the ammo within our weapon
        Ammo = ClipSize;
    }

    // Inherited method for Using the weapon
	public override void Using(UnitManager parent)
    {
        if (Time.time > _nextTimeToFire &&
            _reloading == false &&
            Ammo > 0)
        {
            // Play weapon fire audio
            int hitSoundID = Mathf.CeilToInt(Random.Range(0, GunShotSFX.Length));
            _gunAudio.PlayOneShot(GunShotSFX[hitSoundID], 0.4f);

            FireWeapon();

            _nextTimeToFire = Time.time + TimeBetweenShots;
        }
        else if (_nextTimeToFire >= TimeBetweenShots &&
                 _reloading == false &&
                 Ammo <= 0)
        {
            _gunAudio.PlayOneShot(GunEmptySFX, 0.4f);
            _nextTimeToFire = 0f;
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
		if (!_reloading && Ammo < ClipSize)
        {
            StartCoroutine(Reload());
        }

        // TODO: Update the GUI
    }

    // IEnumerator for handling reloads
    IEnumerator Reload()
    {
        _reloading = true;

        // Play reload sound
        _gunAudio.PlayOneShot(ReloadSFX, 0.3f);

        // TODO: Play reload animation
		_reloadStart = Time.time;
        yield return new WaitForSeconds(ReloadTime);

        Ammo = ClipSize;
        _reloading = false;
    }

    // Fire the Ray
    public override void FireWeapon()
    {
        for (var i = 0; i < NumberOfProjectiles; i++)
        {
            Vector3 spread = new Vector3(
                Random.Range(-1, 1),
                Random.Range(-1, 1),
                Random.Range(-1, 1)
            ).normalized * Radius;

            // Enable the light.
            GunLight.enabled = true;

            // Stop the particles from playing if they were, then start the particles.
            _gunParticles.Stop();
            _gunParticles.Play();

            // Fire the Projectile
            GameObject tmpProj = Instantiate(Projectile);
            tmpProj.GetComponent<BasicBullet>().InheritWeaponValues(Damage, ProjectileSpeed, Range);
            tmpProj.transform.position = _firepoint.position;
            tmpProj.transform.rotation = Quaternion.Euler(spread) * _firepoint.rotation;

            // Stop the particles from playing if they were, then start the particles.
            _gunParticles.Stop();
            _gunParticles.Play();
        }

        // Enable the light.
        GunLight.enabled = true;

        // Reset variables for next fire
        _nextTimeToFire = 0f;
        Ammo--;
    }

	public override List<ItemStat> GetStats ()
	{
		List<ItemStat> stats = new List<ItemStat>();
		stats.AddRange (stats_Weapon);
		stats.AddRange (stats_Projectile);
		return stats;
	}

    public void DisableEffects()
    {
        GunLight.enabled = false;
    }
		

	public override int Ammo
	{
		get {
			return _ammo;
		}
		set {
			_ammo = value;
		}
	}

	public override bool IsReloading
	{
		get {
			return _reloading;
		}
	}

	public override float GetReloadPercent()
	{
		return (Time.time - _reloadStart) / ReloadTime;
	}
}