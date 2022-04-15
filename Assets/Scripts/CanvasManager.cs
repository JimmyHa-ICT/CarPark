using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public TexturePool TexturePool;

    [SerializeField] private Image ImgGear;

    public void SetGearImage(int fwMode)
    {
        ImgGear.sprite = TexturePool.SpriteGear[fwMode];
    }
}
