using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public void MoveScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID); 
    }

    public void ChangeGameState(int stateID)
    {
        GameManager.GameState gameState = (GameManager.GameState)stateID;

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().ChangeState(gameState);
    }
}
