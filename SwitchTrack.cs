using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrack : MonoBehaviour {

	private Transform light;
	private Transform falseTrack;
	private Transform trueTrack;

	void Start () {
		light = transform.parent.Find("Light");
		falseTrack = transform.parent.Find("FalseTrack");
		trueTrack = transform.parent.Find("TrueTrack");
		trueTrack.GetComponent<Collider>().enabled = false;
	}

	void OnTriggerEnter (Collider ball) {
		if(ball.GetComponent<Ball>()) {
			Color lightColor = light.GetComponent<Light>().GetColor();
			Color ballColor = ball.GetComponent<Ball>().GetColor();
			if(lightColor.Equals(ballColor)) {
				falseTrack.GetComponent<Collider>().enabled = false;
				trueTrack.GetComponent<Collider>().enabled = true;
			} else {
				falseTrack.GetComponent<Collider>().enabled = true;
				trueTrack.GetComponent<Collider>().enabled = false;
			}
		}
	}

}
