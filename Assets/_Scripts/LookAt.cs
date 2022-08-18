using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour

{

    private GameObject enemyObject;

    private float distance;
    private float minimumDistance = 2f;

        void Start()
        {
            enemyObject = GameObject.FindWithTag("Enemy");
        }

        void Update()
        {
            distance = Vector3.Distance(enemyObject.transform.position, transform.position);

            if (distance <= minimumDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, enemyObject.transform.position, 3f * Time.deltaTime);
            }


    }
}