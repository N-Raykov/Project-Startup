using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 1f;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float rotationSpeed = 0.15f;


    GameObject[] interactables;
    List<GameObject> interactablesInRange;

    private void Awake()
    {
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _speed;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);
        }

        _rb.velocity = direction;
    }

    private void Update()
    {
        Controls();
    }

    void Controls()
    {
        interactablesInRange = new List<GameObject>();

        foreach (GameObject interactable in interactables)
        {
            if (interactable.GetComponent<OverworldInteractable>().CanInteract() == true)
            {
                interactablesInRange.Add(interactable);
            }
                interactable.GetComponent<OverworldInteractable>().isClosest = false;
        }

        GameObject closestInteractable = GetClosestInteractable(interactablesInRange);

        if(closestInteractable != null)
        {
            closestInteractable.GetComponent<OverworldInteractable>().isClosest = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            closestInteractable.GetComponent<OverworldInteractable>().Interact();
        }
    }

    GameObject GetClosestInteractable(List<GameObject> interactables)
    {
        GameObject closest = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject potentialTarget in interactables)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPos;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closest = potentialTarget;
                closestDistanceSqr = dSqrToTarget;   
            }
        }
        return closest;
    }
}
