using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static void Shuffle<T>(this List<T> colors)
    {
        for (int i = 0; i < colors.Count; i++)
        {
            int randomIndex = Random.Range(i, colors.Count);

            T temp = colors[randomIndex];
            colors[randomIndex] = colors[i];
            colors[i] = temp;
        }
    }
}
