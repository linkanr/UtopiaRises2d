using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TimeCalc
{
    const int yearLenght = 360;
    const int seasonLength = 90;
    public static TimeStruct TimeToTimeStruct(float time)
    {
   
        int iTime = Mathf.FloorToInt(time);
        int nyear = TimeToTimeStructElement(iTime,TimeElementEnum.year,out iTime);
        int nseason = TimeToTimeStructElement(iTime, TimeElementEnum.season, out iTime);
        int nday = iTime;

        TimeStruct timeStruct = new TimeStruct { year = nyear, day = nday, season = nseason };
        return timeStruct;
    }
    public static TimeStruct TimeToTimeStruct(int time)
    {


        int nyear = TimeToTimeStructElement(time, TimeElementEnum.year, out time);
        int nseason = TimeToTimeStructElement(time, TimeElementEnum.season, out time);
        int nday = time;

        TimeStruct timeStruct = new TimeStruct { year = nyear, day = nday, season = nseason };
        return timeStruct;
    }

    public static int TimeToTimeStructElement(int _timeAmount, TimeElementEnum type, out int newTime)
    {
        switch (type) 
        {
            case TimeElementEnum.year:
                {
                    int amount = Mathf.FloorToInt(_timeAmount / yearLenght);
                    newTime = _timeAmount - (amount * yearLenght);
                    return amount;
                }
            case TimeElementEnum.season:
                {
                    int amount = Mathf.FloorToInt(_timeAmount / seasonLength);
                    newTime = _timeAmount - (amount * seasonLength);
                    return amount;
                }
            case TimeElementEnum.day:
                {
                    Debug.LogError("converting says to days");
                    break;
                }
        }
        Debug.LogError("something wrong with time calculation");
        newTime =0;
        return 0;
    }
    public static int TimeStructToTime(TimeStruct timeStruct)
    {
        int newTime = 0;
        newTime += timeStruct.year * yearLenght;
        newTime += timeStruct.season * seasonLength;
        newTime += timeStruct.day;
        return newTime;
    }
    public static TimeStruct Add(TimeStruct A, TimeStruct B) 
    {
        int a = TimeStructToTime(A);
        int b = TimeStructToTime(B);
        return TimeToTimeStruct(a + b);
    }
    public static TimeStruct Subrtract(TimeStruct A, TimeStruct B)
    {
        int a = TimeStructToTime(A);
        int b = TimeStructToTime(B);
        return TimeToTimeStruct(a - b);
    }
    public static List<string> TimeToString(TimeStruct timeStruct) 
    {
        List<string> newList = new List<string>
        {
            timeStruct.year.ToString()
            , timeStruct.season.ToString()
            , timeStruct.day.ToString()
        };
        return newList;
    }

    public static string Season(int _season)
    {
    
        {
            switch (_season)
            {
                case 0:
                    return "winter";

                case 1:
                    return "spring";
                case 2:
                    return "summer";
                case 3:
                    return "fall";
                default:
                    return "N/A";
            }
        }
    }
}


public struct TimeStruct
{
    public int day;
    public int season;
    public int year;
    public readonly int StructToInt()
    {
       return TimeCalc.TimeStructToTime(this);
    }
    public void Add(int _time)
    {
        int time = StructToInt();
        int combine = time + _time;
        TimeStruct newStuct = TimeCalc.TimeToTimeStruct(combine);
        day = newStuct.day;
        season = newStuct.season;
        year = newStuct.year;  
    }
    public void Subtract(int _time)
    {
        int time = StructToInt();
        int combine = time + _time;
        TimeStruct newStuct = TimeCalc.TimeToTimeStruct(combine);
        day = newStuct.day;
        season = newStuct.season;
        year = newStuct.year;
    }
    public readonly string GetString(bool addColor = false)
    {
        List<string> list = new List<string>();
        list = TimeCalc.TimeToString(this);

        string newString ="";
        if (addColor)
        {
            int i = 0;
            List<string> colorString = new List<string>()
            {
                "<color=red>","<color=green>","<color=blue>"

            };
            foreach (string s in list)
            {

                newString +=  colorString[i]+ s + "</color>";
                i++;
            }
        }
        else
        {
            foreach (string s in list)
            {
                newString += s;
            }
        }


        return newString;
    }
}
public enum TimeElementEnum
{
    day,
    season,
    year
}