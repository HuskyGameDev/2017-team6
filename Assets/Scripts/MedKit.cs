using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : Item {

	[Header("ItemStat Constants")]
	protected List<ItemStat> stats_Medkit = new List<ItemStat> {
		new ItemStat {name="Health Restored",field="healed",baseVal=50,canUpgrade=false,increaseOnLv=true,increment=1,limit=100}
	};

	int healed = 50;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	// Inherited method for Using the item
	public override void Using(UnitManager parent)
	{
		if (parent.currentHealth < parent.maxHealth) {
			// Heal the unit holding the medkit
			parent.currentHealth = Mathf.Min (parent.currentHealth + healed, parent.maxHealth);

			// Decrement or remove this item
			if (current > 1)
				current--;
			else
				parent.removeHeldItem ();
		}
	}

	// Inherited method for reloading
	public override void Reloading()
	{
	}

	public override List<ItemStat> GetStats ()
	{
		List<ItemStat> stats = new List<ItemStat>();
		stats.AddRange (stats_Medkit);
		return stats;
	}
}
