using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private GamePlay _gamePlay;
    [SerializeField] private UIGamePlay _uiGamePlay;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        _gamePlay.Init();
        _uiGamePlay.Setup();
    }
}
