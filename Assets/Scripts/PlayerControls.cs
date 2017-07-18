using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
	public float moveSensitivity = 1f;
	public float maxHorizontalSpeed = 5;
	public float maxVerticalSpeed = 5;

	private Vector2 touchOrigin = -Vector2.one;
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
				touchOrigin = touch.position;
			} else if (touch.phase == TouchPhase.Ended && touchOrigin.x >= 0) {
				Vector2 touchEnd = touch.position;
				horizontalMove = touchEnd.x - touchOrigin.x;
				verticalMove = touchEnd.y - touchOrigin.y;
				touchOrigin.x = -1;
			}
		}
		return new Vector2 (horizontalMove, verticalMove);
	}

	private void MovePlayer (Vector2 swipeMove)
	{
		Vector3 moveForce = -swipeMove.normalized * moveSensitivity;
		playerRigidbody.AddForce (moveForce);
		ApplySpeedLimits ();
	}
}
