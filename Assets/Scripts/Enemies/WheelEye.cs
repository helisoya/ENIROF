using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelEye : Enemy
{
    protected override void CalculateNewDestination()
    {
        destination = new Vector2(
            Random.Range(
                -bounds.width / 2.0f,
                bounds.width / 2.0f),
            spawPoint.y);
    }
}
