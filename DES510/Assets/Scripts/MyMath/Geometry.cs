using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyMath
{
    public class Geometry
    {
        public static float FormatDegree(float degree)
        {
            degree %= 360;

            if (degree>180)
            {
                degree -=360 ;
            }else if (degree<-180)
            {
                degree += 360;
            }
            return degree;
        }

        public static Vector2 RotateVector2(Vector2 target,float degree)
        {
            float radian = degree * Mathf.Deg2Rad;
            float cos = Mathf.Cos(radian);
            float sin = Mathf.Sin(radian);
            return new Vector2(target.x*cos- target.y*sin, target.x * sin+ target.y*cos);
        }

        public static Vector3 RotateVector3(Vector3 dir, Vector3 lookat, float angle)
        {
            // 创建旋转四元数，angle为旋转角度，lookat为旋转轴
            Quaternion rotation = Quaternion.AngleAxis(angle, lookat.normalized);
            // 应用旋转到向量dir
            Vector3 rotatedVector = rotation * dir;
            return rotatedVector;
        }

        /// <summary>
        /// Calculates the signed angle between two vectors.
        /// </summary>
        /// <param name="from">The starting vector.</param>
        /// <param name="to">The destination vector.</param>
        /// <returns>Returns the angle in degrees from 'from' to 'to'.</returns>
        public static float SignedAngle(Vector2 from, Vector2 to)
        {
            // Calculate the unsigned angle
            float angle = Vector2.Angle(from, to);

            // Determine the direction using the cross product (z-component)
            float sign = Mathf.Sign(from.x * to.y - from.y * to.x);

            // Combine angle and direction
            return angle * sign;
        }



        /// <summary>
        /// 用貝茲曲線進行差值運算
        /// </summary>
        /// <param name="Start">線段起點</param>
        /// <param name="End">線段終點</param>
        /// <param name="StartDir">起點的向量</param>
        /// <param name="EndDir">終點的向量</param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 LerpWithBezier(Vector3 Start, Vector3 End, Vector3 StartDir, Vector3 EndDir, float t)
        {
            Vector3 startEnd = Start + StartDir;
            Vector3 endStart = End + EndDir;

            Vector3 resultStart = Vector3.Lerp(Start, startEnd, t);
            Vector3 resultEnd = Vector3.Lerp(endStart, End, t);
            Vector3 result = Vector3.Lerp(resultStart, resultEnd, t); ;

            return result;
        }

        /// <summary>
        /// 用於計算圓形的移動
        /// </summary>
        /// <param name="center">圓心</param>
        /// <param name="normal">法向量</param>
        /// <param name="radius">半徑</param>
        /// <param name="speed">速度</param>
        /// <param name="time">經過時間</param>
        /// <returns></returns>
        public static Vector3 Circle(Vector3 center, Vector3 normal, float radius, float speed, float time,bool clockwise)
        {
            //Debug.Log($"Clockwise={clockwise}");

            // 計算移動的角度（弧度）
            float angle = (speed * time) / radius*(clockwise?1:-1);

            // 找到兩個正交的單位向量在圓的平面上
            Vector3 arbitraryVector = (normal != Vector3.up) ? Vector3.up : Vector3.forward;  // 確保不與法線平行
            Vector3 tangent = Vector3.Cross(normal, arbitraryVector).normalized;
            Vector3 binormal = Vector3.Cross(normal, tangent).normalized;

            // 使用圓的參數方程計算位置
            Vector3 pointOnCircle = center + radius * (Mathf.Cos(angle) * tangent + Mathf.Sin(angle) * binormal);
            return pointOnCircle;
        }

        /// <summary>
        /// 從角度取圓形上的點
        /// </summary>
        /// <param name="center"></param>
        /// <param name="normal"></param>
        /// <param name="radius"></param>
        /// <param name="angle"></param>
        /// <param name="clockwise"></param>
        /// <returns></returns>
        public static Vector3 Circle(Vector3 center, Vector3 normal, float radius, float angle, bool clockwise)
        {
            // 轉換角度為弧度
            float radians = angle * Mathf.Deg2Rad;
            if (clockwise)
            {
                radians = -radians; // 順時針旋轉，角度為負
            }

            // 計算平面上的兩個正交單位向量
            Vector3 arbitraryVector = (normal != Vector3.up) ? Vector3.up : Vector3.forward; // 確保不與法線平行
            Vector3 tangent = Vector3.Cross(normal, arbitraryVector).normalized;
            Vector3 binormal = Vector3.Cross(normal, tangent).normalized;

            // 使用參數方程計算旋轉後的點
            Vector3 pointOnCircle = center + radius * (Mathf.Cos(radians) * tangent + Mathf.Sin(radians) * binormal);
            return pointOnCircle;
        }

        /// <summary>
        /// 從角度換算需要多少時間
        /// </summary>
        /// <param name="angleInDegrees"></param>
        /// <param name="speed"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static float CalculateTime(float angleInDegrees, float speed, float radius)
        {
            // 轉換角度為弧度
            float angleInRadians = angleInDegrees * Mathf.PI / 180;

            // 計算所需時間
            float timeRequired = (angleInRadians * radius) / speed;

            return timeRequired;
        }
    }

}

