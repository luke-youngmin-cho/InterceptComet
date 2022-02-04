using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Y axis is upside
public class OvalOrbit : MonoBehaviour
{
    [SerializeField] private st_OvalElements ovalElements;
    [SerializeField] float _degree;
    private float degree { 
        set 
        {
            float tmpValue = value;
            if( Mathf.Abs(tmpValue) >= 360)
            {
                tmpValue = 0;
            }
            _degree = tmpValue;
        }
        get 
        {
            degree = _degree;
            return _degree; 
        }
    }
    private float pi
    {
        get { return _degree * Mathf.Deg2Rad; }
    }
    [SerializeField] float _gradientDegree;
    private float gradientDegree {
        set {
            float tmpValue = value;
            if( Mathf.Abs(tmpValue) < 360)
            {
                tmpValue = 0;
            }
            _gradientDegree = tmpValue;
        }
        get { return _gradientDegree; }
    }
    private float gradientPi
    {
        get { return gradientDegree * Mathf.Deg2Rad; }
    }

    private Vector3 xUnit;
    private Vector3 yUnit;
    private Vector3 zUnit;
    private float xLength;
    private float yLength;
    private float zLength;
    private void Awake()
    {
        xUnit = (ovalElements.xPlusTransform.position - ovalElements.xMinusTransform.position).normalized;
        yUnit = (ovalElements.yPlusTransform.position - ovalElements.yMinusTransform.position).normalized;
        zUnit = (ovalElements.zPlusTransform.position - ovalElements.zMinusTransform.position).normalized;
        xLength = (ovalElements.xPlusTransform.position - ovalElements.xMinusTransform.position).magnitude;
        yLength = (ovalElements.yPlusTransform.position - ovalElements.yMinusTransform.position).magnitude;
        zLength = (ovalElements.zPlusTransform.position - ovalElements.zMinusTransform.position).magnitude;
    }
    public Vector3 CalcPos(float t)// 0 <= t <= 2pi
    {        
        float xRel = Mathf.Sin(pi) * Mathf.Cos(t) * xLength / 2;
        float yRel = Mathf.Cos(pi) * yLength / 2; 
        float zRel = Mathf.Sin(pi) * Mathf.Sin(t) * zLength / 2;
        Vector3 absPos = (xRel * xUnit) + (yRel * yUnit) + (zRel * zUnit) + ovalElements.centorPos;
        return absPos;
    }
}

[System.Serializable]
public struct st_OvalElements
{
    public Vector3 centorPos;
    public Transform xPlusTransform;
    public Transform xMinusTransform;
    public Transform yPlusTransform;
    public Transform yMinusTransform;
    public Transform zPlusTransform;
    public Transform zMinusTransform;
}