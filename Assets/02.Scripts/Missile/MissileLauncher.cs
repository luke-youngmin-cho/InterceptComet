using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    public bool isAvailable = false;
    private GameObject _missile;

    public GameObject missile
    {
        set
        {
            _missile = value;
            
        }
        get
        {
            return _missile;
        }
    }
    public Transform missilePreviewPoint;
    [SerializeField] Transform missileCreatePoint;
    private GameObject previewMissileGO;
    private void Start()
    {
        Player.instance.missileLaunchers.Add(this);
        Earth.instance.missileLaunchers.Add(this.gameObject.transform);
        isAvailable = true;
    }
    private void OnDestroy()
    {
        Player.instance.missileLaunchers.Remove(this);
        Earth.instance.missileLaunchers.Remove(this.gameObject.transform);
    }
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
            tmpMissile.dir = (transform.position - transform.parent.position).normalized;
            tmpMissile.isLaunched = true;
        }
    }
    public void SetPreviewMissile(GameObject previewMissile)
    {
        if(previewMissileGO != null)
        {
            Destroy(previewMissileGO);
        }
        previewMissileGO = Instantiate(previewMissile, missilePreviewPoint);
        previewMissileGO.GetComponent<Collider>().enabled = false;
        previewMissileGO.GetComponent<Missile>().enabled = false;
    }
}
