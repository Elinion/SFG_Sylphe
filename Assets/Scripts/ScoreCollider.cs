using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCollider : MonoBehaviour
{
	private bool isActive = false;

	public bool IsActive {
		get { return isActive; }
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == Tags.SilverLine) {
			isActive = true;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == Tags.SilverLine) {
			isActive = false;
		}
	}
}
