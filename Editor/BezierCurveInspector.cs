using UnityEditor;
using UnityEngine;
using System;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor
{

    private BezierCurve Curve;
    private Transform HandleTransform;
    private Quaternion HandleRotation;

    private const int lineSteps = 1000;
    private const float directionScale = 0.5f;



    private void OnSceneGUI()
    {

        Curve = target as BezierCurve;
        HandleTransform = Curve.transform;
        HandleRotation = Tools.pivotRotation == PivotRotation.Local ? HandleTransform.rotation : Quaternion.identity;
        if (Curve.Points == null || Curve.Points.Length == 0)
        {
            return;
        }

        Vector3[] points = new Vector3[Curve.Points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = ShowPoint(i);
        }

        Handles.color = Color.gray;
        for (int i = 0; i < points.Length - 1; i++)
        {
            Handles.DrawLine(points[i], points[i + 1]);
        }


        Vector3 lineStart = Curve.GetPoint(0f);

        for (int i = 1; i <= lineSteps; i++)
        {
            Vector3 lineEnd = Curve.GetPoint(i / (float)lineSteps);
            Handles.color = Color.white;
            Handles.DrawLine(lineStart, lineEnd);
            lineStart = lineEnd;
        }
    }


    private Vector3 ShowPoint(int index)
    {
        Vector3 point = HandleTransform.TransformPoint(Curve.Points[index]);
        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, HandleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(Curve, "Move Point");
            EditorUtility.SetDirty(Curve);
            Curve.Points[index] = HandleTransform.InverseTransformPoint(point);
        }
        return point;
    }

}