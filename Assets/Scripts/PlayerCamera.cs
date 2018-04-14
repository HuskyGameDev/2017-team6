using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    //Assign externally
    public Transform player;

    public float horizontalRotation = 0f; //TODO Allow input to control this
    public float playerDistance = 10f;
    public float minPlayerDistance = 5f;
    public float maxPlayerDistance = 15f;
    public float angleFromTop = 45f;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateCamPos();
	}

    void UpdateCamPos()
    {
        playerDistance -= Input.GetAxis("Mouse ScrollWheel") * 2;
        playerDistance = Mathf.Clamp(playerDistance, minPlayerDistance, maxPlayerDistance);

        //Use distanceFromPlayer and angleFromTop to position the camera relative to the player
        Quaternion rot = Quaternion.Euler(90f - angleFromTop, horizontalRotation, 0f);
        Vector3 pos = player.position + ((rot * Vector3.forward) * -playerDistance);

        transform.rotation = rot;
        transform.position = pos;
    }

	public float getHorizontalRotation()
    {
		return horizontalRotation;
	}
}
