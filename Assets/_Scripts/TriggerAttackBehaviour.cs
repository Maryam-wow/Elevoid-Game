using UnityEngine;

public class TriggerAttackBehaviour : MonoBehaviour
{
    public int damage = 5;
    
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
}
