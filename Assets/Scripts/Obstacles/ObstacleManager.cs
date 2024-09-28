using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleScaling
{
    public int scoreUnder;
    public float scaleFactor;
}

public class ObstacleManager : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private float baseCooldown;
    private float cooldown;
    private float cooldownStart;
    private float[] spawns = { -10, 0, 10 };

    [Header("Scale")]
    [SerializeField] private ObstacleScaling[] scalings;


    [Header("Components")]
    [SerializeField] private Obstacle obstaclePrefab;
    [SerializeField] private Sprite[] sprites;

    float ComputeCooldownScale()
    {
        int score = Player.instance.score;
        foreach(ObstacleScaling scaling in scalings)
        {
            if(score <= scaling.scoreUnder)
            {
                return scaling.scaleFactor;
            }
        }


        return 1.0f;


    }

    void Update()
    {
        if (Player.instance.waitingStart) return;

        cooldown = baseCooldown * ComputeCooldownScale();
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
