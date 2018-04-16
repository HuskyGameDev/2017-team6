using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

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
			if (recipeCosts.Length > 9) {
				CraftedItemName = recipeCosts[0];
				PrefabName = "Prefabs/" + recipeCosts[2];
				ScrapCost = Convert.ToInt32(recipeCosts[4]);
				EnergyCost = Convert.ToInt32(recipeCosts[6]);
				WireCost = Convert.ToInt32(recipeCosts[8]);
				Description = recipeCosts[10];
			}
		}
	}

	public class CraftingManager: MonoBehaviour
	{	

		RecipePanel _recipePanel;
		private List<CraftingRecipe> _recipes;
		private Inventory _inventory;
		private int _index;
		private UI_Game _ui;
		private RecipeList _recipeList;
		private Color _color;
		private Image _image;

		void Start()
		{
			_recipePanel = transform.Find ("RecipePanel").GetComponent<RecipePanel> ();
			_recipes = new List<CraftingRecipe> ();
			_inventory = (Inventory) GameObject.Find ("Player").GetComponent<Inventory>();
			_ui = (UI_Game)GameObject.Find ("PlayerUI").GetComponent<UI_Game> ();
			_recipeList = transform.Find ("CraftingList/Recipes").GetComponent<RecipeList> ();
			_image = transform.GetComponent<Image> ();
			_color = _image.color;
			_color.a = 0f;
			_image.color = _color;

			// read from the text file
			StreamReader reader = new StreamReader ("Assets/Resources/Prefabs/CraftingRecipes/crafting-recipes.txt");
			_index = 0;
			string line = reader.ReadLine ();
			while (line != null) {
				// add to list
				CraftingRecipe newRecipe = new CraftingRecipe(line);
				_recipes.Add (newRecipe);
				_recipeList.AddRecipe (newRecipe);
				line = reader.ReadLine ();
			}
			_recipeList.Select (0);
			_recipePanel.ShowRecipe (_recipes[_index]);
			reader.Close ();
			enabled = false;
		}

		public void CraftItem()
		{
			// return if the window isn't enabled
			if (!enabled) return;

			// craft the item at the current index
			CraftingRecipe recipe = _recipes [_index];


			if (_inventory.resources.scrap < recipe.ScrapCost || _inventory.resources.energy < recipe.EnergyCost || _inventory.resources.wire < recipe.WireCost) {
				_ui.ShowAlert ("Not enough resources to craft", "", 1f);
				return;
			}
			Item newItem = Resources.Load (recipe.PrefabName, typeof(Item)) as Item;
			Debug.Log (newItem.itemName);
			// see if it was added to the inventory
			if (_inventory.AddItem (newItem, 1)) {
				_inventory.resources.scrap -= recipe.ScrapCost;
				_inventory.resources.energy -= recipe.EnergyCost;
				_inventory.resources.wire -= recipe.WireCost;
				_ui.ShowAlert (newItem.itemName + " successfully crafted", "", 1f);
			} else {
				GameObject.Destroy (newItem);
				_ui.ShowAlert ("Item already owned", "", 1f);
			}
		}

		public void Select(int index)
		{
			_index = index;
			_recipePanel.ShowRecipe (_recipes [_index]);
		}



		// the caller needs to remember to set the variable enabled to true
		public void OpenCraftingWindow()
		{
			enabled = true;
			_color.a = 1f;
			_image.color = _color;
		}

		// the caller needs to remember to set the variable enabled to false
		public void CloseCraftingWindow()
		{
			enabled = false;
			_color.a = 0f;
			_image.color = _color;
		}
	}
}

