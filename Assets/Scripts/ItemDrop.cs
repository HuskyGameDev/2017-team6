using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour {

	public bool shouldSpin = true;
	public float spinSpeed = 0.5f;

	//References
	MeshFilter itemMesh;
	Light glowLight;

	// Use this for initialization
	void Start () {
		itemMesh = GetComponentInChildren<MeshFilter> ();
		glowLight = GetComponentInChildren<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldSpin) {
			transform.eulerAngles += (Vector3.up * spinSpeed);
		}
	}

	public void setMesh (Mesh mesh) {
		itemMesh.mesh = mesh;
	}

	public void setGlowColor(Color color) {
		glowLight.color = color;
	}
}
