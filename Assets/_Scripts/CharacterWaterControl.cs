using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


[System.Serializable]
public class BehaviourState
{
    public string ID;
    public GameObject Obj;
}
public class CharacterWaterControl : MonoBehaviour
{
    [SerializeField] Animator _Anim;

    [SerializeField] WaterBallControll waterBallController;
    //[SerializeField] WaterBender waterBenderController;
    [SerializeField] WaterTubeController waterTubeController;
    [SerializeField] float _TurnSpeed;
    Vector3 waterBallTarget;
    Vector3 waterBendTarget;
    Vector3 waterTubeTarget;
    public GameObject explodeEffect;
    private int damage = 5;
    private bool hasWaterFired = false;
    public AudioSource PlayerWaterAttack1;

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Att1"))
        {
            if (hasWaterFired == false)
                StopAllCoroutines();
            StartCoroutine(Coroutine_WaterBall());
            //WaterBallHitCheck();
            //PlayerWaterAttack1.Play();
        }
        //if (CrossPlatformInputManager.GetButtonDown("Att2"))
        //{
        //    if (hasWaterFired == false)
         //       StopAllCoroutines();
         //   StartCoroutine(Coroutine_WaterBend());
            //WaterBallHitCheck();
            //PlayerWaterAttack1.Play();
        //}
        if (CrossPlatformInputManager.GetButtonDown("Att3"))
        {
            if (hasWaterFired == false)
                StopAllCoroutines();
            StartCoroutine(Coroutine_WaterTube());
            //WaterBallHitCheck();
            //PlayerWaterAttack1.Play();
        }


        IEnumerator Coroutine_WaterBall()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!waterBallController.WaterBallCreated())
                {
                        yield return StartCoroutine(Coroutine_Turn());
                    _Anim.SetTrigger("CreateWaterBall");
                    gameObject.GetComponent<AudioSource>().Play();

                    }
                else
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    yield return StartCoroutine(Coroutine_Turn());
                    if (Physics.Raycast(ray, out hit))
                    {
                        waterBallTarget = hit.point;
                        _Anim.SetTrigger("ThrowWaterBall");
                        gameObject.GetComponent<AudioSource>().Play();

                            if (hit.collider.CompareTag("Enemy22"))
                            {
                                Debug.Log("HIT : " + damage);
                                hit.collider.GetComponent<EnemyHealthSystem>().TakeDamage(damage);
                            }
                            else
                            {
                                Debug.Log("MISS");
                            }
                        }
                }
            }
            yield return null;
        }
    }

    }

    private void AnimationCallback_CreateWaterBall()
    {
        if (!waterBallController.WaterBallCreated())
        {
            waterBallController.CreateWaterBall();
        }
    }

    private void AnimationCallback_ThrowBall()
    {
        if (waterBallController.WaterBallCreated())
        {
            waterBallController.ThrowWaterBall(waterBallTarget);
        }
    }

    //IEnumerator Coroutine_WaterBend()
    //{
    //    while (true)
    //    {
     //       if (Input.GetMouseButtonDown(0))
       //     {
       //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       //         RaycastHit hit;
        //        yield return StartCoroutine(Coroutine_Turn());
        //        gameObject.GetComponent<AudioSource>().Play();
        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            waterBendTarget = hit.point;
        //            _Anim.SetTrigger("WaterBend");
        //            if (hit.collider.CompareTag("Enemy22"))
         //           {
         //               Debug.Log("HIT : " + damage);
        //                hit.collider.GetComponent<EnemyHealthSystem>().TakeDamage(damage);
        //            }
        //            else
        //            {
         //               Debug.Log("MISS");
         //           }
         //       }
         //   }
            //yield return null;
        //}
   // }

    //private void AnimationCallback_WaterBend()
    //{
     //   waterBenderController.Attack(waterBendTarget);
    //}

    IEnumerator Coroutine_WaterTube()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                yield return StartCoroutine(Coroutine_Turn());
                gameObject.GetComponent<AudioSource>().Play();
                if (Physics.Raycast(ray, out hit))
                {
                    waterTubeTarget = hit.point;
                    _Anim.SetTrigger("WaterTube");
                    if (hit.collider.CompareTag("Enemy22"))
                    {
                        Debug.Log("HIT : " + damage);
                        hit.collider.GetComponent<EnemyHealthSystem>().TakeDamage(damage);
                    }
                    else
                    {
                        Debug.Log("MISS");
                    }

                }
            }
            yield return null;
        }
    }

    private void AnimationCallback_WaterTube()
    {
        waterTubeController.InstantiateWaterTube(waterTubeTarget);
    }

    IEnumerator Coroutine_Turn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = (hit.point - transform.position);
            direction.y = 0;
            direction = direction.normalized;
            Vector3 startForward = transform.forward;
            float angle = Vector3.Angle(startForward, direction);
            _Anim.SetFloat("Turn", Vector3.Cross(startForward, direction).y);
            float lerp = 0;
            while (lerp < 1)
            {
                transform.forward = Vector3.Slerp(startForward, direction, lerp);
                lerp += Time.deltaTime * _TurnSpeed / angle;
                yield return null;
            }
            _Anim.SetFloat("Turn", 0);
            
            }
        }

        }
      