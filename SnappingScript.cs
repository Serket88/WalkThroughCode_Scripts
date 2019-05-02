using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingScript : MonoBehaviour {

	public Snapper snapper;
	public bool snap, unsnap;

	public void Snap () {
		snapper.Snap();
	}

	public void Unsnap () {
		snapper.Unsnap();
	}

	void Update () {
		if(snap) Snap();
		else if(unsnap) Unsnap();
		snap = false;
		unsnap = false;
	}

}
