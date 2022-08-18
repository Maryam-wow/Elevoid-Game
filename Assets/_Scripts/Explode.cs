using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    //public GameObject _enemy;
    //public GameObject _explode;

    private void OnCollisionEnter(Collision collision)
    {
       // if (collision.transform.tag == "Void")
       if (collision.gameObject.TryGetComponent<PlayerHealths>(out PlayerHealths PlayerHealthsComponent))
        {
            PlayerHealthsComponent.PlayerTakeDamage(1);
            //Destroy(collision.transform.gameObject); //destroy enemy
            //Instantiate(_explode, collision.transform.position, collision.transform.rotation);
           SumScore.Add(10);
        }
        Destroy(gameObject); //Destroy Bullet in all cases
    }
}


