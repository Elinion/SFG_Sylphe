using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public float minDistanceToGetPoints = 5f;
	public float maxPointsToGet = 100f;

	private Transform player;

	private int points = 0;

	public int Points {
		get { return points; }
	}

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag (Tags.Player).transform;
	}

	void Update ()
	{
		points = Mathf.Max (points, CalculatePoints ());
	}

	private int CalculatePoints ()
	{
		float points = 0;
		float distanceFromPlayer = (player.position - transform.position).magnitude;
		if (distanceFromPlayer <= minDistanceToGetPoints) {
			points = maxPointsToGet - distanceFromPlayer * maxPointsToGet / minDistanceToGetPoints;
		}
		return Mathf.FloorToInt (points);
	}

}
