using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehical : MonoBehaviour
{
    Transform m_PlayerTransform;
    [SerializeField] Transform m_Hook, m_PlayerExitHook;
    [SerializeField] float m_Distance, speed = 1, drag = 0.05f, cameraSpeed = 8;
    [SerializeField] KeyCode m_KeyCode;
    [SerializeField] bool RotateOnlyInMotion = false;
    [SerializeField] float DistanceNeededForRotation = 1;
    [SerializeField] VehicleCamera m_VehicleCamera;
    [SerializeField] bool Jump = false, UseGround = true;
    [SerializeField] float jumpForce = 1;
    [SerializeField] Vector3 onGroundStart, onGroundEnd;
    [SerializeField] GameObject ObjectToEnableWhileRiding;

    float RotationYOffset;
    Rigidbody m_rigidbody;
    GameObject m_Indicator;
    Player m_Player;
    bool IsRiding = false;
    new Camera camera;
    Vector3 lastPos;

    private void Start()
    {
        GameObject l_fpsHook = new GameObject();
        camera = Camera.main;
        m_Player = Player.Instance;
        m_PlayerTransform = m_Player.m_ExampleCharacterController.transform;
        m_Indicator = IndicatorManager.AddIndicator(m_Hook, m_KeyCode.ToString());
        m_VehicleCamera.gameObject.SetActive(false);
        RotationYOffset = m_VehicleCamera.transform.rotation.y;
        if (ObjectToEnableWhileRiding != null)
            ObjectToEnableWhileRiding.SetActive(false);
    }
    void Update()
    {

        if (Input.GetKeyDown(m_KeyCode))
        {
            if (IsRiding)
            {
                UnDrive();
            }
            else if (Vector3.Distance(transform.position, m_PlayerTransform.transform.position) <= m_Distance)
            {
                Drive();
            }
        }

        if (Vector3.Distance(transform.position, m_PlayerTransform.transform.position) <= m_Distance && IsRiding == false)
        {
            m_Indicator.SetActive(true);
        }
        else
        {
            m_Indicator.SetActive(false);
        }

        if (IsRiding && m_rigidbody != null)
        {
            if (Input.GetKey(KeyCode.W))
            {
                m_rigidbody.AddForce(transform.forward * speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                m_rigidbody.AddForce(-transform.forward * speed);
            }
            if (Jump && Input.GetKeyDown(KeyCode.Space) && (UseGround == false || isOnGround()))
            {
                m_rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }
            // if (Input.GetKey(KeyCode.A))
            // {
            //     m_rigidbody.AddForce(-transform.right * speed);
            // }
            // if (Input.GetKey(KeyCode.D))
            // {
            //     m_rigidbody.AddForce(transform.right * speed);
            // }
            var rotation = m_VehicleCamera.player.transform.rotation;
            rotation.x = transform.rotation.x;
            rotation.z = transform.rotation.z;
            rotation.y -= RotationYOffset;
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * cameraSpeed * ((RotateOnlyInMotion) ? Vector3.Distance(transform.position, lastPos) * DistanceNeededForRotation : 1));
        }
        lastPos = transform.position;
    }

    private void Drive()
    {
        m_VehicleCamera.gameObject.SetActive(true);
        m_Player.Ride();
        IsRiding = true;
        m_rigidbody = gameObject.AddComponent<Rigidbody>();
        setupRigidbody();
        if (ObjectToEnableWhileRiding != null)
            ObjectToEnableWhileRiding.SetActive(true);
    }
    private void UnDrive()
    {
        if (Physics.Linecast(m_PlayerExitHook.position, m_PlayerExitHook.position - new Vector3(0, 10, 0), out RaycastHit hit))
        {
            m_Player.m_ExampleCharacterController.transform.position = hit.point + new Vector3(0, 0.1f, 0);
        }
        else
        {
            m_Player.m_ExampleCharacterController.transform.position = m_PlayerExitHook.position;
        }

        m_Player.UnRide();
        m_VehicleCamera.gameObject.SetActive(false);
        IsRiding = false;
        if (m_rigidbody != null)
            Destroy(m_rigidbody);
        if (ObjectToEnableWhileRiding != null)
            ObjectToEnableWhileRiding.SetActive(false);
    }

    void setupRigidbody()
    {
        m_rigidbody.freezeRotation = true;
        m_rigidbody.drag = drag;
    }

    bool isOnGround()
    {
        return Physics.Linecast(transform.TransformPoint(onGroundStart), transform.TransformPoint(onGroundEnd));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.TransformPoint(onGroundStart), transform.TransformPoint(onGroundEnd));
    }
}
