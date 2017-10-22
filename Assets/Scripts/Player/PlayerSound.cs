using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {

    public AudioClip playerHit;

    private AudioSource source;

	void Awake () {
        source = GetComponent<AudioSource>();
	}
	
    // Play sound when player gets hit
	public void playHitSound()
    {
        source.PlayOneShot(playerHit, 0.5f);
    }
}
