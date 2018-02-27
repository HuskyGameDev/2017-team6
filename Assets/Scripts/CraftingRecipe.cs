using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp
{
	public class CraftingRecipe : MonoBehaviour
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
			_headerText = transform.Find("Header/NameText").GetComponent<Text> ();
			_descriptionText = transform.Find("Description/DescriptionText").GetComponent<Text> ();
			_scrapCostText = transform.Find("Description/ScrapCost").GetComponent<Text> ();
			_energyCostText = transform.Find("Description/EnergyCost").GetComponent<Text> ();
			_wireCostText = transform.Find("Description/WireCost").GetComponent<Text> ();
			_image = GetComponent<Image> ();

			_headerText.text = CraftedItemName;
			_descriptionText.text = Description;
			_scrapCostText.text = ScrapCost.ToString () + " Scrap" + (ScrapCost == 1 ? "" : "s"); 
			_energyCostText.text = EnergyCost.ToString () + " Energy";
			_wireCostText.text = WireCost.ToString () + " Wire" + (WireCost == 1 ? "" : "s"); 
			enabled = false;
			_image.CrossFadeAlpha (0f, 0, true);
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

