using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : Interactable
{
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float movementSpeed = 0.1f;
    [SerializeField] float maxDistance = 5f;

    private Vector3 initialLocalPosition;

    void Start()
    {
        initialLocalPosition = transform.localPosition;
    }

    protected override void Update()
    {
        base.Update();

        if (isSelected && Input.GetMouseButton(0))
        {
            RotateAndMoveScrew();
        }

        CheckDistance();
    }

    void RotateAndMoveScrew()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        transform.Translate(-Vector3.forward * movementSpeed * Time.deltaTime, Space.World);
    }

    void CheckDistance()
    {
        float distance = Vector3.Distance(transform.localPosition, initialLocalPosition);

        if (distance >= maxDistance)
        {
            Destroy(this.gameObject);
        }
    }
}
