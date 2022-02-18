using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static public DataManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private Dictionary<string, MissileData> dic_MissileData = new Dictionary<string, MissileData>();
    public void LoadAllMissileData()
    {
        var missileDataJson = Resources.Load("MissileData/MissileDataJson");
        TextAsset textData = missileDataJson as TextAsset;
        MissileDataJson jsonData = JsonUtility.FromJson<MissileDataJson>(textData.ToString());
        
        for (int i = 0; i < jsonData.list_name.Count; i++)
        {
            MissileData data = new MissileData
            {
                name = jsonData.list_name[i],
                isPurchased = jsonData.list_isPurchased[i],
                priceGold = jsonData.list_priceGold[i],
                priceGem = jsonData.list_priceGem[i],
                image = Resources.Load<Texture>(jsonData.list_imagePath[i])
            };
            dic_MissileData.Add(data.name, data);
        }
    }
    public void LoadPlayerData()
    {
        var playerData = Resources.Load("PlayerData/");
        TextAsset textData = playerData as TextAsset;
        PlayerDataJson jsonData = JsonUtility.FromJson<PlayerDataJson>(textData.ToString());
        PlayerData data = new PlayerData
        {
            id = jsonData.id,
            nickName = jsonData.nickName,
            level = jsonData.level,
            gold = jsonData.gold,
            gem = jsonData.gem,
            country = jsonData.country,
            scoreRank = jsonData.scoreRank,
            inventory_MissileList = GetGameObjectsInResources(jsonData.inventory_MissileList),
            deck_MissileList1 = GetGameObjectsInResources(jsonData.deck_MissileList1),
            deck_MissileList2 = GetGameObjectsInResources(jsonData.deck_MissileList2),
            deck_MissileList3 = GetGameObjectsInResources(jsonData.deck_MissileList3),
            deck_MissileList4 = GetGameObjectsInResources(jsonData.deck_MissileList4),
            deck_MissileList5 = GetGameObjectsInResources(jsonData.deck_MissileList5),
        };
    }
    private List<GameObject> GetGameObjectsInResources(List<string> paths)
    {
        List<GameObject> list = new List<GameObject>();
        foreach (string path in paths)
        {
            GameObject go = Resources.Load<GameObject>(path);
            list.Add(go);
        }
        return list;
    }

    // for developer
    public void SaveMissileJsonData(List<MissileData> missileDatas)
    {
        MissileDataJson tmpMissileDataJson = new MissileDataJson();
        foreach (MissileData missileData in missileDatas)
        {
            tmpMissileDataJson.list_name.Add(missileData.name);
            tmpMissileDataJson.list_isPurchased.Add(missileData.isPurchased);
            tmpMissileDataJson.list_priceGold.Add(missileData.priceGold);
            tmpMissileDataJson.list_priceGem.Add(missileData.priceGem);
            tmpMissileDataJson.list_imagePath.Add($"/MissileData/MissileImages/{missileData.image.name}");
        }
        string path = Application.persistentDataPath + "/MissileData/MissileDataJson";
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }
        System.IO.File.WriteAllText(path + "MissileData.json", JsonUtility.ToJson(tmpMissileDataJson));
    }
}
