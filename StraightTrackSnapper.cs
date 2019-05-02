using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightTrackSnapper : Snapper {

	public Transform northPole;
	public Transform southPole;

	public override void Snap () {
		// get list of all poles within range
		List<Transform> poles = northPole.GetComponent<Pole>().GetPoles();
		poles.AddRange(southPole.GetComponent<Pole>().GetPoles());

		if(poles.Count > 0) {
			// get poles to snap
			Transform myPole, otherPole;
			if(poles.Exists(pole => !pole.GetComponent<Pole>().isNorth)) {
				otherPole = poles.Find(
					pole => !pole.GetComponent<Pole>().isNorth
				);
				myPole = northPole;
			} else {
				otherPole = poles[0];
				myPole = southPole;
			}

			// rotate and transform
			transform.eulerAngles = otherPole.eulerAngles;
			transform.position =
				otherPole.position -
				transform.TransformVector(myPole.localPosition);

			// snap
			northPole.GetComponent<Pole>().Snap();
			southPole.GetComponent<Pole>().Snap();
		}
	}

	public override void Unsnap () {
		northPole.GetComponent<Pole>().Unsnap();
		southPole.GetComponent<Pole>().Unsnap();
	}

}
