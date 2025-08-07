using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    private Tween _tween;
    private const float SPEED = .5f;
    private Health _health;

    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _count;

    public void Setup(Health health)
    {
        _health = health;
        _health.OnChangedHealth += ChangedHealth;
        _health.OnIncreaseHealth += IncreaseHealth;

        _slider.maxValue = _health.MaxHealth;
        _slider.value = _health.MaxHealth;
        ShowCount();
    }

    private void OnDisable()
    {
        if (_health == null)
            return;

        _tween.Kill();
        _slider.value = _slider.maxValue;
        _health.OnChangedHealth -= ChangedHealth;
        _health.OnIncreaseHealth -= IncreaseHealth;
    }

    private void ChangedHealth()
    {
        _tween = _slider.DOValue(_health.Value, SPEED);
        ShowCount();
    }

    private void IncreaseHealth()
    {
        _slider.maxValue = _health.MaxHealth;
        _slider.value = _health.MaxHealth;
        ShowCount();
    }

    private void ShowCount()
    {
        _count.text = _health.Value.ToString();
    }
}
