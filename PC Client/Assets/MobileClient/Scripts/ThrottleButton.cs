using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrottleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        MobileInputSender.Instance.Throttle = 1;
        transform.localScale *= 0.9f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MobileInputSender.Instance.Throttle = 0;
        transform.localScale /= 0.9f;
    }
}
