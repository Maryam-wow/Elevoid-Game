using System;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceHealthBar : MonoBehaviour
{
    public Slider slider;

    public bool followMainCameraOnly = true;
    
    // caching main camera
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        FollowCameraTransform(followMainCameraOnly ? _mainCamera.transform : Camera.current.transform);

        // ** same as follows:
        // if (followMainCameraOnly)
        // {
        //     FollowCameraTransform(_mainCamera.transform);
        // }
        // else
        // {
        //     FollowCameraTransform(Camera.current.transform);
        // }
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    private void FollowCameraTransform(Transform transformToFollow)
    {
        transform.LookAt(transformToFollow);
    }
}

