using System;
using UnityEngine;

[Serializable]
public class WaveProperty
{
    [SerializeField, Range(1, 100)] private int _enemyCount;
    [SerializeField, Range(0.1f, 10f)] private float _spawnInterval;
    [SerializeField, Range(1f, 5f)] private float _speedMultiplier = 1f;
    [SerializeField, Range(1f, 5f)] private float _healthMultiplier = 1f;

    public int EnemyCount => _enemyCount;
    public float SpawnInterval => _spawnInterval;
    public float SpeedMultiplier => _speedMultiplier;
    public float HealthMultiplier => _healthMultiplier;

    public WaveProperty(int enemyCount, float spawnInterval, float speedMultiplier, float healthMultiplier)
    {
        _enemyCount = enemyCount;
        _spawnInterval = spawnInterval;
        _speedMultiplier = speedMultiplier;
        _healthMultiplier = healthMultiplier;
    }
}