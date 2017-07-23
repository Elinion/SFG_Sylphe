using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
	private Rigidbody2D playerRigidbody;

	void Awake ()
	{
		playerRigidbody = GameObject.FindGameObjectWithTag (Tags.Player).GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		if (playerRigidbody.velocity.magnitude > 0) {
			// Rotating the ball also rotates the bonus visual effects
			Vector2 velocity = playerRigidbody.velocity * -1;
			transform.rotation = Quaternion.LookRotation (velocity);
		}
	}

}
