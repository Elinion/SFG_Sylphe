using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
	public GameObject levelEndHUD;
	public GameObject newRecord;
	public GameObject bestScore;
	public Text bestScoreText;
	public Text finishMessage;
	public GameObject zeroStarPerformance;
	public GameObject oneStarPerformance;
	public int oneStarMinScore;
	public GameObject twoStarPerformance;
	public int twoStarMinScore;
	public GameObject threeStarPerformance;
	public int threeStarMinScore;
	public GameObject trophyPerformance;
	public int trophyMinScore;

	private Player player;
	private Score score;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag (Tags.Player).GetComponent<Player> ();
		score = GameObject.FindGameObjectWithTag (Tags.Score).GetComponent<Score> ();
		levelEndHUD.SetActive (false);
	}

	void Start ()
	{
		float score = GameManager.instance.dataToSave.bestScore;
		bestScoreText.text = Mathf.FloorToInt (score).ToString ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == Tags.Player) {
			ShowFinishHUD ();
			ShowPlayerPerformance ();
			HandleNewRecords ();
			StopPlayer ();
		}
	}

	private void HandleNewRecords ()
	{
		if (score.Points > GameManager.instance.dataToSave.bestScore) {
			GameManager.instance.dataToSave.bestScore = score.Points;
			GameManager.instance.SaveData ();
			bestScore.SetActive (true);
			bestScoreText.text = score.Points.ToString ();
		} else {
			newRecord.SetActive (false);
		}
	}

	private void ShowFinishHUD ()
	{
		levelEndHUD.SetActive (true);
	}

	private void ShowPlayerPerformance ()
	{
		if (score.Points >= trophyMinScore) {
			trophyPerformance.SetActive (true);
		} else if (score.Points >= threeStarMinScore) {
			threeStarPerformance.SetActive (true);
		} else if (score.Points >= twoStarMinScore) {
			twoStarPerformance.SetActive (true);
		} else if (score.Points >= oneStarMinScore) {
			oneStarPerformance.SetActive (true);
		} else {
			zeroStarPerformance.SetActive (true);
		}
	}

	private void StopPlayer ()
	{
		player.Freeze ();
		score.enabled = false;
	}
}
