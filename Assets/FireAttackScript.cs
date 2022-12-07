using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FireAttackScript : MonoBehaviour
{
    //Vector3 _voidTarget;
    private int damage = 5;
    public ParticleSystem particleSystem;
    //List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();
    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        //if (CrossPlatformInputManager.GetButtonDown("Fire"))
        //{
            //LookAtVoid();
          //  particleSystem.Play();
            //OnParticleCollision();
        //}
        if (Input.GetMouseButtonDown(0))
        {
            particleSystem.Play();
            OnParticleCollision();
        }
    }
    private void OnParticleCollision()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
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
//void LookAtVoid()
// {
//  transform.LookAt(_voidTarget);
// }
// }
