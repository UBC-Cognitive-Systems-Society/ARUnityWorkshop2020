using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Robbie : MonoBehaviour
{

    Animator animator;
    public bool isIKActive = true;
    public bool isTurning = false;
    [SerializeField] private Transform lookAtObj;
    [SerializeField] private float waveTime;
    [SerializeField] private float turnThreshold;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private IEnumerator OnMouseDown()
    {
        animator.SetBool("Wave", true);
        yield return new WaitForSeconds(waveTime);
        animator.SetBool("Wave", false);
    }

    private void Update()
    {
        if (!isTurning)
        {
            Vector3 relativePos = transform.InverseTransformDirection(lookAtObj.position);
            if (relativePos.z < turnThreshold)
            {
                animator.SetTrigger("TurnAround");
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            if (isIKActive)
            {
                
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(lookAtObj.position);
            }
            else
            {
                animator.SetLookAtWeight(0);
            }
        }
    }
}
