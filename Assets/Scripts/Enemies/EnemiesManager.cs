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


    [Tooltip("Sound effects")]
    [SerializeField] private AudioSource Enemy_Audiosource;
    [SerializeField] private AudioClip[] Enemy_death_Sounds;
    [Range(0.0f,3.0f)] public float volume= 1.0f;    

        [SerializeField] private AudioSource Score_Audiosource;
    [SerializeField] private AudioClip[] Score_Sounds;


    private void PlayEnemyRandomDeathSound(){
        AudioClip Enemy_death_Sounds = GetRandomClip();
        Enemy_Audiosource.PlayOneShot(Enemy_death_Sounds,volume);
    }

        private void PlayRandomScoreSound(){
        AudioClip Score_Sounds = GetRandomClipScore();
        Score_Audiosource.PlayOneShot(Score_Sounds,volume);
    }

        private AudioClip GetRandomClipScore(){
        return Score_Sounds[UnityEngine.Random.Range(0,Enemy_death_Sounds.Length)];
        Score_Audiosource.volume=UnityEngine.Random.Range(0.02f,0.05f);
        Score_Audiosource.pitch=UnityEngine.Random.Range(0.9f,2f);
        
    }

    private AudioClip GetRandomClip(){
        return Enemy_death_Sounds[UnityEngine.Random.Range(0,Enemy_death_Sounds.Length)];
        Enemy_Audiosource.volume=UnityEngine.Random.Range(0.02f,0.05f);
        Enemy_Audiosource.pitch=UnityEngine.Random.Range(0.7f,1.8f);
        
    }

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
            SpawnEnemy(GetRandomEnemyType());
            timeSinceLastSpawn = 0f;
        }
    }

    private EnemyType GetRandomEnemyType()
    {
        return (EnemyType)UnityEngine.Random.Range(0, enemyPrefabs.Length);
    }

    void SpawnEnemy(EnemyType enemyType = EnemyType.BasicEye)
    {
        // Instancier l'ennemi dans le Canvas
        Enemy enemyInstance = Instantiate(enemyPrefabs[(int)enemyType],transform);

        // Ajouter l'ennemi a la liste
        enemies.Add(enemyInstance);

        enemyInstance.transform.position = enemyInstance.SpawPoint;
    }

    public void ProcessFire(Vector2 pointerPos)
    {
        Camera camera = playerMovements.FiringFront ? frontCamera : backCamera;
        Ray ray = camera.ScreenPointToRay(pointerPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 600, LayerMask.GetMask("Enemy"))) // Condition pour detruire
        {
            Player.instance.AddScore(10);
            PlayRandomScoreSound();
            PlayEnemyRandomDeathSound();
            DestroyChildEnemy(hit.transform.GetComponent<Enemy>());
        }

    }
    public void DestroyChildEnemy(Enemy enemy)
    {
        enemies.Remove(enemy); // Retirer de la liste
        Destroy(enemy.gameObject); // Detruire l'objet
    }
}
