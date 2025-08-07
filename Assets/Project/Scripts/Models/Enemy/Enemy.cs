using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool _isMove;
    private EnemyPoolHandler _enemyPool;

    [field: SerializeField] public Transform Target { get; private set; }
    [field: SerializeField, Range(1f, 5f)] public int Damage { get; private set; }
    
    // Добавляем поля для периодического урона
    [SerializeField, Range(0.1f, 2f)] private float _attackInterval = 1f;
    private Coroutine _attackCoroutine;

    [SerializeField] private UIHealthBar _uiHealthBar;
    [SerializeField, Range(10, 100)] private int _startHealth = 20;
    [SerializeField, Range(.1f, 2f)] private float _startSpeedMove = .2f;

    public Health Health { get; private set; }
    public SpeedMove SpeedMove { get; private set; }
    public EnemyMover EnemyMover { get; private set; }

    public void Init(EnemyPoolHandler enemyPool)
    {
        _enemyPool = enemyPool;

        _isMove = true;
        Health = new(_startHealth);
        _uiHealthBar.Setup(Health);

        SpeedMove = new(_startSpeedMove);
        EnemyMover = new(SpeedMove, this);
    }

    private void Update()
    {
        if (_isMove)
            EnemyMover.Handler();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Weapon weapon))
        {
            weapon.Stop();
            Health.TakeDamage(weapon.Damage);
            if (Health.IsAlive == false)
            {
                Health.Reset();
                _enemyPool.Return(this);
            }
        }
        else if (collision.TryGetComponent(out Player player))
        {
            _isMove = false;
            // Запускаем корутину для нанесения периодического урона
            if (_attackCoroutine == null)
            {
                _attackCoroutine = StartCoroutine(Attack(player));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _isMove = true;
            // Останавливаем корутину, когда игрок выходит из зоны
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
    }

    // Корутина для нанесения урона
    private IEnumerator Attack(Player player)
    {
        while (player.Health.IsAlive)
        {
            player.Health.TakeDamage(Damage);
            yield return new WaitForSeconds(_attackInterval);
        }
        
        // Останавливаем корутину, если игрок умер
        _attackCoroutine = null;
    }
}