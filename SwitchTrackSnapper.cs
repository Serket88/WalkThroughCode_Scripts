using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrackSnapper : Snapper {

	public Transform northPole;
	public Transform southPole1;
	public Transform southPole2;

	public override void Snap () {
		// get list of all poles within range
		List<Transform> poles = northPole.GetComponent<Pole>().GetPoles();
		poles.AddRange(southPole1.GetComponent<Pole>().GetPoles());
		poles.AddRange(southPole2.GetComponent<Pole>().GetPoles());

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
				if(
					Vector3.Distance(southPole1.position, otherPole.position) <
					Vector3.Distance(southPole2.position, otherPole.position)
				) {
					myPole = southPole1;
				}
				else {
					myPole = southPole2;
				}
			}

			// rotate and transform
			transform.eulerAngles = otherPole.eulerAngles;
			transform.Rotate(-myPole.localEulerAngles);
			transform.position =
				otherPole.position -
				transform.TransformVector(myPole.localPosition);

			// snap
			northPole.GetComponent<Pole>().Snap();
			southPole1.GetComponent<Pole>().Snap();
			southPole2.GetComponent<Pole>().Snap();
		}
	}

	public override void Unsnap () {
		northPole.GetComponent<Pole>().Unsnap();
		southPole1.GetComponent<Pole>().Unsnap();
		southPole2.GetComponent<Pole>().Unsnap();
	}

}
