using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    // Spawn the "wave" stored in toSpawn
    public void SpawnWave(GameObject[] toSpawn)
    {
        int i;
        for (i = 0; i < toSpawn.Length; i++)
        {
            GameObject spawnedObj = Instantiate(toSpawn[i]);
            spawnedObj.transform.position = transform.position;
        }
    }
}
