using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GearButton : MonoBehaviour, IPointerDownHandler
{
    public Sprite[] sprite;
    private Image spriteRenderer;
    private int m_gear = 1;

    private void Start()
    {
        spriteRenderer = GetComponent<Image>();
        m_gear = 1;
        spriteRenderer.sprite = sprite[0];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //MobileInputSender.Instance.Gear *= -1;
        m_gear *= -1;
        PlayerInput.Instance.Gear = m_gear;

        if (m_gear == 1)
        {
            spriteRenderer.sprite = sprite[0];
        }
        else
        {
            spriteRenderer.sprite = sprite[1];
        }
    }
}
