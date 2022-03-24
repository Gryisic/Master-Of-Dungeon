using UnityEngine;

public class DestinationSelector 
{
    public static Vector2 Destination() 
    {
        return MathExtension.GetCentralPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
