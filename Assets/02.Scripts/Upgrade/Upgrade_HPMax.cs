using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_HPMax : UpgradeElement
{
    public override void OnUpgrade()
    {
        base.OnUpgrade();
        level++;
        float amount = level * 500;
        Earth.instance.IncreaseHPMax(amount);
    }
    public override void OnDowngrade()
    {
        base.OnDowngrade();
        float amount = level * 500;
        Earth.instance.DecreaseHPMax(amount);
    }
}