using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUse : MonoBehaviour {

  public BaseWeapon[] weaponList;

  BaseWeapon currentWeapon;
  GameObject weaponHolder;

	// Use this for initialization
	void Awake () {
    weaponHolder = GameObject.Find("WeaponHolder");

    // NOTE: Weapon to be spawned will be base on the inventory manager in the future
    attachWeapon(weaponList[0]);
	}

  void Update()
  {
    if (Input.GetButton("Fire1")) {
      currentWeapon.Fire();
    }

    if (Input.GetKeyDown("r")) {
      currentWeapon.Reloading();
    }
  }

  public void attachWeapon(BaseWeapon weapon)
  {
    currentWeapon = Instantiate(
      weapon,
      weaponHolder.transform.position,
      weaponHolder.transform.rotation
    );
    currentWeapon.transform.parent = weaponHolder.transform;
  }
}
