using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public float modifier = 1f;
	public Text scoreText;
	public ScoreCollider bestScoreCollider;
	public int bestPointsPerSecond = 32;
	public ScoreCollider goodScoreCollider;
	public int goodPointsPerSecond = 16;
	public ScoreCollider mediumScoreCollider;
	public int mediumPointsPerSecond = 8;
	public ScoreCollider lowScoreCollider;
	public int lowPointsPerSecond = 4;
	public int minPointsPerSecond = 2;

	private float score = 0f;

	void Update ()
	{
		float gainedPoints = GetPoints () * modifier;
		score += gainedPoints;
		score = Mathf.Max (score, 0f);
		scoreText.text = Mathf.FloorToInt (score).ToString ();
	}

	public void LosePoints (int pointsLost)
	{
		score -= pointsLost;
		score = Mathf.Max (score, 0f);
	}

	private float GetPoints ()
	{
		float points = minPointsPerSecond;
		if (bestScoreCollider.IsActive) {
			points = bestPointsPerSecond;
		} else if (goodScoreCollider.IsActive) {
			points = goodPointsPerSecond;
		} else if (mediumScoreCollider.IsActive) {
			points = mediumPointsPerSecond;
		} else if (lowScoreCollider.IsActive) {
			points = lowPointsPerSecond;
		}
		points *= Time.deltaTime;
		return points;
	}
}
