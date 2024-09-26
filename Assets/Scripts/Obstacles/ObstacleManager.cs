using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private float cooldown;
    private float cooldownStart;
    private float[] spawns = { -10, 0, 10 };


    [Header("Components")]
    [SerializeField] private Obstacle obstaclePrefab;
    [SerializeField] private Sprite[] sprites;

    void Update()
    {
        if (Time.time - cooldownStart >= cooldown)
        {
            Obstacle obj = Instantiate(obstaclePrefab,
                new Vector3(250, 5f, spawns[Random.Range(0, spawns.Length)]),
                Quaternion.Euler(0, 90, 0),
                transform
            );
            obj.Init(sprites[Random.Range(0, sprites.Length)]);
            obj.gameObject.SetActive(true);
            cooldownStart = Time.time;
        }
    }
}
