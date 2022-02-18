using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public struct MissileDataJson
{
    public List<string> list_name;
    public List<bool> list_isPurchased;
    public List<int> list_priceGold;
    public List<int> list_priceGem;
    public List<string> list_imagePath;
}
public struct MissileData
{
    public string name;
    public bool isPurchased;
    public int priceGold;
    public int priceGem;
    public Texture image;
    public GameObject prefab;
}