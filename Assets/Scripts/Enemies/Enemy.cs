using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float zoomSpeed;
    [SerializeField] protected float maxScale;
    [SerializeField] protected float stationnaryDelay;
    protected Vector2 destination;
    protected bool waitNewDirection;
    protected RectTransform rectTransform;
    protected EnemiesManager enemiesManager;
    protected Canvas canvas;
    public Canvas Canvas { set => canvas = value; }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        enemiesManager = FindAnyObjectByType<EnemiesManager>();
        waitNewDirection = false;
        CalculateNewDestination();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Calculer un facteur logarithmique
        float logFactor = Mathf.Log10(1 + zoomSpeed * Time.deltaTime);
        // Appliquer la mise a l'echelle logarithmique
        rectTransform.localScale += Vector3.one * logFactor;

        if (rectTransform.localScale.x > maxScale)
        {
            Player.instance.TakeDamage();
            enemiesManager.DestroyChildEnemy(this);
        }

        if (waitNewDirection) return;

        if (rectTransform.anchoredPosition.Equals(destination))
            StartCoroutine(NewDirection());
        rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, destination, speed);
    }

    protected virtual IEnumerator NewDirection()
    {
        waitNewDirection = true;

        yield return new WaitForSeconds(stationnaryDelay);

        CalculateNewDestination();

        waitNewDirection = false;
    }
    protected virtual void CalculateNewDestination()
    {
        destination = new Vector2(
            Random.Range(
                -canvas.renderingDisplaySize.x / 2.2f,
                canvas.renderingDisplaySize.x / 2.2f),
            Random.Range(
                canvas.renderingDisplaySize.y / 16.0f,
                canvas.renderingDisplaySize.y / 2.0f));
    }
}
