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
    [SerializeField] private Canvas frontCanvas; // Le Canvas avant
    [SerializeField] private Canvas backCanvas; // Le Canvas arriere

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
        if (Player.instance.waitingStart) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnEnemy(EnemyType.TriangleEye);
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnEnemy(EnemyType enemyType = EnemyType.BasicEye)
    {
        // Choisir le bon canvas en foncion du type d'ennemi
        Canvas canvas = frontCanvas;
        if (enemyType == EnemyType.WheelEye)
            canvas = backCanvas;

        // Instancier l'ennemi dans le Canvas
        Enemy enemyInstance = Instantiate(enemyPrefabs[(int)enemyType], canvas.GetComponent<RectTransform>());

        // Definir le canvas de l'instance
        enemyInstance.Canvas = canvas;

        // Deplacer l'instance au debut des enfants du RectTransform
        enemyInstance.transform.SetSiblingIndex(0);

        // Ajouter l'ennemi a la liste
        enemies.Add(enemyInstance);

        // Recuperer le RectTransform de l'ennemi
        RectTransform enemyRectTransform = enemyInstance.GetComponent<RectTransform>();

        // Appliquer la position a l'ennemi
        enemyRectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
    }

    public void ProcessFire(Vector2 pointerPos)
    {
        foreach (Enemy enemy in enemies)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(enemy.GetComponent<RectTransform>(), pointerPos, null)) // Condition pour detruire
            {
                Player.instance.AddScore(10);
                DestroyChildEnemy(enemy);
                break;
            }
        }

    }
    public void DestroyChildEnemy(Enemy enemy)
    {
        enemies.Remove(enemy); // Retirer de la liste
        Destroy(enemy.gameObject); // Detruire l'objet
    }
}
