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
	public Item currentEquipped;
    private Transform weaponHolder;
    private Inventory inventoryMngr; // Hotbar consists of indices 0-5
	private Item currentItem;
	private Dictionary<Item, WeaponStats> weaponStatus;

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
					removeHeldItem ();
					currentEquipped = null;
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

		// this gets called before Awake, so ensure it's initialized
		if (weaponStatus == null) {
			weaponStatus = new Dictionary<Item, WeaponStats> ();
		}


		if (currentEquipped != null && currentItem != null) {
			if (currentItem is Weapon) {
				weaponStatus [currentItem].UpdateStats ((Weapon) currentEquipped);
			}
			GameObject.Destroy (currentEquipped.gameObject);
		}

		// set the current item to the item we're switching to
		currentItem = weapon;
		// if it's null, we're done
		if (weapon == null) {
			return;
		}

		// instantiate the item
		currentEquipped = Instantiate (
			weapon,
			weaponHolder.transform.position,
			weaponHolder.transform.rotation
		);
		currentEquipped.transform.parent = weaponHolder;

		// if it's a weapon, add it to the dictionary of weapon stats
		if (weapon is Weapon) {
			if (weaponStatus.ContainsKey (weapon)) {
				weaponStatus [weapon].LoadStats ((Weapon)currentEquipped);
			} else {
				weaponStatus.Add (weapon, new WeaponStats ());
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
