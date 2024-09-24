using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private float maxTurnAngle;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxZPosition;
    private float currentAngle;

    [Header("Components")]
    [SerializeField] private Transform cameraRoot;

    void Awake()
    {
        currentAngle = 0f;
    }

    void Update()
    {
        float input = -Input.GetAxis("Horizontal");
        float targetAngle = input * maxTurnAngle;

        if (Mathf.Abs(targetAngle - currentAngle) >= 0.1)
        {
            float side = currentAngle > targetAngle ? -1 : 1;
            currentAngle = Mathf.Clamp(currentAngle + side * Time.deltaTime * turnSpeed, -maxTurnAngle, maxTurnAngle);
        }
        else
        {
            currentAngle = targetAngle;
        }


        cameraRoot.rotation = Quaternion.Euler(0, 90, currentAngle);
        cameraRoot.position = new Vector3(
            cameraRoot.position.x,
            cameraRoot.position.y,
            Mathf.Clamp(cameraRoot.position.z + input * moveSpeed * Time.deltaTime, -maxZPosition, maxZPosition));
    }
}
