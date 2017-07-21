using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public Text scoreText;
	public float displayScoreMultiplier = 1f;
	public ScoreCollider bestScoreCollider;
	public int bestPointsPerSecond = 32;
	public ScoreCollider goodScoreCollider;
	public int goodPointsPerSecond = 16;
	public ScoreCollider mediumScoreCollider;
	public int mediumPointsPerSecond = 8;
	public ScoreCollider lowScoreCollider;
	public int lowPointsPerSecond = 4;
	public int minPointsPerSecond = 2;
	public GameObject multiplierHUD;
	public Text multiplierText;
	public float defaultMultiplier = 1f;
	public float bonusDuration = 3f;
	public float bonusMultiplier = 2f;
	public float bonusActivationTime = 2.5f;
	public float goodBonusMultiplier = 3f;

	private float score = 0f;
	private float timeOnLine = 0f;
	private float lastTimeOnLine = 0f;
	private float lastTimeBonusWasActivated = 0f;
	private float lastMultiplierReached;

	void Awake ()
	{
		lastMultiplierReached = defaultMultiplier;
		multiplierHUD.SetActive (false);
	}

	void Update ()
	{
		float multiplier = GetMultiplier ();
		float gainedPoints = GetPoints () * multiplier;
		score += gainedPoints;
		score = Mathf.Max (score, 0f);

		float displayScore = score * displayScoreMultiplier;
		scoreText.text = Mathf.FloorToInt (displayScore).ToString ();

		// Show the score multiplier if a bonus is currently activated
		if (multiplier > defaultMultiplier) {
			multiplierHUD.SetActive (true);
			multiplierText.text = multiplier.ToString ("F1");
		} else {
			multiplierHUD.SetActive (false);
		}
	}

	public void LosePoints (int pointsLost)
	{
		score -= pointsLost;
		score = Mathf.Max (score, 0f);
	}

	public void CancelAllBonuses ()
	{
		lastMultiplierReached = defaultMultiplier;
		lastTimeOnLine = Time.time;
		timeOnLine = 0f;
	}

	private float GetMultiplier ()
	{
		float multiplier = defaultMultiplier;

		// Calculate how long the user has been on the line
		if (goodScoreCollider.IsActive) {
			timeOnLine += Time.deltaTime;
			lastTimeOnLine = Time.time;
		} else {
			timeOnLine = 0f;
		}
			
		// If the user has been long enough on the line,
		// increase the score multiplier
		if (timeOnLine > bonusActivationTime) {
			if (bestScoreCollider.IsActive) {
				multiplier = goodBonusMultiplier;
			} else {
				multiplier = bonusMultiplier;
			}
		} 
			
		// If a bonus is currently activated, check if the player should get a better one
		// The bonus remains active for the bonusDuration time,
		// even if the player isn't on the line anymore
		if (Time.time - lastTimeBonusWasActivated < bonusDuration) {
			if (lastMultiplierReached < multiplier) {
				lastMultiplierReached = multiplier;
				lastTimeBonusWasActivated = Time.time;
			} else {
				multiplier = lastMultiplierReached;
			}
		}
		// Otherwise grant bonus if the player has been long enough on the line
		else if (multiplier > defaultMultiplier) {
			lastMultiplierReached = multiplier;
			lastTimeBonusWasActivated = Time.time;
		}
		return multiplier;
	}

	private float GetPoints ()
	{
		float points = minPointsPerSecond;

		// The player has multipler circle collider with increasing radius
		// The shortest gives the most point, the longest gives the least points
		if (bestScoreCollider.IsActive) {
			points = bestPointsPerSecond;
			timeOnLine += Time.deltaTime;
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
