using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightMove : MonoBehaviour
{
        //public Transform[] SpawnPoints;
        private GameObject[] ghost;
        //public static int enemyCount;
        //public static int enemyLimit;
        float fMinX = 0.1f;
        float fMaxX = 6f;
        int Direction = -1;
        float fEnemyX;




        // Start is called before the first frame update
        //void Start()
        //{
            //StartCoroutine(StartSpawning());
        //}

       // IEnumerator StartSpawning()
        //{
          //  yield return new WaitForSeconds(20);
          //  for (int i = 0; i < 3; i++)
         //   {
                //Instantiate(ghost[i], SpawnPoints[i].position, Quaternion.identity);
          //  }

       // }

        // Update is called once per frame
        void Update()
        {
            
            switch (Direction)
            {
                case -1:
                    // Moving Left
                    if (fEnemyX > fMinX)
                    {
                        fEnemyX -= 1.0f;
                    }
                    else
                    {

                        // Hit left boundary, change direction
                        Direction = 1;
                    }
                    break;

                case 1:
                    // Moving Right
                    if (fEnemyX < fMaxX)
                    {
                        fEnemyX += 1.0f;
                    }
                    else
                    {
                        // Hit right boundary, change direction
                        Direction = -1;
                    }
                    break;
            }

            gameObject.transform.localPosition = new Vector3(fEnemyX, 0.0f, 0.0f);


        }
    }