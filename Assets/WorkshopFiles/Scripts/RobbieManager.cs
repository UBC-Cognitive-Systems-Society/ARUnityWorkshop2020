using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class RobbieManager : MonoBehaviour
{
    public static RobbieManager Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            if (_instance != null) Debug.Log("Setting duplicate managers");
            _instance = value;
        }
    }

    public static int speed = 10;

    private static RobbieManager _instance = null;

    [SerializeField] GameObject robbie;
    [SerializeField] GameObject arCamera;
    [SerializeField] GameObject ui;
    public bool isRobbieActive = false;

    [SerializeField] ARAnchorManager anchorManager;
    [SerializeField] ARPlaneManager planeManager;

    private ARAnchor currentAnchor;

    private void Awake()
    {
        Instance = this;
    }

    public void PositionRobbie(ARAnchor anchor)
    {
        // TODO
    }

    public void ResetRobbie()
    {
        // TODO
    }
}


