using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour {

  public Transform tlCorner, trCorner, blCorner, brCorner;
  public int mapSizeX = 10;
  public int mapSizeY = 10;

  int[,] tiles;

	// Use this for initialization
	void Start () {
    tiles = new int[mapSizeX, mapSizeY];

    for(int x = 0; x < mapSizeX; x++) {
      for(int y = 0; y < mapSizeY; y++) {
        tiles[x, y] = 0;
      }
    }
	}

	// Update is called once per frame
	void Update () {

	}
}
