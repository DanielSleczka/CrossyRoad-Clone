using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private MenuView menuView;
    [SerializeField] private GameView gameView;
    [SerializeField] private LoseView loseView;

    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private ScoreSystem scoreSystem;

    private void Start()
    {
        menuView.ShowView();
        menuView.OnStartButtonClicked_AddListener(StartGameState);
        menuView.OnExitButtonClicked_AddListener(Application.Quit);

        loseView.OnResetButtonClicked_AddListener(RestartGameState);
        loseView.OnExitButtonClicked_AddListener(Application.Quit);

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (player.isAlive())
        {
            player.UpdatePlayerMovement();
        }
    }

    public void StartGameState()
    {
        menuView.HideView();
        gameView.ShowView();
        scoreSystem.InitializeSystem();
        player.InitializePlayerMovement();
    }

    public void LoseState()
    {
        gameView.HideView();
        loseView.ShowScoreValue(scoreSystem.GetScore());
        loseView.ShowView();
    }

    public void RestartGameState()
    {
        loseView.HideView();
        SceneManager.LoadSceneAsync(0);
    }

}
