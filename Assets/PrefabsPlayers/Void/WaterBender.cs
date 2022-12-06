using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class WaterBender : MonoBehaviour
{
    [SerializeField] bool _update;
    [SerializeField] WaterBendingControll _WaterPrefab;

    // Update is called once per frame
    void Update()
    {
        if (!_update)
        {
            return;
        }
        Ray rayFrom = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(rayFrom, out hit))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //if(Physics.Raycast(ray,out hit))
            {
                Attack(hit.transform);
            }
        }
    }

    public void Attack(Transform target)
    {
        WaterBendingControll water = Instantiate(_WaterPrefab, transform.position, Quaternion.identity);
        water.WaterBend(target);
    }
}
