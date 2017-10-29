using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// Basic Inventory class
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

    [System.Serializable]
    public class Hotbar
    {
        public Item[] weapons;
        public Item[] consumables;
        public Item crafter;
    }

    public Resources resources;
    public Hotbar hotbar;
    // Array of items that can be used by the player
    public Item[] items;

    private void Start()
    {
        items = new Item[invRow * invCol];
        itemCount = 0;
    }

    public void AddItem(Item item, int numItem = 1)
    {
        bool containsItem = false;
        for (int i = 0; i < items.Length; i++)
        {
            // If item already exists: increment the item count without exceeding limits
            if (items[i].name == item.name)
            {
                items[i].current = Mathf.Clamp(numItem + items[i].current, 0, items[i].max);
                containsItem = true;
            }
        }

        // If the item does not already exist add it
        if (!containsItem && itemCount < (invRow * invCol))
        {
            // Add the item to the first open spot in the inventory
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    items[i] = item;

                    // TODO: Update the inventory menu to contain this item
                }
            }
        }
    }

    public void AddItemToHotbar(int itemIdx)
    {
        // Equip the item onto the player
    }

    public void RemoveItem(int itemIdx)
    {
        // Drop onto the ground
        // Destroy from inventory menu
    }
}
