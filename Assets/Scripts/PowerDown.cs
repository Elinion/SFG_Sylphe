using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDown : MonoBehaviour
{
	public int pointsLost = 50;
	public float rotationSpeed = 5f;

	private Score score;
	private CameraShake cameraShake;

	void Awake ()
	{
		score = GameObject.FindGameObjectWithTag (Tags.Score).GetComponent<Score> ();
		cameraShake = GameObject.FindGameObjectWithTag (Tags.MainCamera).GetComponent<CameraShake> ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == Tags.Player) {
			score.LosePoints (pointsLost);
			cameraShake.Shake ();
		}
	}

	void Update ()
	{
		transform.RotateAround (transform.position, Vector3.back, rotationSpeed * Time.deltaTime);
	}
}
