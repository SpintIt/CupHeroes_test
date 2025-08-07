using UnityEngine;

public class GamePlay : MonoBehaviour
{
    private UIGamePlay _uiGamePlay;

    [SerializeField] private Scroller _scrollerBackground;
    [SerializeField] private Player _player;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private UICards _uiCards;

    public GameStateType State { get; private set; }

    public void Init(UIGamePlay uiGamePlay)
    {
        _uiGamePlay = uiGamePlay;
        State = GameStateType.Run;

        _player.Init(this);

        _player.OnRun += Run;
        _player.OnStop += Stop;
        _player.Health.OnDied += (Health health) =>
        {
            State = GameStateType.Death;
            _uiGamePlay.GameOver.Show();
        };

        _enemyController.Init();

        _uiCards.Setup(_enemyController, _player);
    }

    private void OnDestroy()
    {
        _player.OnRun -= Run;
        _player.OnStop -= Stop;
    }

    private void Update()
    {
        if (State == GameStateType.Run)
        {
            _scrollerBackground.Handler();
        }
    }

    private void Run()
    {
        State = GameStateType.Run;
    }

    private void Stop()
    {
        State = GameStateType.Stop;
    }
}
