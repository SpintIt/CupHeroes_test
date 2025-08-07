using TMPro;
using UnityEngine;

public class UIGamePlay : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private Transform _scorePanel;
    [SerializeField] private TMP_Text _scoreCount;

    [Header("Cards")]
    [SerializeField] private Transform _cardsPanel;

    [Header("Player State")]
    [SerializeField] private Transform _playerStatePanel;
    [SerializeField] private TMP_Text _speedCount;
    [SerializeField] private TMP_Text _powerCount;

    public void Setup()
    {
        _scorePanel.Show();
        _playerStatePanel.Show();
        HideCards();
    }

    private void ShowCards()
    { 
        _cardsPanel.Show();
    }

    private void HideCards()
    {
        _cardsPanel.Hide();
    }
}
