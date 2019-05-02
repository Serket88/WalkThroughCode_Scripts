using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedController))]
public class BreakControl : MonoBehaviour {

    public GameManager gMan;

    private GameObject fakeClaw;
    private SteamVR_TrackedController trackedController;

    private void Start()
    {
        trackedController = GetComponent<SteamVR_TrackedController>();
        trackedController.Gripped += snikt;
        trackedController.Ungripped += unSnikt;
        trackedController.MenuButtonUnclicked += controlPause;
    }

    private void Update()
    {
        //  happens every frame
    }

    private void snikt(object sender, ClickedEventArgs e)
    {
        //Debug.Log("Gripping!");
        fakeClaw = Instantiate(gMan.deleteTool) as GameObject;
        fakeClaw.transform.SetParent(this.transform);
        fakeClaw.transform.localPosition = new Vector3(0.0f, -0.02f, 0.06f);
        fakeClaw.transform.localRotation = Quaternion.Euler(90, 0, 0);
        fakeClaw.GetComponent<Knife>().gMan = gMan;
    }

    private void unSnikt(object sender, ClickedEventArgs e)
    {
        //Debug.Log("Ungripping!");
        Destroy(fakeClaw);
    }

    public void controlPause(object sender, ClickedEventArgs e)
    {
        gMan.pauseGame();
    }
}
