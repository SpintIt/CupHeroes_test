using UnityEngine;

public class SpeedShoot
{
    private float _value = .5f;

    private float _shootTimer;
    private bool _canShoot = true;

    public float Value => _value;

    public SpeedShoot(float startSpeed)
    { 
        _value = startSpeed;
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