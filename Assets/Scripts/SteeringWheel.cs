using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SteeringWheel : MonoBehaviour, IDragHandler
{
    float offset;
    Camera mainCam;

    [HideInInspector] public float Angle
    {
        get
        {
            return transform.eulerAngles.z;
        }
    }
    
    private void Start()
    {
        mainCam = Camera.main;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(eventData.position);
        Vector3 dir = new Vector3(mousePos.x - transform.position.x,
                                    mousePos.y - transform.position.y,
                                    0);
        transform.up = dir;
    }
}
