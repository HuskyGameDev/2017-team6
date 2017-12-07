using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic class for managing the Hotbar
public class Hotbar : MonoBehaviour
{
    public Item[] items;
    public int currentItem;

    // Use this for initialization
    void Start()
    {
        //items = new Item[6];
        currentItem = 0;

        // TODO: Set item 5 to be the crafting item
    }

    // Get the item after the current item on the hotbar
    public Item NextItem()
    {
        if (currentItem + 1 < items.Length)
        {
            currentItem += 1;
        } else
        {
            currentItem = 0;
        }
        return items[currentItem];
    }

    // Get the item before the current item on the hotbar
    public Item PreviousItem()
    {
        if (currentItem - 1 >= 0)
        {
            currentItem -= 1;
        }
        else
        {
            currentItem = items.Length - 1;
        }
        return items[currentItem];
    }

    // Removes an item from the hotbar by the item's name
    public void RemoveItem(string itemName)
    {
        for (int i = 0; i < items.Length; ++i)
        {
            if (items[i].itemName == itemName)
            {
                items[i] = null;
            }
        }
    }
}
