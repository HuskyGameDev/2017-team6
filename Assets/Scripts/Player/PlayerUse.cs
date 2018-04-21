using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponStats {
	public int WeaponDamage;
	public int CurrentAmmo;
	public float RefireTime;
	public float ReloadTime;
	public int ClipSize;
	public void UpdateStats(Weapon weapon) {
		WeaponDamage = weapon.Damage;
		CurrentAmmo = weapon.Ammo;
		RefireTime = weapon.TimeBetweenShots;
		ReloadTime = weapon.ReloadTime;
		ClipSize = weapon.ClipSize;
	}

	public void LoadStats(Weapon weapon) {
		weapon.Damage = WeaponDamage;;
		weapon.Ammo = CurrentAmmo;
		weapon.TimeBetweenShots = RefireTime;
		weapon.ReloadTime = ReloadTime;
		weapon.ClipSize = ClipSize;
	}
};

// Class used by the "player" to interact with inventory items
public class PlayerUse : MonoBehaviour
{
	public int selectedIndex;

	private PlayerManager playerManager;
    public Item[] weaponList;
	public Item currentEquipped { get; private set;}
    private Transform weaponHolder;
    private Inventory inventoryMngr; // Hotbar consists of indices 0-5
	private int itemID;
	private Dictionary<int, WeaponStats> weaponStatus;

    // Use this for initialization
    void Awake()
    {
		playerManager = GetComponent<PlayerManager> ();

		// Find weapon holder
        Transform[] transforms = this.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in transforms)
        {
            if (t.name == "WeaponHolder")
            {
                weaponHolder = t;
            }
        }
		itemID = 0;
        // Get the inventory component
        inventoryMngr = GetComponent<Inventory>();
		// Attach the first weapon to the player
		selectedIndex = 0;
		attachItem(inventoryMngr.items[0]);
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
			if (currentEquipped != null) {
				currentEquipped.Using (playerManager);
				if (currentEquipped.IsConsumable) {
					currentEquipped = null;
					removeHeldItem ();
				}
			}
        }

        if (Input.GetKeyDown("r"))
        {
			if (currentEquipped != null) {
				currentEquipped.Reloading ();
			}
        }

		if (Input.GetButtonDown("NextItem"))
        {
			// Edge cases handled in method
			ChangeSelected (selectedIndex + 1);
        }

        if (Input.GetButtonDown("PrevItem"))
        {
			ChangeSelected (selectedIndex - 1);
        }
		for (int i = 1; i <= 6; i++)
		{
			if (Input.GetButtonDown ("Hotbar" + (i)))
			{
				ChangeSelected (i-1);
			}
		}
    }

	void ChangeSelected(int hotbarIndex)
	{
		if (hotbarIndex >= 0 && hotbarIndex < 6)
		{
			selectedIndex = hotbarIndex;
			attachItem (inventoryMngr.items [hotbarIndex]);
		}
	}

	public void updateSelected()
	{
		ChangeSelected (selectedIndex);
	}

    // Attach the selected item onto the player
    public void attachItem(Item weapon)
    {
        // TODO: Play Equip Animation

		if (weaponStatus == null) {
			weaponStatus = new Dictionary<int, WeaponStats> ();
		}


		if (currentEquipped != null) {
			if (currentEquipped is Weapon) {
				weaponStatus [currentEquipped.ItemID].UpdateStats ((Weapon) currentEquipped);
			}
			GameObject.Destroy (currentEquipped.gameObject);
		}


		if (weapon == null) {
			return;
		}

		if (weapon.ItemID == 0) {
			weapon.ItemID = ++itemID;
		}

		currentEquipped = Instantiate (
			weapon,
			weaponHolder.transform.position,
			weaponHolder.transform.rotation
		);
		currentEquipped.transform.parent = weaponHolder;
		if (weapon is Weapon) {
			if (weaponStatus.ContainsKey (weapon.ItemID)) {
				weaponStatus [weapon.ItemID].LoadStats ((Weapon)currentEquipped);
			} else {
				weaponStatus.Add (weapon.ItemID, new WeaponStats ());
			}
		}
	}

	public void removeHeldItem()
	{
		if (currentEquipped != null)
			GameObject.Destroy (currentEquipped.gameObject);

		inventoryMngr.RemoveItem (selectedIndex);
	}
}
