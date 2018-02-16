using UnityEngine;

public abstract class Weapon : Item
{
    [Header("Weapon Info")]
    public int Damage = 20;                     // The damage inflicted by each bullet
    public float Range = 20f;                   // The distance the gun can fire
    public float TimeBetweenShots = 0.15f;      // The time between each shot
    public int ClipSize = 30;
    public float ReloadTime = 5.0f;             // Time to reload

    [Header("Sound Effects")]
    public AudioClip[] GunShotSFX;
    public AudioClip ReloadSFX;
    public AudioClip GunEmptySFX;

    [Header("Fire Variance Attributes")]
    public float Radius = 2.0f;                 // Scale of the circle.
    public float Z = 10f;                       // Used to set the distance of the circle

    public abstract void FireWeapon();
}