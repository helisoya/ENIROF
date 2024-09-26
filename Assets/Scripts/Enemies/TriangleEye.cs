using System.Collections;
using UnityEngine;

public class TriangleEye : Enemy
{    protected override IEnumerator NewDirection()
    {
        waitNewDirection = true;
        /*while(rectTransform.localScale.y != 0.0f)
        {
            rectTransform.localScale -= Vector3.up*0.1f;
            if (rectTransform.localScale.y < 0.0f)
                rectTransform.localScale.Set(rectTransform.localScale.x, 0.0f, rectTransform.localScale.z);
            yield return new WaitForSeconds(0.05f);
        }*/

        CalculateNewDestination();

        rectTransform.anchoredPosition = destination;

        /*while (rectTransform.localScale.y != rectTransform.localScale.x)
        {
            rectTransform.localScale += Vector3.up * 0.1f;
            if (rectTransform.localScale.y > rectTransform.localScale.x)
                rectTransform.localScale.Set(rectTransform.localScale.x, rectTransform.localScale.x, rectTransform.localScale.z);
            yield return new WaitForSeconds(0.05f);
        }*/

        yield return new WaitForSeconds(stationnaryDelay);

        waitNewDirection = false;
    }

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
