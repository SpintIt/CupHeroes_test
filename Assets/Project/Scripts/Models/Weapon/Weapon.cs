using DG.Tweening;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private WeaponPoolHandler _weaponPool;
    private Tween _tweener;

    [SerializeField] private float _arcHeight = 2f;

    public int Damage { get; private set; }

    public void Shoot(Transform target, WeaponPoolHandler weaponPool, int damage)
    {
        _weaponPool = weaponPool;
        Damage = damage;
        _tweener = transform.DOJump(target.position, _arcHeight, 1, 0.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() => _weaponPool.Return(this));
    }

    public void Stop()
    { 
        _tweener.Kill();
        _weaponPool.Return(this);
    }
}
