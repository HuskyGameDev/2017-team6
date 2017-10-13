using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  public float speed = 6f;

  Vector3 movement;
  Animator anim;
  Rigidbody playerRBody;
  Camera playerCam;

  int floorMask;
  float camRayLength = 100f;

	// Initializes player variables on startup
	void Awake () {
    floorMask = LayerMask.GetMask("Floor");

    anim = GetComponent<Animator>();
    playerRBody = GetComponent<Rigidbody>();

    // Get the Main Character from the scene
    playerCam = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

  // Runs all necessary updates for the player
  void FixedUpdate()
  {
    // Get the horizontal and vertical directions for movement input
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");

    // Move the player
    Move(h,v);

    // Turn the player
    Turning();

    // Animate the player base on direction of movement
    // Animating(h,v);
  }

  // Moves the player the specified direction
	void Move(float h, float v)
  {
    movement.Set(h, 0f, v);

    movement = movement.normalized * speed * Time.deltaTime;

    playerRBody.MovePosition(transform.position + movement);
  }

  // Rotates the player base on the Camera position and Mouse position
  void Turning ()
  {
    Ray camRay = playerCam.ScreenPointToRay (Input.mousePosition);

    RaycastHit floorHit;

    // Get the position of the hit on the floor.
    if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
      Vector3 playerToMouse = floorHit.point - transform.position;

      playerToMouse.y = 0f;

      Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

      playerRBody.MoveRotation(newRotation);
    }
  }
}
