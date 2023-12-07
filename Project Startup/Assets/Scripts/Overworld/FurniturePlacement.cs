using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePlacement : OverworldInteractable
{
    public GameObject furniture;
    [SerializeField] int cost;
    [SerializeField] GameObject costTextGameObject;
    private TextMesh costText;

    private void Start()
    {
        costText = costTextGameObject.GetComponent<TextMesh>();
        costText.text = cost.ToString();
    }

    protected override void Update()
    {
        base.Update();

        ShowCost();
    }

    private void ShowCost()
    {
        costTextGameObject.SetActive(isSelected);
    }

    public override void Interact()
    {
        if(gameManager.money - cost >= 0)
        {
            gameManager.PlaceFurniture(furniture.name);
            gameManager.money -= cost;
            Place();
        }
    }

    public void Place()
    {
        furniture.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
