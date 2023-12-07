using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldInteractable : Interactable
{
    protected GameManager gameManager;

    private GameObject player;

    [SerializeField] float interactDistance = 1f;

    [SerializeField] protected Color outOfRangeInteractColor = Color.white;

    [SerializeField] protected Color inRangeInteractableColor = Color.yellow;

    [System.NonSerialized] public bool isClosest = false;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player");
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
