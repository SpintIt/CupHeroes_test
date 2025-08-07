using System.Collections;
using DG.Tweening; // Необходимо добавить
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool _isMove;
    private EnemyPoolHandler _enemyPool;

    [field: SerializeField] public Transform Target { get; private set; }
    [field: SerializeField, Range(1f, 5f)] public int Damage { get; private set; }

    [SerializeField, Range(0.1f, 2f)] private float _attackInterval = 1f;
    private Coroutine _attackCoroutine;

    [SerializeField] private UIHealthBar _uiHealthBar;
    [SerializeField, Range(10, 100)] private int _startHealth = 20;
    [SerializeField, Range(.1f, 2f)] private float _startSpeedMove = .2f;

    [Header("Movement Animation")]
    [SerializeField] private float _jumpHeight = 0.5f; // Насколько высоко подпрыгивает
    [SerializeField] private float _animDuration = 0.5f; // Длительность одного цикла анимации

    private Tween _jumpTween;
    private float _startLocalYPosition;

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

        // Сохраняем исходную Y-позицию для анимации
        _startLocalYPosition = transform.localPosition.y;

        StartMoveAnimation();
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
            StopMoveAnimation();

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
            StartMoveAnimation();

            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
    }

    private IEnumerator Attack(Player player)
    {
        while (player.Health.IsAlive)
        {
            player.Health.TakeDamage(Damage);
            yield return new WaitForSeconds(_attackInterval);
        }

        _attackCoroutine = null;
    }

    private void StartMoveAnimation()
    {
        _jumpTween = transform.DOLocalMoveY(_startLocalYPosition + _jumpHeight, _animDuration)
            .SetEase(Ease.OutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void StopMoveAnimation()
    {
        if (_jumpTween != null && _jumpTween.IsActive())
        {
            _jumpTween.Kill();
            // Возвращаем объект к его исходной позиции
            transform.localPosition = new Vector3(transform.localPosition.x, _startLocalYPosition, transform.localPosition.z);
        }
    }
}