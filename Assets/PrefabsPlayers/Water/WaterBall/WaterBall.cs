using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    [SerializeField] ParticleSystem _WaterBallParticleSystem;
    [SerializeField] AnimationCurve _SpeedCurve;
    [SerializeField] float _Speed;
    [SerializeField] ParticleSystem _SplashPrefab;
    [SerializeField] ParticleSystem _SpillPrefab;

    public void Throw(Transform target, int power = 5)
    {
        StopAllCoroutines();
        StartCoroutine(Coroutine_Throw(target, power));
    }

    IEnumerator Coroutine_Throw(Transform target, int damage) 
    {
        float lerp = 0;
        Vector3 startPos = transform.position;
        while (lerp < 1)
        {
            transform.position = Vector3.Lerp(startPos, target.position, _SpeedCurve.Evaluate(lerp));
            float magnitude = (transform.position - target.position).magnitude;
            if (magnitude < 0.4f)
            {
                break;
            }
            lerp += Time.deltaTime * _Speed;
            yield return null;
        }
        _WaterBallParticleSystem.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        ParticleSystem splas = Instantiate(_SplashPrefab, target.position, Quaternion.identity);
        Vector3 forward = target.position - startPos;
        forward.y = 0;
        splas.transform.forward = forward;
        if (Vector3.Angle(startPos - target.position, Vector3.up) > 30)
        {
            ParticleSystem spill = Instantiate(_SpillPrefab, target.position, Quaternion.identity);
            spill.transform.forward = forward;
        }
        Destroy(gameObject, 0.5f);

        DamageIfTargetIsTheVoid(target, damage);
    }

    private void DamageIfTargetIsTheVoid(Transform target, int damage)
    {
        if (!target.parent.TryGetComponent(out VoidBehaviour v))
        {
            Debug.LogWarning("Target is not VoidBehaviour, it is: " + target.gameObject.name);
            return;
        }
        
        v.TakeDamage(damage);
    }
}
