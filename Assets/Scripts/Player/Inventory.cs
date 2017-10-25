using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// Basic Inventory class
// Basic implementation modified from http://answers.unity3d.com/questions/972660/using-enums-to-design-an-inventory-system.html
public class Inventory
{
    [System.Serializable]
    // Class used to keep track of the amount of item we have remaining for a given item
    public class Amount
    {
        // Keep track of current amount of item
        public int current;
        // Max number of item that we are keeping track of
        public int max;
        
        // Basic Constructor
        public Amount() { }
        // Custom Constructor to create amount
        public Amount(int current, int max)
        {
            this.max = max;
            this.addCurrent(current);
        }

        // Add to the current amount
        public void addCurrent(int value)
        {
            current = Mathf.Clamp(current + value, 0, max);
        }
    };
    
    [System.Serializable]
    // Base class for Consumables
    public class Consumables
    {
        public enum Types
        {
            HPup, Ammo
        }

        public Types type;
        public Amount amount;
      
        // Constructor
        public Consumables() { }
        // Constructor
        public Consumables(Types type, Amount amount)
        {
            this.type = type;
            this.amount = amount;
        }
    };

    [System.Serializable]
    // Base class for Equipment
    // Gun, Mellee
    public class Equipment
    {
        public enum Types
        {
            Range, Mellee
        }

        public Types type;
        public Amount amount;

        // Constructor
        public Equipment() { }
        // Custom Constructor
        public Equipment(Types type, Amount amount)
        {
            this.type = type;
            this.amount = amount;
        }
        // Custom Constructor
        public Equipment(Types type)
        {
            this.type = type;
            this.amount = new Amount();
        }
    }

    [System.Serializable]
    // Base class for Resources
    public class Resources
    {
        public enum Types
        {
            Scrap, Energy, Ore
        }

        public Types type;
        public Amount amount;

        // Constructor
        public Resources() { }
        // Custom Constructor
        public Resources(Types type, Amount amount)
        {
            this.type = type;
            this.amount = amount;
        }
        // Custom Constructor
        public Resources(Types type)
        {
            this.type = type;
            this.amount = new Amount();
        }
    }

    public Resources[] resources;
    public Equipment[] equipment;
    public Consumables[] consumables;

    public void AddEquipment(Equipment.Types type, int value)
    {

    }
}
