using UnityEngine;

[RequireComponent(typeof(ARPlacementIndicator))]
public class ARObjectPlacer : MonoBehaviour
{
    [SerializeField] private GameObject objectToSet;
    
    private ARPlacementIndicator _indicator;
    private Transform _placementIndicatorTransform;

    private void Start()
    {
        _indicator = GetComponent<ARPlacementIndicator>();
        
        objectToSet.SetActive(false);
    }

    private void Update()
    {
        _placementIndicatorTransform = _indicator.GetPlacementIndicatorTransform();
        if (Input.anyKeyDown && _placementIndicatorTransform != null)
        {
            objectToSet.SetActive(true);
            objectToSet.transform.position = _placementIndicatorTransform.position;
            objectToSet.transform.rotation = _placementIndicatorTransform.rotation;

            _indicator.enabled = false;
            this.enabled = false;
        }
    }
}