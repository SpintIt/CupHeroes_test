using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private GamePlay _gamePlay;
    [SerializeField] private UIGamePlay _uiGamePlay;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        _uiGamePlay.Setup();
        _gamePlay.Init(_uiGamePlay);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   
    }
}
