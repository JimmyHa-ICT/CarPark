using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BrakeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        //MobileInputSender.Instance.Brake = 1;
        PlayerInput.Instance.BrakePressed = true;
        transform.localScale *= 0.9f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //MobileInputSender.Instance.Brake = 0;
        PlayerInput.Instance.BrakePressed = false;
        transform.localScale /= 0.9f;
    }
}
