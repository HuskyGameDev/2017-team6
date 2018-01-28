using System.Collections;
using System.Linq;
using UnityEngine;

public class ProjectileWeapon1 : Item
{
    [Header("Weapon Info")]
    public int NumberOfProjectiles = 1;
    public int ProjectileDamage = 20;           // The damage inflicted by each bullet
    public float ProjectileRange = 20f;         // The distance the gun can fire
    public float TimeBetweenShots = 0.15f;      // The time between each shot
    public int ClipSize = 30;
    public float ReloadTime = 5.0f;             // Time to reload

    [Header("Fire Variance Attributes")]
    public float Radius = 2.0f;                 // Scale of the circle.
    public float Z = 10f;                       // Used to set the distance of the circle

    [Header("Projectile")]
    public GameObject Projectile;               // Reference to projectile game object

    [Header("Sound Effects")]
    public AudioClip[] GunShotSFX;
    public AudioClip ReloadSFX;
    public AudioClip GunEmptySFX;

    [Header("Game Objects/Resources")]
    public Light GunLight;                      // Reference to the light component

    protected Transform _firepoint;              // Point where bullets come out of in our gun

    protected int _shootableMask;                // A layer mask so the raycast only hits things on the shootable layer

    protected ParticleSystem _gunParticles;      // Reference to the particle system
    protected AudioSource _gunAudio;             // Reference to the audio source

    //float effectsDisplayTime = 0.2f;            // The proportion of the timeBetweenBullets that the effects will display for
    //bool timerRunning = false;                  // timer begins at this value

	protected int _ammo;
    protected bool _reloading = false;
    protected float _nextTimeToFire = 0.0f;      // Timer to keep track of fire rate

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
        _ammo = ClipSize;
    }

    // Inherited method for Using the weapon
    public override void Using()
    {
        if (Time.time > _nextTimeToFire &&
            _reloading == false &&
            _ammo > 0)
        {
            // Play weapon fire audio
            int hitSoundID = Mathf.CeilToInt(Random.Range(0, GunShotSFX.Length));
            _gunAudio.PlayOneShot(GunShotSFX[hitSoundID], 0.4f);

            FireProjectiles();

            _nextTimeToFire = Time.time + TimeBetweenShots;
        }
		else if (_nextTimeToFire >= TimeBetweenShots && 
				 _reloading == false && 
				 _ammo <= 0)
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
        if (!_reloading)
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

        yield return new WaitForSeconds(ReloadTime);

        _ammo = ClipSize;
        _reloading = false;
    }

    // Fire the Ray
    protected virtual void FireProjectiles()
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
        _ammo--;
    }

    public void DisableEffects()
    {
        GunLight.enabled = false;
    }
}
