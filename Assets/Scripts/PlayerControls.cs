using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
	public float constantForwardMoveForce = 50f;
	public float maxSwipeForce = 50f;
	public float maxSwipeLength = 600f;
	public float maxHorizontalSpeed = 5;
	public float maxVerticalSpeed = 5;

	private Vector2 touchOrigin = -Vector2.one;
	private Rigidbody2D playerRigidbody;
	private Vector2 swipeInput = Vector2.zero;

	void Awake ()
	{
		playerRigidbody = GetComponent<Rigidbody2D> ();
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
			} else if (touch.phase == TouchPhase.Ended) {
				horizontalMove = touch.position.x - touchOrigin.x;
				verticalMove = touch.position.y - touchOrigin.y;
				touchOrigin = -Vector2.one;
			}
		}
		return new Vector2 (horizontalMove, verticalMove);
	}

	private void MovePlayer (Vector2 swipeMove)
	{
		float swipeLength = swipeMove.magnitude;
		float moveSpeed = swipeLength * maxSwipeForce / maxSwipeLength;
		Vector2 moveForce = -swipeMove.normalized * moveSpeed;
		moveForce.y += constantForwardMoveForce;
		playerRigidbody.AddForce (moveForce);
		ApplySpeedLimits ();
	}
}
