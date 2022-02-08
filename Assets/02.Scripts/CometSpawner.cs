using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometSpawner : MonoBehaviour
{
    #region singleton
    static public CometSpawner instance;
    private void Awake()
    {
        if (instance != null) 
            Destroy(instance.gameObject);
        instance = this;
    }
    #endregion
    public Transform target;
    public List<st_CometSpawnInfo> list_spawnInfo;
    [HideInInspector]public List<GameObject> list_Comet = new List<GameObject>();
    public void Spawn()
    {
        foreach (st_CometSpawnInfo info in list_spawnInfo)
        {
            for (int i = 0; i < info.spawnNum; i++)
            {
                GameObject tmpComet = Instantiate(info.cometPrefab, transform);
                tmpComet.transform.position = CreateRandomPositionInCircleRange(info.radiusMin, info.radiusMax);
                tmpComet.GetComponent<OvalOrbitFor2Point>().SetTarget(target);
                tmpComet.GetComponent<OvalOrbitFor2Point_Sklent>().SetTarget(target);
                list_Comet.Add(tmpComet);
            }
        }
    }
    private Vector3 CreateRandomPositionInCircleRange(float radiusMin, float radiusMax)
    {
        float x = Random.Range(radiusMin, radiusMax);
        float y = 0;
        float z = Random.Range(radiusMin, radiusMax);

        bool tmpXSign = Random.Range(0, 2) > 0;
        bool tmpZSign = Random.Range(0, 2) > 0;
        if (tmpXSign)
            x = -x;
        if (tmpZSign)
            z = -z;
        return new Vector3(x, y, z);
    }
}
[System.Serializable]
public struct st_CometSpawnInfo
{
    public float radiusMin;
    public float radiusMax;
    public GameObject cometPrefab;
    public int spawnNum;
}
