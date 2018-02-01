using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	GameObject[] allCubez;
	GameObject currentFace;

	CubeParent frontParent;
	CubeParent backParent;
	CubeParent topParent;
	CubeParent bottomParent;
	CubeParent leftParent;
	CubeParent rightParent;

	private bool keyPressed;
	private bool rotating;

	int direction = 0;
	int axis;
	string face;
	int mirrorView = 1;

	private float speed = 100;
	private int finalAngle = 90;
	private float startAngle = 0;

	private float x = 0;
	private float y = 0;
	private float z = 0;

	void Start ()
	{
		frontParent = new CubeParent ();
		backParent = new CubeParent ();
		topParent = new CubeParent ();
		bottomParent = new CubeParent ();
		leftParent = new CubeParent ();
		rightParent = new CubeParent ();
	
		keyPressed = false;
		rotating = false;

		allCubez = GameObject.FindGameObjectsWithTag ("Cube");
	}


	public string GetHitFace(RaycastHit hit)
	{
		Vector3 incomingVec = hit.normal - Vector3.up;

		//Debug.Log (incomingVec);

		// Condition comparison based on distance being almost zero
		// For certain edge and corner pieces, distance was not exactly zero 
		//and they were being excluded from their respective faces

		if ((Vector3.Distance(incomingVec, new Vector3 (0, -1, -1))) < 0.05) {
			return "Front";
		}

		if ((Vector3.Distance(incomingVec, new Vector3 (0, -1, 1))) < 0.05) {
			return "Back";
		}

		if ((Vector3.Distance(incomingVec, new Vector3 (0, 0, 0))) < 0.05) {
			return "Top";
		}

		if ((Vector3.Distance(incomingVec, new Vector3 (0, -2, 0))) < 0.05) {
			return "Bottom";
		}

		if ((Vector3.Distance(incomingVec, new Vector3 (-1, -1, 0))) < 0.05) {
			return "Left";
		}

		if ((Vector3.Distance(incomingVec, new Vector3 (1, -1, 0))) < 0.05) {
			return "Right";
		}

		return "No face: " + incomingVec;
	}
		
	void FixedUpdate ()
	{
		if (!rotating) {
			if (Input.GetMouseButtonDown (0)) { // add touch input here
				RaycastHit hitInfo = new RaycastHit ();
				bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);
				if (hit) {
					face = GetHitFace (hitInfo);
					//face = hitInfo.transform.gameObject.name;
					currentFace = GameObject.FindGameObjectWithTag(face);
					initFace ();
				}
			}

			if (currentFace != null) {
				/* if face is on inverted side of cube, swipe direction should be opposite
					to cooperate with user view so mirroredView var will switch the direction */

				if (SwipeManager.Instance.isSwiping (SwipeDirection.Left)) {
					// Clockwise Rotation (1)
					direction = 1 * mirrorView;
					keyPressed = true;
				}
				if (SwipeManager.Instance.isSwiping (SwipeDirection.Right)) {
					//Debug.Log ("Right swipe");
					// Clockwise Rotation (-1)
					direction = -1 * mirrorView;
					keyPressed = true;
				}
			}
		}

		if (keyPressed) {    
			runRotation ();
		}
	}

	void runRotation ()
	{
		if (Mathf.Abs (startAngle) < finalAngle) {
			rotating = true;
			switch (axis) {
			case 2:
				z += direction * axisSetter ();
				RotateMethod ();
				if (face == "Front") {
					frontParent.setXYZ (x, y, z);
				} else {
					backParent.setXYZ (x, y, z);
				}
				break;
			case 1:
				y += direction * axisSetter ();
				RotateMethod ();
				if (face == "Top") {
					topParent.setXYZ (x, y, z);
				} else {
					bottomParent.setXYZ (x, y, z);
				}
				break;	
			case 0:
				x += direction * axisSetter ();
				RotateMethod ();
				if (face == "Left") {
					leftParent.setXYZ (x, y, z);
				} else {
					rightParent.setXYZ (x, y, z);
				}
				break;
			}
		}
		if (Mathf.Abs (startAngle) >= finalAngle) {
			keyPressed = false;
			rotating = false;
			startAngle = 0f;
		}
	}

	/* RotateMethod: rotation scripting with Euler angles correctly. here we store our 
	   Euler angle in a class variable, and only use it to apply it as a Euler angle. */

	void RotateMethod ()
	{
		startAngle += Time.deltaTime * speed;	
		currentFace.transform.rotation = Quaternion.Euler (x, y, z);
	}

	float axisSetter ()
	{
		return Time.deltaTime * speed;
	}

	void initFace ()
	{
		switch (face) {

		// Front
		case "Front":
			currentFace.transform.DetachChildren ();
			axis = 2;
			addChildren (axis, -2.0f);
			setXYZ (frontParent.getX (), frontParent.getY (), frontParent.getZ ());
			mirrorView = 1;
			break;

		// Back
		case "Back":
			currentFace.transform.DetachChildren ();
			axis = 2;
			addChildren (axis, 2.0f);
			setXYZ (backParent.getX (), backParent.getY (), backParent.getZ ());
			mirrorView = -1;
			break;

		// Top
		case "Top":
			currentFace.transform.DetachChildren ();
			axis = 1;
			addChildren (axis, 2.0f);
			setXYZ (topParent.getX (), topParent.getY (), topParent.getZ ());
			mirrorView = 1;
			break;

		// Bottom
		case "Bottom":
			currentFace.transform.DetachChildren ();
			axis = 1;
			addChildren (axis, -2.0f);
			setXYZ (bottomParent.getX (), bottomParent.getY (), bottomParent.getZ ());
			mirrorView = -1;
			break;

		// Left
		case "Left":
			currentFace.transform.DetachChildren ();
			axis = 0;
			addChildren (axis, -2.0f);
			setXYZ (leftParent.getX (), leftParent.getY (), leftParent.getZ ());
			mirrorView = 1;
			break;

		// Right
		case "Right":
			currentFace.transform.DetachChildren ();
			axis = 0;
			addChildren (axis, 2.0f);
			setXYZ (rightParent.getX (), rightParent.getY (), rightParent.getZ ());
			mirrorView = -1;
			break;
		}
	}

	void addChildren (int AXIS, float location)
	{
		foreach (GameObject obj in allCubez) {
			Vector3 loc = obj.transform.position;
			float axis = 0;
			switch (AXIS) {
			case 0:
				axis = loc.x;
				break;
			case 1:
				axis = loc.y;
				break;
			case 2:
				axis = loc.z;
				break;
			}

			if (Mathf.Round (axis) == location) {
				obj.transform.parent = currentFace.transform;
			}
		}
	}

	void setXYZ (float xx, float yy, float zz)
	{
		x = xx;
		y = yy;
		z = zz;
	}
}
