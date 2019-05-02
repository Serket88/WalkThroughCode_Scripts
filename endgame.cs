using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endgame : MonoBehaviour {

    public GameManager gMan;

    //  If this is all the thing needs i'll be surprised
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.GetComponent<startball>())
        {
            Debug.Log("Endgame");
            gMan.win();
        }
    }
}
