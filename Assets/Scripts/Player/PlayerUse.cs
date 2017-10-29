using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class used by the "player" to interact with inventory items
public class PlayerUse : MonoBehaviour
{

    public BaseWeapon[] weaponList;

    private BaseWeapon currentWeapon;
    private Transform weaponHolder;
    private Inventory inventoryMngr;

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
        attachWeapon(weaponList[0]);
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            currentWeapon.Fire();
        }

        if (Input.GetKeyDown("r"))
        {
            currentWeapon.Reloading();
        }
    }

    public void updateWeaponList()
    {
        // inventoryMngr
    }

    public void attachWeapon(BaseWeapon weapon)
    {
        currentWeapon = Instantiate(
          weapon,
          weaponHolder.transform.position,
          weaponHolder.transform.rotation
        );
        currentWeapon.transform.parent = transform;
    }
}
