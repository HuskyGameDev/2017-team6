using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// Basic Inventory class
// Class used by the Player Use class to determine what items are:
//  collected
//  able to be used
//  currently equipped
// Basic implementation modified from http://answers.unity3d.com/questions/972660/using-enums-to-design-an-inventory-system.html
public class Inventory : MonoBehaviour
{
    // Basic Inventory information
    [Header("Inventory Info")]
    public int maxItems;
    public int itemCount;

    // Resources class for keeping track of available resources
    [System.Serializable]
    public class Resources
    {
        public int scrap;
        public int energy;
        public int wire;
    }

    [Header("The Inventory")]
    // Class containing all the resources
    public Resources resources;
    // Array of items that can be used by the player
    public List<Item> items;

	//[Header("UI Information")]
	// Reference to the game UI
	private UI_Game ui;

    private void Start()
    {
        items.Capacity = maxItems;
		ui = (UI_Game)GameObject.Find ("PlayerUI").GetComponent<UI_Game> ();
		for(int i=0; i<24; i++)
			items.Add(null);
		itemCount = 0;
    }

	// returns true if the item was successfully added to the inventory,
	// false otherwise
    public bool AddItem(Item item, int numItem = 1)
    {
        bool containsItem = false;
        foreach (Item _item in items)
        {
			if (_item == null) {
				continue;
			}
			/*
            // If item already exists: increment the item count without exceeding limits
            if (_item.itemName == item.itemName)
            {
                _item.current = Mathf.Clamp(numItem + _item.current, 0, _item.max);
                containsItem = true;
            }
			*/
        }
        // If the item does not already exist add it
        if (!containsItem && itemCount < maxItems)
        {
			for (int i = 0; i < maxItems; i++) {
				if (items [i] == null) {
					items [i] = item;
					itemCount++;
					break;
				}
			}

			// Update the GUI
			ui.UpdateInventory();

			return true;
        }
		return false;
    }

    // Removes an item from the inventory and updates the Inventory GUI
    //  also removes the item from the hotbar if needed
    public void RemoveItem(int itemIdx)
    {
        Item tmp = items[itemIdx];

        // Destroy from inventory menu
        items[itemIdx] = null;

        // Update the GUI
		ui.UpdateInventory();
    }

	// TODO: DropItem method
}
