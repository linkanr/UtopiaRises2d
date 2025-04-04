using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GeneralUtils
{
    public static float fit(float _value, float _minIn, float _maxIn, float _minOut, float _maxOut)
    {
        float clampValue = Mathf.Clamp(_value, _minIn, _maxIn);
        float fraq = (clampValue - _minIn) / (_maxIn - _minIn);
        return Mathf.Lerp(_minOut, _maxOut, fraq);

    }
    public static float fit01(float _value, float _minOut, float _maxOut)
    {
        float clampValue = Mathf.Clamp(_value, 0, 1);

        return Mathf.Lerp(_minOut, _maxOut, clampValue);

    }
    public static string GetNumberWithDecimal(float _number, int _decimals)
    {
        float pow = Mathf.Pow(10f, _decimals);
        int adjustedNumber = (int)Mathf.Round(_number * pow);
        string text = adjustedNumber.ToString();

        if (text.Length == 1)
        {
            text = "0" + text;
        }

        return text.Insert(text.Length - _decimals, ".") + " km/h";
    }
    public static bool CompareColors(Color a, Color b)
    {
        Vector3 vectorA = new Vector3(a.r, a.g, a.b);
        Vector3 vectorB = new Vector3(b.r, b.g, b.b);
        if (Vector3.Distance(vectorA, vectorB) < 0.01f)
        {
            return true;
        }
        return false;
    }
    public static bool CompareColors(Color a, Color b, float errorThreshold)
    {
        Vector3 vectorA = new Vector3(a.r, a.g, a.b);
        Vector3 vectorB = new Vector3(b.r, b.g, b.b);
        if (Vector3.Distance(vectorA, vectorB) < errorThreshold)
        {
            return true;
        }
        return false;
    }
    public static void ShuffleList<T>(List<T> inputList)
    {
        for (int i = 0; i < inputList.Count - 1; i++)
        {
            T temp = inputList[i];
            int rand = Random.Range(i, inputList.Count);
            inputList[i] = inputList[rand];
            inputList[rand] = temp;
        }
    }
    public static List<T> GetRandomFromList<T>(List<T> inputList,int amount)
    {
        List<T> tempInputList = new List<T>();
        tempInputList.AddRange(inputList);
        List<T> outputList = new List<T>();
        if (amount > tempInputList.Count)
        {
            Debug.LogError("Amount is bigger than the list");
        }
        for (int i = 0; i < amount; i++)
        {
            ShuffleList(tempInputList);
            outputList.Add(tempInputList[0]);
            tempInputList.RemoveAt(0);
        }
        return outputList;
    }
    public static List<T> AddListToList<T>(List<T> inputList, List<T> addList)
    {
        List<T> outputList = new List<T>();
        foreach (var item in inputList)
        {
            outputList.Add(item);
        }
        foreach (var item in addList)
        {
            outputList.Add(item);
        }
        return outputList;
    }


    public static int GetWeightedRandom(params object[] args)
    {
        if (args.Length % 2 != 0)
            throw new System.ArgumentException("Arguments must be in pairs of (int returnValue, float weight)");

        List<(int value, float weight)> entries = new List<(int, float)>();

        for (int i = 0; i < args.Length; i += 2)
        {
            if (!(args[i] is int) || !(args[i + 1] is float))
                throw new System.ArgumentException("Each pair must be (int returnValue, float weight)");

            entries.Add(((int)args[i], (float)args[i + 1]));
        }

        float totalWeight = entries.Sum(e => e.weight);
        float randomValue = UnityEngine.Random.value * totalWeight;

        float cumulative = 0f;
        foreach (var entry in entries)
        {
            cumulative += entry.weight;
            if (randomValue < cumulative)
                return entry.value;
        }

        // Fallback - should not hit if weights are valid
        return entries[entries.Count - 1].value;
    }

    public static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }


    public static Vector2 rotate(Vector2 v, float delta, int radians0Degrees1)
    {
        if (radians0Degrees1 == 1)
        {
            delta = Mathf.Deg2Rad * delta;
        }
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }
    public static Vector2 rotate(float x, float y, float delta, int radians0Degrees1)
    {
        if (radians0Degrees1 == 1)
        {
            delta = Mathf.Deg2Rad * delta;
        }
        return new Vector2(
            x * Mathf.Cos(delta) - y * Mathf.Sin(delta),
            x * Mathf.Sin(delta) + y * Mathf.Cos(delta)
        );
    }
    public static IEnumerator pause(float time)
    {
        yield return new WaitForSeconds(time);
    }

}   
