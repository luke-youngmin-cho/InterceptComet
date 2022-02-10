using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_DefenderPerformance : UpgradeElement
{
    float speed;
    float radius;
    float damageReduce;
    public override void OnUpgrade()
    {
        base.OnUpgrade();
        level++;
        speed = level + Defender.speedOffset;
        radius = level + Defender.radiusOffset;
        damageReduce = Mathf.Pow(0.95f, level);
        Defender.speed = speed;
        Defender.radius = radius;
        Defender.damageReduce = damageReduce;
    }
    public override void OnDowngrade()
    {
        base.OnDowngrade();
    }

}