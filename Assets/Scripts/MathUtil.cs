#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public interface MathUtil
{
    static T GetRandomElement<T>(List<T> collection)
    {
        var index = Random.Range(0, collection.Count());
        return collection[index];
    }

    static Color GetRandomColor()
    {
        return new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
        );
    }

    static T GetRandomElement<T>(T[] arr)
    {
        var index = Random.Range(0, arr.Length);
        return arr[index];
    }
}