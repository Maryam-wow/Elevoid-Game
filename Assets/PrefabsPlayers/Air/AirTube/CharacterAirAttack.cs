using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterAirAttack : MonoBehaviour
{
    [SerializeField] WaterTubeController waterTubeController;
    Transform waterTubeTarget;
    private int damage = 5;
    
    [SerializeField] private float attackDelay = 1.5f;

    [Header("Self-Component References")]
    [SerializeField] private GameObject puddleSplashObject;
    [SerializeField] private CharacterAirControl characterAirControl;
    
    private bool _isAttackEnabled;

    private Coroutine _attackTriggerCo;

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Att3"))
        {
            _isAttackEnabled = true;
            puddleSplashObject.SetActive(true);
        }
        
        if(_isAttackEnabled) StartAttack();
    }

    private void StartAttack()
    {
        if (!_isAttackEnabled) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            _isAttackEnabled = false;
            characterAirControl.StartTurning();
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            gameObject.GetComponent<AudioSource>().Play();
            if (Physics.Raycast(ray, out hit))
            {
                waterTubeTarget = hit.transform;
                StartWaterTube();
            
                if (_attackTriggerCo != null) StopCoroutine(_attackTriggerCo);
                _attackTriggerCo = StartCoroutine(AttackApplicationCoroutine());
            }
        }
    }

    IEnumerator AttackApplicationCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        puddleSplashObject.SetActive(false);
        
        yield return new WaitForSeconds(attackDelay);
        ApplyDamage(waterTubeTarget);
        _isAttackEnabled = true;
    }

    private void ApplyDamage(Transform target)
    {
        if (!target.transform.parent.TryGetComponent(out VoidBehaviour v))
        {
            Debug.LogWarning("Target is not VoidBehaviour, it is: " + target.gameObject.name);
            return;
        }
        
        v.TakeDamage(damage);
    }

    private void StartWaterTube()
    {
        waterTubeController.InstantiateWaterTube(waterTubeTarget.position);
    }
}
