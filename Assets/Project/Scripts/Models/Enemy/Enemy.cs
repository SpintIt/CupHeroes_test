using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyPoolHandler _enemyPool;

    [field: SerializeField] public Transform Target { get; private set; }

    [SerializeField] private UIHealthBar _uiHealthBar;
    [SerializeField, Range(10, 100)] private int _startHealth = 20;
    [SerializeField, Range(.1f, 2f)] private float _startSpeedMove = .2f;

    public Health Health { get; private set; }
    public SpeedMove SpeedMove { get; private set; }
    public EnemyMover EnemyMover { get; private set; }

    public void Init(EnemyPoolHandler enemyPool)
    {
        _enemyPool = enemyPool;

        Health = new(_startHealth);
        _uiHealthBar.Setup(Health);

        SpeedMove = new(_startSpeedMove);
        EnemyMover = new(SpeedMove, this);
    }

    private void Update()
    {
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
    }
}
