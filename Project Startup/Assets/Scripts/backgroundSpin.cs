using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundSpin : MonoBehaviour
{
    public float rotationSpeed = 30f;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 55f, -45f);
    }
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
