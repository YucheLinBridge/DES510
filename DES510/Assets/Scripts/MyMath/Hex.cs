using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyMath {
    public class Hex
    {
        // 偏移坐标到立方坐标的转换
        public static (int x, int y, int z) OffsetToCube(int col, int row)
        {
            int x = col - (row - (row & 1)) / 2;
            int z = row;
            int y = -x - z;
            return (x, y, z);
        }

        // 立方坐标到偏移坐标的转换
        public static (int col, int row) CubeToOffset(int x, int y, int z)
        {
            int col = x + (z - (z & 1)) / 2;
            int row = z;
            return (col, row);
        }


        // 立方坐标到偏移坐标的转换
        public static Vector2Int CubeToOffset_Vector(int x, int y, int z)
        {
            int col = x + (z - (z & 1)) / 2;
            int row = z;
            return new Vector2Int(col, row);
        }

        // 线性插值
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        // 立方坐标的线性插值
        public static (float x, float y, float z) CubeLerp((int x, int y, int z) a, (int x, int y, int z) b, float t)
        {
            return (Lerp(a.x, b.x, t), Lerp(a.y, b.y, t), Lerp(a.z, b.z, t));
        }

        // 四舍五入立方坐标
        public static (int x, int y, int z) CubeRound((float x, float y, float z) cube)
        {
            int rx = Mathf.RoundToInt(cube.x);
            int ry = Mathf.RoundToInt(cube.y);
            int rz = Mathf.RoundToInt(cube.z);

            float x_diff = Mathf.Abs(rx - cube.x);
            float y_diff = Mathf.Abs(ry - cube.y);
            float z_diff = Mathf.Abs(rz - cube.z);

            if (x_diff > y_diff && x_diff > z_diff)
            {
                rx = -ry - rz;
            }
            else if (y_diff > z_diff)
            {
                ry = -rx - rz;
            }
            else
            {
                rz = -rx - ry;
            }

            return (rx, ry, rz);
        }

        // 立方坐标间的距离
        public static float CubeDistance((int x, int y, int z) a, (int x, int y, int z) b)
        {
            return Mathf.Max(Mathf.Abs(a.x - b.x), Mathf.Abs(a.y - b.y), Mathf.Abs(a.z - b.z));
        }


        // 计算直线经过的六边形
        public static List<Vector2Int> GetLine(Vector2Int start, Vector2Int end)
        {
            List<Vector2Int> results = new List<Vector2Int>();

            var cubeStart = OffsetToCube(start.x, start.y);
            var cubeEnd = OffsetToCube(end.x, end.y);
            float N = CubeDistance(cubeStart, cubeEnd);
            for (int i = 0; i <= N; i++)
            {
                var cubeResult = CubeRound(CubeLerp(cubeStart, cubeEnd, 1.0f / N * i));
                var result = CubeToOffset(cubeResult.x, cubeResult.y, cubeResult.z);
                results.Add(new Vector2Int(result.col, result.row));
            }

            return results;
        }

        public static List<Vector2Int> GetLine(Vector2Int start, Vector2Int end, int minX, int minY, int maxX, int maxY)
        {
            List<Vector2Int> results = new List<Vector2Int>();

            var cubeStart = OffsetToCube(start.x, start.y);
            var cubeEnd = OffsetToCube(end.x, end.y);
            float N = CubeDistance(cubeStart, cubeEnd);
            int count = 0;
            while (true)
            {
                var cubeResult = CubeRound(CubeLerp(cubeStart, cubeEnd, 1.0f / N * count));
                var result = CubeToOffset(cubeResult.x, cubeResult.y, cubeResult.z);
                if (result.col < minX || result.col >= maxX || result.row < minY || result.row >= maxY)
                {
                    break;
                }
                results.Add(new Vector2Int(result.col, result.row));
                count++;
            }


            return results;
        }

        public static float HexDistance(Vector2Int start, Vector2Int end)
        {
            var a = OffsetToCube(start.x, start.y);
            var b = OffsetToCube(end.x, end.y);
            return Mathf.Max(Mathf.Abs(a.x - b.x), Mathf.Abs(a.y - b.y), Mathf.Abs(a.z - b.z));
        }


        public static Vector2Int GetEndOfLine(Vector2Int start,Vector3Int dir,int length)
        {
            var cube_start = OffsetToCube(start.x, start.y);
            var cube_end = (cube_start.x+dir.x*length, cube_start.y + dir.y * length, cube_start.z + dir.z * length);
            return CubeToOffset_Vector(cube_end.Item1,cube_end.Item2,cube_end.Item3);
        }



        public static List<HexLine> GetSerialLineInPoints(List<Vector2Int> points)
        {
            List<HexLine> result = new List<HexLine>();
            HashSet<(int x, int y, int z)> cube_points = new HashSet<(int x, int y, int z)>();
            int minX = 100;int minY = 100;int minZ = 100;
            int maxX = -100;int maxY = -100;int maxZ = -100;

            for (int i=0;i<points.Count;i++)
            {
                var cube = OffsetToCube(points[i].x, points[i].y);
                cube_points.Add(cube);

                if (cube.x<minX)
                {
                    minX = cube.x;
                }

                if (cube.y < minY)
                {
                    minY = cube.y;
                }

                if (cube.z < minZ)
                {
                    minZ = cube.z;
                }


                if (cube.x >maxX)
                {
                    maxX = cube.x;
                }

                if (cube.y > maxY)
                {
                    maxY = cube.y;
                }

                if (cube.z > maxZ)
                {
                    maxZ = cube.z;
                }
            }


            Vector2Int start=Vector2Int.zero, end=Vector2Int.zero;
            bool hasStarted=false;
            int length = 0;

            //y+z-
            for (int i=minX;i<=maxX;i++)
            {
                for (int j=minY;j<=maxY;j++)
                {
                    int z = -(i+j);

                    if (cube_points.Contains((i, j, z)))
                    {
                        if (!hasStarted)
                        {
                            hasStarted = true;
                            start = CubeToOffset_Vector(i, j, z);
                        }
                        else
                        {
                            end = CubeToOffset_Vector(i, j, z);
                        }
                        length++;
                    }
                    else if(hasStarted)
                    {
                        hasStarted = false;
                        if (length >= 2)
                        {
                            result.Add(new HexLine(new Vector3Int(0, 1, -1), start, end, length));
                        }
                        length = 0;
                    }
                }



                if (hasStarted)
                {
                    hasStarted = false;
                    if (length >= 2)
                    {
                        result.Add(new HexLine(new Vector3Int(0, 1, -1), start, end, length));
                    }
                    length = 0;
                }
            }


            //x+z-
            for (int i = minY; i <= maxY; i++)
            {
                for (int j = minX; j <= maxX; j++)
                {
                    int z = -(i + j);

                    if (cube_points.Contains((j,i, z)))
                    {
                        if (!hasStarted)
                        {
                            hasStarted = true;
                            start = CubeToOffset_Vector(j,i, z);
                        }
                        else
                        {
                            end = CubeToOffset_Vector(j,i, z);
                        }
                        length++;
                    }
                    else if (hasStarted)
                    {
                        hasStarted = false;
                        if (length >= 2)
                        {
                            result.Add(new HexLine(new Vector3Int(1, 0, -1), start, end, length));
                        }
                        length = 0;
                    }
                }


                if (hasStarted)
                {
                    hasStarted = false;
                    if (length >= 2)
                    {
                        result.Add(new HexLine(new Vector3Int(1, 0, -1), start, end, length));
                    }
                    length = 0;
                }
            }


            //y+x-
            for (int i = minZ; i <= maxZ; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    int x = -(i + j);

                    if (cube_points.Contains((x, j, i)))
                    {
                        if (!hasStarted)
                        {
                            hasStarted = true;
                            start = CubeToOffset_Vector(x, j, i);
                        }
                        else
                        {
                            end = CubeToOffset_Vector(x, j, i);
                        }
                        length++;
                    }
                    else if(hasStarted)
                    {
                        hasStarted = false;
                        if (length >= 2)
                        {
                            result.Add(new HexLine(new Vector3Int(-1,1,0),start, end, length));
                        }
                        length = 0;
                    }
                }

                if (hasStarted)
                {
                    hasStarted = false;
                    if (length >= 2)
                    {
                        result.Add(new HexLine(new Vector3Int(-1, 1, 0), start, end, length));
                    }
                    length = 0;
                }

            }

            return result;
        }

        public class HexLine {
            public Vector3Int Dir;
            public Vector2Int Start;
            public Vector2Int End;
            public int Length;

            public HexLine(Vector3Int _dir,Vector2Int _start, Vector2Int _end,int _len)
            {
                Dir = _dir;
                Start = _start;
                End = _end;
                Length = _len;

                //Debug.Log($"HexLine:\nDir={Dir}\nStart={Start}\nEnd={End}\nLength={Length}");
            }
        }

    }

}
