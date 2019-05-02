using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour {

	public List<Color> colors;
	public Color startColor;

	void Start () {
		ChangeColor(startColor);
	}

	public Color GetColor () {
		return GetComponent<Renderer>().material.color;
	}

	public void ChangeColor (Color color) {
		GetComponent<Renderer>().material.color = color;
	}

	public void CycleColor () {
        Debug.Log("Called in light");
		for(int i = 0; i < colors.Count; ++i) {
			if(GetColor().Equals(colors[i])) {
				if(i == colors.Count - 1) ChangeColor(colors[0]);
				else ChangeColor(colors[i+1]);
				return;
			}
		}
		ChangeColor(colors[0]);
	}

}
