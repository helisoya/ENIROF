using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab; // Le prefab de l'ennemi (RectTransform)
    [SerializeField] private float spawnInterval = 5f; // Intervalle de spawn en secondes
    private Canvas canvas; // Le transform du Canvas parent

    private float timeSinceLastSpawn;
    private List<Enemy> enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Enemy>();
        canvas = FindAnyObjectByType<Canvas>();
        timeSinceLastSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnEnemy()
    {
        // Instancier l'ennemi dans le Canvas
        Enemy enemyInstance = Instantiate(enemyPrefab, canvas.GetComponent<RectTransform>());

        // Déplacer l'instance au début des enfants du RectTransform
        enemyInstance.transform.SetSiblingIndex(0);

        // Ajouter l'ennemi à la liste
        enemies.Add(enemyInstance);

        // Récupérer le RectTransform de l'ennemi
        RectTransform enemyRectTransform = enemyInstance.GetComponent<RectTransform>();

        // Appliquer la position à l'ennemi
        enemyRectTransform.anchoredPosition = new Vector2(0.0f,0.0f);
    }

    public void ProcessFire(Vector2 pointerPos)
    {
        foreach (Enemy enemy in enemies)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(enemy.GetComponent<RectTransform>(), pointerPos, null)) // Condition pour détruire
            {
                DestroyChildEnemy(enemy);
                break;
            }
        }

    }
    public void DestroyChildEnemy(Enemy enemy)
    {
        enemies.Remove(enemy); // Retirer de la liste
        Destroy(enemy.gameObject); // Détruire l'objet
    }
}
