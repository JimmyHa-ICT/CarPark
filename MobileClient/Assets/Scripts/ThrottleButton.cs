using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrottleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        //MobileInputSender.Instance.Throttle = 1;
        PlayerInput.Instance.ThrottlePressed = true;
        transform.localScale *= 0.9f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //MobileInputSender.Instance.Throttle = 0;
        PlayerInput.Instance.ThrottlePressed = false;
        transform.localScale /= 0.9f;
    }
}
