using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
	private Vector2 touchOrigin = -Vector2.one;

	void Update ()
	{
		int horizontal = 0;
		int vertical = 0;
		if (Input.touchCount > 0) {
			Touch touch = Input.touches [0];
			if (touch.phase == TouchPhase.Began) {
				touchOrigin = touch.position;
			} else if (touch.phase == TouchPhase.Ended && touchOrigin.x >= 0) {
				Vector2 touchEnd = touch.position;
				float horizontalMove = touchEnd.x - touchOrigin.x;
				float verticalMove = touchEnd.y - touchOrigin.y;
				touchOrigin.x = -1;
				if (Mathf.Abs (horizontalMove) > Mathf.Abs (verticalMove)) {
					horizontal = horizontalMove > 0 ? 1 : -1;
				} else {
					vertical = verticalMove > 0 ? 1 : -1;
				}
			}
		}

		if (horizontal == 1) {
			Debug.Log ("Swipe right");
		} else if (horizontal == -1) {
			Debug.Log ("Swipe left");
		} else if (vertical == 1) {
			Debug.Log ("Swipe up");
		} else if (vertical == -1) {
			Debug.Log ("Swipe down");
		}
	}
}
