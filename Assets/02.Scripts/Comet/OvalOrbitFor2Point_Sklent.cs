using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvalOrbitFor2Point_Sklent : OvalOrbitFor2Point
{
    [SerializeField] float tolerance;
    override public void InitializeUnitVectors()
    {
        if (parametersOK == false) return;

        Vector3 toleranceDir = (ovalElements.zPlusTransform.position - ovalElements.zMinusTransform.position).normalized;
        Vector3 toleranceVec = toleranceDir * tolerance;
        
        xLength = ovalElements.xLength;
        yLength = ovalElements.yLength;
        zLength = (ovalElements.zPlusTransform.position - ovalElements.zMinusTransform.position + toleranceVec ).magnitude;
        centorPos = new Vector3((ovalElements.zPlusTransform.position.x + ovalElements.zMinusTransform.position.x - toleranceVec.x) / 2,
                                (ovalElements.zPlusTransform.position.y + ovalElements.zMinusTransform.position.y - toleranceVec.y) / 2,
                                (ovalElements.zPlusTransform.position.z + ovalElements.zMinusTransform.position.z - toleranceVec.z) / 2);
        circumference = 2 * Mathf.PI * Mathf.Sqrt(((xLength / 2) * (xLength / 2) + (zLength / 2) * (zLength / 2)) / 2);
        float z1 = ovalElements.zPlusTransform.position.z;
        float z2 = ovalElements.zMinusTransform.position.z - toleranceVec.z;
        float x1 = ovalElements.zPlusTransform.position.x;
        float x2 = ovalElements.zMinusTransform.position.x - toleranceVec.x;
        float y1 = ovalElements.zPlusTransform.position.y;
        float y2 = ovalElements.zMinusTransform.position.y - toleranceVec.y;

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
        zMinusPos = ovalElements.zMinusTransform.position - toleranceVec;

        xUnit = (xPlusPos - xMinusPos).normalized;
        yUnit = (yPlusPos - yMinusPos).normalized;
        zUnit = (zPlusPos - zMinusPos).normalized;

        isAvailable = true;
    }
    /*override public Vector3 CalcPos(float t)// 0 <= t <= 2pi
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
    }*/
}

