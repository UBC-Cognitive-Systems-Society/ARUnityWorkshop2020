using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraObj : MonoBehaviour
{

    [SerializeField] Transform cameraObj;

    // Update is called once per frame
    void Update()
    {
        //Vector3 position = cameraObj.position;
        //position.y = 0f;
        transform.LookAt(cameraObj);
        //transform.up = Vector3.up;
        
        Vector3 rotation = transform.eulerAngles;
        rotation.x = 0f;
        transform.eulerAngles = rotation;
        
    }
}
