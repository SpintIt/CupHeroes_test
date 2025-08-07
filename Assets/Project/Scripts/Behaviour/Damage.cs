public class Damage
{
    private int _value = 10;

    public int Value => _value;

    public Damage(int startDamage)
    { 
        _value = startDamage;
    }

    public void IncreaseDamage(float percent)
    {
        if (percent < 1)
            return;

        _value = (int)(_value * percent);
    }
}
