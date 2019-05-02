using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour {

	public static float radius = 0.15f;
	public bool isNorth;
	public List<Transform> snappedTo = new List<Transform>();

	public bool IsSnapped() {
		return snappedTo.Count != 0;
	}

	public List<Transform> GetPoles () {
		List<Transform> result = new List<Transform>();
		Collider[] colliders = Physics.OverlapSphere(
			transform.position, radius
		);
		foreach(Collider collider in colliders) {
			Pole poleScript = collider.GetComponent<Pole>();
			if(
				poleScript &&
				poleScript.snappedTo.Count == 0 &&
				collider.transform != transform
			) {
				result.Add(collider.transform);
			}
		}
		return result;
	}

	public void Snap () {
		snappedTo.Clear();
		List<Transform> poles = GetPoles();
		foreach(Transform pole in poles) {
			snappedTo.Add(pole);
			pole.GetComponent<Pole>().Snap(transform);
		}
	}

	public void Snap (Transform pole) {
		snappedTo.Add(pole);
	}

	public void Unsnap () {
		foreach(Transform pole in snappedTo)
			pole.GetComponent<Pole>().Unsnap(transform);
		snappedTo.Clear();
	}

	public void Unsnap (Transform pole) {
		snappedTo.Remove(pole);
	}

}
