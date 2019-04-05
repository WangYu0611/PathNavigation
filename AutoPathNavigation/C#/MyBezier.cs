using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezierHelper
{
    public static Vector3 BezierValue(float t, Vector3[] points)
    {
        if (points.Length < 1)  // 一个点都没有
            throw new ArgumentOutOfRangeException();

        int count = points.Length;
        Vector3[] tmp_points = new Vector3[count];
        for (int i = 1; i < count; ++i)
        {
            for (int j = 0; j < count - i; ++j)
            {
                if (i == 1)
                {
                    tmp_points[j].x = (float)(points[j].x * (1 - t) + points[j + 1].x * t);
                    tmp_points[j].y = (float)(points[j].y * (1 - t) + points[j + 1].y * t);
                    tmp_points[j].z = (float)(points[j].z * (1 - t) + points[j + 1].z * t);

                    continue;
                }
                tmp_points[j].x = (float)(tmp_points[j].x * (1 - t) + tmp_points[j + 1].x * t);
                tmp_points[j].y = (float)(tmp_points[j].y * (1 - t) + tmp_points[j + 1].y * t);
                tmp_points[j].z = (float)(tmp_points[j].z * (1 - t) + tmp_points[j + 1].z * t);
            }
        }



        return tmp_points[0];
    }

}

