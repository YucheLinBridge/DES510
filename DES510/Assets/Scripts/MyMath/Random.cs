using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace MyMath
{
    public class MyRandom
    {

        public static List<int> ListExcept(List<int> a, List<int> b)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < a.Count; i++)
            {
                if (!b.Contains(a[i]))
                {
                    result.Add(a[i]);
                }
            }

            return result;
        }

        public static List<int> RandomOrder(int n)
        {
            List<int> numbers = new List<int>();
            List<int> rndlist = new List<int>();
            for (int i = 0; i < n; i++)
            {
                numbers.Add(i);
            }

            for (int i = 0; i < n; i++)
            {
                int tmp = numbers[Random.Range(0, numbers.Count)];
                rndlist.Add(tmp);
                numbers.Remove(tmp);
            }

            return rndlist;
        }
        public static List<int> RandomNum(int n, int min, int max)
        {
            List<int> orders = RandomOrder(n);
            List<int> maxlist = RandomOrder(max);
            List<int> minList = RandomOrder(min);
            List<int> exceptList = ListExcept(maxlist, minList);
            List<int> result = new List<int>();

            if (n < max - min)
            {
                for (int i = 0; i < n; i++)
                {
                    result.Add(exceptList[orders[i]]);
                }
            }
            else
            {
                for (int i = 0; i < (max - min); i++)
                {
                    result.Add(exceptList[orders[i]]);
                }

            }
            return result;
        }

        public static int GetRandomWithinRange(int min, int max, List<int> exclude)
        {
            List<int> lst = new List<int>();
            for (int i = min; i < max; i++)
            {
                if (!exclude.Contains(i))
                {
                    lst.Add(i);
                }
            }

            return lst[Random.Range(0, lst.Count)];

        }


        public static Vector2 RandomVector2()
        {
            float a = Random.Range(-1f, 1f);
            float b = Random.Range(-1f, 1f);
            return new Vector2(a, b);
        }

        public static Vector2 RandomVector2(float r)
        {
            float a = Random.Range(-1f, 1f);
            float b = Random.Range(-1f, 1f);
            return new Vector2(a, b) * r;
        }

        public static Vector2 RandomVector2(Vector2 v1, Vector2 v2)
        {
            float minX = v1.x < v2.x ? v1.x : v2.x;
            float maxX = v1.x > v2.x ? v1.x : v2.x;
            float minY = v1.y < v2.y ? v1.y : v2.y;
            float maxY = v1.y > v2.y ? v1.y : v2.y;

            return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }

        public static Vector3 RandomVector3(float r)
        {
            float x = Random.Range(-1f, 0.99f);
            float y = Random.Range(-1f, 0.99f);
            float z = Random.Range(-1f, 0.99f);
            return new Vector3(x, y, z) * r;
        }
        public static Vector3 RandomVector3(Vector3 v1, Vector3 v2)
        {
            float minX = v1.x < v2.x ? v1.x : v2.x;
            float maxX = v1.x > v2.x ? v1.x : v2.x;
            float minY = v1.y < v2.y ? v1.y : v2.y;
            float maxY = v1.y > v2.y ? v1.y : v2.y;
            float minZ = v1.z < v2.z ? v1.z : v2.z;
            float maxZ = v1.z > v2.z ? v1.z : v2.z;

            Vector3 result = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
            //Debug.Log(minX+" "+maxX);
            return result;
        }
        public static Vector3 RandomVector3OnCircumference(float r)
        {
            float a = Random.Range(-1f, 1f);
            float b = Random.Range(-1f, 1f);
            Vector3 result = new Vector3(a, 0, b);
            return result.normalized * r;
        }


        public static Vector3 RandomVector3OnCircumference(float min, float r)
        {
            float a = Random.Range(-1.0f, 1.1f);
            float b = Random.Range(-1f, 1f);
            a *= (r - min);
            b *= (r - min);

            a = a > 0 ? a + min : a - min;
            b = b > 0 ? b + min : b - min;

            Vector3 result = new Vector3(a, 0, b);
            return result;
        }



        /// <summary>
        /// 依照權重抽籤
        /// </summary>
        /// <param name="weights">存放權重的list</param>
        /// <returns></returns>
        public static int RandomWithWeights(List<int> _weights)
        {
            List<int> weights = new List<int>();
            weights.AddRange(_weights);
            if (weights.Count == 1)
            {
                return 0;
            }
            else if (weights.Count < 1)
            {
                Debug.LogError("無法計算空陣列");
                return 0;
            }

            int accumulatedWeight = 0;
            for (int i = 0; i < weights.Count; i++)
            {
                if (weights[i] > 0)
                {
                    accumulatedWeight += weights[i];
                    weights[i] = accumulatedWeight;
                }
            }
            int rnd = Random.Range(1, accumulatedWeight + 1);
            for (int i = 0; i < weights.Count; i++)
            {
                if (weights[i] >= rnd)
                {
                    return i;
                }
            }

            return 0;//只有空陣列，或所有權重都為0時會回傳
        }
        /// <summary>
        /// 依照權重抽籤
        /// </summary>
        /// <param name="weights">存放權重的list</param>
        /// <returns></returns>
        public static int RandomWithWeightsNonAllocate(List<int> weights)
        {
            if (weights.Count == 1)
            {
                return 0;
            }
            else if (weights.Count < 1)
            {
                Debug.LogError("無法計算空陣列");
                return 0;
            }

            int accumulatedWeight = 0;
            for (int i = 0; i < weights.Count; i++)
            {
                if (weights[i] > 0)
                {
                    accumulatedWeight += weights[i];
                    weights[i] = accumulatedWeight;
                }
            }
            int rnd = Random.Range(1, accumulatedWeight + 1);
            for (int i = 0; i < weights.Count; i++)
            {
                if (weights[i] >= rnd)
                {
                    return i;
                }
            }

            return 0;//只有空陣列，或所有權重都為0時會回傳
        }


        /// <summary>
        /// 依照權重抽num個，如果carepeat==false則不會重複
        /// </summary>
        /// <param name="weights">存放權重的list</param>
        /// <returns></returns>
        public static int[] RandomWithWeights(List<int> _ids, List<int> _weights, int num, bool canrepeat)
        {
            int[] results = new int[num];
            List<int> weights = new List<int>();
            List<int> ids = new List<int>();
            weights.AddRange(_weights);
            ids.AddRange(_ids);

            if (weights.Count < num && !canrepeat)
            {
                Debug.Log($"數量不足\n需求數量：{num}\n物件數量：{weights.Count}");
                num = weights.Count;
            }
            else if (weights.Count == 0)
            {
                Debug.LogError("權重陣列不能為零");
                return results;
            }



            if (canrepeat)
            {
                int accumulatedWeight = 0;
                for (int i = 0; i < weights.Count; i++)
                {
                    if (weights[i] > 0)
                    {
                        accumulatedWeight += weights[i];
                        weights[i] = accumulatedWeight;
                    }
                }

                for (int j = 0; j < num; j++)
                {
                    int rnd = Random.Range(1, accumulatedWeight + 1);
                    for (int i = 0; i < weights.Count; i++)
                    {
                        if (weights[i] >= rnd)
                        {
                            results[j] = ids[i];
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < num; j++)
                {
                    int accumulatedWeight = 0;
                    for (int i = 0; i < weights.Count; i++)
                    {
                        if (weights[i] > 0)
                        {
                            accumulatedWeight += weights[i];
                            weights[i] = accumulatedWeight;
                        }
                    }

                    int rnd = Random.Range(1, accumulatedWeight + 1);
                    for (int i = 0; i < weights.Count; i++)
                    {
                        if (weights[i] >= rnd)
                        {
                            results[j] = ids[i];
                            weights.RemoveAt(i);
                            ids.RemoveAt(i);
                            break;
                        }
                    }
                }
            }


            return results;
        }
    }

}
