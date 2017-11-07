using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Crafting component used to craft items for the player
// Attach this to the Crafting GUI when available
public class Crafting : MonoBehaviour
{
    // Array of items that can be crafted
    public Item[] craftableItems;
    // Used to reference the craftable items
    public Sprite[] GUIitems;

    private Inventory inventoryMngr;

    // Use this for initialization
    void Start()
    {
        inventoryMngr = GetComponent<Inventory>();
        GUIitems = new Sprite[craftableItems.Length];
    }

    // Calls Inventory class AddItem() to craft the item
    public void CraftItem(int itemIdx)
    {
        if (CanCraftItem(itemIdx))
        {
            inventoryMngr.AddItem(craftableItems[itemIdx], 1);
        }
        
        // TODO: Show that you are not able to craft item
        // Play a sound?
    }

    // Updates the GUI to show all craftable items
    public void UpdateCraftable()
    {
        for (int i = 0; i < craftableItems.Length; ++i)
        {
            if (CanCraftItem(i))
            {
                // TODO: Set the item to be craftable in GUI
            }
        }
    }

    // Find an item by name
    public Item FindItemByName(string name)
    {
        Item found = null;
        for (int i = 0; i < craftableItems.Length; ++i)
        {
            if (craftableItems[i].name == name)
            {
                found = craftableItems[i];
                break;
            }
        }

        return found;
    }

    // Check to see if an item can be crafted
    public bool CanCraftItem(int itemIdx)
    {
        Item found = craftableItems[itemIdx];
        
        // If item is not found return false
        if (found == null)
            return false;

        if (found.itemCost.scrap > inventoryMngr.resources.scrap ||
            found.itemCost.energy > inventoryMngr.resources.energy ||
            found.itemCost.wire > inventoryMngr.resources.wire)
        {
            return false;
        }

        return true;
    }
}
