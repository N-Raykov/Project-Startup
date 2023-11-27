using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private List<Renderer> renderers;

    [SerializeField] private Color outOfRangeInteractColor = Color.white;

    [SerializeField] private Color inRangeInteractableColor = Color.yellow;

    [SerializeField] private GameObject player;

    [SerializeField] float interactDistance = 1f;

    protected GameManager gameManager;

    [System.NonSerialized] public bool isClosest = false;

    private bool isSelected = false;

    private List<Material> materials;

    Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        materials = new List<Material>();

        foreach (var renderer in renderers)
        {
            materials.AddRange(new List<Material>(renderer.materials));
        }
    }

    public void ToggleHighlight(bool isOn, Color color = new Color())
    {
        if (isOn)
        {
            foreach (var material in materials)
            {
                material.EnableKeyword("_EMISSION");

                material.SetColor("_EmissionColor", color);
            }
        }
        else
        {
            foreach (var material in materials)
            {
                material.DisableKeyword("_EMISSION");
            }
        }
    }

    private void Update()
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

    void HightLight()
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

    public virtual void Interact()
    {

    }
}
