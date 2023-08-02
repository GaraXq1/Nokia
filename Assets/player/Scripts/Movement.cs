using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 3;
    [SerializeField] float sprintSpeed = 5;
    [SerializeField] float crouchSpeed = 1.5f;
    [SerializeField] float gravity = -20f;
    [SerializeField] float jumpSpeed = 15;
    [SerializeField] Transform cam;
    CharacterController controller;
    Animator anim;
    Vector3 direction;
    Vector3 moveDir;

    bool isCrouch = false;
    float animSpeed = 1;
    float constSpeed;
    float vertical;
    float horizontal;
    float coolDownSpeed = 5;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    const string Speed = "Speed";
    const string Jump = "Jumping";
    const string h = "H";
    const string v = "V";

    private void Start()
    {
        constSpeed = speed;
    }
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (controller.isGrounded)
        {
            direction = new Vector3(horizontal, 0f, vertical).normalized * speed;
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetTrigger(Jump);
                direction.y = jumpSpeed;
            }
        }
        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
        }
        direction.y += gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
        Sprint();
        Crouch();
        if(!isCrouch && !Input.GetKey(KeyCode.LeftShift))
        {
            HandleMovement(constSpeed, 2f, 2.5f);

            animSpeed = Mathf.Lerp(animSpeed, 1, Time.deltaTime * coolDownSpeed);
        }
        Animations();
    }
    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!isCrouch)
            {
                HandleMovement(crouchSpeed, 3f, 2.5f);
                animSpeed = Mathf.Lerp(animSpeed, 2, Time.deltaTime * coolDownSpeed);
            }
        }
    }
    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouch = !isCrouch;
        }
        if (isCrouch)
        {
            HandleMovement(sprintSpeed, 1.5f, 1.5f);
            animSpeed = Mathf.Lerp(animSpeed, 0, Time.deltaTime * coolDownSpeed);
        }
    }
    void Animations()
    {
        anim.SetFloat(Speed, animSpeed);
        anim.SetFloat(h, horizontal);
        anim.SetFloat(v, vertical);
    }
    void HandleMovement(float currentSpeed, float backwardValue, float sidesValue)
    {
        if (direction.z < 0f)
        {
            speed = currentSpeed / backwardValue;
        }
        else if (direction.x != 0f)
        {
            speed = currentSpeed / sidesValue;
        }
        else
        {
            speed = currentSpeed;
        }
    }
}