using UnityEngine;

public class TriggerFire : MonoBehaviour
{
    public int damage = 5;
    public ParticleSystem particleSystem;

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
        
            v.TakeDamage(damage);
        }
    }
    private void OnParticleCollision()
    {
        particleSystem.Play();
    }
}
