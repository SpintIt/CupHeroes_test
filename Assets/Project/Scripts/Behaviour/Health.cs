using System;
using UnityEngine;
using UnityEngine.Events;

public class Health
{
    public int Value { get; private set; }

    public int MaxHealth { get; private set; }

    public bool IsAlive => Value > 0;

    public event UnityAction<int> OnChangedHealth;
    public event UnityAction OnIncreaseHealth;
    public event UnityAction<Health> OnDied;

    public Health(int value)
    {
        Value = MaxHealth = value;
    }

    public void IncreaseHealth(float percent)
    {
        if (percent < 1)
            return;

        MaxHealth = Mathf.CeilToInt(MaxHealth * percent);
        Reset();
    }

    public void Reset()
    {
        Value = MaxHealth;
        OnIncreaseHealth?.Invoke();
    }

    public void TakeDamage(int count)
    {
        if (count < 0 || Value <= 0)
            return;

        Value = Math.Clamp(Value - count, 0, MaxHealth);
        OnChangedHealth?.Invoke(count);

        if (IsAlive == false)
            OnDied?.Invoke(this);
    }
}
