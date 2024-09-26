using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float maxScale;
    [SerializeField] private Vector2 destination;
    [SerializeField] private float maxDestinationDistance;
    private RectTransform rectTransform;
    private EnemiesManager enemiesManager;
    private Canvas canvas;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        enemiesManager = FindAnyObjectByType<EnemiesManager>();
        canvas = FindAnyObjectByType<Canvas>();
        CalculateNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculer un facteur logarithmique
        float logFactor = Mathf.Log10(1 + zoomSpeed * Time.deltaTime);
        // Appliquer la mise à l'échelle logarithmique
        rectTransform.localScale += Vector3.one * logFactor;

        if (rectTransform.localScale.x > maxScale)
            enemiesManager.DestroyChildEnemy(this);
        if (rectTransform.anchoredPosition.Equals(destination))
            CalculateNewDestination();
        rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, destination, speed);
    }

    private void CalculateNewDestination()
    {
        destination = new Vector2(
            Random.Range(-canvas.renderingDisplaySize.x/2.0f, canvas.renderingDisplaySize.x/2.0f),
            Random.Range(0, canvas.renderingDisplaySize.y/2.0f));

        // Si la magnitude actuelle est plus grande que 0.8f, la réduire
        if ((rectTransform.anchoredPosition - destination).magnitude > maxDestinationDistance)
        {
            destination = rectTransform.anchoredPosition - (rectTransform.anchoredPosition - destination).normalized * maxDestinationDistance;
        }
    }
}
