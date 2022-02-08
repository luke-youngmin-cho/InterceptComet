using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvalOrbitFor2Point_Sklent : OvalOrbitFor2Point
{
    [SerializeField] float toleranceX;
    [SerializeField] float toleranceY;
    [SerializeField] float toleranceZ;

    override public Vector3 CalcPos(float t)// 0 <= t <= 2pi
    {
        if (parametersOK == false)
        {
            Destroy(this.gameObject);
            return Vector3.zero;
        }
        float xRel = Mathf.Sin(pi) * Mathf.Cos(t) * (xLength + toleranceX) / 2;
        float yRel = Mathf.Cos(pi) * (yLength + toleranceY) / 2;
        float zRel = Mathf.Sin(pi) * Mathf.Sin(t) * (zLength + toleranceZ) / 2;
        Vector3 absPos = (xRel * xUnit) + (yRel * yUnit) + (zRel * zUnit) + centorPos;
        return absPos;
    }
}

