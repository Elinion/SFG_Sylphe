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
	public bool autoSignIn = false;

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

		if (autoSignIn) {
			if (Social.localUser.authenticated) {
				GoBackToMenu ();
			} else {
				SignIn ();
			}
		} 

		if (Social.localUser.authenticated) {
			signOutButton.SetActive (true);
			signOutDescription.SetActive (true);
		} else {
			signInButton.SetActive (true);
			signInDescription.SetActive (true);
		}
	}

	void Update ()
	{
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
			if (success) {
				GoBackToMenu ();
			} else {
				HideAllMessages ();
				signInFailedMessage.SetActive (true);
			}
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