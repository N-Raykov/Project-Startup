using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    GameManager gameManager;

    private void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void MoveScene(int sceneID)
    {
        gameManager.ChangeScene(sceneID); 
    }

    public void ChangeGameState(int stateID)
    {
        GameManager.GameState gameState = (GameManager.GameState)stateID;

        gameManager.ChangeState(gameState);
    }

    public void EnableMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void DisableMenu(GameObject menu)
    {
        menu.SetActive(false);
    }
}
