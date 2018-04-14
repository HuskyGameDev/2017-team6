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
		public string CraftedItem;
		public int ScrapCost;
		public int EnergyCost;
		public int WireCost;
		public string Description;

		// format for the text file is item name,,prefab name,,scrap cost,,energy cost,,wire cost,,description
		public CraftingRecipe(string lineOfText) 
		{
			string[] recipeCosts = lineOfText.Split (",,".ToCharArray());
			CraftedItemName = recipeCosts[0];
			CraftedItem = recipeCosts[2];
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
			//_inventory = GameObject.Find ("Inventory");
			// read from the text file
			StreamReader reader = new StreamReader ("Assets/Prefabs/CraftingRecipes/crafting-recipes.txt");
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
			Debug.Log ("Crafting item");
		}
	}
}

