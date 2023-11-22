using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] Material baseMat;
    [SerializeField] Material shineMat;

    private void OnMouseEnter()
    {
        GetComponent<MeshRenderer>().material = shineMat; 
    }

    private void OnMouseExit()
    {
        GetComponent<MeshRenderer>().material = baseMat;
    }
}
