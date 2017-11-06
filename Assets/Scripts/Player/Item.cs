using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple class used to keep track of items
[System.Serializable]
public abstract class Item : MonoBehaviour {

    [Header("Base Item Info")]
    public int max;
    public int current;

    public string itemName;
    public Sprite itemImg;
    public enum Types {
        Weapon, Ammo, Consumable
    };

    public abstract void Using();

    public abstract void Reloading();
}
