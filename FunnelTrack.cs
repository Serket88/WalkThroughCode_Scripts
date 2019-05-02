using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnelTrack : MonoBehaviour {

	private Transform straightTrack;
	private Transform curvedTrack;

	void Start () {
		straightTrack = transform.parent.Find("StraightTrack2");
		curvedTrack = transform.parent.Find("CurvedTrack");
	}

	void OnTriggerEnter (Collider ball) {
		if(ball.GetComponent<Ball>()) {
			if(name == "Cube1") {
				curvedTrack.GetComponent<Collider>().enabled = false;
				straightTrack.GetComponent<Collider>().enabled = true;
			} else if(name == "Cube2") {
				curvedTrack.GetComponent<Collider>().enabled = true;
				straightTrack.GetComponent<Collider>().enabled = false;
			}
		}
	}

}
