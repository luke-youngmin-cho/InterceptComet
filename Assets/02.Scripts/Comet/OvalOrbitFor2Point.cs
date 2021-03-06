using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvalOrbitFor2Point : MonoBehaviour
{
    public st_OvalElementsFor2Points ovalElements;
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
    public bool parametersOK
    {
        get
        {
            if (ovalElements.zMinusTransform == null )
                return false;
            else
                return true;
        }
    }
    [HideInInspector] public bool isAvailable;
    [HideInInspector] public Vector3 centorPos;
    [HideInInspector] public Vector3 xUnit;
    [HideInInspector] public Vector3 yUnit;
    [HideInInspector] public Vector3 zUnit;
    [HideInInspector] public float xLength;
    [HideInInspector] public float yLength;
    [HideInInspector] public float zLength;
    [HideInInspector] public Vector3 xPlusPos;
    [HideInInspector] public Vector3 xMinusPos;
    [HideInInspector] public Vector3 yPlusPos;
    [HideInInspector] public Vector3 yMinusPos;
    [HideInInspector] public Vector3 zPlusPos;
    [HideInInspector] public Vector3 zMinusPos;
    [HideInInspector] public float circumference;
    private void Awake()
    {
        if(ovalElements.zMinusTransform != null)
        {
            InitializeUnitVectors();
        }
    }
    public void SetTarget(Transform target)
    {
        ovalElements.zMinusTransform = target;
        InitializeUnitVectors();
    }
    virtual public void InitializeUnitVectors()
    {
        if (parametersOK == false) return;
        xLength = ovalElements.xLength;
        yLength = ovalElements.yLength;
        zLength = (ovalElements.zPlusTransform.position - ovalElements.zMinusTransform.position).magnitude;
        centorPos = new Vector3((ovalElements.zPlusTransform.position.x + ovalElements.zMinusTransform.position.x) / 2,
                                (ovalElements.zPlusTransform.position.y + ovalElements.zMinusTransform.position.y) / 2,
                                (ovalElements.zPlusTransform.position.z + ovalElements.zMinusTransform.position.z) / 2);

        circumference = 2 * Mathf.PI * Mathf.Sqrt(((xLength / 2) * (xLength / 2) + (zLength / 2) * (zLength / 2)) / 2);
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

        isAvailable = true;
    }
    
    virtual public Vector3 CalcPos(float t)// 0 <= t <= 2pi
    {
        if(parametersOK == false )
        {
            Destroy(this.gameObject);
            return Vector3.zero;
        }

        float xRel = Mathf.Sin(pi) * Mathf.Cos(t) * xLength / 2;
        float yRel = Mathf.Cos(pi) * yLength / 2;
        float zRel = Mathf.Sin(pi) * Mathf.Sin(t) * zLength / 2;
        Vector3 absPos = (xRel * xUnit) + (yRel * yUnit) + (zRel * zUnit) + centorPos;
        return absPos;
    }
}
