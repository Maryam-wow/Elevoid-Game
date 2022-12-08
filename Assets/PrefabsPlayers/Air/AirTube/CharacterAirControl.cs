using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]
public class BehaviourAirState
{
    public string ID;
    public GameObject Obj;
}

public class CharacterAirControl : MonoBehaviour
{
    [SerializeField] Animator _Anim;
    [SerializeField] float _TurnSpeed;
    [SerializeField] WaterTubeController waterTubeController;
    Vector3 waterTubeTarget;
    private int damage = 5;

    private bool _isAttackEnabled;

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Att3"))
        {
            _isAttackEnabled = true;
        }
        
        if(_isAttackEnabled) StartAttack();
    }

    private void StartAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Coroutine_Turn());
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            gameObject.GetComponent<AudioSource>().Play();
            if (Physics.Raycast(ray, out hit))
            {
                waterTubeTarget = hit.point;
                AnimationCallback_WaterTube();
                _Anim.SetTrigger("WaterTube");
                if (hit.collider.CompareTag("Void"))
                {
                    Debug.Log("HIT : " + damage);
                    hit.collider.GetComponentInParent<VoidBehaviour>().TakeDamage(damage);
                }
                else
                {
                    Debug.Log("MISS");
                }
            }
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
                yield return new WaitForEndOfFrame();
            }

            _Anim.SetFloat("Turn", 0);
        }
    }
}