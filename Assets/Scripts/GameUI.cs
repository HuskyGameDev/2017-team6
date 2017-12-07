using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	//UI element references
	RectTransform healthBar;
	Text healthText;
	Text scrapCount;
	Text energyCount;
	Text wireCount;
	Transform[] hotbarItems;
	//Player component references
	PlayerManager player;
	Inventory inventory;
	Hotbar hotbar;

	// Use this for initialization
	void Start () {
		healthBar = transform.Find ("HealthPanel/HealthBarBack/HealthBar").GetComponent<RectTransform>();
		healthText = transform.Find ("HealthPanel/HealthBarBack/HealthText").GetComponent<Text>();
		scrapCount = transform.Find ("HealthPanel/MaterialsPanel/ScrapCount").GetComponent<Text>();
		energyCount = transform.Find ("HealthPanel/MaterialsPanel/EnergyCount").GetComponent<Text>();
		wireCount = transform.Find ("HealthPanel/MaterialsPanel/WireCount").GetComponent<Text>();
		hotbarItems = transform.Find ("Hotbar").GetComponentsInChildren<Transform> ();

		GameObject playerObj = GameObject.Find ("Player");
		player = playerObj.GetComponent<PlayerManager>();
		inventory = playerObj.GetComponent<Inventory> ();
		hotbar = playerObj.GetComponent<Hotbar> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateHealth ();
		UpdateMaterials ();
		UpdateHotbar ();
	}

	void UpdateHealth() {
		Vector2 scale = healthBar.sizeDelta;
		scale.x = ((float)player.currentHealth / (float)player.startHealth) * 200;
		healthBar.sizeDelta = scale;
		//Set hp text to 0 if negative
		healthText.text = Mathf.Max(0, player.currentHealth).ToString ();
	}

	void UpdateMaterials() {
		scrapCount.text = inventory.resources.scrap.ToString();
		energyCount.text = inventory.resources.energy.ToString();
		wireCount.text = inventory.resources.wire.ToString();
	}

	void UpdateHotbar() {
		for(int i = 0; i < hotbar.items.Length && i < 6; i++) {
			if (hotbar.items [i] != null) {
				//Set the ui image to that of the hotbar item
				Image image = hotbarItems[i].Find("Image").GetComponent<Image>();
				image.sprite = hotbar.items[i].itemImg;
			}
		}
		//Highlight the square of the selected item
		for (int i = 0; i < 6; i++) {
			Image box = hotbarItems[i].GetComponent<Image>();
			if (hotbar.currentItem == i) {
				//Make the selected box yellow
				box.color = Color.HSVToRGB (46, 255, 255);
			} else {
				//Make everything else white
				box.color = Color.white;
			}
		}
	}
}
