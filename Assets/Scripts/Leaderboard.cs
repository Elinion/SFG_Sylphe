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
		Social.ShowLeaderboardUI ();
	}
}
