using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyMath
{
    public class Str2List
    {
        public static List<int> StringToIntList(string x, char split)
        {
            List<int> result = new List<int>();

            if (x != string.Empty)
            {
                string[] strarray = x.Split(split, System.StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strarray.Length; i++)
                {
                    int tmp = 0;
                    if (!int.TryParse(strarray[i], out tmp))
                    {
                        tmp = 0;
                    }
                    else
                    {
                        result.Add(tmp);
                    }
                }
            }

            return result;
        }



        public static string IntListToString(List<int> list, char split)
        {
            string result = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                if (i == 0)
                {
                    result = list[i].ToString();
                }
                else
                {
                    result += $"{split}{list[i]}";
                }
            }

            return result;
        }


        public static string IntArrayToString(int[] list, char split)
        {
            string result = string.Empty;
            for (int i = 0; i < list.Length; i++)
            {
                if (i == 0)
                {
                    result = list[i].ToString();
                }
                else
                {
                    result += $"{split}{list[i]}";
                }
            }

            return result;
        }


        public static int[] StringToIntArray(string x, char split)
        {
            if (x != string.Empty)
            {
                string[] strarray = x.Split(split, System.StringSplitOptions.RemoveEmptyEntries);
                int[] result = new int[strarray.Length];
                for (int i = 0; i < strarray.Length; i++)
                {
                    if (!int.TryParse(strarray[i], out int tmp))
                    {
                        result[i] = -1;
                    }
                    else
                    {
                        result[i] = tmp;
                    }
                }

                return result;
            }
            else
            {
                return new int[0];
            }

        }
    }
}

