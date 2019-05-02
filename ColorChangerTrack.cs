using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerTrack : MonoBehaviour {

	public bool cycle;
	private Transform light;

	void Start () {
		light = transform.parent.Find("Light");
	}

	void OnTriggerEnter (Collider ball) {
		if(ball.GetComponent<Ball>()) {
			Color color = light.GetComponent<Light>().GetColor();
			ball.GetComponent<Ball>().ChangeColor(color);
			if(cycle) light.GetComponent<Light>().CycleColor();
		}
	}

}
