using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrack : MonoBehaviour {

	public bool spawnBall;
	public GameObject TestBall;

	public void SpawnTestBall () {
		Vector3 shift = new Vector3(0, 0.5f, 0);
		Vector3 position = transform.position + shift;
		Object.Instantiate(TestBall, position, Quaternion.identity);
	}

	void Update () {
		if(spawnBall) SpawnTestBall();
		spawnBall = false;
	}

}
