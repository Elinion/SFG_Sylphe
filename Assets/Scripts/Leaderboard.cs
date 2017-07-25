using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class Leaderboard : MonoBehaviour
{
	public void Show ()
	{
		PlayGamesPlatform.Instance.LoadScores (
			GPGSIds.leaderboard_general,
			LeaderboardStart.PlayerCentered,
			100,
			LeaderboardCollection.Public,
			LeaderboardTimeSpan.AllTime,
			(data) => {
			});
	}
}
