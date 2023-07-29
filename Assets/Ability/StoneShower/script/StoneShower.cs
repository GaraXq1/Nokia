using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneShower : MonoBehaviour
{
    Rigidbody rb;
    Vector3 launchDistination;
    float launchSpeed= 3f;
    // Start is called before the first frame update
    void Start()
    {
        launchDistination= new Vector3(Random.Range(-0.2f,0.2f), Random.Range(-0.2f,-0.5f), Random.Range(1f,1.5f));
        rb = GetComponent<Rigidbody>();


        rb.AddForce(launchDistination*launchSpeed,ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
