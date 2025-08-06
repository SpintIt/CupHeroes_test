using UnityEngine;

public class Health : MonoBehaviour
{
    public int Value { get; private set; }

    public int MaxHealth => _maxHealth;
    [SerializeField, Range(1, 1000)] private int _maxHealth;

    public bool IsAlive => Value > 0;

    public event UnityAction OnChangedHealth;

    private void Awake()
    {
        Value = MaxHealth;
    }

    public void TakeDamage(int count)
    {
        if (count < 0 || Value <= 0)
            return;

        Value = Math.Clamp(Value - count, 0, MaxHealth);

        OnChangedHealth?.Invoke();
    }

}
