using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
	public float moveSpeed = 100f;
	public float maxHorizontalSpeed = 5;
	public float maxVerticalSpeed = 5;
	public float touchSensitivity = 50f;

	private Vector2 touchOrigin = -Vector2.one;
	private Vector2 lastTouch = -Vector2.one;
	private Rigidbody playerRigidbody;
	private Vector2 swipeInput = Vector2.zero;

	void Awake ()
	{
		playerRigidbody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		MovePlayer (swipeInput);
	}

	void Update ()
	{
		swipeInput = GetSwipeInput ();
	}

	private void ApplySpeedLimits ()
	{
		Vector3 playerVelocity = playerRigidbody.velocity;
		if (Mathf.Abs (playerVelocity.x) > maxHorizontalSpeed) {
			playerVelocity.x = playerVelocity.x > 0 ? maxHorizontalSpeed : -maxHorizontalSpeed;
		}
		if (Mathf.Abs (playerVelocity.y) > maxVerticalSpeed) {
			playerVelocity.y = playerVelocity.y > 0 ? maxVerticalSpeed : -maxVerticalSpeed;
		}
		playerRigidbody.velocity = playerVelocity;
	}

	private Vector2 GetSwipeInput ()
	{
		float horizontalMove = 0f;
		float verticalMove = 0f;
		if (Input.touchCount > 0) {
			Touch touch = Input.touches [0];
			if (touch.phase == TouchPhase.Began) {
				// Record touch start position
				touchOrigin = touch.position;
				lastTouch = touch.position;
			} else if (touch.phase == TouchPhase.Moved && touchOrigin.x >= 0) {
				// Calculate touch direction (aka swipe move)
				Vector2 touchEnd = touch.position;
				Vector2 touchMove = new Vector2 ();
				touchMove.x = touchEnd.x - lastTouch.x;
				touchMove.y = touchEnd.y - lastTouch.y;

				// Move the player when the swipe is big enough or finished
				if (touchMove.magnitude > touchSensitivity) {
					lastTouch = touch.position;
					horizontalMove = touchMove.x;
					verticalMove = touchMove.y;
				}
			} else if (touch.phase == TouchPhase.Ended) {
				horizontalMove = touch.position.x - touchOrigin.x;
				verticalMove = touch.position.y - touchOrigin.y;
				touchOrigin = -Vector2.one;
				lastTouch = -Vector2.one;
			}
		}
		return new Vector2 (horizontalMove, verticalMove);
	}

	private void MovePlayer (Vector2 swipeMove)
	{
		Vector3 moveForce = -swipeMove.normalized * moveSpeed;
		playerRigidbody.AddForce (moveForce);
		ApplySpeedLimits ();
	}
}
