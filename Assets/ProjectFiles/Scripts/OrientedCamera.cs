using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientedCamera : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        this.transform.up = Vector3.up;
    }
}
