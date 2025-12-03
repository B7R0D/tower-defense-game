using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;

    [Header("Wave Settings")]
    public int maxWaves = 15;
    public int enemiesPerWave = 30;
    public float spawnInterval = 0.3f; // hoe snel enemies binnen de wave spawnen

    [Header("Health Scaling")]
    public float baseHealth = 50f;
    public float healthIncreasePerWave = 20f;

    [Header("UI")]
    public TMP_Text waveText;

    private int currentWave = 0;
    private int enemiesSpawned = 0;
    private int enemiesAlive = 0;
    private float spawnTimer = 0f;
    private bool spawningActive = false;

    void Start()
    {
        UpdateWaveUI();
        StartNextWave();
    }

    void Update()
    {
        // Stop als alle waves klaar zijn
        if (currentWave > maxWaves)
            return;

        if (!spawningActive) return;

        spawnTimer += Time.deltaTime;

        // Spawn enemies totdat we aan 30 zitten
        if (enemiesSpawned < enemiesPerWave)
        {
            if (spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }
        else
        {
            // Wave is volledig gespawned → stop met spawnen
            spawningActive = false;
        }
    }

    void SpawnEnemy()
    {
        enemiesSpawned++;
        enemiesAlive++;

        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        // HP scaling
        EnemyHealth hp = enemy.GetComponent<EnemyHealth>();
        if (hp != null)
            hp.hp = baseHealth + (healthIncreasePerWave * (currentWave - 1));

        // Laat enemy weten dat hij de spawner moet informeren wanneer hij dood gaat
        hp.spawner = this;
    }

    public void EnemyDied()
    {
        enemiesAlive--;

        // Wanneer alle 30 enemies dood zijn → start volgende wave
        if (enemiesAlive <= 0)
        {
            StartNextWave();
        }
    }

    void StartNextWave()
    {
        // Wave 15 is klaar → stop het spel
        if (currentWave == maxWaves)
        {
            currentWave++;
            UpdateWaveUI();
            Debug.Log("All waves complete!");
            return;
        }

        currentWave++;
        enemiesSpawned = 0;
        enemiesAlive = 0;
        spawningActive = true;

        UpdateWaveUI();

        Debug.Log("Starting wave " + currentWave);
    }

    void UpdateWaveUI()
    {
        if (waveText != null)
            waveText.text = currentWave + "/" + maxWaves;
    }
}
