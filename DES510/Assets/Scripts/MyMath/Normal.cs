using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyMath
{
    public class Normal
    {
        public static int Abs(int target)
        {
            return target > 0 ? target : -target;
        }

        public static float Abs(float target)
        {
            return target > 0 ? target : -target;
        }

        public static bool CheckFlags(int target,int flags)
        {
            var result = target & flags;
            return result==flags;
        }

        public static int AddFlags(int target, int flags)
        {
            return target | flags;
        }

        public static int RemoveFlags(int target, int flags)
        {
            return target & ~flags;
        }
    }

}
