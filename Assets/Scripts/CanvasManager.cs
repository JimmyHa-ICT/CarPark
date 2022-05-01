using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager SharedInstance;

    private void Awake()
    {
        if (SharedInstance == null)
            SharedInstance = this;
        else if (SharedInstance != this)
            Destroy(gameObject);
    }

    public TexturePool TexturePool;

    [SerializeField] private Image ImgGear;

    public void SetGearImage(int fwMode)
    {
        if (fwMode == 1)
            ImgGear.sprite = TexturePool.SpriteGear[0];
        else
            ImgGear.sprite = TexturePool.SpriteGear[1];
    }
}
