using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [SerializeField] float acceleration = 10;
    [SerializeField] float speed=20;
    [SerializeField] float jumpForce = 15;
    [SerializeField] Transform cam;
    [SerializeField] float sprintRate=1.5f;
    [SerializeField] float crouchRate=0.5f;
    Animator anim;

    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    float targetAngle;
    float angle;
    float tempSpeed;
    float horizontal;
    float vertical;
    bool isCrouching;
    bool isRuning;
    
    string velocityX = "VelocityX";
    string velocityZ = "VelocityZ";
    string isCrouch = "IsCrouch";

    Rigidbody rb;
    Vector3 moveDir;
    Vector2 moveAnimation;
    Vector3 relative;

    void Start()
    {
        tempSpeed = speed;
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if (direction.magnitude >= 0.1f)

        {
            

            targetAngle =  cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.AddRelativeForce(direction* acceleration, ForceMode.Force);
            
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);

           
        }
       

    }
    private void Update()
    {
        relative = transform.InverseTransformDirection(Vector3.forward);
        Debug.Log(relative);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = tempSpeed * sprintRate;
            isRuning = true;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
            isRuning = false;
        }
        else
        {
            isRuning = false;
            speed = tempSpeed;
        }

        if (isCrouching)
        {
            speed = tempSpeed * crouchRate;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        Animation();

    }

    private void Animation()
    {

        if (isRuning)
        {
            moveAnimation = new(horizontal * 2, vertical * 2);
        }
        else if (isCrouching)
        {
            moveAnimation = new(horizontal / 2, vertical / 2);
        }
        else
        {
            moveAnimation = new(horizontal, vertical);
        }

        //Debug.Log(moveAnimation);
        anim.SetFloat(velocityX, moveAnimation.x);
        anim.SetFloat(velocityZ, moveAnimation.y);
        anim.SetBool(isCrouch, isCrouching);
    }


}
