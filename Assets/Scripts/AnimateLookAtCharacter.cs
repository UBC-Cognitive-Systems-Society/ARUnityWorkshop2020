using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimateLookAtCharacter : MonoBehaviour
{

    Animator animator;
    public bool isIKActive = true;


    [SerializeField] private Transform lookAtObj;
    [SerializeField] private float turnSpeed;
    


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
