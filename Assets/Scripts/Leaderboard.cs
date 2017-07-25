using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Leaderboard : MonoBehaviour
{
	public void Show ()
	{
		Social.ShowLeaderboardUI ();
	}
}
