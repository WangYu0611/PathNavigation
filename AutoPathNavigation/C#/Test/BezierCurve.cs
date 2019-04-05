using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BezierCurve : MonoBehaviour
{
    public Vector3[] Points;


    public Vector3 GetPoint(float t)
    {
        return transform.TransformPoint(BezierHelper.BezierValue(t, Points));
    }

}
