using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterTrack : MonoBehaviour {

	private Transform light;
	private bool triggerOn;

	void Start () {
		light = transform.parent.Find("Light");
		triggerOn = true;
	}

	void OnTriggerEnter (Collider ball) {
		if(ball.GetComponent<Ball>()) {
			if(!triggerOn) return;
			GameObject[] otherTeleporters = GameObject.FindGameObjectsWithTag("Teleporter");
			GameObject teleporterEnd = otherTeleporters[0];
			int count = 0;
			for(int i = 0; i < otherTeleporters.Length; ++i) {
				Color c = otherTeleporters[i].transform.Find("Light").GetComponent<Light>().GetColor();
				if(
					light.GetComponent<Light>().GetColor().Equals(c) &&
					otherTeleporters[i].transform != this.transform.parent
				) {
					teleporterEnd = otherTeleporters[i];
					++count;
				}
			}
			if(count == 0) {
				Debug.Log("There's not other teleporter with the same color!");
			} else if(count == 1) {
				teleporterEnd.transform.Find("Cube").GetComponent<TeleporterTrack>().triggerOn = false;
				Vector3 newPosition = teleporterEnd.transform.Find("Cube").position;
				ball.GetComponent<Ball>().Teleport(newPosition);
				float x = teleporterEnd.transform.eulerAngles.x - transform.eulerAngles.x;
				float y = teleporterEnd.transform.eulerAngles.y - transform.eulerAngles.y - 180;
				float z = teleporterEnd.transform.eulerAngles.z - transform.eulerAngles.z;
				ball.GetComponent<Rigidbody>().velocity =
					Quaternion.Euler(x, y, z) * ball.GetComponent<Rigidbody>().velocity;
			} else if(count > 1) {
				Debug.Log("There are too many teleporters with the same color!");
			}
		}
	}

	void OnTriggerExit (Collider ball) {
		triggerOn = true;
	}

}
