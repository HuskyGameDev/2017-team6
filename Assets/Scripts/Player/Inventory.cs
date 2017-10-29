using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// Basic Inventory class
// Basic implementation modified from http://answers.unity3d.com/questions/972660/using-enums-to-design-an-inventory-system.html
public class Inventory : MonoBehaviour
{
    public int maxItems = 40;
    public int itemCount;

    public Resources[] resources;
    public Item[] items;

    private void Start()
    {
        items = new Item[maxItems];
        itemCount = 0;
        Debug.Log(items.Length);
    }

    public void AddItem(Item item, int numItem = 1)
    {
        bool containsItem = false;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name == item.name)
            {
                // Increment the item count without exceeding limits
                items[i].current = Mathf.Clamp(numItem + items[i].current, 0, items[i].max);
                containsItem = true;
            }
        }

        if (!containsItem && )
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

    public void EquipItem(int itemIdx)
    {
        // Equip the item onto the player
    }

    public void RemoveItem(int itemIdx)
    {
        // Drop onto the ground
        // Destroy from inventory menu
    }
}
