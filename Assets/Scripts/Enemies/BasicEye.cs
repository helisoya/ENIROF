using System.Collections;
using UnityEngine;

public class BasicEye : Enemy
{
    protected override void CalculateNewDestination()
    {
        destination = new Vector2(
            Random.Range(
                -bounds.width / 2.0f,
                bounds.width / 2.0f),
            Random.Range(
                0.0f,
                bounds.height / 2.0f));
    }
}
