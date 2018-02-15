using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayerAngle : MonoBehaviour {

	public float rotationAngle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//get angle of the main camera
		rotationAngle = GameObject.Find ("Main Camera").GetComponent<PlayerCamera>().getHorizontalRotation();

		//dubug the minimap camera angle to console
		Debug.Log ("This is the angle of the minimap Camera:" + rotationAngle);

		//change the minimap camera angle to match the main camera
		transform.rotation = Quaternion.Euler (90, rotationAngle, 0);
	}
}
