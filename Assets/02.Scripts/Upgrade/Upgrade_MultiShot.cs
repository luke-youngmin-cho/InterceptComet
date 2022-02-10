using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_MultiShot : UpgradeElement
{
    [SerializeField] GameObject missileLauncherPrefab;
    private List<Transform> missileLaunchers;
    public override void OnUpgrade()
    {
        base.OnUpgrade();
        missileLaunchers = Earth.instance.missileLaunchers;
        Transform buildPoint = Earth.instance.transform.GetChild(0).GetChild(0);
        Instantiate(missileLauncherPrefab, buildPoint);
        StartCoroutine(RearrangeMissileLaunchers());
        level++;
    }
    public override void OnDowngrade()
    {
        base.OnDowngrade();
        missileLaunchers = Earth.instance.missileLaunchers;
        Transform lastMissileLauncherTransform = missileLaunchers[missileLaunchers.Count - 1];
        Destroy(lastMissileLauncherTransform.gameObject);
        StartCoroutine(RearrangeMissileLaunchers());
        level--;
    }
    IEnumerator RearrangeMissileLaunchers()
    {
        yield return new WaitUntil(() => CheckAllLauncherReady());
        int count = missileLaunchers.Count;
        for (int i = 0; i < count; i++)
        {
            float zPos = (1f / (count - 1f)) * i - 0.5f;
            Debug.Log(zPos);
            missileLaunchers[i].localPosition = new Vector3(-1f, 0f, zPos);
        }
        Player.instance.RefreshMissile();
    }
    private bool CheckAllLauncherReady()
    {     
        bool isReady = true;
        foreach (Transform item in missileLaunchers)
        {
            if (item.GetComponent<MissileLauncher>().isAvailable == false)
            {
                isReady = false;
                break;
            }
        }
        return isReady;     
    }
}
