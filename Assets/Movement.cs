using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float speed= 10;
    [SerializeField]
    float jumpForce=15;
    Rigidbody rb;

    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = new (Input.GetAxisRaw("Horizontal"),0f,Input.GetAxisRaw("Vertical"));
        rb.velocity = new (dir.x*speed,rb.velocity.y,dir.z*speed);
        if (Input.GetButton("Jump"))
        {

            rb.velocity=new (rb.velocity.x, jumpForce, rb.velocity.z);
        }

    }
}
