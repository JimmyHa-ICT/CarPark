using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Car Skin", order = 2)]
public class CarSkinList : ScriptableObject
{
    public Sprite[] Skins;
}
