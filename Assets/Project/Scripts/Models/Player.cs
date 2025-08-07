using DG.Tweening; // Необходимо добавить
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private GamePlay _gamePlay;

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
    public Health Health { get; private set; }
    public SpeedShoot Speed { get; private set; }
    public Damage Damage { get; private set; }

    [Header("Score")]
    [SerializeField] private UICounter _uiCounterCount;
    [SerializeField] private UICounter _uiCounterSpeed;
    [SerializeField] private UICounter _uiCounterDamage;
    [SerializeField, Range(1, 5)] private int _countForKill = 1;
    public Counter CounterCount { get; private set; }
    public Counter CounterSpeed { get; private set; }
    public Counter CounterDamage { get; private set; }

    [Header("Wobble Animation")]
    [SerializeField] private float _wobbleAngle = 5f;
    [SerializeField] private float _wobbleDuration = 1f;
    private Tween _wobbleTween;

    public event UnityAction OnRun;
    public event UnityAction OnStop;

    public void Init(GamePlay gamePlay)
    {
        _gamePlay = gamePlay;
        _weaponPool = new WeaponPoolHandler(_prefabWeapon, _parentPoolWeapons, _startWeaponCount);
        _weaponSpawner = new(_weaponPool);
        Speed = new(_startSpeed);
        Damage = new(_startDamage);
        Health = new(_startHealth);
        _uiHealth.Setup(Health);
        CounterCount = new(_uiCounterCount);
        CounterSpeed = new(_uiCounterSpeed);
        CounterSpeed.Set(Speed.Value);
        CounterDamage = new(_uiCounterDamage);
        CounterDamage.Set(Damage.Value);

        StartWobbleAnimation();
    }

    private bool _isEnemyDetected = false;

    private void FixedUpdate()
    {
        if (_gamePlay.State == GameStateType.Death)
            return;

        RaycastHit2D hit = Physics2D.Raycast(_parentPoolWeapons.transform.position, Vector2.right, _raycastDistance, _enemyLayer);
        bool enemyIsPresent = hit.collider != null;

        if (enemyIsPresent && !_isEnemyDetected)
        {
            _isEnemyDetected = true;
            Stop();
            StopWobbleAnimation();
        }
        else if (!enemyIsPresent && _isEnemyDetected)
        {
            _isEnemyDetected = false;
            Run();
            StartWobbleAnimation();
        }

        if (enemyIsPresent && Speed.TryShoot())
        {
            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                enemy.Health.OnDied -= Kill;
                enemy.Health.OnDied += Kill;

                _weaponSpawner.Spawn()
                    .Shoot(enemy.Target, _weaponPool, Damage.Value);

                Speed.OnShot();
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
        CounterCount.Increase(_countForKill);
    }

    private void StartWobbleAnimation()
    {
        if (_wobbleTween != null && _wobbleTween.IsActive())
            return;

        _wobbleTween = transform.DORotate(new Vector3(0, 0, _wobbleAngle), _wobbleDuration / 2)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void StopWobbleAnimation()
    {
        if (_wobbleTween != null && _wobbleTween.IsActive())
        {
            _wobbleTween.Kill();
            transform.rotation = Quaternion.identity;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_parentPoolWeapons.transform.position, Vector2.right * _raycastDistance);
    }
}