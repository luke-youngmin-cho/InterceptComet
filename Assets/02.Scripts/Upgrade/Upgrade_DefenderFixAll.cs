using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_DefenderFixAll : UpgradeElement
{
    public override void OnUpgrade()
    {
        base.OnUpgrade();
        foreach (GameObject gameObject in UpgradeManager.instance.deffenders)
        {
            Defender defender = gameObject.GetComponent<Defender>();
            defender.durability = defender.durabilityMax;
        } 
    }
    public override void OnDowngrade()
    {
        base.OnDowngrade();
    }

}