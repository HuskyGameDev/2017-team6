using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace AssemblyCSharp
{
	public class CraftingRecipe
	{
		public string CraftedItemName;
		public string PrefabName;
		public int ScrapCost;
		public int EnergyCost;
		public int WireCost;
		public string Description;

		// format for the text file is item name,,prefab name,,scrap cost,,energy cost,,wire cost,,description
		public CraftingRecipe(string lineOfText) 
		{
			string[] recipeCosts = lineOfText.Split (",,".ToCharArray());
			CraftedItemName = recipeCosts[0];
			PrefabName = "Prefabs/" + recipeCosts[2];
			ScrapCost = Convert.ToInt32(recipeCosts[4]);
			EnergyCost = Convert.ToInt32(recipeCosts[6]);
			WireCost = Convert.ToInt32(recipeCosts[8]);
			Description = recipeCosts[10];
		}
	}

	public class CraftingManager: MonoBehaviour
	{	

		RecipePanel _recipePanel;
		private List<CraftingRecipe> _recipes;
		private Inventory _inventory;
		private int _index;
		void Start()
		{
			_recipePanel = transform.Find ("RecipePanel").GetComponent<RecipePanel> ();
			_recipes = new List<CraftingRecipe> ();
			_inventory = (Inventory) GameObject.Find ("Player").GetComponent<Inventory>();
			// read from the text file
			StreamReader reader = new StreamReader ("Assets/Resources/Prefabs/CraftingRecipes/crafting-recipes.txt");
			_index = 0;
			string line = reader.ReadLine ();
			while (line != null) {
				// add to list
				_recipes.Add (new CraftingRecipe(line));
				line = reader.ReadLine ();
			}
			_recipePanel.ShowRecipe (_recipes[_index]);
			reader.Close ();
			enabled = true;
		}

		public void CraftItem()
		{
			CraftingRecipe recipe = _recipes [_index];

			if (_inventory.resources.scrap < recipe.ScrapCost || _inventory.resources.energy < recipe.EnergyCost || _inventory.resources.wire < recipe.WireCost) {
				// make some message about not being able to craft
				return;
			}
			Item newItem = Resources.Load (recipe.PrefabName, typeof(Item)) as Item;
			// see if it was added to the inventory
			if (_inventory.AddItem (newItem, 1)) {
				_inventory.resources.scrap -= recipe.ScrapCost;
				_inventory.resources.energy -= recipe.EnergyCost;
				_inventory.resources.wire -= recipe.WireCost;
			} else {
				GameObject.Destroy (newItem);
				// show message for being unable to craft
			}
		}
	}
}

