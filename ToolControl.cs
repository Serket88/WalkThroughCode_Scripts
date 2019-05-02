using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedController))]
public class ToolControl : MonoBehaviour
{
    //  GameManager
    public GameManager gMan;
    //  Determines what radius the track pieces should appear in
    private float toolRadius;
    //  Handles the radius to search for interaction points and the mask to search for them
    public float snapRadius = 1.0f;
    //  Determines the local rotation of each piece
    private float toolRat;

    //  List of track pieces available to the user; assigned in editor or some other way
    private List<GameObject> fakeTools;
    //  Reference to the tracked controller component
    private SteamVR_TrackedController trackedController;
    

    private void Start()
    {
        trackedController = GetComponent<SteamVR_TrackedController>();
        //  Assigns the Display and Destroy tool functions to the grip and ungrip events
        trackedController.Gripped += DisplayTools;
        trackedController.Ungripped += DestroyTools;
        trackedController.MenuButtonUnclicked += controlPause;
        trackedController.PadUnclicked += runProg;
        trackedController.TriggerUnclicked += trigInteract;

        fakeTools = new List<GameObject>();
    }

    private void Update()
    {
        //if (trackedController.OnGripped())
    }

    //  Displays all available track pieces when the user is gripping the pads
    private void DisplayTools(object sender, ClickedEventArgs e)
    {
        //Debug.Log("Gripping!");

        int numTools = gMan.tools.Count;
        float degSeparation = 360 / numTools;
        float curTheta = 0.0f;
        int toolIndex = 0;
        float uiScale = gMan.toolReductionPercent / 100;
        toolRadius = gMan.toolRadius;
        toolRat = gMan.toolRotation;

        foreach (GameObject tool in gMan.tools)
        {
            GameObject uiTool = Instantiate(tool) as GameObject;
            tool.gameObject.GetComponent<Valve.VR.InteractionSystem.Movable>().isFake = true;
            tool.gameObject.GetComponent<Valve.VR.InteractionSystem.Movable>().pieceIndex = toolIndex;
            tool.gameObject.GetComponent<Valve.VR.InteractionSystem.Movable>().gMan = gMan;
            uiTool.transform.SetParent(this.transform);

            uiTool.transform.localScale *= uiScale;

            float thetaRad = curTheta * (Mathf.PI / 180);

            float newX = Mathf.Cos(thetaRad) * toolRadius;
            float newZ = Mathf.Sin(thetaRad) * toolRadius;

            uiTool.transform.localPosition = new Vector3(newX, 0.0f, newZ);
            uiTool.transform.localRotation = Quaternion.identity;

            //  added slight rotation to prevent overlap
            uiTool.transform.Rotate(Vector3.up, toolRat, Space.Self);
            //uiTool.transform.Rotate(Vector3.up, (curTheta - 102), Space.Self);
            //uiTool.transform.Rotate(Vector3.up, Time.deltaTime, Space.Self);

            fakeTools.Add(uiTool);
            curTheta += degSeparation;
            toolIndex++;
        }
    }

    //  Destroys all the track pieces when the user releases the pads
    private void DestroyTools(object sender, ClickedEventArgs e)
    {
        //Debug.Log("Ungripping!");
        
        foreach(GameObject tool in fakeTools)
        {
            Destroy(tool);
        }

        fakeTools.Clear();
    }

    public void controlPause(object sender, ClickedEventArgs e)
    {
        gMan.pauseGame();
    }

    public void runProg(object sender, ClickedEventArgs e)
    {
        gMan.run();
    }

    public void trigInteract(object sender, ClickedEventArgs e)
    {
        //  Check for nearby connection points
        //Collider[] inCollide = Physics.OverlapSphere(this.transform.position, snapRadius, mask);
        Collider[] inCollide = Physics.OverlapSphere(this.transform.position, snapRadius);
        Debug.Log("inCollide count: " + inCollide.Length);

        foreach(Collider potLight in inCollide)
        {
            Debug.Log("Thing found: " + potLight.name);
            if(potLight.transform.parent.GetComponent<LightChanger>())
            {
                Debug.Log("Found one");
                potLight.transform.parent.GetComponent<LightChanger>().CycleColor();
            }
        }
    }

    /*
    private void TryDash(object sender, ClickedEventArgs e)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance > minDashRange && hit.distance < maxDashRange)
            {
                StartCoroutine(DoDash(hit.point));
            }
        }
    }


    private IEnumerator DoDash(Vector3 endPoint)
    {
        if (maskAnimator != null)
            maskAnimator.SetBool("Mask", true);

        yield return new WaitForSeconds(0.1f);

        float elapsed = 0f;

        Vector3 startPoint = cameraRigRoot.position;

        while (elapsed < dashTime)
        {
            elapsed += Time.deltaTime;
            float elapsedPct = elapsed / dashTime;

            cameraRigRoot.position = Vector3.Lerp(startPoint, endPoint, elapsedPct);
            yield return null;
        }

        if (maskAnimator != null)
            maskAnimator.SetBool("Mask", false);
    }
    */
}