using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// Basic Inventory class
// Basic implementation modified from http://answers.unity3d.com/questions/972660/using-enums-to-design-an-inventory-system.html
public class Inventory : MonoBehaviour
{
    public Resources[] resources;
    public Item[] items;

    public void AddItem(Item item, int numItem = 1)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name == item.name)
            {
                items[i].current += numItem;
            }

            if (items[i] == null)
            {
                items[i] = item;
                
                // Update the inventory menu to contain this item
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
