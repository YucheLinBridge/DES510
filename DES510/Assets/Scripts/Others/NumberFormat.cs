using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberFormat 
{
    public enum ToInt {
        Round,
        Floor,
        Ceil
    }


    private const string free = "Free";

    public static string OnlySecond(float time)
    {
        return Mathf.Round(time).ToString();
    }


    public static string EasyFormatByTime(float time)
    {
        int seconds = Mathf.CeilToInt(time);
        return string.Format("{0:00}:{1:00}", seconds / 60, seconds%60);
    }


    public static string OnlySecond(float time,ToInt toInt)
    {
        switch (toInt)
        {
            case ToInt.Round:
                return Mathf.Round(time) + "s";
            case ToInt.Floor:
                return Mathf.Floor(time) + "s";
            case ToInt.Ceil:
                return Mathf.Ceil(time)+"s";
            default:
                return Mathf.Round(time) + "s";
        }

        
    }
    public static string EasyFormatByTime(float time, ToInt toInt)
    {
        int seconds = 0;
        switch (toInt)
        {
            case ToInt.Round:
                seconds = Mathf.RoundToInt(time);
                break;
            case ToInt.Floor:
                seconds = Mathf.FloorToInt(time);
                break;
            case ToInt.Ceil:
                seconds = Mathf.CeilToInt(time);
                break;
            default:
                seconds = Mathf.RoundToInt(time);
                break;
        }

        return string.Format("{0:00}:{1:00}", seconds / 60, seconds % 60);
    }

    public static string EasyFormatFloat(float num, ToInt type)
    {
        switch (type)
        {
            case ToInt.Round:
                return $"{Mathf.RoundToInt(num)}";
            case ToInt.Floor:
                return $"{Mathf.FloorToInt(num)}";
            case ToInt.Ceil:
                return $"{Mathf.CeilToInt(num)}";
            default:return $"{Mathf.RoundToInt(num)}";
        }
    }

    public static string EasyFormatByKMB(float num, bool freeflag)
    {
        string result = "0";

        if (num<=0 && freeflag) { return free; }
        
        switch (num)
        {
            case >= 1000000000: result = string.Format("{0:.##}B", num / 1000000000); break;
            case >= 1000000: result = string.Format("{0:.##}M", num / 1000000); break;
            case >= 1000: result = string.Format("{0:.##}K", num / 1000); break;
            default: result = string.Format("{0:0}", num); break;
        }

        return result;
    }

    public static string EasyFormatByPercentage(float num)
    {
        return num * 100 + "%";
    }
}
