using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AutoPathTool : MonoBehaviour
{
    public List<Vector3> PathPoints;//所有路点
    public GameObject PointPrefabs;

    public List<GameObject> PointList = new List<GameObject>(); //对象池

    [Header("步长补偿系数")]
    [Tooltip("在AutoPathTool发射速度大于1时生效")]
    [Range(0f, 0.1f)]
    public float CompensationFactor;

    private int pathLength;
    public int PathLength
    {
        get { return pathLength; }
    }



    //计时器
    private float gapTime;
    [Header("发射速度")]
    public float FiringGap = 0.1f;



    private void GetPathPoints()
    {
        PathPoints.Clear();
        BezierCurve bezierCurve = GetComponent<BezierCurve>();
        for (int i = 0; i < bezierCurve.Points.Length; i++)
        {
            PathPoints.Add(bezierCurve.Points[i]);
        }
    }

    private void Reset()
    {
        pathLength = PathPoints.Count;
    }

    private void Update()
    {

        GetPathPoints();
        pathLength = PathPoints.Count;
        if (pathLength == 0)
        {
            return;
        }

        if (FiringGap < gapTime)
        {
            CreatePath(PathPoints);
            gapTime = 0;
        }
        else
        {
            gapTime += Time.deltaTime;
        }
    }

    private void CreatePath(List<Vector3> pathPoint)
    {
        if (PointList.Count == 0)
        {
            GameObject go = Instantiate(PointPrefabs, transform);
            PointMove point = go.GetComponent<PointMove>();
            go.transform.position = PathPoints[0];

        }
        else
        {

            if (PointList[0] == null)
            {
                PointList.RemoveAt(0);
                return;
            }

            PointList[0].SetActive(true);
            PointList[0].transform.position = PathPoints[0];
            PointList.Remove(PointList[0]);
        }



    }
}





