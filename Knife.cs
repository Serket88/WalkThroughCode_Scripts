using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {

    public GameManager gMan;

    //  If this is all the thing needs i'll be surprised
    private void OnTriggerEnter(Collider other)
    {
        gMan.vibeRight();
        Destroy(other.transform.parent.gameObject);
        //Destroy(other.gameObject);
    }
}
