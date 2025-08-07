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
    [SerializeField] private float _flyDistance = 50f;
    [SerializeField] private float _flyDuration = 1f;

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

    private void ChangedHealth(int value)
    {
        _tween = _slider.DOValue(_health.Value, SPEED);
        ShowCount();

        SpawnDamageText(value);
    }

    private void SpawnDamageText(int damageValue)
    {
        GameObject textObject = DamageTextPooler.Instance.GetDamageText();

        textObject.transform.SetParent(textObject.transform);

        textObject.transform.position = transform.position;
        textObject.GetComponent<TMP_Text>().text = damageValue.ToString();
        CanvasGroup canvasGroup = textObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;

        float randomX = Random.Range(-1f, 1f);
        Vector3 randomDirection = new Vector3(randomX, 1f, 0f).normalized;
        Vector3 targetPosition = textObject.transform.localPosition + randomDirection * _flyDistance;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(textObject.transform.DOLocalMove(targetPosition, _flyDuration).SetEase(Ease.OutQuad));
        sequence.Join(canvasGroup.DOFade(0f, _flyDuration));

        sequence.OnComplete(() => DamageTextPooler.Instance.ReturnToPool(textObject));
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
