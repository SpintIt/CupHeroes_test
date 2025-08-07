using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICards : MonoBehaviour
{
    private Player _player;
    private EnemyController _enemyController;

    [SerializeField] private List<TMP_Text> _textCost;
    [SerializeField] private Transform _cardsPanel;
    [SerializeField, Range(1, 10)] private int _startCost = 5;
    private int _cost;

    public void Setup(EnemyController enemyController, Player player)
    {
        _enemyController = enemyController;
        _player = player;

        _cost = _startCost;
        ShowCost();
        _enemyController.OnEndWave += ShowCards;
    }

    private void OnDestroy()
    {
        _enemyController.OnEndWave -= ShowCards;
    }

    public void Continue()
    {
        _enemyController.Continue();
        HideCards();
    }

    public void ShowCards()
    {
        _cardsPanel.Show();
    }

    public void HideCards()
    {
        _cardsPanel.Hide();
    }

    public void IncreaseDamage()
    {
        TrySpend(() =>
        {
            _player.Damage.IncreaseDamage(1.2f);
            _player.CounterDamage.Set(_player.Damage.Value);
        });
    }

    public void IncreaseSpeed()
    {
        TrySpend(() =>
        {
            _player.Speed.IncreaseSpeed(1.1f);
            _player.CounterSpeed.Set(_player.Speed.Value);
        });
    }

    public void IncreaseHealth()
    {
        TrySpend(() =>
        {
            _player.Health.IncreaseHealth(1.15f);
        });
    }

    private void TrySpend(Action callback)
    {
        if (_player.CounterCount.TrySpend(_cost))
        {
            callback();
            _cost++;
            ShowCost();

            if (_player.CounterCount.Value < _cost)
                Continue();
        }
    }

    private void ShowCost()
    {
        _textCost.ForEach(item => item.text = $"{_cost} монет");        
    }
}
