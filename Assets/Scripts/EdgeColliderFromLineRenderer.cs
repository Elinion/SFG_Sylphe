using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeColliderFromLineRenderer : MonoBehaviour
{

	EdgeCollider2D edgeCollider;
	LineRenderer lineRenderer;

	void Awake ()
	{
		edgeCollider = GetComponent<EdgeCollider2D> ();
		lineRenderer = GetComponent<LineRenderer> ();
		PopulateLinePointsIntoEdgeCollider ();
	}

	private void PopulateLinePointsIntoEdgeCollider ()
	{
		int numberOfPoints = lineRenderer.positionCount;
		Vector3[] positions = new Vector3[numberOfPoints];
		lineRenderer.GetPositions (positions);
		Vector2[] points = new Vector2[numberOfPoints];
		for (int i = 0; i < numberOfPoints; i++) {
			points [i] = new Vector2 (positions [i].x, positions [i].y);
		}
		edgeCollider.points = points;
	}
}
