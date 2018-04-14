using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expload_On_Contact : MonoBehaviour {
	
	public int Damage;
	public ParticleSystem explosionEffect;

	public AudioSource audioSource;    
	public AudioClip explosion;

	void Start(){
		//effct.GetComponent<ParticleSystem>().enableEmission = false;
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = explosion;
	}

	void Awake(){
		Damage = 50;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") {
			ParticleSystem effect = Instantiate (explosionEffect) as ParticleSystem;
			effect.transform.position = transform.position;
			AudioSource.PlayClipAtPoint(explosion,transform.position);
			effect.Play ();
			Destroy(effect.gameObject, effect.duration);

			col.GetComponent<PlayerManager>().applyDamage(Damage);
			Destroy (this.transform.parent.gameObject);
		}
	}

}
