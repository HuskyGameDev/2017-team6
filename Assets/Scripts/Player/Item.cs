using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Simple class used to keep track of items
[System.Serializable]
public abstract class Item : MonoBehaviour {
	public int ItemID;

	public bool IsConsumable;

    [System.Serializable]
    // Basic class for Cost of Item
    public class ItemCost
    {
        public int scrap;
        public int energy;
        public int wire;
    }

	public class ItemStat
	{
		public string name;
		public string field;

		public float baseVal;
		public bool canUpgrade;
		public bool increaseOnLv;
		public float increment;
		public float limit; // Max or min value
	}

	public enum ItemType
	{
		Weapon, Melee, Throwable, Armor, Consumable
	};

	public enum ItemTier
	{
		Standard, Unique, Rare, Epic, Legendary, Perfect, Hacked
	};

    [Header("Base Item Info")]
    public int max;									// The max amount of instances that can fit in a stack
    public int current;								// The current amount in this stack

    public string itemName;							// Display name of the item
	public ItemType itemType;						// The type of this item
    public Sprite itemImg;							// Item icon image
	//public Transform itemMesh;

	[Header("Item Tier")]
	public int level = 1;							// A general measure of this item's worth
	public ItemTier itemTier = ItemTier.Standard;	// The tier level dictated by level

    [Header("Item Cost")]
    public ItemCost itemCost;						// The amount of each material needed to craft, if craftable

	public abstract void Using(UnitManager parent);
    public abstract void Reloading(); // TODO Remove?

	public abstract List<ItemStat> GetStats ();

	//public abstract ItemUpgrade[] GetUpgrades();
	//public abstract void Upgrade(ItemUpgrade upgrade);

	//public abstract void UpdateLevel();
}
