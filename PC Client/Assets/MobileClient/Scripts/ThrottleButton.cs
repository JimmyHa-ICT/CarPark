using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrottleButton : MonoBehaviour
{
    public void OnUpdate(bool isPressed)
    {
        if (isPressed)
            transform.localScale = Vector3.one * .9f * 4f;
        else
            transform.localScale = Vector3.one * 4f;
    }
}
