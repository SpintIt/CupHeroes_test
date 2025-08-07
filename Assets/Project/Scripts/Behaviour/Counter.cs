public class Counter
{
    private int _value = 0;
    public int Value => _value;

    private UICounter _uiCounter;

    public Counter(UICounter uiCounter)
    {
        _uiCounter = uiCounter;
        Show();
    }

    public void Increase(int value)
    {
        if (value < 0)
            return;

        _value += value;
        Show();
    }

    private void Show()
    {
        _uiCounter.SetCount(_value.ToString());
    }
}