using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float maxScale;
    [SerializeField] private Vector2 destination;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 anchoredPosition;
    [SerializeField] private Vector3 localPosition;
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindAnyObjectByType<Canvas>();
        CalculateNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.localScale += Vector3.one * (zoomSpeed * Time.deltaTime);
        if(rectTransform.localScale.x > maxScale)
            Destroy(gameObject);
        if (rectTransform.anchoredPosition.Equals(destination))
            CalculateNewDestination();
        rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, destination, speed);


        localPosition = rectTransform.localPosition;
        anchoredPosition = rectTransform.anchoredPosition;
        position = rectTransform.position;
    }

    private void CalculateNewDestination()
    {
        destination = new Vector2(
            Random.Range(-canvas.renderingDisplaySize.x/2.0f, canvas.renderingDisplaySize.x/2.0f),
            Random.Range(0, canvas.renderingDisplaySize.y/2.0f));
        
        /*if((destination - rectTransform.position).magnitude > 9.0f)
            destination = rectTransform.position + (destination - rectTransform.position).normalized*9.0f;*/
    }
}
