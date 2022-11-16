using System;
using KinematicCharacterController.Examples;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Transform CurrentPlayer;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }
    [SerializeField] float m_CurrentHealth, m_MaxHealth;
    [SerializeField] GameObject m_HealthBar;
    internal void DealDamage(float Damage)
    {
        m_CurrentHealth = Mathf.Clamp(m_CurrentHealth - Damage, 0, m_MaxHealth);
        m_HealthBar.transform.localScale = new((float)m_CurrentHealth / (float)m_MaxHealth, 1, 1);
        if (m_CurrentHealth <= 0)
        {
            if (currentVehicle != null)
                currentVehicle.UnDrive();
            IsAlive = false;
            m_Animator.Play("death");
        }
    }

    internal void dealDamagerPerHour(float hours)
    {
        float dealDamageInSeconds = hours * 3600f;
        float factor = Time.deltaTime / dealDamageInSeconds;
        float damageFactor = Mathf.Lerp(0, m_MaxHealth, factor);
        DealDamage(damageFactor);
    }

    public static Player instance;
    public static bool IsAlive = true;
    public static Vehical currentVehicle;
    [SerializeField] internal ExampleCharacterController m_ExampleCharacterController;
    [SerializeField] ExampleCharacterCamera CharacterCamera;
    [SerializeField] GameObject m_model_character;
    [SerializeField] float sprintSpeed;
    [SerializeField] Animator m_Animator;
    [SerializeField] Transform m_player;
    [SerializeField] LayerMask cycleLandMask;
    new Camera camera;
    float normalSpeed, YPos;
    bool IsOnCycle = false;
    private void Awake()
    {
        normalSpeed = m_ExampleCharacterController.MaxStableMoveSpeed;
        lastCharacterPos = m_player.position;
        camera = Camera.main;
        CurrentPlayer = m_ExampleCharacterController.transform;
    }

    Vector3 lastCharacterPos;
    void Update()
    {
        bool shiftPressed = false;
        if (Input.GetKey(KeyCode.LeftShift) || IsOnCycle)
        {
            m_ExampleCharacterController.MaxStableMoveSpeed = sprintSpeed;
            shiftPressed = true;
        }
        else
        {
            m_ExampleCharacterController.MaxStableMoveSpeed = normalSpeed;
        }

        Vector3 newPos = m_player.position - lastCharacterPos;
        lastCharacterPos = m_player.position;

        if (newPos.magnitude <= 0.0001f)
        {
            m_Animator.SetFloat("X", 0);
            YPos = 0;
        }
        else
        {
            if (shiftPressed)
            {
                m_Animator.SetFloat("X", 0);
                YPos = 1;
            }
            else
            {
                m_Animator.SetFloat("X", 0);
                YPos = 0.5f;
            }
        }

        m_Animator.SetFloat("Y", Mathf.Lerp(m_Animator.GetFloat("Y"), YPos, Time.unscaledDeltaTime * 30));
    }

    public void SetJumpStart()
    {
        m_Animator.SetTrigger("Jump");
    }

    public void SetJumpEnd()
    {
        m_Animator.SetTrigger("Land");
    }

    public void Ride()
    {
        CharacterCamera.gameObject.SetActive(false);
        m_ExampleCharacterController.gameObject.SetActive(false);
    }

    public void UnRide()
    {
        CharacterCamera.gameObject.SetActive(true);
        m_ExampleCharacterController.gameObject.SetActive(true);
    }
}
