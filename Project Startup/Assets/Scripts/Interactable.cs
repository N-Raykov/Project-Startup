using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private List<Renderer> renderers;

    protected bool isSelected = false;

    private List<Material> materials;

    private Color baseHighlightColor = Color.yellow;

    protected Ray ray;
    protected RaycastHit hit;

    protected virtual void Awake()
    {
        materials = new List<Material>();

        if (renderers.Count != 0)
        {
            foreach (var renderer in renderers)
            {
                materials.AddRange(new List<Material>(renderer.materials));
            }
        }
        else
        {
            Debug.Log("The interactable: " + this.gameObject + " has no assigned renderers for highlighting");
        }
    }

    protected void ToggleHighlight(bool isOn, Color color = new Color())
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

    protected virtual void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject)
        {
            isSelected = true;
        }
        else
        {
            isSelected = false;
        }

        HightLight();
    }

    protected virtual void HightLight()
    {
        if (isSelected == true)
        {
            ToggleHighlight(true, baseHighlightColor);
        }
        else
        {
            ToggleHighlight(false);
        }
    }

    public virtual void Interact()
    {

    }
}
