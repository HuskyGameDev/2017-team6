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
    public List<Item> inventoryItems;

    [Header("Hotbar Attributes")]
    // Reference to the hotbar object
    public Hotbar hotbar;

	[Header("UI Information")]
	// Reference to the game UI
	public GameUI ui;

    private void Start()
    {
        inventoryItems = new List<Item>();
        inventoryItems.Capacity = maxItems;
        itemCount = 0;
    }

    public void AddItem(Item item, int numItem = 1)
    {
        bool containsItem = false;
        foreach (Item _item in inventoryItems)
        {
            // If item already exists: increment the item count without exceeding limits
            if (_item.name == item.name)
            {
                _item.current = Mathf.Clamp(numItem + _item.current, 0, _item.max);
                containsItem = true;
            }
        }

        // If the item does not already exist add it
        if (!containsItem && itemCount < maxItems)
        {
            inventoryItems.Add(item);

			// Update the GUI
			ui.UpdateInventory();
        }
    }

    public void AddItemToHotbar(int itemIdx)
    {
        // Equip the item onto the player
    }

    // Removes an item from the inventory and updates the Inventory GUI
    //  also removes the item from the hotbar if needed
    public void RemoveItem(int itemIdx)
    {
        Item tmp = inventoryItems[itemIdx];
        // TODO: Drop onto the ground

        // If item in Hotbar remove it
        hotbar.RemoveItem(tmp.name);

        // Destroy from inventory menu
        inventoryItems[itemIdx] = null;

        // Update the GUI
		ui.UpdateInventory();
    }
}
