using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RochShield : MonoBehaviour
{
    void Update()
    {
        Vector3 upValue = new Vector3(0f, 5 * Time.deltaTime, 0f);
        if(transform.position.y < 0.9)
        {
            transform.Translate(upValue);
        }

    }
}
