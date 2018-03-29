using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Damage : MonoBehaviour {

	public int Damage;

	void Awake(){
		Damage = 5;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") {
			col.GetComponent<PlayerManager>().applyDamage(Damage);
		}
	}
}
