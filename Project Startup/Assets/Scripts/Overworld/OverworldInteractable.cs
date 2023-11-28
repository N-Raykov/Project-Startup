using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldInteractable : Interactable
{
    protected GameManager gameManager;

    [SerializeField] private GameObject player;

    [SerializeField] float interactDistance = 1f;

    [SerializeField] private Color outOfRangeInteractColor = Color.white;

    [SerializeField] private Color inRangeInteractableColor = Color.yellow;

    [System.NonSerialized] public bool isClosest = false;

    protected override void Awake()
    {
        base.Awake();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    protected override void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject || isClosest == true)
        {
            isSelected = true;
        }
        else
        {
            isSelected = false;
        }

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject && CanInteract() == true)
        {
            Interact();
        }

        HightLight();
    }

    protected override void HightLight()
    {
        if (CanInteract() == true && isSelected == true)
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

    public bool CanInteract()
    {
        if (Vector3.Distance(this.gameObject.transform.position, player.transform.position) <= interactDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
