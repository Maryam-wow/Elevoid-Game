using System.Collections;
using UnityEngine;

public class CharacterAirControl : MonoBehaviour
{
    [SerializeField] float _TurnSpeed;
    private Coroutine _turnCo;

    public void StartTurning()
    {
        if (_turnCo != null) StopCoroutine(_turnCo);
        _turnCo = StartCoroutine(Coroutine_Turn());
    }

    IEnumerator Coroutine_Turn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = (hit.point - transform.position);
            direction.y = 0;
            direction = direction.normalized;
            Vector3 startForward = transform.forward;
            float angle = Vector3.Angle(startForward, direction);
            float lerp = 0;
            while (lerp < 1)
            {
                transform.forward = Vector3.Slerp(startForward, direction, lerp);
                lerp += Time.deltaTime * _TurnSpeed / angle;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}