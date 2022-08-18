using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "ghost")
        {
            Destroy(collision.transform.gameObject); //destroy ghost
            // Instantiate(explode, collision.transform.position, collision.transform.rotation);
            GameManager.Instance.IncreaseScore(10);

            Destroy(this.gameObject, 3);
        }
    }

}
