using System;
using System.Collections;
using System.Collections.Generic; // Добавляем using для List
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    private bool _isPause;

    [Header("Pool Enemies")]
    [SerializeField, Range(1, 50)] private int _startEnemyCount = 10;
    [SerializeField] private Transform _parentBallEnemies;
    [SerializeField] private Transform _spawnerPoint;
    [SerializeField] private Enemy _prefabEnemy;
    private EnemyPoolHandler _enemyPool;
    private EnemySpawner _enemySpawner;

    [Header("Waves")]
    [SerializeField] private WaveProperty[] _initialWaves; // Исходный массив волн
    [SerializeField] private float _delayBetweenWaves = 5f;
    [SerializeField, Range(0.1f, 1f)] private float _endlessModeIncrease = 0.1f; // 10%

    private List<WaveProperty> _waves; // Динамический список волн
    private int _currentWaveIndex = 0;
    private Coroutine _spawnCoroutine;
    private int _enemiesAliveInWave = 0;

    public event UnityAction OnEndWave;

    public void Init()
    {
        _enemyPool = new EnemyPoolHandler(_prefabEnemy, _parentBallEnemies, _startEnemyCount);
        _enemySpawner = new(_enemyPool);
        
        // Копируем начальные волны в динамический список
        _waves = new List<WaveProperty>(_initialWaves);
        
        StartNextWave();
    }
    
    // Метод для снятия паузы
    public void Continue()
    {
        _isPause = false;
    }

    private void StartNextWave()
    {
        if (_currentWaveIndex < _waves.Count) // Используем .Count для списка
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }

            _spawnCoroutine = StartCoroutine(SpawnWave(_waves[_currentWaveIndex]));
        }
        else
        {
            // Если все волны пройдены, начинаем бесконечный режим
            Debug.Log("Все предопределенные волны пройдены! Запускаем бесконечный режим.");
            StartEndlessMode();
        }
    }

    private void StartEndlessMode()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }

        // Берем последнюю волну из списка
        WaveProperty lastWave = _waves[_waves.Count - 1];

        // Создаем новую волну, увеличивая все параметры
        WaveProperty newWave = new WaveProperty(
            Mathf.RoundToInt(lastWave.EnemyCount * (1 + _endlessModeIncrease)),
            lastWave.SpawnInterval,
            lastWave.SpeedMultiplier * (1 + _endlessModeIncrease),
            lastWave.HealthMultiplier * (1 + _endlessModeIncrease)
        );

        // Добавляем новую волну в список!
        _waves.Add(newWave);
        
        // Запускаем спаун последней добавленной волны
        _spawnCoroutine = StartCoroutine(SpawnWave(_waves[_waves.Count - 1]));
    }

    private IEnumerator SpawnWave(WaveProperty wave)
    {
        if (_currentWaveIndex > 0)
        {
            Debug.Log($"Волна {_currentWaveIndex} завершена. Пауза перед следующей волной...");
            _isPause = true;
            OnEndWave?.Invoke();
            
            while (_isPause)
            {
                yield return null;
            }
            
            Debug.Log($"Пауза закончилась. Отсчет до старта следующей волны {_delayBetweenWaves} секунд...");
            yield return new WaitForSeconds(_delayBetweenWaves);
        }

        Debug.Log($"Старт волны {_currentWaveIndex + 1}!");

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
        
        if (_currentWaveIndex >= _waves.Count)
        {
            StartEndlessMode();
        }
        else
        {
            StartNextWave();
        }
    }

    private void OnEnemyDied(Health health)
    {
        health.OnDied -= OnEnemyDied;
        _enemiesAliveInWave--;
    }
}