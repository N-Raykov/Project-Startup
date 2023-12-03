using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    GameManager gameManager;
    UIManager UIManager;

    private void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
    }

    public void MoveScene(int sceneID)
    {
        gameManager.ChangeScene(sceneID); 
    }

    public void EnableMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void DisableMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        UIManager.TogglePauseMenu();
    }
}
