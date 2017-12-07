using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class used by the "player" to interact with inventory items
public class PlayerUse : MonoBehaviour
{

    public Item[] weaponList;

    private Item currentEquipped;
    private Transform weaponHolder;
    private Inventory inventoryMngr;
    private Hotbar hotbar;

    // Use this for initialization
    void Awake()
    {
        Transform[] transforms = this.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in transforms)
        {
            if (t.name == "WeaponHolder")
            {
                weaponHolder = t;
            }
        }

        // Get the inventory component
        inventoryMngr = GetComponent<Inventory>();

        // NOTE: Weapon to be spawned will be base on the inventory manager in the future
        attachItem(weaponList[0]);
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            currentEquipped.Using();
        }

        if (Input.GetKeyDown("r"))
        {
            currentEquipped.Reloading();
        }

        if (Input.GetButton("NextItem"))
        {
            attachItem(hotbar.NextItem());
        }

        if (Input.GetButton("PrevItem"))
        {
            attachItem(hotbar.PreviousItem());
        }
    }

    // Attach the selected item onto the player
    public void attachItem(Item weapon)
    {
        // TODO: Play Equip Animation
        currentEquipped = Instantiate(
          weapon,
          weaponHolder.transform.position,
          weaponHolder.transform.rotation
        );
        currentEquipped.transform.parent = weaponHolder;
    }
}
