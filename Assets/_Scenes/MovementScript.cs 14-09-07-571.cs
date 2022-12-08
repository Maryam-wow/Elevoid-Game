using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementScript : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    public RectTransform gamePad;
    public float moveSpeed = 0.2f;
    private GameObject playerGameObject;
    private PlayerPackage player;


    Vector3 move;

    bool walk;

    private Coroutine _moveCo;

    public void OnDrag(PointerEventData eventData)
    {
        playerGameObject = GameManager.Instance.GetPlayerGameObject();

        transform.position = eventData.position;
        transform.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)gamePad.position, gamePad.rect.width * 0.5f);

        move = new Vector3(transform.localPosition.x, 0f, transform.localPosition.y).normalized; // no movement in y

        if (!walk)
        {
            walk = true;
            playerGameObject.GetComponent<Animator>().SetBool("walk", true); // on drag start the walk animation
        }
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
        
        walk = false;
        playerGameObject.GetComponent<Animator>().SetBool("walk", false);
    }

    IEnumerator PlayerMovement()
    {
        while (true)
        {
            GameObject playerGameObject = GameManager.Instance.GetPlayerGameObject();

            if (move != Vector3.zero)
            {
                playerGameObject.transform.rotation = Quaternion.Slerp
                        (playerGameObject.transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * 5.0f);
            }

            yield return new WaitForEndOfFrame();

        }
    }

}
