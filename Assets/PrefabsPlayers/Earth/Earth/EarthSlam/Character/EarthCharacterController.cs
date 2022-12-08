using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class EarthCharacterController : MonoBehaviour
{
    [SerializeField] CrackControll _CrackPrefab;
    [SerializeField] Animator _Anim;
    Vector3 direction;
    private int damage = 5;

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("F"))
            {
            direction = transform.forward;
            SlamEffect();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Void"))
            {
                Debug.Log("HIT : " + damage);
                hit.collider.GetComponent<VoidBehaviour>().TakeDamage(damage);
            }
            else
            {
                Debug.Log("MISS");
            }
        }
        }

        void SlamEffect()
    {
        Vector3 pos = transform.position;
        pos.y = 0;
        CrackControll crackControll = Instantiate(_CrackPrefab, pos, Quaternion.identity);
        crackControll.transform.forward = direction;
        crackControll.Open(15);
    }
    }
}
