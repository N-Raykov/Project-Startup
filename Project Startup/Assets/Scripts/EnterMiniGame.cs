using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMiniGame : Interactable
{
    [SerializeField] private int minigameSceneID;

    public override void Interact()
    {
        //gameManager.ChangeState(GameManager.GameState.MiniGaming);
        //gameManager.ChangeScene(minigameSceneID);
        Debug.Log(minigameSceneID);
    }
}
