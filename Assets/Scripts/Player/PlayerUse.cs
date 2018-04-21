using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class used by the "player" to interact with inventory items
public class PlayerUse : MonoBehaviour
{
	public int selectedIndex;

	private PlayerManager playerManager;
    public Item[] weaponList;
	public Item currentEquipped { get; private set;}
    private Transform weaponHolder;
    private Inventory inventoryMngr; // Hotbar consists of indices 0-5

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

		if(currentEquipped != null)
			GameObject.Destroy(currentEquipped.gameObject);

        currentEquipped = Instantiate(
          weapon,
          weaponHolder.transform.position,
          weaponHolder.transform.rotation
        );
        currentEquipped.transform.parent = weaponHolder;
    }

	public void removeHeldItem()
	{
		if (currentEquipped != null)
			GameObject.Destroy (currentEquipped.gameObject);

		inventoryMngr.RemoveItem (selectedIndex);
	}
}
