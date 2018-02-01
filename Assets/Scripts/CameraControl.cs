using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	// for free camera pan, currently unused

	public Transform target;

	public float xSpeed = 12.0f;
	public float ySpeed = 12.0f;

	public Vector3 position;

	public bool isActivated;

	float x = 0.0f;
	float y = 0.0f;

	void Start ()
	{
		target = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		Vector3 angles = target.transform.eulerAngles;

		x = angles.y;
		y = angles.x;
	}

	void LateUpdate ()
	{
		// only update if the mousebutton is held down
		if (Input.GetMouseButtonDown (0)) {
			//isActivated = true;
		} 

		// if mouse button is let UP then stop rotating camera
		if (Input.GetMouseButtonUp (0)) {
			isActivated = false;
		} 
			
		if (isActivated) { 
			//  get the distance the mouse moved in the respective direction
			x += Input.GetAxis ("Mouse X") * xSpeed;
			y -= Input.GetAxis ("Mouse Y") * ySpeed;	 

			// when mouse moves left and right we actually rotate around local y axis	
			target.transform.RotateAround (Vector3.zero, Vector3.up, x);

			// when mouse moves up and down we actually rotate around the local x axis	
			target.transform.RotateAround (Vector3.zero, Vector3.right, y);

			// reset back to 0 so it doesn't continue to rotate while holding the button
			x = 0;
			y = 0; 	
		} 


			
	}


}