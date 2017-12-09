using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public GameObject[] toSpawn;
    int current;

    // Use this for initialization
    void Start() {
        current = 0;
    }

    void SpawnNextObject()
    {
        GameObject spawnedObj = Instantiate(toSpawn[current]);
        spawnedObj.transform.position = transform.position;
    }
}
