using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnPlane : MonoBehaviour
{

    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] ARAnchorManager anchorManager;
    [SerializeField] ARPlaneManager planeManager;
    [SerializeField] Camera mainCam;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    // Update is called once per frame
    void Update()
    {
        if(!RobbieManager.Instance.isRobbieActive && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                if(raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    ARRaycastHit hit = hits[0];
                    Pose hitPose = hit.pose;
                    if (hit.trackable is ARPlane plane)
                    {
                        ARAnchor toAttach = anchorManager.AttachAnchor(plane, hitPose);

                        if (toAttach != null)
                        {
                            RobbieManager.Instance.PositionRobbie(toAttach);
                        }
                    }
                }
            }
        }
    }
}
