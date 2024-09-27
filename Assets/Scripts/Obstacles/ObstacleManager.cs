using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private float baseCooldown;
    private float cooldown;
    private float cooldownStart;
    private float[] spawns = { -10, 0, 10 };


    [Header("Components")]
    [SerializeField] private Obstacle obstaclePrefab;
    [SerializeField] private Sprite[] sprites;

    float ComputeCooldownScale()
    {
        int score = GameManager.instance.currentScore;
        print(score);
        if (score <= 100) return 1f;
        if (score <= 250) return 0.75f;
        if (score <= 500) return 0.5f;
        if (score <= 750) return 0.3f;

        return 0.2f;
    }

    void Update()
    {
        if (Player.instance.waitingStart) return;

        cooldown = baseCooldown * ComputeCooldownScale();
        print(cooldown);

        if (Time.time - cooldownStart >= cooldown)
        {
            Obstacle obj = Instantiate(obstaclePrefab,
                new Vector3(500, 3.5f, spawns[Random.Range(0, spawns.Length)]),
                Quaternion.Euler(0, 90, 0),
                transform
            );
            obj.Init(sprites[Random.Range(0, sprites.Length)]);
            obj.gameObject.SetActive(true);
            cooldownStart = Time.time;
        }
    }
}
