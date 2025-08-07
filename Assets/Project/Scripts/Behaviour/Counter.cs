using System;

public class Counter
{
    private float _value = 0;
    public float Value => _value;

    private UICounter _uiCounter;

    public Counter(UICounter uiCounter)
    {
        _uiCounter = uiCounter;
        Show();
    }

    public void Set(float value)
    {
        if (value < 0)
            return;

        _value = value;
        Show();
    }

    public void Increase(float value)
    {
        if (value < 0)
            return;

        _value += value;
        Show();
    }

    public bool TrySpend(float value)
    { 
        if (value < 0)
        {
            return false;
        }

        if (_value >= value)
        {
            _value -= value;
            Show();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Show()
    {
        _uiCounter.SetCount(Math.Round(_value, 3).ToString());
    }
}