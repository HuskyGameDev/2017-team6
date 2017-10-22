using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TileInfo {
  public float xPos, yPos;

  public TileInfo(float x, float y)
  {
    this.xPos = x;
    this.yPos = y;
  }
}

public class GridMap : MonoBehaviour {

  public Transform tlCorner, trCorner, blCorner, brCorner;
  public int mapSizeX = 10;
  public int mapSizeY = 10;

  TileInfo[,] tiles;

  float gridXLength, gridYLength;

	// Use this for initialization
	void Start () {
    tiles = new TileInfo[mapSizeX, mapSizeY];

    gridXLength = Vector3.Distance(tlCorner.transform.position, trCorner.transform.position) / mapSizeX;
    gridYLength = Vector3.Distance(tlCorner.transform.position, blCorner.transform.position) / mapSizeY;

    // Debug.Log(tlCorner.position.x + " " + tlCorner.position.z);

    for(int x = 0; x < mapSizeX; x++) {
      for(int y = 0; y < mapSizeY; y++) {
        tiles[x, y] = new TileInfo(
          tlCorner.position.x + (gridXLength * x) + (gridXLength/2),
          tlCorner.position.z - ((gridYLength * y) + (gridYLength/2))
        );

        // Debug.Log(tiles[x,y].xPos + " " + tiles[x,y].yPos);
        // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // cube.transform.position = new Vector3(tiles[x,y].xPos, 0, tiles[x,y].yPos);
      }
    }
	}

  public Vector3 getTilePosition(int x, int y)
  {
    TileInfo tile = tiles[x,y];
    return new Vector3(tile.xPos, 0f, tile.yPos);
  }
}
