using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CharacterMovement : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    public RectTransform gamePad;
    public float moveSpeed = 0.2f;
    //private GameObject gameObject;
    //private GameObject PlayerRef;
    public GameObject PlayerRef;


    Vector3 move;

    bool walk;
    void Start()
    {

    }
    void Awake()
    {
        // PlayerRef = GameObject.Find("Void)");
        //GameObject[] PlayerRef;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)gamePad.position, gamePad.rect.width * 0.5f);

        move = new Vector3(transform.localPosition.x, 0f, transform.localPosition.y).normalized; // no movement in y
        if (!walk)
        {
            walk = true;
            PlayerRef.GetComponent<Animator>().SetBool("walk", true); // on drag start the walk animation
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
            // do the movement when touched down
            StartCoroutine(PlayerMovement());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
            transform.localPosition = Vector3.zero; // joystick returns to mean pos when not touched
            move = Vector3.zero;
            StopCoroutine(PlayerMovement());
            walk = false;
        PlayerRef.GetComponent<Animator>().SetBool("walk", false);
    }

    IEnumerator PlayerMovement()
        {
            while(true)
            {
            PlayerRef.transform.transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
            Vector3 currentPosition = gameObject.transform.position;

       //GameManager.Instance.UpdatePlayerPosition(position);
            
            if (move != Vector3.zero)

                PlayerRef.transform.rotation = Quaternion.Slerp
                        (PlayerRef.transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * 5.0f);

           
                yield return null;

        }
    }

}


 