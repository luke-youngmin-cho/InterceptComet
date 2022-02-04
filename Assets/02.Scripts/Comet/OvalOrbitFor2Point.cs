using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvalOrbitFor2Point : MonoBehaviour
{
    [SerializeField] private st_OvalElementsFor2Points ovalElements;
    [SerializeField] private float _degree;
    private float degree
    {
        set
        {
            float tmpValue = value;
            if (Mathf.Abs(tmpValue) >= 360)
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
    public float pi
    {
        get { return _degree * Mathf.Deg2Rad; }
    }
    [SerializeField] float _gradientDegree;
    private float gradientDegree
    {
        set
        {
            float tmpValue = value;
            if (Mathf.Abs(tmpValue) < 360)
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

    [HideInInspector] public Vector3 centorPos;
    [HideInInspector] public Vector3 xUnit;
    [HideInInspector] public Vector3 yUnit;
    [HideInInspector] public Vector3 zUnit;
    [HideInInspector] public float xLength;
    [HideInInspector] public float yLength;
    [HideInInspector] public float zLength;
    Vector3 xPlusPos;
    Vector3 xMinusPos;
    Vector3 yPlusPos;
    Vector3 yMinusPos;
    Vector3 zPlusPos;
    Vector3 zMinusPos;
    private void Awake()
    {
        xLength = ovalElements.xLength;
        yLength = ovalElements.yLength;
        zLength = (ovalElements.zPlusTransform.position - ovalElements.zMinusTransform.position).magnitude;
        centorPos = new Vector3((ovalElements.zPlusTransform.position.x + ovalElements.zMinusTransform.position.x) / 2,
                                (ovalElements.zPlusTransform.position.y + ovalElements.zMinusTransform.position.y) / 2,
                                (ovalElements.zPlusTransform.position.z + ovalElements.zMinusTransform.position.z) / 2);

        float z1 = ovalElements.zPlusTransform.position.z;
        float z2 = ovalElements.zMinusTransform.position.z;
        float x1 = ovalElements.zPlusTransform.position.x;
        float x2 = ovalElements.zMinusTransform.position.x;
        float y1 = ovalElements.zPlusTransform.position.y;
        float y2 = ovalElements.zMinusTransform.position.y;

        float xPlusPos_x = centorPos.x - (xLength * (z2 - z1)) / (2 * Mathf.Sqrt((z2 - z1) * (z2 - z1) + (x2 - x1) * (x2 - x1)));
        float xMinusPos_x = centorPos.x + (xLength * (z2 - z1)) / (2 * Mathf.Sqrt((z2 - z1) * (z2 - z1) + (x2 - x1) * (x2 - x1)));
        float xPlusPos_z = centorPos.z + (xLength * (x2 - x1)) / (2 * Mathf.Sqrt((z2 - z1) * (z2 - z1) + (x2 - x1) * (x2 - x1)));
        float xMinusPos_z = centorPos.z - (xLength * (x2 - x1)) / (2 * Mathf.Sqrt((z2 - z1) * (z2 - z1) + (x2 - x1) * (x2 - x1)));
        xPlusPos = new Vector3(xPlusPos_x, centorPos.y, xPlusPos_z);
        xMinusPos = new Vector3(xMinusPos_x, centorPos.y, xMinusPos_z);

        float yPlusPos_x = centorPos.x - (yLength * (y2 - y1)) / (2 * Mathf.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1)));
        float yMinusPos_x = centorPos.x + (yLength * (y2 - y1)) / (2 * Mathf.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1)));
        float yPlusPos_y = centorPos.y + (yLength * (x2 - x1)) / (2 * Mathf.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1)));
        float yMinusPos_y = centorPos.y - (yLength * (x2 - x1)) / (2 * Mathf.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1)));
        yPlusPos = new Vector3(yPlusPos_x, yPlusPos_y, centorPos.z);
        yMinusPos = new Vector3(yMinusPos_x, yMinusPos_y, centorPos.z);

        zPlusPos = ovalElements.zPlusTransform.position;
        zMinusPos = ovalElements.zMinusTransform.position;

        xUnit = (xPlusPos - xMinusPos).normalized;
        yUnit = (yPlusPos - yMinusPos).normalized;
        zUnit = (zPlusPos - zMinusPos).normalized;
    }
    
    virtual public Vector3 CalcPos(float t)// 0 <= t <= 2pi
    {
        float xRel = Mathf.Sin(pi) * Mathf.Cos(t) * xLength / 2;
        float yRel = Mathf.Cos(pi) * yLength / 2;
        float zRel = Mathf.Sin(pi) * Mathf.Sin(t) * zLength / 2;
        Vector3 absPos = (xRel * xUnit) + (yRel * yUnit) + (zRel * zUnit) + centorPos;
        return absPos;
    }
}
