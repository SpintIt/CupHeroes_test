using UnityEngine;

public class SpeedShoot
{
    private float _startValue;
    private float _value = .5f;

    private float _shootTimer;
    private bool _canShoot = true;

    public float Value => _value;

    public SpeedShoot(float startSpeed)
    { 
        _startValue = _value = startSpeed;
    }

    public void IncreaseSpeed(float percent)
    {
        if (percent < 0)
            return;

        _value += _value - _value * percent;
        Debug.Log(_value);
    }

    public bool TryShoot()
    {
        if (!_canShoot)
        {
            _shootTimer += Time.fixedDeltaTime;
            if (_shootTimer >= _value)
            {
                _canShoot = true;
                _shootTimer = 0f;
            }
        }

        return _canShoot;
    }

    public void OnShot()
    {
        _canShoot = false;
        _shootTimer = 0f;
    }
}