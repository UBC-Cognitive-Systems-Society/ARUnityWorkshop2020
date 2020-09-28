using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobbieCharacterController : MonoBehaviour
{

    Animator anim;
    [SerializeField] Transform cam;
    [SerializeField] private float waveTime;
    [SerializeField] private float maxSpeed = 1f;
    [SerializeField] private float speedIncrement = 0.01f;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] private float turnThreshold;
    float turnSmoothVelocity;
    float speed = 0f;
    int speedId = Animator.StringToHash("Speed");

    Robbie robbie;

    public bool allowMovement = true;

    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        robbie = GetComponentInChildren<Robbie>();
        controller = GetComponent<CharacterController>();
    }

    private IEnumerator OnMouseDown()
    {
        anim.SetBool("Wave", true);
        yield return new WaitForSeconds(waveTime);
        anim.SetBool("Wave", false);
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
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
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
