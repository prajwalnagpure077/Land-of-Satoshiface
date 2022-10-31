using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] Transform directionalLight;
    [SerializeField] float hours = 5;

    float seconds = 0;
    private void Awake()
    {
        seconds = hours * 3600;
    }
    private void Update()
    {
        directionalLight.Rotate(new Vector3(360 * (Time.deltaTime / seconds), 0, 0), Space.Self);
    }
}
