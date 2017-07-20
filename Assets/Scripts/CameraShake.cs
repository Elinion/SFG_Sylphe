using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public float duration = .5f;
	public float magnitude = .2f;

	public void Shake ()
	{
		StartCoroutine (DoShake ());
	}

	IEnumerator DoShake ()
	{
		float elapsed = 0.0f;

		Vector3 originalCamPos = transform.localPosition;

		while (elapsed < duration) {

			elapsed += Time.deltaTime;          

			float percentComplete = elapsed / duration;         
			float damper = 1.0f - Mathf.Clamp (4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;

			transform.localPosition = new Vector3 (x, y, originalCamPos.z);

			yield return null;
		}

		transform.localPosition = originalCamPos;
	}
}
