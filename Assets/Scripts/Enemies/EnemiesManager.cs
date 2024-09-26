using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    enum EnemyType { BasicEye, WheelEye, TriangleEye, LongEye, Anomaly }

    [SerializeField] private BasicEye basicEyePrefab;
    [SerializeField] private WheelEye wheelEyePrefab;
    [SerializeField] private TriangleEye triangleEyePrefab;
    [SerializeField] private float spawnInterval = 5f; // Intervalle de spawn en secondes
    [SerializeField] private PlayerMovements playerMovements;
    [SerializeField] private Camera frontCamera;
    [SerializeField] private Camera backCamera;

    private Enemy[] enemyPrefabs;
    private float timeSinceLastSpawn;
    private List<Enemy> enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemyPrefabs = new Enemy[]
        {
            basicEyePrefab,
            wheelEyePrefab,
            triangleEyePrefab
        };
        enemies = new List<Enemy>();
        timeSinceLastSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnEnemy(EnemyType.BasicEye);
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnEnemy(EnemyType enemyType = EnemyType.BasicEye)
    {
        // Instancier l'ennemi dans le Canvas
        Enemy enemyInstance = Instantiate(enemyPrefabs[(int)enemyType],transform);

        // Ajouter l'ennemi a la liste
        enemies.Add(enemyInstance);
    }

    public void ProcessFire(Vector2 pointerPos)
    {
        Camera camera = playerMovements.FiringFront ? frontCamera : backCamera;
        Ray ray = camera.ScreenPointToRay(pointerPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 600, LayerMask.GetMask("Enemy"))) // Condition pour detruire
        {
            Player.instance.AddScore(10);
            DestroyChildEnemy(hit.transform.GetComponent<Enemy>());
        }

    }
    public void DestroyChildEnemy(Enemy enemy)
    {
        enemies.Remove(enemy); // Retirer de la liste
        Destroy(enemy.gameObject); // Detruire l'objet
    }
}
