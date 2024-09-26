using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float stationnaryDelay;
    [SerializeField] protected Rect bounds;
    [SerializeField] protected bool onFront = true;
    [SerializeField] protected Vector3 spawPoint;
    protected Animator animator;
    protected float speedFactor;
    protected Vector2 destination;
    protected bool waitNewDirection;
    protected EnemiesManager enemiesManager;

    public Vector3 SpawPoint { get => spawPoint; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        enemiesManager = FindAnyObjectByType<EnemiesManager>();
        waitNewDirection = false;
        speedFactor = 0.5f;
        CalculateNewDestination();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position += ScrollerManager.instance.GetCurrentScrollSpeed() / speedFactor * Time.deltaTime * (onFront?Vector3.left:Vector3.right);
        speedFactor += Time.deltaTime * 0.425f;

        if (onFront ? transform.position.x <= 0 : transform.position.x >= 0)
        {
            Player.instance.TakeDamage();
            enemiesManager.DestroyChildEnemy(this);
        }

        if (waitNewDirection) return;

        if (transform.position.z == destination.x && transform.position.y == destination.y)
            StartCoroutine(NewDirection());
        Vector2 newPos = Vector2.MoveTowards(new Vector2(transform.position.z, transform.position.y), destination, speed);

        transform.position = new Vector3(transform.position.x, newPos.y, newPos.x);
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
                -bounds.width / 2.0f,
                bounds.width / 2.0f),
            Random.Range(
                0.0f,
                bounds.height / 2.0f));
    }
}
