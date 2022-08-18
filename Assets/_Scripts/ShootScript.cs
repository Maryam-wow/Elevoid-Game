using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ShootScript : MonoBehaviour
{
    public ARRaycastManager Ray;
    public GameObject arCamera;
    public Rigidbody projectFile;
    public Transform FireposStart; // empty game object to get to the start pos x,y,z
    public Button shoot;
    private bool _isShooting;


    public float shootForce = 700.0f;
    void shootTask()
    {
        _isShooting = true;
    }
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject ()) return;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && _isShooting)
        {
            Vector3 myTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            FireposStart.transform.LookAt(myTouch);
            Rigidbody clone = Instantiate(projectFile, FireposStart.transform.position, FireposStart.transform.rotation) as Rigidbody;
            clone.AddForce(clone.transform.forward * 2000);


        }
        _isShooting = false;
    }
}