using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    GameObject playerObj;
    SpawnPoint[] enemySpawners;

	// Use this for initialization
	void Start () {
        playerObj = GameObject.Find("Player");
        enemySpawners = GameObject.FindObjectsOfType<SpawnPoint>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (SpawnPoint sp in enemySpawners)
        {
            
        }
	}

    // Next wave to spawn.
    void SpawnNextWave()
    {

    }
}
