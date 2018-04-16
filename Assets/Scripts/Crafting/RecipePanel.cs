using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp
{
	public class RecipePanel : MonoBehaviour
	{
		[Header("Material Costs")]
		public int ScrapCost;
		public int EnergyCost;
		public int WireCost;


		[Header("Crafting Yield")]
		public string CraftedItem;
		public string CraftedItemName;
		public string Description;

		private Text _headerText;
		private Text _descriptionText;
		private Text _scrapCostText;
		private Text _energyCostText;
		private Text _wireCostText;
		private Image _image;


		// used for initialization
		void Start()
		{
			// get references to the text
			_headerText = transform.Find("Header/NameText").GetComponent<Text> ();
			_descriptionText = transform.Find("Description/DescriptionText").GetComponent<Text> ();
			_scrapCostText = transform.Find("Description/ScrapCost").GetComponent<Text> ();
			_energyCostText = transform.Find("Description/EnergyCost").GetComponent<Text> ();
			_wireCostText = transform.Find("Description/WireCost").GetComponent<Text> ();
			_image = GetComponent<Image> ();

			// set the text
			_headerText.text = CraftedItemName;
			_descriptionText.text = Description;
			_scrapCostText.text = ScrapCost.ToString () + " Scrap" + (ScrapCost == 1 ? "" : "s"); 
			_energyCostText.text = EnergyCost.ToString () + " Energy";
			_wireCostText.text = WireCost.ToString () + " Wire" + (WireCost == 1 ? "" : "s"); 
		}

		public void ShowRecipe(CraftingRecipe recipe)
		{
			_headerText.text = recipe.CraftedItemName;
			_descriptionText.text = recipe.Description;
			ScrapCost = recipe.ScrapCost;
			_scrapCostText.text = ScrapCost.ToString () + " Scrap" + (ScrapCost == 1 ? "" : "s");
			EnergyCost = recipe.EnergyCost;
			_energyCostText.text = EnergyCost.ToString () + " Energy";
			WireCost = recipe.WireCost;
			_wireCostText.text = WireCost.ToString () + " Wire" + (WireCost == 1 ? "" : "s"); 
		}

		// creates the item specified by the recipe
		public Item CraftItem()
		{
			return Instantiate (Resources.Load(CraftedItem, typeof(Item))) as Item;
		}

		public void MakeVisible()
		{
			if (enabled)
				return;
			enabled = true;
			_image.CrossFadeAlpha (1f, 0, true);
		}

		public void Hide()
		{
			if (!enabled)
				return;
			//_image.color.a = 0.0;
			enabled = false;
			_image.CrossFadeAlpha (0f, 0, true);
		}
	}
}

