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
    //Animator anim;

    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    float targetAngle;
    float angle;
    float tempSpeed;
    bool isCrouching;
    
    string velocityX = "VelocityX";
    string velocityZ = "VelocityZ";
    string isCrouch = "IsCrouch";

    Rigidbody rb;
    Vector3 moveDir;

    void Start()
    {
        tempSpeed = speed;
        rb = GetComponent<Rigidbody>();
        //anim = GetComponentInChildren<Animator>();
    }
    private void FixedUpdate()
    {

        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = tempSpeed * sprintRate;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
           
        }
        else
        {
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

       

        
       /* anim.SetFloat(velocityX, rb.velocity.x);
        anim.SetFloat(velocityZ, rb.velocity.z);
        anim.SetBool(isCrouch, isCrouching);
        Debug.Log(rb.velocity.x + " " + rb.velocity.z);*/
    }
    


}
