using System;
using UnityEngine.Events;

public class Health
{
    public int Value { get; private set; }

    public int MaxHealth { get; private set; }

    public bool IsAlive => Value > 0;

    public event UnityAction OnChangedHealth;
    public event UnityAction OnIncreaseHealth;
    public event UnityAction<Health> OnDied;

    public Health(int value)
    {
        Value = MaxHealth = value;
    }

    public void IncreaseHealth(float persent)
    {
        if (persent < 1)
            return;

        MaxHealth = (int)(Value * persent);
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
        OnChangedHealth?.Invoke();

        if (IsAlive == false)
            OnDied?.Invoke(this);
    }
}
