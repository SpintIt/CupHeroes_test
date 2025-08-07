using TMPro;
using UnityEngine;

public class UIGamePlay : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private Transform _scorePanel;
    [SerializeField] private TMP_Text _scoreCount;

    [Header("Cards")]
    [SerializeField] private UICards _uiCards;

    [Header("Player State")]
    [SerializeField] private Transform _playerStatePanel;
    [SerializeField] private TMP_Text _speedCount;
    [SerializeField] private TMP_Text _powerCount;

    [Header("Player State")]
    [field: SerializeField] public Transform GameOver { get; private set; }

    public void Setup()
    {
        _scorePanel.Show();
        _playerStatePanel.Show();
        _uiCards.HideCards();
        GameOver.Hide();
    }
}
