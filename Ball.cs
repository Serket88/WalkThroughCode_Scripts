using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float maxAngularVelocity;

	void Start () {
		GetComponent<Rigidbody>().maxAngularVelocity = maxAngularVelocity;
	}

	public Color GetColor () {
		return GetComponent<Renderer>().material.color;
	}

	public void ChangeColor (Color color) {
		GetComponent<Renderer>().material.color = color;
	}

	public void Teleport (Vector3 newPosition) {
		transform.position = newPosition;
	}

}
