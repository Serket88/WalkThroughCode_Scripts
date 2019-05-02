using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnelTrackSnapper : Snapper {

	public Transform northPole1;
	public Transform northPole2;
	public Transform southPole;

	public override void Snap () {
		// get list of all poles within range
		List<Transform> poles = northPole1.GetComponent<Pole>().GetPoles();
		poles.AddRange(northPole2.GetComponent<Pole>().GetPoles());
		poles.AddRange(southPole.GetComponent<Pole>().GetPoles());

		if(poles.Count > 0) {
			// get poles to snap
			Transform myPole, otherPole;
			if(poles.Exists(pole => !pole.GetComponent<Pole>().isNorth)) {
				otherPole = poles.Find(
					pole => !pole.GetComponent<Pole>().isNorth
				);
				if(
					Vector3.Distance(northPole1.position, otherPole.position) <
					Vector3.Distance(northPole2.position, otherPole.position)
				) {
					myPole = northPole1;
				}
				else {
					myPole = northPole2;
				}
			} else {
				otherPole = poles[0];
				myPole = southPole;
			}

			// rotate and transform
			transform.eulerAngles = otherPole.eulerAngles;
			transform.Rotate(-myPole.localEulerAngles);
			transform.position =
				otherPole.position -
				transform.TransformVector(myPole.localPosition);

			// snap
			northPole1.GetComponent<Pole>().Snap();
			northPole2.GetComponent<Pole>().Snap();
			southPole.GetComponent<Pole>().Snap();
		}
	}

	public override void Unsnap () {
		northPole1.GetComponent<Pole>().Unsnap();
		northPole2.GetComponent<Pole>().Unsnap();
		southPole.GetComponent<Pole>().Unsnap();
	}

}
