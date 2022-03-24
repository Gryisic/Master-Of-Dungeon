using System;
using UnityEngine;

public static class MathExtension 
{
    public static Vector2 RoundedVector(Vector2 vector) => new Vector2(RoundedValue(vector.x), RoundedValue(vector.y));

    public static Vector2 GetCentralPosition(Vector2 vector) => 
        new Vector2(IntegerPartOfValue(vector.x) + 0.5f * Direction(vector.x), 
            IntegerPartOfValue(vector.y) + 0.5f * Direction(vector.y));

    public static Vector2 Direction(Vector2 from, Vector2 to) => (from - to).normalized;

    public static int Direction(float value) => value > 0 ? 1 : -1;

    public static int Direction(float from, float to) => to - from > 0 ? 1 : -1;

    public static int IntegerPartOfValue(float value) => (int)Math.Truncate(value);

    public static float FractionalPartOfValue(float value) => value - IntegerPartOfValue(value);

    public static int RoundedValue(float value)
    {
        float absoluteValue = Mathf.Abs(value);

        int finalValue = FractionalPartOfValue(absoluteValue) < 0.5f ? Mathf.RoundToInt(absoluteValue) : Mathf.CeilToInt(absoluteValue);

        return finalValue * Direction(value);
    }
}
