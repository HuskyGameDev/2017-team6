using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	public class RecipeList : MonoBehaviour {

		private int _numberOfRecipes;
		private List<RecipeNamePanel> _recipes;
		public CraftingManager Crafting_Manager;

		private int _index;
	
		// Use this for initialization
		void Start () {
			_recipes = new List<RecipeNamePanel> ();
			_numberOfRecipes = 0;
			_index = -1;
		}


		public void AddRecipe(CraftingRecipe recipe)
		{
			// add recipe to the list
			//_recipes.Add (recipe);

			// create a new recipe prefab
			GameObject newRecipe = Instantiate (Resources.Load("Prefabs/CraftingRecipes/RecipeName")) as GameObject;
			RectTransform recipeTransform = newRecipe.GetComponent<RectTransform> ();
			recipeTransform.SetParent(GetComponent<RectTransform>(), false);

			//set the position
			float rectHeight = recipeTransform.rect.height; // get the height
			// since the anchor is the center, need 1/2 the height + height * number of recipes
			Vector2 newPosition = recipeTransform.anchoredPosition;
			newPosition.y = newPosition.y - _numberOfRecipes * rectHeight;
			recipeTransform.anchoredPosition = newPosition;


			//set the text of it
			RecipeNamePanel namePanel = newRecipe.GetComponent<RecipeNamePanel>();
			namePanel.SetText (recipe.CraftedItemName);
			namePanel.Index = _numberOfRecipes++;
			namePanel.Deselect ();
			// add to the list
			_recipes.Add (namePanel);
		}


		public void Select(int index)
		{
			if (index == _index)  return;
			// deselect the current recipe
			if (_index == -1) {
				_index = index;
				_recipes [_index].Select ();
				return;
			}
			_recipes [_index].Deselect ();

			// the current one will already be highlighted when this function is called
			_index = index;
			Crafting_Manager.Select (index);
		}

	}
}
