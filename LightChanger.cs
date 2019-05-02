using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChanger : MonoBehaviour {

	public GameObject light;

	public void CycleColor () {
        Debug.Log("Changing " + this.name);
		light.GetComponent<Light>().CycleColor();
	}

}
