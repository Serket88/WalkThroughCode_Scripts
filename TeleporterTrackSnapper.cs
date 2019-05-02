using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterTrackSnapper : Snapper {

	public Transform northSouthPole;

	public override void Snap () {
		// get list of all poles within range
		List<Transform> poles = northSouthPole.GetComponent<Pole>().GetPoles();

		if(poles.Count > 0) {
			// get pole to snap to
			Transform otherPole = poles[0];

			// rotate and transform
			transform.eulerAngles = otherPole.eulerAngles;
			if(!otherPole.GetComponent<Pole>().isNorth)
				transform.Rotate(new Vector3(0, 180, 0));
			transform.position =
				otherPole.position -
				transform.TransformVector(northSouthPole.localPosition);

			// snap
			northSouthPole.GetComponent<Pole>().Snap();
		}
	}

	public override void Unsnap () {
		northSouthPole.GetComponent<Pole>().Unsnap();
	}
}
