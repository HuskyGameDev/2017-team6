using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {

    public AudioClip[] playerHit;

    private AudioSource source;

	void Awake () {
        source = GetComponent<AudioSource>();
	}
	
    // Play sound when player gets hit
	public void playHitSound()
    {
        int hitSoundID = Mathf.CeilToInt(Random.Range(0, 4));
        source.PlayOneShot(playerHit[hitSoundID], 0.1f);
    }
}
