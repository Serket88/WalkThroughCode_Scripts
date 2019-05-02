using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour {

    //  What layer do you want to look for O-points on?
    public LayerMask mask;

    //  Stores reference to O-point
    private Transform oPoint;
    //  Stores reference to parent
    private Transform track;

    private void Awake()
    {
        track = this.transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter called with " + other.name);
        oPoint = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        oPoint = null;
    }

    public void connectNear()
    {
        //Debug.Log("connectNear was called in " + this.name);
        if (oPoint != null)
        {
            track.transform.eulerAngles = oPoint.eulerAngles;

            track.transform.position = oPoint.position - transform.TransformVector(this.transform.localPosition);

            /*
            //  Handles orientation
            track.rotation = oPoint.rotation;

            Vector3 transVec = oPoint.position - this.transform.position;
            track.position += transVec;
            //  Magic number = object scale * distance to endpoint
            track.position += new Vector3(0.0f, 0.0f, -0.48375f);
            */
        }
    }
}
