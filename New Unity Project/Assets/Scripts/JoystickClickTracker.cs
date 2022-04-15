using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickClickTracker : MonoBehaviour, IPointerDownHandler,IDragHandler,IPointerUpHandler
{
    public string name = "";
    public float rangeLimit = 1.1f; // max range for joystic
    public float sensitivityLimit = 0.1f; //minimal distance to get trigger

    RectTransform rectTransform;
    Vector3 startPosition;
    Vector2 onClickPostion;

    Vector2 inputAxis = Vector2.zero;
    bool isHolding = false;
    bool isClicked = false;
    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition3D;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        onClickPostion = eventData.pressPosition; //position on screen point on click down the Joystick
    }
    //Wait for next update then release the click event
    IEnumerator StopClickEvent() // not for joystick
    {
        yield return waitForEndOfFrame;
        isClicked = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
            Vector3 movementVector = Vector3.ClampMagnitude((eventData.position - onClickPostion) , (rectTransform.sizeDelta.x * rangeLimit) + (rectTransform.sizeDelta.x * sensitivityLimit));
            Vector3 movePos = startPosition + movementVector;
            rectTransform.anchoredPosition = movePos;

            //Update inputAxis
            float inputX = 0;
            float inputY = 0;
            if (Mathf.Abs(movementVector.x) > rectTransform.sizeDelta.x * sensitivityLimit)
            {
                inputX = (movementVector.x - (rectTransform.sizeDelta.x * sensitivityLimit * (movementVector.x > 0 ? 1 : -1))) / (rectTransform.sizeDelta.x * rangeLimit);
            }
            if (Mathf.Abs(movementVector.y) > rectTransform.sizeDelta.x * sensitivityLimit)
            {
                inputY = (movementVector.y - (rectTransform.sizeDelta.x * sensitivityLimit * (movementVector.y > 0 ? 1 : -1))) / (rectTransform.sizeDelta.x * rangeLimit);
            }
            inputAxis = new Vector2(inputX, inputY);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
           isHolding = false;
            //Reset Joystick position
            rectTransform.anchoredPosition = startPosition;
            inputAxis = Vector2.zero;
    }


    public Vector2 GetInputAxis()
    {
        return inputAxis;
    }

    public bool GetClickedStatus()
    {
        return isClicked;
    }

    public bool GetHoldStatus()
    {
        return isHolding;
    }


}
