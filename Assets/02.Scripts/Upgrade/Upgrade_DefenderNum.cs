using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_DefenderNum : UpgradeElement
{
    [SerializeField] GameObject defenderPrefab;
    [SerializeField] float offset = 5f;
    public override void OnUpgrade()
    {
        base.OnUpgrade();
        
        List<GameObject> defenders = UpgradeManager.instance.deffenders;
        
        GameObject defenderGO = Instantiate(defenderPrefab);
        defenders.Add(defenderGO);
        level++;
    }
    public override void OnDowngrade()
    {
        base.OnDowngrade();
        List<GameObject> defenders = UpgradeManager.instance.deffenders;
        

    }
    
}