using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class EarthCharacterController : MonoBehaviour
{
    [SerializeField] Animator _Anim;
    [SerializeField] CrackControll _CrackPrefab;
    Vector3 direction;
    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("F"))
        {
            direction = transform.forward;
            _Anim.SetTrigger("Attack");
        }
    }

    private void AnimationCallback_SlamEffect()
    {
        Vector3 pos = transform.position;
        pos.y = 0;
        CrackControll crackControll = Instantiate(_CrackPrefab, pos, Quaternion.identity);
        crackControll.transform.forward = direction;
        crackControll.Open(15);
    }
}
