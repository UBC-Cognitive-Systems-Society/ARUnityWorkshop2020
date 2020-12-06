# Workshop Steps
Note that this is a very, very rough script and not very detailed.
Could use a lot of improvement

1. Showcase starter project
  1. Go over Unity window components
  1. Explain game object
  1. Explain components
  1. Explain C#
    1. What a class is,
    1. What variables are
  1. Show monobehaviour
    1. Start and Update
    1. Explain how to access transform
    1. Time.deltaTime
1. Remove non AR components
  1. Camera
  1. MovementController
  1. MouseFollower
  1. Plane
1. Set Up AR
  1. GameObject > XR
  1. AR Session, AR session Origin
  1. AR Plane Manager, AR Anchor Manager, AR Raycast Manager
  1. Drag in a plane prefab to the plane manager
  1. Fix missing Camera References
1. Add placeholder components
  1. Place On Plane
  1. Drag in cube prefab
  1. Add missing code from place on plane
1. Robbie Manager
  1. Add robbie back into scene
  1. Add Robbie Manager
  1. Add UI
  1. Change place on plane to call robbie manager toattach: Instead of Instantiate, replace with `RobbieManager.Instance.PositionRobbie(toAttach)`
  1. Add missing code from RobbieManager
1. Add Back in Robbie character controller
  1. Make sure look at camera object is set
  1. Add missing code from ARCharacterController

# Missing Code
### PlaceOnPlane.cs
```
if (Input.touchCount > 0)
{
    Touch touch = Input.GetTouch(0);
    if (touch.phase == TouchPhase.Began)
    {
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            ARRaycastHit hit = hits[0];
            Pose hitPose = hit.pose;
            if (hit.trackable is ARPlane plane)
            {
                ARAnchor toAttach = anchorManager.AttachAnchor(plane, hitPose);
                if(toAttach != null){
                  Instantiate(robbie, toAttach.transform);
                }
            }
        }
    }
}        
```
### RobbieManager.cs
```
public void PositionRobbie(ARAnchor anchor)
        {
            isRobbieActive = true;
            currentAnchor = anchor;
            robbie.transform.parent = currentAnchor.transform;
            robbie.transform.localPosition = Vector3.zero;
            robbie.transform.localRotation = Quaternion.identity;
            Vector3 cameraPosition = arCamera.transform.position;
            cameraPosition.y = robbie.transform.position.y;
            robbie.transform.LookAt(cameraPosition);
            robbie.SetActive(true);
            ui.SetActive(true);
            planeManager.SetTrackablesActive(false);
            planeManager.enabled = false;

        }

 public void ResetRobbie()
        {
            robbie.SetActive(false);
            ui.SetActive(false);
            planeManager.enabled = true;
            planeManager.SetTrackablesActive(true);
            Destroy(currentAnchor);
            currentAnchor = null;
            robbie.transform.parent = this.transform;
            isRobbieActive = false;
        }
```    
### RobbieARController.cs
```    
 Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        Vector2 touchPosition = touch.position;
                        Ray ray = cam.ScreenPointToRay(touch.position);
                        if (Physics.Raycast(ray, out RaycastHit hit))
                        {
                            if (hit.collider == controller)
                            {
                                StartCoroutine(TriggerWave());
                            }
                        }
                    }
```
