using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : OverworldInteractable
{
    [SerializeField] private int minigameSceneID;

    public string miniGameName;

    [SerializeField] private Color completedColor = Color.green;

    public bool completed { get; set; }

    public override void Interact()
    {
        if (completed == false)
        {
            gameManager.ChangeScene(minigameSceneID);
            gameManager.miniGameInProgress = miniGameName;
        }
    }

    protected override void HightLight()
    {
        if (completed == true && isSelected == true)
        {
            ToggleHighlight(true, completedColor);
        }
        else if (CanInteract() == true && isSelected == true)
        {
            ToggleHighlight(true, inRangeInteractableColor);
        }
        else if (isSelected == true)
        {
            ToggleHighlight(true, outOfRangeInteractColor);
        }
        else
        {
            ToggleHighlight(false);
        }
    }
}
