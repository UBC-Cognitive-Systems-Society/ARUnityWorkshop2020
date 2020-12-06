using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CompletedWorkshop
{
    public class RobbieARController : RobbieCharacterController
    {
        [SerializeField] protected bool allowRotation = true;

        [SerializeField] protected float turnThreshold = -0.3f;
        [SerializeField] protected float angleThreshold = 100f;
        [SerializeField] protected float turnDuration = 2f;
        //[SerializeField] protected Transform orientedCamera = default;
        [SerializeField] protected Transform targetRotation = default;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponentInChildren<Animator>();
            controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (allowMovement)
            {

                Vector3 relativePos = transform.InverseTransformDirection(cam.transform.position);
                if (allowRotation)
                {
                    float angle = Quaternion.Angle(transform.rotation, targetRotation.rotation);
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
                }
            }
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
}
