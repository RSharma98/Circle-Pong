using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour {

	LineRenderer lineRenderer;
	public float circleSize = 5;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		if(lineRenderer != null){
			lineRenderer.positionCount = 360;
			for (int i = 0; i < 360; i++) {
				float theta = (i / 360f) * 2 * Mathf.PI;	//Calculate theta to draw accurate circle
				lineRenderer.SetPosition(i, new Vector2(Mathf.Sin(theta) * circleSize, Mathf.Cos(theta) * circleSize));
			}
		}
	}
}
