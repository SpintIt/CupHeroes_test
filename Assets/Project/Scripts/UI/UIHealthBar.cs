using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
    private GameManager _gameManager;
    private const float SPEED = .5f;
    private Tweener _tweener;

    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;

    public Tweener DoShakeTween { get; private set; }

    private void OnEnable()
    {
        _gameManager.OnPauseGame += StopAnimate;
        _gameManager.OnContinueGame += StartAnimate;
        _health.OnChangedHealth += OnChangedHealth;
    }

    private void OnDisable()
    {
        _gameManager.OnPauseGame -= StopAnimate;
        _gameManager.OnContinueGame -= StartAnimate;
        _health.OnChangedHealth -= OnChangedHealth;
    }

    private void OnDestroy()
    {
        _tweener.Kill();
        _gameManager.OnPauseGame -= StopAnimate;
        _gameManager.OnContinueGame -= StartAnimate;
        _health.OnChangedHealth -= OnChangedHealth;
    }

    private void Awake()
    {
        _slider.maxValue = _health.MaxHealth;
        _slider.value = _health.MaxHealth;

        _tweener = transform.DOShakePosition(
            10.0f, 
            strength: new Vector3(.5f, .5f, 0), 
            vibrato: 1, 
            randomness: 5, 
            snapping: false, 
            fadeOut: false, 
            randomnessMode: ShakeRandomnessMode.Harmonic
        ).SetLoops(-1, LoopType.Yoyo);
    }

    protected virtual void StopAnimate()
        => _tweener.Pause();

    private void StartAnimate()
        => _tweener.Play();

    private void OnChangedHealth()
    {
        _slider.DOValue(_health.Value, SPEED);
    }

}
