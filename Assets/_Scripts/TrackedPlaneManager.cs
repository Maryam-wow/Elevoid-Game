using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedPlaneManager : MonoBehaviour

{
    [SerializeField] private Vector2 dimensionsForBigPlane;

    public GameObject enemyPrefab;
    public GameObject playerPrefab;

    private ARPlane aRPlane;
    private GameObject PlaneMarkerPrefab;
    private List <ARPlane> aLLarPlanes;
    private ARPlaneManager arPlaneManager;
    private ARRaycastManager rayManager;
    private Vector2 TouchPosition;
    private bool objectSpawned = false;
    private Dictionary<string, GameObject> trackedPlanes = new Dictionary<string, GameObject>();
    void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();

        PlaneMarkerPrefab.SetActive(false);
    }
    
    void ShowMarker()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            PlaneMarkerPrefab.transform.position = hits[0].pose.position;
            PlaneMarkerPrefab.SetActive(true);
        
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {

               
            Instantiate(enemyPrefab, hits[0].pose.position, enemyPrefab.transform.rotation);
            Instantiate(playerPrefab, hits[0].pose.position, playerPrefab.transform.rotation);
            }
        }
    }
    private void Enable()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        arPlaneManager = FindObjectOfType<ARPlaneManager>();
        arPlaneManager.planesChanged += OnPlanesChanged;


    }
    private void Disable()
    {
        arPlaneManager.planesChanged -= OnPlanesChanged;

    }
    private void OnPlanesChanged(ARPlanesChangedEventArgs eventArgs)
    {
        
        if (eventArgs.added != null && eventArgs.added.Count > 0)
            aLLarPlanes.AddRange(eventArgs.added);
        {
            if(aRPlane.extents.x * aRPlane.extents.y >= dimensionsForBigPlane.x * dimensionsForBigPlane.y)
            {
                
                
                //GameObject.Instantiate dimensionsForBigPlane);
            }
        }
        void Update()
        {
            if (!objectSpawned)
            {
                ShowMarker();
            }
        }

        foreach (ARPlane plane in eventArgs.updated)
        {
            OnPlaneUpdated(aRPlane);
        }
        foreach (ARPlane plane in eventArgs.removed)
        {
            OnPlaneRemoved(aRPlane);
        }
    }
    private void OnPlaneAdded(ARPlane Plane)
    {
        print($"Plane with id ){Plane.trackableId} added");
        // TODO: Place Enemy on Plane

        playerPrefab = FindObjectOfType<GameObject>();
        enemyPrefab = FindObjectOfType<GameObject>();

        //EnemyPrefab.transform.position, EnemyPrefab.transform.rotation);
    }
    private void OnPlaneRemoved(ARPlane Plane)
    {
        print($"Plane with id ){Plane.trackableId} removed");
        // TODO: Remove Enemy on Plane
        GameObject.Destroy(playerPrefab);
        GameObject.Destroy(enemyPrefab);
    }
    private void OnPlaneUpdated(ARPlane Plane)
    {
        print($"Plane with id ){Plane.trackableId} updated");

    }
}