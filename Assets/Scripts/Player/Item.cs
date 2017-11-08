using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple class used to keep track of items
[System.Serializable]
public abstract class Item : MonoBehaviour {

    [System.Serializable]
    // Basic class for Cost of Item
    public class ItemCost
    {
        public int scrap;
        public int energy;
        public int wire;
    }

    [Header("Base Item Info")]
    public int max;
    public int current;

    public string itemName;
    public Sprite itemImg;
    public enum Types {
        Weapon, Ammo, Consumable
    };

    [Header("Item Cost")]
    public ItemCost itemCost;

    public abstract void Using();

    public abstract void Reloading();
}
