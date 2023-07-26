using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float maxSpeed=20;
    [SerializeField] float jumpForce = 15;
    [SerializeField] Transform cam;
    [SerializeField] Animator anim;

    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    float targetAngle;
    float angle;
    float tempMaxSpeed;
    bool isCrouching;

    string velocityX = "VelocityX";
    string velocityZ = "VelocityZ";
    string isCrouch = "IsCrouch";

    Rigidbody rb;
    Vector3 moveDir;

    void Start()
    {
        tempMaxSpeed = maxSpeed;
       rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {

        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        if (direction.magnitude >= 0.1f)

        {
            

            targetAngle =  cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.AddRelativeForce(direction* speed, ForceMode.Force);
            
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

           
        }
       

    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = tempMaxSpeed * 1.5f;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
           
        }
        else
        {
            maxSpeed = tempMaxSpeed;
        }

        if (isCrouching)
        {
            maxSpeed = tempMaxSpeed * 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        Vector2 noramalizedSpeed = new Vector2(rb.velocity.x, rb.velocity.z).normalized;

        Debug.Log(noramalizedSpeed.x + "   " + noramalizedSpeed.y);
        anim.SetFloat(velocityX, noramalizedSpeed.x);
        anim.SetFloat(velocityZ, noramalizedSpeed.y);
        anim.SetBool(isCrouch, isCrouching);

    }
    


}
