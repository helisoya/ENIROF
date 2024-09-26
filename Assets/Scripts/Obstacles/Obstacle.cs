using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int distanceToPlayerRequiredToHit;
    private Vector3 direction = new Vector3(-1, 0, 0);

    public void Init(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    void Update()
    {
        transform.position += direction * GameManager.instance.GetScrollSpeed() * Time.deltaTime;

        if (transform.position.x <= 0)
        {
            float distToPlayer = Mathf.Abs(Player.instance.GetBodyZ() - transform.position.z);
            if (distToPlayer <= distanceToPlayerRequiredToHit)
            {
                Player.instance.TakeDamage();
            }
            else
            {
                Player.instance.IncrementMult();
            }
            Destroy(gameObject);
        }
    }
}
