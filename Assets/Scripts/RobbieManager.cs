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

    private static RobbieManager _instance = null;

    private TextureMapper texMapper;

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
        texMapper = GetComponentInChildren<TextureMapper>();
        if(TextureManager.Instance.CurrentTextureToApply != null)
        {
            texMapper.SetTexture(TextureManager.Instance.CurrentTextureToApply);
        }
    }

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


}
