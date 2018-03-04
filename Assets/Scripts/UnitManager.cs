using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitManager : MonoBehaviour {

	public int maxHealth = 100;
	public int currentHealth;

	protected bool isDead;
	protected bool damaged;

	public abstract void applyDamage (int damage);
	protected abstract void Death();

	public abstract void removeHeldItem();
}
