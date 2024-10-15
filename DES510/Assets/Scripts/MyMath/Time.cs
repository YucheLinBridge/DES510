using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyMath
{
    public class Time
    {
        public static bool Timer(ref float timer,float duration,bool reset)
        {
            timer += UnityEngine.Time.deltaTime;

            if (timer>=duration)
            {
                if (reset)
                {
                    timer = 0;
                }

                return true;
            }

            return false;
        }
    }

}
