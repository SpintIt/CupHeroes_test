public class SpeedMove
{
    private float _value = .5f;

    public float Value => _value;

    public SpeedMove(float startSpeed)
    { 
        _value = startSpeed;
    }

    public void IncreaseSpeed(float percent)
    {
        if (percent < 1)
            return;

        _value *= percent;
    }
}
