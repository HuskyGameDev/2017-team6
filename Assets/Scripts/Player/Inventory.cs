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
    public int invRow;
    public int invCol;
    public int itemCount;

    // Resources class for keeping track of available resources
    [System.Serializable]
    public class Resources
    {
        public int scrap;
        public int energy;
        public int wire;
    }

    // Class containing all the resources
    public Resources resources;
    // Array of items that can be used by the player
    public Item[] inventoryItems;

    // Reference to the hotbar object
    public Hotbar hotbar;

    private void Start()
    {
        inventoryItems = new Item[invRow * invCol];
        itemCount = 0;
    }

    public void AddItem(Item item, int numItem = 1)
    {
        bool containsItem = false;
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            // If item already exists: increment the item count without exceeding limits
            if (inventoryItems[i].name == item.name)
            {
                inventoryItems[i].current = Mathf.Clamp(numItem + inventoryItems[i].current, 0, inventoryItems[i].max);
                containsItem = true;
            }
        }

        // If the item does not already exist add it
        if (!containsItem && itemCount < (invRow * invCol))
        {
            // Add the item to the first open spot in the inventory
            for (int i = 0; i < inventoryItems.Length; i++)
            {
                if (inventoryItems[i] == null)
                {
                    inventoryItems[i] = item;

                    // TODO: Update the GUI
                }
            }
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

        // TODO: Update the GUI
    }

    public void RefreshInventoryGUI()
    {
        Debug.Log("No function created to update GUI");
    }
}
