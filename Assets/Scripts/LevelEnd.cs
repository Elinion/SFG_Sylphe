using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
	public GameObject levelEndHUD;

	void Awake ()
	{
		levelEndHUD.SetActive (false);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == Tags.Player) {
			levelEndHUD.SetActive (true);
		}
	}
}
