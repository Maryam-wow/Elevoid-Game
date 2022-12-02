using System.Collections;
using UnityEngine;

public class VoidBehaviour : MonoBehaviour
{
    [SerializeField] private WaterBendingControll waterBenderObjectReference;

    [SerializeField] private float initialDelay = 5f;
    [SerializeField] private float playerHitDelay = 2f;

    private Transform _tower;
    private WaterBendingControll _waterBendingControll;

    private Coroutine _attackCoroutine;

    private bool _gotHitByPlayer;
    
    private void Start()
    {
        _tower = GameObject.FindWithTag("Tower").transform;

        LookAtTower();
        _attackCoroutine = StartCoroutine(AttackCoroutine(initialDelay));
    }

    private void OnDestroy()
    {
        StopAttacking();
    }

    public void ReceiveHit()
    {
        _gotHitByPlayer = true;
    }

    public void StopAttacking()
    {
        StopCoroutine(_attackCoroutine);
    }

    IEnumerator AttackCoroutine(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            if (_gotHitByPlayer)
            {
                yield return new WaitForSeconds(playerHitDelay);
                _gotHitByPlayer = false;
            }

            StartAttack();
        }
    }

    void StartAttack()
    {
        _waterBendingControll = Instantiate(waterBenderObjectReference, transform.position, Quaternion.identity);
        
        _waterBendingControll.WaterBend(_tower.position);
    }

    void LookAtTower()
    {
        transform.LookAt(_tower);
    }
}
