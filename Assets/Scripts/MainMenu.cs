using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MainMenu : MonoBehaviour
{

	public GameObject leaderboardButton;

	void Awake ()
	{
		leaderboardButton.SetActive (Social.localUser.authenticated);
	}
}
