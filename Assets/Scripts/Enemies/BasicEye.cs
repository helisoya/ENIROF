using System.Collections;
using UnityEngine;

public class BasicEye : Enemy
{
    protected override void CalculateNewDestination()
    {
        destination = new Vector2(
            Random.Range(
                -canvas.renderingDisplaySize.x/2.2f,
                canvas.renderingDisplaySize.x/2.2f),
            Random.Range(
                canvas.renderingDisplaySize.y /16.0f,
                canvas.renderingDisplaySize.y/2.0f));
    }
}
