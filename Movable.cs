using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(Interactable))]
    public class Movable : MonoBehaviour
    {
        //  Not sure what this does
        public Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers);

        //  Used to interface with the scene at large
        public GameManager gMan;
        //  Used to determine whether the piece is a part of UI or not
        public bool isFake = false;
        //  Used to determine which piece this is
        public int pieceIndex;
        //  Used to keep track of how big spawned tools should be
        private float growthMod;

        [SerializeField]    //  Used for increasing the size of ui tools
        private float uiGrowth;

        //-------------------------------------------------
        void Awake()
        {
            //Debug.Log("No Hand Hovering");
            growthMod = 1 / (gMan.toolReductionPercent / 100);
        }


        //-------------------------------------------------
        // Called when a Hand starts hovering over this object
        //-------------------------------------------------
        private void OnHandHoverBegin(Hand hand)
        {
            if(hand.GuessCurrentHandType() == Hand.HandType.Right)
            {
                //Debug.Log("Hovering hand:" + hand.name);
                if (isFake)
                {
                    //Debug.Log(this.name);
                    //  Increase the size slightly, try and vibrate briefly?

                    this.transform.localScale += new Vector3(uiGrowth, uiGrowth, uiGrowth);

                    //  Vibrate briefly
                    gMan.vibeRight();
                }
            }
        }


        //-------------------------------------------------
        // Called when a Hand stops hovering over this object
        //-------------------------------------------------
        private void OnHandHoverEnd(Hand hand)
        {
            if (hand.GuessCurrentHandType() == Hand.HandType.Right)
            {
                //Debug.Log("No Hand Hovering");
                if (isFake)
                {
                    //Debug.Log("Fake offHover");
                    //  Decrease the size slightly

                    this.transform.localScale -= new Vector3(uiGrowth, uiGrowth, uiGrowth);
                }
            }
        }


        //-------------------------------------------------
        // Called every Update() while a Hand is hovering over this object
        //-------------------------------------------------
        private void HandHoverUpdate(Hand hand)
        {
            if (hand.GuessCurrentHandType() == Hand.HandType.Right)
            {
                //  Trigger gets pulled
                if (hand.GetStandardInteractionButtonDown())
                {
                    //  Hand is not already holding this object
                    if (hand.currentAttachedObject != gameObject)
                    {
                        //  Hand is hovering over toolbox item
                        if (isFake)
                        {
                            //  Spawn a new item and attach it
                            GameObject newPiece = Instantiate(gMan.tools[pieceIndex]) as GameObject;
                            //newPiece.transform.localScale *= growthMod;
                            newPiece.transform.position = hand.transform.position;
                            newPiece.transform.rotation = hand.transform.rotation;
                            newPiece.GetComponent<Movable>().isFake = false;

                            // Call this to continue receiving HandHoverUpdate messages,
                            // and prevent the hand from hovering over anything else
                            hand.HoverLock(newPiece.GetComponent<Interactable>());

                            // Attach this object to the hand
                            hand.AttachObject(newPiece, newPiece.GetComponent<Movable>().attachmentFlags);
                        }
                        else
                        {
                            // Call this to continue receiving HandHoverUpdate messages,
                            // and prevent the hand from hovering over anything else
                            hand.HoverLock(GetComponent<Interactable>());

                            //  Unsnap in case
                            if (this.GetComponent<SnappingScript>())
                            {
                                GetComponent<SnappingScript>().Unsnap();
                            }

                            //this.GetComponentInChildren<MeshCollider>().convex = false;

                            //  if it's the ball enable physics
                            if (this.GetComponentInChildren<Ball>())
                            {
                                this.GetComponentInChildren<Rigidbody>().isKinematic = true;
                            }

                            // Attach this object to the hand
                            hand.AttachObject(gameObject, attachmentFlags);
                        }
                    }
                }
            }
        }


        //-------------------------------------------------
        // Called when this GameObject becomes attached to the hand
        //-------------------------------------------------
        private void OnAttachedToHand(Hand hand)
        {
            //Debug.Log("Attached to hand:  " + hand.name);
        }


        //-------------------------------------------------
        // Called when this GameObject is detached from the hand
        //-------------------------------------------------
        private void OnDetachedFromHand(Hand hand)
        {
            //Debug.Log("Detached from hand:  " + hand.name);
            // Call this to undo HoverLock
            hand.HoverUnlock(GetComponent<Interactable>());
        }


        //-------------------------------------------------
        // Called every Update() while this GameObject is attached to the hand
        //-------------------------------------------------
        private void HandAttachedUpdate(Hand hand)
        {
            //Trigger got released
            if (!hand.GetStandardInteractionButton())
            {
                // Detach ourselves late in the frame.
                // This is so that any vehicles the player is attached to
                // have a chance to finish updating themselves.
                // If we detach now, our position could be behind what it
                // will be at the end of the frame, and the object may appear
                // to teleport behind the hand when the player releases it.
                StartCoroutine(LateDetach(hand));
            }
        }

        //-------------------------------------------------
        private IEnumerator LateDetach(Hand hand)
        {
            yield return new WaitForEndOfFrame();

            /*
            if (this.GetComponentInChildren<Connector>())
            {
                Debug.Log("Trying to snap");
                this.GetComponentInChildren<Connector>().connectNear();
            }
            */

            //this.GetComponentInChildren<MeshCollider>().convex = false;

            if (this.GetComponent<SnappingScript>())
            {
                GetComponent<SnappingScript>().Snap();
            }

            //  if it's the ball enable physics
            if (this.GetComponentInChildren<Ball>())
            {
                this.GetComponentInChildren<Rigidbody>().isKinematic = false;
            }

            hand.DetachObject(gameObject);
        }

        //-------------------------------------------------
        // Called when this attached GameObject becomes the primary attached object
        //-------------------------------------------------
        private void OnHandFocusAcquired(Hand hand)
        {
        }


        //-------------------------------------------------
        // Called when another attached GameObject becomes the primary attached object
        //-------------------------------------------------
        private void OnHandFocusLost(Hand hand)
        {
        }
    }
}
