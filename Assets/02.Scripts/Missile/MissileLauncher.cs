using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    private GameObject _missile;

    public GameObject missile
    {
        set
        {
            if (_missile != null)
                Destroy(_missile);
            _missile = value;
            
        }
        get
        {
            return _missile;
        }
    }
    public Transform missilePreviewPoint;
    [SerializeField] Transform missileCreatePoint;
    [SerializeField] Transform earth;
    
    public bool IsMissileEquiped()
    {
        return _missile != null;
    }
    public float GetEnergyRequired()
    {
        return _missile.GetComponent<Missile>().energyRequired;
    }
    public void LaunchMissile()
    {
        if (_missile != null)
        {
            GameObject tmpMissileGO = Instantiate(_missile,missileCreatePoint);
            missileCreatePoint.DetachChildren();
            Missile tmpMissile = tmpMissileGO.GetComponent<Missile>();
            tmpMissile.dir = (transform.position - earth.position).normalized;
            tmpMissile.isLaunched = true;
        }
    }
}
