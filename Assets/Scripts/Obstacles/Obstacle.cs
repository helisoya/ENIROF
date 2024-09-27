using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Vector3 direction = new Vector3(-1, 0, 0);

    private bool dealtDamageToPlayer = false;

    public void Init(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        dealtDamageToPlayer = false;
    }

    void Update()
    {
        transform.position += direction * ScrollerManager.instance.GetCurrentScrollSpeed() * Time.deltaTime;

        if (transform.position.x <= 0)
        {
            if (!dealtDamageToPlayer)
            {
                Player.instance.IncrementMult();
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (dealtDamageToPlayer) return;

        Player.instance.TakeDamage();
        dealtDamageToPlayer = true;
    }
}
