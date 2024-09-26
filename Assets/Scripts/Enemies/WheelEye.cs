using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelEye : Enemy
{
    protected override void CalculateNewDestination()
    {
        destination = new Vector2(
            Random.Range(
                -canvas.renderingDisplaySize.x / 2.2f,
                canvas.renderingDisplaySize.x / 2.2f),
            rectTransform.anchoredPosition.y);
    }
}
