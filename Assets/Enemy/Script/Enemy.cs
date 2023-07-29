using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float detectionDistance = 10f;
    [SerializeField] private float detectionAngle = 60f;
    [SerializeField] private float detectionFrequency = 0.5f;
    [SerializeField] CharacterController PlayerController;
    private float detectionTimer;

    private void Update()
    {
        detectionTimer -= Time.deltaTime;
        if (detectionTimer <= 0f)
        {
            detectionTimer = detectionFrequency;

            // Check if player is within detection distance and angle
            Vector3 directionToPlayer = PlayerController.transform.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer.normalized);
            if (distanceToPlayer < detectionDistance && angleToPlayer < detectionAngle)
            {
                transform.rotation = Quaternion.Euler(0f, angleToPlayer, 0f);
                // Player detected, implement behavior here
                Debug.Log("Player detected!");
            }
        }
    }
}
