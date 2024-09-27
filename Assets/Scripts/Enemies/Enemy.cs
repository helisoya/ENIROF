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
    [SerializeField] protected float distanceToDie;
    protected Animator animator;
    protected float speedFactor;
    protected Vector2 destination;
    protected bool waitNewDirection;
    protected EnemiesManager enemiesManager;
    protected bool isExploding = false;
    protected bool isDying = false;

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
        if(isExploding || isDying) return;

        transform.position += ScrollerManager.instance.GetCurrentScrollSpeed() / speedFactor * Time.deltaTime * (onFront?Vector3.left:Vector3.right);
        speedFactor += Time.deltaTime * 0.45f;

        if (Mathf.Abs(transform.position.x) <= distanceToDie)
            StartCoroutine(Explode());

        if (waitNewDirection) return;

        if (transform.position.z == destination.x && transform.position.y == destination.y)
            StartCoroutine(NewDirection());
        Vector2 newPos = Vector2.MoveTowards(new Vector2(transform.position.z, transform.position.y), destination, speed);

        transform.position = new Vector3(transform.position.x, newPos.y, newPos.x);
    }

    protected IEnumerator Explode()
    {
        isExploding = true;
        animator.SetTrigger("Explode");
        yield return new WaitForSeconds(1.5f);
        Player.instance.TakeDamage();
        Die();
    }

    public void Die()
    {
        isDying = true;
        StartCoroutine(DieCoroutine());
    }

    protected IEnumerator DieCoroutine()
    {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(0.5f);
        enemiesManager.DestroyChildEnemy(this);
    }

    protected virtual IEnumerator NewDirection()
    {
        animator.SetBool("IsMoving", false);
        waitNewDirection = true;

        yield return new WaitForSeconds(stationnaryDelay);

        CalculateNewDestination();

        waitNewDirection = false;
        animator.SetBool("IsMoving", true);
    }
    protected virtual void CalculateNewDestination()
    {
        destination = new Vector2(
            Random.Range(
                -bounds.width / 2.0f,
                bounds.width / 2.0f),
            Random.Range(
                bounds.height / 8.0f,
                bounds.height / 2.0f));
    }
}
