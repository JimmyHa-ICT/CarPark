using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GearButton : MonoBehaviour, IPointerDownHandler
{
    public Sprite[] sprite;
    private Image spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MobileInputSender.Instance.Gear *= -1;

        if (MobileInputSender.Instance.Gear == 1)
        {
            spriteRenderer.sprite = sprite[0];
        }
        else
        {
            spriteRenderer.sprite = sprite[1];
        }
    }
}
