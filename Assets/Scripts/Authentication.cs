using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;

public class Authentication : MonoBehaviour
{
	public GameObject signInButton;
	public GameObject signOutButton;
	public GameObject signInDescription;
	public GameObject signOutDescription;
	public GameObject signingInMessage;
	public GameObject signingOutMessage;
	public GameObject signInFailedMessage;
	public bool skipWhenAuthenticated = false;

	private bool receivedSignInResponse = false;
	private bool signInSuccessful = false;
	private bool signingOut = false;

	void Awake ()
	{
		signInButton.SetActive (false);
		signInDescription.SetActive (false);
		signOutButton.SetActive (false);
		signOutDescription.SetActive (false);
		HideAllMessages ();
	}

	void Start ()
	{
		GooglePlayGames.PlayGamesPlatform.Activate ();

		if (Social.localUser.authenticated) {
			if (skipWhenAuthenticated) {
				GoBackToMenu ();
			} else {
				signOutDescription.SetActive (true);
				signOutButton.SetActive (true); 
			}
		} else {
			signInDescription.SetActive (true);
			signInButton.SetActive (true);
		}
	}

	void Update ()
	{
		if (receivedSignInResponse) {
			receivedSignInResponse = false;
			if (signInSuccessful) {
				GoBackToMenu ();
			} else {
				HideAllMessages ();
				signInFailedMessage.SetActive (true);
			}
		}
		if (signingOut) {
			if (!Social.localUser.authenticated) {
				GoBackToMenu ();
			}
		}
	}

	public void SignIn ()
	{
		HideAllMessages ();
		signingInMessage.SetActive (true);
		Social.localUser.Authenticate ((bool success) => {
			receivedSignInResponse = true;
			signInSuccessful = success;
		});
	}

	public void SignOut ()
	{
		HideAllMessages ();
		signingOutMessage.SetActive (true);
		signingOut = true;
		((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut ();
	}

	public void Skip ()
	{
		GoBackToMenu ();
	}

	private void GoBackToMenu ()
	{
		SceneManager.LoadScene ("menu");
	}

	private void HideAllMessages ()
	{
		signingInMessage.SetActive (false);
		signingOutMessage.SetActive (false);
		signInFailedMessage.SetActive (false);
	}
}