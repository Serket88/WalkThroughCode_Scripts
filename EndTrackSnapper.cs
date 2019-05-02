using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrackSnapper : Snapper {

	public Transform northPole;

	public override void Snap () {
		// get list of all poles within range
		List<Transform> poles = northPole.GetComponent<Pole>().GetPoles();

		if(
			poles.Count > 0 &&
			poles.Exists(pole => !pole.GetComponent<Pole>().isNorth)
		) {
			// get pole to snap to
			Transform otherPole =
				poles.Find(pole => !pole.GetComponent<Pole>().isNorth);

			// rotate and transform
			transform.eulerAngles = otherPole.eulerAngles;
			transform.position =
				otherPole.position -
				transform.TransformVector(northPole.localPosition);

			// snap
			northPole.GetComponent<Pole>().Snap();
		}
	}

	public override void Unsnap () {
		northPole.GetComponent<Pole>().Unsnap();
	}
}
