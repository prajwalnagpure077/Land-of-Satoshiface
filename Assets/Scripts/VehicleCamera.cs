using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCamera : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;
    [SerializeField] Transform followTarget;
    [SerializeField] Transform alternativeTarget;


    void Start()
    {
        // Lock and Hide the Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        // Collect Mouse Input

        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the Camera around its local X axis

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;


        // Rotate the Player Object and the Camera around its Y axis

        player.Rotate(Vector3.up * inputX);

        player.position = followTarget.position;

        if (Input.GetKeyDown(KeyCode.C))
        {
            switchView();
        }
    }

    void switchView()
    {
        Transform tempTarget = followTarget;
        followTarget = alternativeTarget;
        alternativeTarget = tempTarget;
    }
}