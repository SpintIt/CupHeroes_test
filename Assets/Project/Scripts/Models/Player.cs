using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _raycastDistance = 5f;
    [SerializeField] private LayerMask _enemyLayer;

    [Header("Pool Weapon")]
    [SerializeField, Range(1, 50)] private int _startWeaponCount = 10;
    [SerializeField] private Transform _parentPoolWeapons;
    [SerializeField] private Weapon _prefabWeapon;
    private WeaponPoolHandler _weaponPool;
    private WeaponSpawner _weaponSpawner;

    [Header("State")]
    [SerializeField] private UIHealthBar _uiHealth;
    [SerializeField, Range(.1f, 5f)] private float _startSpeed = .5f;
    [SerializeField, Range(1, 100)] private int _startDamage = 10;
    [SerializeField, Range(50, 200)] private int _startHealth = 100;
    private Health _health;
    private SpeedShoot _speed;
    private Damage _damage;

    [Header("Score")]
    [SerializeField] private UICounter _uiCounterCount;
    [SerializeField, Range(1, 5)] private int _countForKill = 1;
    private Counter _counter;

    public event UnityAction OnRun;
    public event UnityAction OnStop;

    public void Init()
    {
        _weaponPool = new WeaponPoolHandler(_prefabWeapon, _parentPoolWeapons, _startWeaponCount);
        _weaponSpawner = new(_weaponPool);
        _speed = new(_startSpeed);
        _damage = new(_startDamage);
        _health = new(_startHealth);
        _uiHealth.Setup(_health);
        _counter = new(_uiCounterCount);
    }

    private bool _isEnemyDetected = false;

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(_parentPoolWeapons.transform.position, Vector2.right, _raycastDistance, _enemyLayer);
        bool enemyIsPresent = (hit.collider != null);

        if (enemyIsPresent && !_isEnemyDetected)
        {
            _isEnemyDetected = true;
            Stop();
        }
        else if (!enemyIsPresent && _isEnemyDetected)
        {
            _isEnemyDetected = false;
            Run();
        }

        if (enemyIsPresent && _speed.TryShoot())
        {
            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                enemy.Health.OnDied -= Kill;
                enemy.Health.OnDied += Kill;

                _weaponSpawner.Spawn()
                    .Shoot(enemy.Target, _weaponPool, _damage.Value);
                
                _speed.OnShot();
            }
        }
    }

    public void Run()
    {
        OnRun?.Invoke();
    }

    public void Stop()
    {
        OnStop?.Invoke();
    }

    public void Kill(Health health)
    {
        health.OnDied -= Kill;
        _counter.Increase(_countForKill);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_parentPoolWeapons.transform.position, Vector2.right * _raycastDistance);
    }
}
