using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Pool Enemies")]
    [SerializeField, Range(1, 50)] private int _startEnemyCount = 10;
    [SerializeField] private Transform _parentBallEnemies;
    [SerializeField] private Transform _spawnerPoint;
    [SerializeField] private Enemy _prefabEnemy;
    private EnemyPoolHandler _enemyPool;
    private EnemySpawner _enemySpawner;

    [Header("Waves")]
    [SerializeField] private WaveProperty[] _waves;
    [SerializeField] private float _delayBetweenWaves = 5f;

    private int _currentWaveIndex = 0;
    private Coroutine _spawnCoroutine;
    private int _enemiesAliveInWave = 0;

    public void Init()
    {
        _enemyPool = new EnemyPoolHandler(_prefabEnemy, _parentBallEnemies, _startEnemyCount);
        _enemySpawner = new(_enemyPool);
        StartNextWave();
    }


    public void StartNextWave()
    {
        if (_currentWaveIndex < _waves.Length)
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }

            _spawnCoroutine = StartCoroutine(SpawnWave(_waves[_currentWaveIndex]));
        }
        else
        {
            Debug.Log("Все волны пройдены!");
            // Здесь можно добавить логику для завершения игры или другого действия
        }
    }

    private IEnumerator SpawnWave(WaveProperty wave)
    {
        if (_currentWaveIndex > 0)
        {
            Debug.Log($"Волна {_currentWaveIndex} завершена. Пауза {_delayBetweenWaves} секунд...");
            yield return new WaitForSeconds(_delayBetweenWaves);
        }

        Debug.Log($"Началась волна {_currentWaveIndex + 1}!");

        _enemiesAliveInWave = wave.EnemyCount;

        for (int i = 0; i < wave.EnemyCount; i++)
        {
            Enemy newEnemy = _enemySpawner.Spawn();
            newEnemy.Init(_enemyPool);
            newEnemy.transform.position = _spawnerPoint.position;

            newEnemy.SpeedMove.IncreaseSpeed(wave.SpeedMultiplier);
            newEnemy.Health.IncreaseHealth(wave.HealthMultiplier);

            newEnemy.Health.OnDied += OnEnemyDied;

            yield return new WaitForSeconds(wave.SpawnInterval);
        }

        while (_enemiesAliveInWave > 0)
        {
            yield return null;
        }

        _currentWaveIndex++;
        StartNextWave();
    }

    private void OnEnemyDied(Health health)
    {
        health.OnDied -= OnEnemyDied;
        _enemiesAliveInWave--;
    }
}
