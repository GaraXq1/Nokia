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

    CharacterController controller;
    Animator anim;
    Vector3 moveDir;
    
    bool isCrouch = false;
    float animSpeed = 1;
    float constSpeed;
    float vertical;
    float horizontal;

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
            moveDir = new Vector3(horizontal, 0f, vertical) * speed;

            if (Input.GetButtonDown("Jump"))
            {
                anim.SetTrigger(Jump);
                moveDir.y = jumpSpeed;
            }
        }

        moveDir.y += gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);


        Sprint();
        Crouch();
        if(!isCrouch && !Input.GetKey(KeyCode.LeftShift))
        {
            speed = constSpeed;
            animSpeed = 1; 
        }
        Animations();

    }
    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
            animSpeed = 2;
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
            speed = crouchSpeed;
            animSpeed = 0;
        }
    }
    void Animations()
    {
        anim.SetFloat(Speed, animSpeed);
        anim.SetFloat(h, horizontal);
        anim.SetFloat(v, vertical);
    }
    void CoolDown()
    {

    }
}