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

    public static Vector2Int GetRight(this Vector2Int position) => new Vector2Int(position.x + 1, position.y);
    public static Vector2Int GetLeft(this Vector2Int position) => new Vector2Int(position.x - 1, position.y);
    public static Vector2Int GetUp(this Vector2Int position) => new Vector2Int(position.x, position.y + 1);
    public static Vector2Int GetDown(this Vector2Int position) => new Vector2Int(position.x, position.y - 1);
}
