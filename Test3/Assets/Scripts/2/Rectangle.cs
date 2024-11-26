using UnityEngine;

public struct Rectangle
{
    public Vector2 Min; 
    public Vector2 Max;

    public Rectangle(Vector2 min, Vector2 max)
    {
        Min= min; Max = max;
    }
}

