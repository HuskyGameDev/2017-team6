using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapscript : MonoBehaviour {

	public Transform player;

	void Start(){
		player = GameObject.Find ("Player").transform;
	}

	// Update is called once per frame
	void Update () {

		Vector3 newPosition = player.position;
		newPosition.y = transform.position.y;
		transform.position = newPosition;
	}
}
