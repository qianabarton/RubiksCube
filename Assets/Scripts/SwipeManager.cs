﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeDirection
{
	None = 0,
	Left = 1,
	Right = 2,
	Up = 4,
	Down = 8,
}

public class SwipeManager : MonoBehaviour
{
	private static SwipeManager instance;
	public static SwipeManager Instance{ get { return instance; } }
			
	public SwipeDirection Direction{set;get;}

	Vector3 touchPosition;
	private float swipeResistanceX = 50.0f;
	private float swipeResistanceY = 100.0f;

	// Use this for initialization
	void Start ()
	{
		instance = this;
	}
	
	// Update is called once per frame
	private void Update ()
	{
		Direction = SwipeDirection.None;
			
		if (Input.GetMouseButtonDown (0)) {
			touchPosition = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp (0)) {
			Vector2 deltaSwipe = touchPosition - Input.mousePosition;

			if (Mathf.Abs (deltaSwipe.x) > swipeResistanceX) {
				// Swipe on X Axis
				Direction |= (deltaSwipe.x < 0) ? SwipeDirection.Right : SwipeDirection.Left;
			}

			if (Mathf.Abs (deltaSwipe.y) > swipeResistanceY) {
				// Swipe on Y Axis
				Direction |= (deltaSwipe.x < 0) ? SwipeDirection.Up : SwipeDirection.Down;
			}
		}
	}

	public bool isSwiping(SwipeDirection dir){
		return (Direction & dir) == dir;
	}
}
