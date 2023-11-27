using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameIsPaused;

    public enum GameState
    {
        MainMenu,
        Paused,
        Gaming,
        MiniGaming,
        Building
    }

    GameState curerentState { get; set; }
    GameState lastState { get; set; }

    public void ChangeState(GameState newState)
    {
        switch (curerentState)
        {
            case GameState.Paused:
                if(newState == GameState.Paused)
                {
                    curerentState = lastState;
                    lastState = GameState.Paused;
                }
                else
                {
                    lastState = curerentState;
                    curerentState = newState;
                }
                break;
            default:
                lastState = curerentState;
                curerentState = newState;
                break;
        }
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    private void Update()
    {
        if (curerentState == GameState.Paused)
        {
            gameIsPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            gameIsPaused = false;
            Time.timeScale = 1f;
        }
    }

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
