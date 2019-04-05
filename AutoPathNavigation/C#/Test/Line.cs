using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Vector3 p0;
    public Vector3 p1;


    Vector3[] LinePos;


    AutoPathTool pathTool;

    // Use this for initialization
    void Start()
    {
        //pathTool = GetComponent<AutoPathTool>();
        //LinePos = new Vector3[pathTool.PathLength];
        //for (int i = 0; i < pathTool.PathLength; i++)
        //{
        //    LinePos[i] = pathTool.PathPoints[i].position;
        //}

        //p0 = LinePos[0];
        //p1 = LinePos[1];
    }


}
