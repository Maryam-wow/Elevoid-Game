using System.Collections;
using UnityEngine;

public class TriggerFire : MonoBehaviour
{
    public int damage = 5;
    public ParticleSystem particleSystem;
    public float attackDuration=0.6f;
    private Coroutine attackCoroutine;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Void"))
        {
            if (!other.transform.parent.TryGetComponent(out VoidBehaviour v))
            {
                Debug.LogWarning("Target is not VoidBehaviour, it is: " + other.gameObject.name);
                return;
            }

            attackCoroutine= StartCoroutine(AttackCoroutine(v));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Void"))
        {
            if (attackCoroutine!=null)
            {
                StopCoroutine(attackCoroutine);

            }
        }
    }
    private void OnParticleCollision()
    {
        particleSystem.Play();
    }
    IEnumerator AttackCoroutine(VoidBehaviour v)
    {
        while (true)
            {
            v.TakeDamage(damage);
            yield return new WaitForSeconds(attackDuration);


        }
    }    


}
