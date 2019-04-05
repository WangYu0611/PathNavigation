using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PointMove : MonoBehaviour
{
    //贝塞尔曲线
    // public float Step
    public const float StepLength = 1000;
    public float CurrentStepLength = 0;

    [Range(1, 1.25f)]
    public float MaxStepLength = 1;

    [Range(0f, 1000f)]
    public float StepLengthFactor = 10; //

    public Vector3[] Points;
    public AutoPathTool pathTool;

    private Vector3 Target;
    private BezierCurve Bezier;

    //动画
    [Range(0, 1)]
    public float PlaySpped = 0.2f;
    public float a;
    private MeshRenderer ArrowMaterial;
    private Color color;

    private void OnEnable()
    {
        pathTool = gameObject.GetComponentInParent<AutoPathTool>();

        a = 0.8f;
        InvokeRepeating("MaterialAniomation", 0, PlaySpped);
        CurrentStepLength = 0;
        Points = new Vector3[pathTool.PathPoints.Count];
        for (int i = 0; i < pathTool.PathPoints.Count; i++)
        {
            Points[i] = pathTool.PathPoints[i];
        }
    }

    private void Start()
    {
        pathTool = gameObject.GetComponentInParent<AutoPathTool>();
        ArrowMaterial = GetComponent<MeshRenderer>();

        color = ArrowMaterial.material.color;


    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentStepLength > MaxStepLength)
        {
            pathTool.PointList.Add(gameObject);
            gameObject.SetActive(false);
            return;
        }

        Bezier = GetComponentInParent<BezierCurve>();

        Target = Bezier.transform.TransformPoint(BezierHelper.BezierValue(CurrentStepLength, Points));
        AutoRotate(Target);

    }

    private void LateUpdate()
    {
        if (Bezier == null)
        {
            return;
        }
        transform.position = Bezier.transform.TransformPoint(BezierHelper.BezierValue(CurrentStepLength, Points));
        float value = pathTool.FiringGap < 1 ? StepLengthFactor / StepLength : StepLengthFactor / StepLength + pathTool.CompensationFactor;
        CurrentStepLength += value;
    }

    public void MaterialAniomation()
    {

        a += Time.deltaTime;

        if (a > 1)
        {
            a = 0.05f;
        }

        ArrowMaterial.material.color = new Color(color.r, color.g, color.b, a);


    }

    public void AutoRotate(Vector3 target)
    {
        Vector3 relativePos = transform.position - Target;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;

    }









}
