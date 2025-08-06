using UnityEngine;

public class GamePlay : MonoBehaviour
{
private bool _isPlay;

    [Header("Pool Balls")]
    [SerializeField, Range(1, 50)] private int _startBallCount = 10;
    [SerializeField] private Transform _parentBallSignals;
    [SerializeField] private Enemy _prefabBall;
    private BallPoolHandler _ballPool;
    private BallSpawner _ballSpawner;
    private Ball _currentBall;

    [Header("Pendulum")]
    [SerializeField] private Pendulum _pendulum;

    [Header("Match Three")]
    [SerializeField] private List<ScoreForColorProperty> _scoresForColor;

    [Header("Scores")]
    [SerializeField] private UIScorePanel _uiScorePanel;
    private ScoreCounter _scoreCounter;

    [Header("GameOver")]
    [SerializeField] private UIGameOver _uiGameOver;


    [Header("Application")]
    [SerializeField] private EventSystem _eventSystem;

    public MatchThree MatchThree { get; private set; }

    public void Init()
    {
        _isPlay = false;

        _uiGameOver.Hide();

        MatchThree = new();

        _eventSystem.Init();

        _ballPool = new BallPoolHandler(_prefabBall, _parentBallSignals, _startBallCount);
        _ballSpawner = new(_ballPool);
        _scoreCounter = new(_uiScorePanel, MatchThree, _scoresForColor);

        /* _eventSystem.OnClick += OnClick;
        _eventSystem.PressAnyKey += OnPressAnyKey;
        MatchThree.OnRemoveBall += RemoveBalls;
        MatchThree.OnGameOver += OnGameOver; */
    }

    /* private void OnDestroy()
    {
        _eventSystem.OnClick -= OnClick;
        _eventSystem.PressAnyKey -= OnPressAnyKey;
        MatchThree.OnRemoveBall -= RemoveBalls;
        MatchThree.OnGameOver -= OnGameOver;
    }

    private void Update()
    {
        if (_isPlay)
        {
            _pendulum.Handler();   
        }
    }

    public void StartGame()
    {
        _isPlay = true;
        _uiScorePanel.Show();
        _scoreCounter.Reset();
        _uiGameOver.Hide();
        TryCreateBall();
    }

    public void StopGame()
    {
        _isPlay = false;
        _uiScorePanel.Hide();
        _uiGameOver.Hide();
        MatchThree.Clear();
        RemoveBalls(MatchThree.GetBalls());
        _ballPool.ReturnAll();
    }

    private void TryCreateBall()
    {
        if (_currentBall && _currentBall.IsConnected == false)
            return;

        if (_currentBall != null)
            _currentBall.Disconnect();

        _currentBall = _ballSpawner.Spawn()
            .Show();

        _currentBall.ConnectTo(_pendulum);
    }

    public void RemoveBalls(List<Ball> balls)
    {
        balls.ForEach(ball => {
            ball.Burst();
            _ballPool.Return(ball);
        });
    }

    private void OnGameOver()
    {
        StopGame();
        _uiGameOver.ShowScore(_scoreCounter.ScoreCount)
            .Show();
    }

    private void OnClick(Vector2 mousePosition)
    { 
        if (_isPlay)
            TryCreateBall();
    }

    private void OnPressAnyKey(string key)
    {
        if (key == Constants.KEY_SPACE)
        {
            TryCreateBall();
        }
    } */
}
