using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private float maxTurnAngle;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxZPosition;
    private float currentAngle;
    private bool firingFront;
    public bool FiringFront { get => firingFront; }


    [Header("Components")]
    [SerializeField] private Transform cameraRoot;
    private float input;


    void Awake()
    {
        firingFront = true;
        currentAngle = 0f;
    }

    void OnMove(InputValue inputValue)
    {
        Vector2 inputVec = inputValue.Get<Vector2>();
        input = -inputVec.x;

        if ((firingFront && inputVec.y <= -0.9f) ||
            (!firingFront && inputVec.y >= 0.9f))
        {
            firingFront = !firingFront;
            GameGUI.instance.SetFrontPointerActive(firingFront);
            print("Toggle Firing");
        }
    }



    void Update()
    {
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
