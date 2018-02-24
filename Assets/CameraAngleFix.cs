using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngleFix : MonoBehaviour {
	//Assign externally
	public Transform player;

	public float horizontalRotation = 0f; //TODO Allow input to control this
	public float distanceFromPlayer = 20f;
	public float angleFromTop = 45f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		UpdateCamPos();
	}

	void UpdateCamPos()
	{
		//Use distanceFromPlayer and angleFromTop to position the camera relative to the player
		Quaternion rot = Quaternion.Euler(90f - angleFromTop, horizontalRotation, 0f);
		Vector3 pos = player.position + ((rot * Vector3.forward) * -distanceFromPlayer);

		transform.rotation = rot;
		transform.position = pos;
	}
}
