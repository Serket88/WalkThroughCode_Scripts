using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(SteamVR_LaserPointer))]
public class VRUIInput : MonoBehaviour
{
    private SteamVR_LaserPointer laserPointer;
    private SteamVR_TrackedController trackedController;

    private void OnEnable()
    {
        laserPointer = GetComponent<SteamVR_LaserPointer>();
        laserPointer.PointerIn -= HandlePointerIn;
        laserPointer.PointerIn += HandlePointerIn;
        laserPointer.PointerOut -= HandlePointerOut;
        laserPointer.PointerOut += HandlePointerOut;

        trackedController = GetComponent<SteamVR_TrackedController>();
        if (trackedController == null)
        {
            trackedController = GetComponentInParent<SteamVR_TrackedController>();
        }
        trackedController.TriggerClicked -= HandleTriggerClicked;
        trackedController.TriggerClicked += HandleTriggerClicked;
    }

    private void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        //Debug.Log("triggered");
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            //Debug.Log("attempted?");
            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
            //Debug.Log("executed");
        }
    }

    private void HandlePointerIn(object sender, PointerEventArgs e)
    {
        var button = e.target.GetComponent<Button>();
        if (button != null)
        {
            button.Select();
            //Debug.Log("HandlePointerIn", e.target.gameObject);
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                Debug.Log("things went horribly wrong");
            }
        }
    }

    private void HandlePointerOut(object sender, PointerEventArgs e)
    {
        var button = e.target.GetComponent<Button>();
        if (button != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            //Debug.Log("HandlePointerOut", e.target.gameObject);
        }
    }
}