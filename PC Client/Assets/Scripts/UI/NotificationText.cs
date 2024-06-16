using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class NotificationText : MonoBehaviour
{
    [SerializeField] private TMP_Text txtNoti;
    [SerializeField] private RectTransform rectTransform;

    public static NotificationText Instance;

    private Sequence s;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnEnable()
    {
        if (s != null)
            s.Kill();
    }

    public void Play(string text)
    {
        gameObject.SetActive(true);
        txtNoti.text = text;
        rectTransform.anchoredPosition = new Vector2(0, 50);
        s = DOTween.Sequence()
                            .Append(rectTransform.DOAnchorPos(new Vector2(0, -50), 0.5f))
                            .AppendInterval(2f)
                            .Append(rectTransform.DOAnchorPos(new Vector2(0, 50), 0.5f))
                            .AppendCallback(() => gameObject.SetActive(false));
    }
}
