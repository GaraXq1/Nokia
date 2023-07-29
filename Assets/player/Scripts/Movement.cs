using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{

    [SerializeField] float acceleration = 10;
    [SerializeField] float speed=20;
    [SerializeField] float jumpForce = 15;
    [SerializeField] float sprintRate=1.5f;
    [SerializeField] float crouchRate=0.5f;
    [SerializeField] Transform cam;
    [SerializeField] float rotationSpeed = 5f;
 
    Animator anim;
    float tempSpeed;
    bool isCrouching;
    float horizontal;
    float vertical;
    float xVel;
    float zVel;
    string velocityX = "VelocityX";
    string velocityZ = "VelocityZ";
    string isCrouch = "IsCrouch";
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Rigidbody rb;
    Vector3 moveDir;
    


    
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
      
            rb.AddRelativeForce(direction * acceleration, ForceMode.Force);
        
       
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);
        xVel = transform.InverseTransformDirection(rb.velocity).x;
        zVel = transform.InverseTransformDirection(rb.velocity).z;
        Debug.Log(xVel+" "+zVel);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = tempSpeed * sprintRate;
            if (horizontal != 0)
            {
                speed = tempSpeed * sprintRate * 0.5f;
            }
            if (vertical < 0)
            {
                speed = tempSpeed * sprintRate * 0.5f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
        }
        else
        {
            if (horizontal != 0)
            {
                speed = tempSpeed * 0.5f;
            }
            if (vertical < 0)
            {
                speed = tempSpeed * 0.5f;
            }
            speed = Mathf.Lerp(speed, tempSpeed, Time.deltaTime*3);
        }

        if (isCrouching)
        {
            speed = tempSpeed * crouchRate;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        


        
        anim.SetFloat(velocityX, xVel);
        anim.SetFloat(velocityZ, zVel);
        anim.SetBool(isCrouch, isCrouching);
        
    }
    


}
