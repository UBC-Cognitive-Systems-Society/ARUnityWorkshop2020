using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobbieMovementController : RobbieCharacterController
{
    [SerializeField] protected float maxSpeed = 1f;
    [SerializeField] protected float speedIncrement = 0.01f;
    [SerializeField] protected float turnSmoothTime = 0.1f;
    protected float turnSmoothVelocity;
    protected float speed = 0f;
    protected int speedId = Animator.StringToHash("velocityXZ");

    private void OnMouseDown()
    {
        StartCoroutine(TriggerWave());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetTrigger("Suprised");
        }

        if (Input.GetKey(KeyCode.E))
        {
            anim.SetTrigger("Dance");
        }

        if (allowMovement)
        {

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                if (speed < maxSpeed) speed += speedIncrement;
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                anim.SetFloat(speedId, speed);
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else
            {
                if (speed > 0) speed -= speedIncrement;
                anim.SetFloat(speedId, speed);
                /*
                if (!isTurning)
                {
                    Vector3 relativePos = transform.InverseTransformDirection(cam.position);
                    if (relativePos.z < turnThreshold)
                    {
                        anim.SetBool("LeftTurn", relativePos.x < 0);

                        anim.SetTrigger("TurnAround");

                    }
                }*/
            }
        }
    }
}
