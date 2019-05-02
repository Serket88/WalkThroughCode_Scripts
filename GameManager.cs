using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


// for general game environment handling
public class GameManager : MonoBehaviour
{
    //  Reference to the CameraRig
    public GameObject player;

    //  List of potential pieces to be spawned
    public List<GameObject> tools = new List<GameObject>();
    public GameObject deleteTool;
    public float toolReductionPercent;
    public float toolRadius = 1.0f;
    public float toolRotation;

    //  Controller references
    public GameObject leftControl;
    public GameObject rightControl;
    public GameObject leftHand;
    public GameObject rightHand;

    //  Reference to the one and only start track
    public GameObject startTrack;
    public RectTransform vicstatus;

    //  Handlers for the pause menu
    public GameObject pauseMenu;
    private bool isPaused;

    private void Awake()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    //=============================================================================
    //
    // Purpose: Functions for handling menu i/o.  Should probably be in a different script but hey, we're crunching.
    //
    //=============================================================================

    public void pauseGame()
    {
        if(!isPaused)
        {
            Debug.Log("You just paused the game");

            //  Enable the lasers
            leftControl.GetComponent<SteamVR_LaserPointer>().enabled = true;
            leftControl.GetComponent<VRUIInput>().enabled = true;
            rightControl.GetComponent<SteamVR_LaserPointer>().enabled = true;
            rightControl.GetComponent<VRUIInput>().enabled = true;

            //  Disable the break control and hand component
            leftControl.GetComponent<ToolControl>().enabled = false;
            leftHand.SetActive(false);
            rightControl.GetComponent<BreakControl>().enabled = false;
            rightHand.SetActive(false);

            //  Enable pause UI
            pauseMenu.GetComponent<Canvas>().enabled = true;
            pauseMenu.transform.parent = player.transform;

            //  Stop time
            Time.timeScale = 0.0f;
            //  Set lock
            isPaused = true;
        } else
        {
            Debug.Log("You just unpaused the game");

            //  Restart time
            Time.timeScale = 1;

            //  Disable pause UI
            pauseMenu.transform.parent = player.transform.GetChild(2).GetChild(0).transform;
            pauseMenu.GetComponent<Canvas>().enabled = false;

            //  Disable the lasers
            leftControl.GetComponent<SteamVR_LaserPointer>().enabled = false;
            leftControl.GetComponent<VRUIInput>().enabled = false;
            rightControl.GetComponent<SteamVR_LaserPointer>().enabled = false;
            rightControl.GetComponent<VRUIInput>().enabled = false;

            //  Enable the controls and hands
            leftControl.GetComponent<ToolControl>().enabled = true;
            leftHand.SetActive(true);
            rightControl.GetComponent<BreakControl>().enabled = true;
            rightHand.SetActive(true);

            //  Set lock
            isPaused = false;
        }
    }

    public void quitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

    public void sceneLoad(int sceNum)
    {
        Debug.Log("Loading scene #" + sceNum);
        SceneManager.LoadScene(sceNum);
    }

    //  "Start" the program (call the ball spawn)
    public void run()
    {
        if(startTrack != null)
        {
            Debug.Log("Attempting to spawn");
            startTrack.GetComponentInChildren<StartTrack>().SpawnTestBall();
        }
    }

    //  Win the game
    public void win()
    {
        vicstatus.GetComponent<TextMeshProUGUI>().text = "COMPLETE SUCCESS!";
    }

    //  Vibrate the right controller
    public void vibeRight()
    {
        SteamVR_TrackedController rTracked = rightControl.GetComponent<SteamVR_TrackedController>();
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)rTracked.controllerIndex);
        StartCoroutine(LongVibration(device, 0.1f, 800));
    }

    IEnumerator LongVibration(SteamVR_Controller.Device device, float length, ushort strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            //  Vibrate for length at given strength
            device.TriggerHapticPulse(strength);
            yield return null;
        }
    }
}
