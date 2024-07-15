using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GearButton : MonoBehaviour
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

    public void OnUpdate(int gear)
    {
        m_gear = gear;
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
