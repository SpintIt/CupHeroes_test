using UnityEngine;
using UnityEngine.Events;

public class GamePlay : MonoBehaviour
{
    private GameStateType _state;

    [SerializeField] private Scroller _scrollerBackground;
    [SerializeField] private Player _player;
    [SerializeField] private EnemyController _enemyController;
    /*private Ball _currentBall;

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

    public MatchThree MatchThree { get; private set; }*/

    public void Init()
    {
        _state = GameStateType.Run;

        _player.Init();

        _player.OnRun += Run;
        _player.OnStop += Stop;

        _enemyController.Init();

        /*_uiGameOver.Hide();

        MatchThree = new();

        _eventSystem.Init();*/

        /*_enemyPool = new EnemyPoolHandler(_prefabEnemy, _parentBallEnemies, _startEnemyCount);
        _enemySpawner = new(_enemyPool);*/
        /*_scoreCounter = new(_uiScorePanel, MatchThree, _scoresForColor);*/

        /* _eventSystem.OnClick += OnClick;
        _eventSystem.PressAnyKey += OnPressAnyKey;
        MatchThree.OnRemoveBall += RemoveBalls;
        MatchThree.OnGameOver += OnGameOver; */
    }

    private void OnDestroy()
    {
        _player.OnRun -= Run;
        _player.OnStop -= Stop;
    }

    private void Update()
    {
        if (_state == GameStateType.Run)
        {
            _scrollerBackground.Handler();
        }
    }

    private void Run()
    {
        _state = GameStateType.Run;
    }

    private void Stop()
    {
        _state = GameStateType.Stop;
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
