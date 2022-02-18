using System;
using System.Collections.Generic;
using UnityEngine;
public struct PlayerDataJson
{
    public string id;
    public string nickName;
    public int level;
    public int gold;
    public int gem;
    public string country;
    public int scoreRank;    
    public List<string> inventory_MissileList;
    public List<string> deck_MissileList1;
    public List<string> deck_MissileList2;
    public List<string> deck_MissileList3;
    public List<string> deck_MissileList4;
    public List<string> deck_MissileList5;
}
public struct PlayerData
{
    public string id;
    public string nickName;
    public int level;
    public int gold;
    public int gem;
    public string country;
    public int scoreRank;
    public List<GameObject> inventory_MissileList;
    public List<GameObject> deck_MissileList1;
    public List<GameObject> deck_MissileList2;
    public List<GameObject> deck_MissileList3;
    public List<GameObject> deck_MissileList4;
    public List<GameObject> deck_MissileList5;
}
