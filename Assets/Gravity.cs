using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    [SerializeField]
    float gravityScale=1;
    
    float gravityStrength = 9.8f; // Strength of the gravity force

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ApplyGravity();
    }

    void ApplyGravity()
    {
        Vector3 gravity = Vector3.down * gravityStrength*gravityScale;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
