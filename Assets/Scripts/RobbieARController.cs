using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RobbieARController : RobbieCharacterController
{
    [SerializeField] protected bool allowRotation = true;

    [SerializeField] protected float turnThreshold = -0.3f;
    [SerializeField] protected float angleThreshold = 100f;
    [SerializeField] protected float turnDuration = 2f;
    //[SerializeField] protected Transform orientedCamera = default;
    [SerializeField] protected Transform targetRotation = default;

    [Header("Debug")]
    [SerializeField] protected TextMeshProUGUI debugText;
    [SerializeField] protected Image debugImage;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(debugImage != null)
            debugImage.color = allowMovement ? Color.green : Color.red;
        if (allowMovement)
        {
            
            Vector3 relativePos = transform.InverseTransformDirection(cam.transform.position);
            /*
            if (debugText != null) debugText.text = relativePos.z.ToString();
            if (relativePos.z < turnThreshold)
            {

                Debug.Log("Turning Around");
                //anim.SetBool("LeftTurn", relativePos.x < 0);

                //anim.SetTrigger("TurnAround");
                StartCoroutine(RotateTowards());

            }
            */
            if (allowRotation)
            {
                float angle = Quaternion.Angle(transform.rotation, targetRotation.rotation);
                if (debugText != null) debugText.text = angle.ToString();
                if (angle > angleThreshold)
                {
                    Debug.Log("Turning Around");
                    anim.SetBool("LeftTurn", relativePos.x < 0);

                    anim.SetTrigger("TurnAround");
                    StartCoroutine(RotateTowards());
                }
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                {
                    Ray ray = cam.ScreenPointToRay(touch.position);
                    if(Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if(hit.collider == controller)
                        {
                            StartCoroutine(TriggerWave());
                        }
                    }
                }
            }

        }



    }

    public void TestRotation()
    {
        StartCoroutine(RotateTowards());
    }

    public IEnumerator RotateTowards()
    {
        allowMovement = false;
        Quaternion origRot = transform.rotation;
        Quaternion targetRot = targetRotation.rotation;
        anim.applyRootMotion = false;
        float t = 0; 
        while (t < turnDuration) 
        {
            //yield return new WaitForEndOfFrame();

            t += Time.deltaTime / turnDuration;

            transform.rotation = Quaternion.Lerp(origRot, targetRot, t);

            yield return null;
        }
        anim.applyRootMotion = true;
        allowMovement = true;
        yield return null;
    }
}
