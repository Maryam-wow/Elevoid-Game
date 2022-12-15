using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARPlacementIndicator : MonoBehaviour
{
    [SerializeField] private GameObject placementIndicatorObject;

    private ARRaycastManager _arRaycastManager;
    private List<ARRaycastHit> _arRaycastHits = new List<ARRaycastHit>();

    private void Start()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        
        placementIndicatorObject.SetActive(false);
    }

    private void Update()
    {
        TryRaycast();
    }

    private void OnDisable()
    {
        placementIndicatorObject.SetActive(false);
    }

    public Transform GetPlacementIndicatorTransform()
    {
        return placementIndicatorObject.activeInHierarchy ? placementIndicatorObject.transform : null;
    }

    private void TryRaycast()
    {
        Vector2 screenCenter = new Vector2(Screen.width/2, Screen.height/2);

        bool didRaycastHit = _arRaycastManager.Raycast(screenCenter, _arRaycastHits, TrackableType.Planes);
        if (didRaycastHit)
        {
            ARRaycastHit hit = _arRaycastHits[0];
            placementIndicatorObject.SetActive(true);
            placementIndicatorObject.transform.position = hit.pose.position;
            placementIndicatorObject.transform.rotation = hit.pose.rotation;
        }
    }
}
