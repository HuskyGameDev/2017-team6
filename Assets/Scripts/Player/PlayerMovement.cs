using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  public float speed = 6f;

  Vector3 movement;
  Animator anim;
  Rigidbody playerRBody;
  int floorMask;
  float camRayLength = 100f;

	// Initializes player variables on startup
	void Awake () {
    floorMask = LayerMask.GetMask("Floor");

    anim = GetComponent<Animator>();
    playerRBody = GetComponent<Rigidbody>();
	}

  // Runs all necessary updates for the player
  void FixedUpdate()
  {
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");

    Move(h,v);

    Turning();

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
    Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

    RaycastHit floorHit;

    if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
      Vector3 playerToMouse = floorHit.point - transform.position;

      playerToMouse.y = 0f;

      Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

      playerRBody.MoveRotation(newRotation);
    }
  }
}
