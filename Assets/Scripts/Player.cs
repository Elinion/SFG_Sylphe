using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	private Rigidbody2D rb;
	private PlayerControls controls;

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D> ();
		controls = GetComponent<PlayerControls> ();
	}

	public void Freeze ()
	{
		controls.enabled = false;
		rb.isKinematic = true;
		rb.simulated = false;
	}

	public void Unfreeze ()
	{
		controls.enabled = true;
		rb.isKinematic = false;
		rb.simulated = true;
	}
}
