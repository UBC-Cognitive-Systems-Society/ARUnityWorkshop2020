# Workshop Steps
Note that this is a very, very rough script and not very detailed.
Could use a lot of improvement

1. Showcase starter project
   - Go over Unity window components
   - Explain game object
   - Explain components
   - Explain C#
     - What a class is,
     - What variables are
   - Show monobehaviour
     - Start and Update
     - Explain how to access transform
     - Time.deltaTime
1. Remove non AR components
   - Camera
   - MovementController
   - MouseFollower
   - Plane
1. Set Up AR
   - GameObject > XR
   - AR Session, AR session Origin
   - AR Plane Manager, AR Anchor Manager, AR Raycast Manager
   - Drag in a plane prefab to the plane manager
   - Fix missing Camera References
1. Add placeholder components
   - Place On Plane
   - Drag in cube prefab
   - Add missing code from place on plane
1. Robbie Manager
   - Add robbie back into scene
   - Add Robbie Manager
   - Add UI
   - Change place on plane to call robbie manager toattach: Instead of Instantiate, replace with `RobbieManager.Instance.PositionRobbie(toAttach)`
   - Add missing code from RobbieManager
1. Add Back in Robbie character controller
   - Make sure look at camera object is set
   - Add missing code from ARCharacterController

## Missing Code
### PlaceOnPlane.cs
``` csharp
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
``` csharp
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
``` csharp   
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
