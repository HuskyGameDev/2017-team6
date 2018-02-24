using UnityEngine;
using System.Collections.Generic;

public abstract class Weapon : Item
{
	[Header("ItemStat Constants")]
	protected List<ItemStat> stats_Weapon = new List<ItemStat> {
		new ItemStat {name="Bullet Damage",field="Damage",baseVal=20,canUpgrade=true,increaseOnLv=true,increment=2,limit=50},
		new ItemStat {name="Range",field="Range",baseVal=20,canUpgrade=true,increaseOnLv=true,increment=2,limit=30},
		new ItemStat {name="Accuracy",field="Radius",baseVal=2,canUpgrade=true,increaseOnLv=false,increment=0.25f,limit=0.25f},
		new ItemStat {name="Shot Delay",field="TimeBetweenShots",baseVal=0.15f,canUpgrade=true,increaseOnLv=false,increment=0.025f,limit=0.0125f},
		new ItemStat {name="Reload Time",field="ReloadTime",baseVal=5,canUpgrade=true,increaseOnLv=false,increment=0.25f,limit=1},
		new ItemStat {name="Clip Size",field="ClipSize",baseVal=30,canUpgrade=true,increaseOnLv=true,increment=5,limit=80}
	};

    [Header("Weapon Info")]
    public int Damage = 20;                     // The damage inflicted by each bullet
    public float Range = 20f;                   // The distance the gun can fire
    public float TimeBetweenShots = 0.15f;      // The time between each shot
    public float ReloadTime = 5.0f;             // Time to reload
	public int ClipSize = 30;

    [Header("Sound Effects")]
    public AudioClip[] GunShotSFX;
    public AudioClip ReloadSFX;
    public AudioClip GunEmptySFX;

    [Header("Fire Variance Attributes")]
    public float Radius = 2.0f;                 // Scale of the circle.
    public float Z = 10f;                       // Used to set the distance of the circle

    public abstract void FireWeapon();
}