using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideCamera : MonoBehaviour
{
	public Transform cam;

	private bool rotating;
	private bool keyPressed;
	private float smoothSpeed = 180f;

	int finalAngle = 89;
	float startAngle = 0;

	private Vector3 axis;

	private void Start ()
	{
		cam = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}

	private void Update ()
	{
		if (!rotating) {
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				keyPressed = true;
				axis = Vector3.up;
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				keyPressed = true;
				axis = Vector3.down;
			}
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				keyPressed = true;
				axis = Vector3.left;
			}
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				keyPressed = true;
				axis = Vector3.right;
			}
		}

		if (keyPressed) {
			RotateCamera ();
		}
	}

	public void RotateCamera ()
	{
		if (Mathf.Abs (startAngle) < finalAngle) {
			startAngle += Time.deltaTime * smoothSpeed;	

			//Debug.Log ("angle = " + startAngle);

			cam.transform.RotateAround (Vector3.zero, axis, Time.deltaTime * smoothSpeed);
		}
		if (Mathf.Abs (startAngle) >= finalAngle) {
			keyPressed = false;
			startAngle = 0f;
		}

	}
}
