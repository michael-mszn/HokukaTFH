using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    private float displayTime;


    void Awake()
    {
        camera = Camera.main;
        displayTime = 0f;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (displayTime <= 0f)
        {
            DisableHealthbar();
        }
        else
        {
            //Ensures health bar is always 2D and always on top of the mob
            transform.rotation = camera.transform.rotation;
            transform.position = target.position + offset;
            displayTime -= Time.deltaTime;
        }
    }
    
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        slider.value = currentHealth / maxHealth;
    }

    public void DisableHealthbar()
    {
        gameObject.SetActive(false);
    }

    public void LightUpHealthbar(float seconds)
    {
        gameObject.SetActive(true);
        displayTime = seconds;
    }

    public void setOffset(Vector3 _offset)
    {
        offset = _offset;
    }

    public float GetDisplayTime()
    {
        return displayTime;
    }
}
