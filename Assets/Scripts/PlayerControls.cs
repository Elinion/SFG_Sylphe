using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
	public float constantForwardMoveForce = 50f;
	public float moveForce = 50f;
	public float maxHorizontalSpeed = 5f;
	public float maxVerticalSpeed = 5f;

	private Vector2 touchOrigin = -Vector2.one;
	private Rigidbody2D playerRigidbody;
	private float swipeStartTime = 0f;
	private float swipeEndTime = 0f;
	private Vector2 swipeInput = Vector2.zero;
	private bool hasUnhandledSwipe = false;

	void Awake ()
	{
		playerRigidbody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate ()
	{
		Vector2 input = Vector2.zero;
		if (hasUnhandledSwipe) {
			input = swipeInput;
			hasUnhandledSwipe = false;
		}
		MovePlayer (input);
	}

	void Update ()
	{
		HandleInput ();
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

	private Vector2 GetKeyboardInput ()
	{
		Vector2 input = Vector2.zero;
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			input.x = 300;
		} 
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			input.x = -300;
		}
		return input;
	}

	private Vector2 GetSwipeInput ()
	{
		Vector2 swipe = Vector2.zero;
		if (Input.touchCount > 0) {
			Touch touch = Input.touches [0];
			if (touch.phase == TouchPhase.Began) {
				touchOrigin = touch.position;
				swipeStartTime = Time.time;
			} else if (touch.phase == TouchPhase.Ended) {
				swipe.x = touch.position.x - touchOrigin.x;
				swipe.y = touch.position.y - touchOrigin.y;
				touchOrigin = -Vector2.one;
				swipeEndTime = Time.time;
				float swipeTime = (swipeEndTime - swipeStartTime) * 1000f;
				float swipeLength = swipe.magnitude;
				float swipeSpeed = swipeLength / swipeTime;
				swipe = swipe.normalized;
				swipe *= swipeSpeed;
				hasUnhandledSwipe = true;
			}
		}
		return swipe;
	}

	private void HandleInput ()
	{
		if (!hasUnhandledSwipe) {
			swipeInput = GetSwipeInput ();
		}
	}

	private void MovePlayer (Vector2 move)
	{
		move *= -moveForce;
		move.y += constantForwardMoveForce;
		playerRigidbody.AddForce (move);
		ApplySpeedLimits ();
	}
}
