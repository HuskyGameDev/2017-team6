using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	//External references
	public Sprite mask;
	//UI element references
	RectTransform healthBar;
	Text healthText;
	Text scrapCount;
	Text energyCount;
	Text wireCount;
	RectTransform itemsPanel;
	Transform[] hotbarItems;
	Transform[] inventoryItems;
	//Player component references
	PlayerManager player;
	Inventory inventory;
	Hotbar hotbar;

	//State
	bool inventoryOpen;
	float itemSwitchStartTime = -1f;

	// Use this for initialization
	void Start () {
		healthBar = transform.Find ("HealthPanel/HealthBarBack/HealthBar").GetComponent<RectTransform>();
		healthText = transform.Find ("HealthPanel/HealthBarBack/HealthText").GetComponent<Text>();
		scrapCount = transform.Find ("HealthPanel/MaterialsPanel/ScrapCount").GetComponent<Text>();
		energyCount = transform.Find ("HealthPanel/MaterialsPanel/EnergyCount").GetComponent<Text>();
		wireCount = transform.Find ("HealthPanel/MaterialsPanel/WireCount").GetComponent<Text>();
		itemsPanel = transform.Find ("Items").GetComponent<RectTransform>();

		Transform hotbarParent = transform.Find ("Items/Hotbar");
		hotbarItems = new Transform[hotbarParent.childCount];
		for (int i = 0; i < hotbarItems.Length; i++) {
			hotbarItems [i] = hotbarParent.GetChild (i);
		}

		Transform inventoryParent = transform.Find ("Items/Inventory");
		inventoryItems = new Transform[inventoryParent.childCount];
		for (int i = 0; i < inventoryItems.Length; i++) {
			inventoryItems [i] = inventoryParent.GetChild (i);
		}

		GameObject playerObj = GameObject.Find ("Player");
		player = playerObj.GetComponent<PlayerManager>();
		inventory = playerObj.GetComponent<Inventory> ();
		hotbar = playerObj.GetComponent<Hotbar> ();

		inventoryOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateHealth ();
		UpdateMaterials ();
		UpdateHotbar ();

		// TODO: Update Input
		if (Input.GetKeyDown (KeyCode.F)) {
			if (inventoryOpen)
				CloseInventory ();
			else
				OpenInventory ();
		}
		UpdateItemPanel ();
	}

	public void UpdateHealth() {
		Vector2 scale = healthBar.sizeDelta;
		scale.x = ((float)player.currentHealth / (float)player.startHealth) * 200;
		healthBar.sizeDelta = scale;
		//Set hp text to 0 if negative
		healthText.text = Mathf.Max(0, player.currentHealth).ToString ();
	}

	public void UpdateMaterials() {
		scrapCount.text = inventory.resources.scrap.ToString();
		energyCount.text = inventory.resources.energy.ToString();
		wireCount.text = inventory.resources.wire.ToString();
	}

	public void UpdateHotbar() {
		for(int i = 0; i < 6; i++) {
			Image image = hotbarItems [i].Find ("Image").GetComponent<Image> ();
			if (i < hotbar.items.Length && hotbar.items [i] != null) {
				//Set the ui image to that of the hotbar item
				image.sprite = hotbar.items [i].itemImg;
			} else {
				//Set the ui image to a clear image
				image.sprite = mask;
			}
		}
		//Highlight the square of the selected item
		for (int i = 0; i < 6; i++) {
			Image box = hotbarItems[i].GetComponent<Image>();
			if (hotbar.currentItem == i) {
				//Make the selected box yellow
				box.color = new Color(1f, 0.765f, 0);
			} else {
				//Make everything else white
				box.color = Color.white;
			}
		}
	}

	public void UpdateInventory() {
		for(int i = 0; i < inventory.inventoryItems.Count && i < 18; i++) {
			Image image = inventoryItems[i].Find("Image").GetComponent<Image>();
			if (i < inventory.inventoryItems.Count && inventory.inventoryItems [i] != null) {
				//Set the ui image to that of the hotbar item
				image.sprite = inventory.inventoryItems [i].itemImg;
			} else {
				//Set the ui image to a clear image
				image.sprite = mask;
			}
		}
	}

	void UpdateItemPanel() {
		float progress = (Time.fixedTime - itemSwitchStartTime) * 4;
		if (inventoryOpen) {
			itemsPanel.anchoredPosition = Vector2.Lerp (Vector2.zero, Vector2.up * 188f, progress);
		} else {
			itemsPanel.anchoredPosition = Vector2.Lerp (Vector2.up * 188f, Vector2.zero, progress);
		}
	}

	public void OpenInventory() {
		inventoryOpen = true;
		itemSwitchStartTime = Time.fixedTime;
		// TODO: Play sound
	}

	public void CloseInventory() {
		inventoryOpen = false;
		itemSwitchStartTime = Time.fixedTime;
		// TODO: Play sound
	}
}
