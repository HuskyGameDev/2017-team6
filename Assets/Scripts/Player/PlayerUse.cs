using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class used by the "player" to interact with inventory items
public class PlayerUse : MonoBehaviour
{
	public int selectedIndex;

    private Item currentEquipped;
    private Transform weaponHolder;
    private Inventory inventoryMngr; // Hotbar consists of indices 0-5

    // Use this for initialization
    void Awake()
    {
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
            currentEquipped.Using();
        }

        if (Input.GetKeyDown("r"))
        {
            currentEquipped.Reloading();
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
			if (inventoryMngr.items [hotbarIndex] != null)
			{
				selectedIndex = hotbarIndex;
				attachItem (inventoryMngr.items [hotbarIndex]);
			}
		}
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
}
