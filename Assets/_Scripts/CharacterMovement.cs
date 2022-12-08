using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMovement : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    public RectTransform gamePad;
    public float moveSpeed = 0.2f;
    private GameObject _playerGameObject;


    Vector3 move;

    bool walk;

    private Coroutine _moveCo;

    private void Start()
    {
        _playerGameObject = GameObject.FindWithTag("Player");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)gamePad.position, gamePad.rect.width * 0.5f);

        move = new Vector3(transform.localPosition.x, 0f, transform.localPosition.y).normalized; // no movement in y
    }
    public void OnPointerDown(PointerEventData eventData)
    {
            // do the movement when touched down
            _moveCo = StartCoroutine(PlayerMovement());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero; // joystick returns to mean pos when not touched
        move = Vector3.zero;

        if (_moveCo != null) StopCoroutine(_moveCo);
    }

    IEnumerator PlayerMovement()
        {
        while (true)
        {
            

            if (move != Vector3.zero)
            {
                _playerGameObject.transform.rotation = Quaternion.Slerp
                        (_playerGameObject.transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * 5.0f);

                _playerGameObject.transform.position +=
                    _playerGameObject.transform.forward * Time.deltaTime * moveSpeed;
            }

            yield return new WaitForEndOfFrame();

        }
    }

}


 