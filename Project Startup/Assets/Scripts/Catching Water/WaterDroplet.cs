using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDroplet : MonoBehaviour
{
    public float fallSpeed = 5f;

    void Update()
    {
        transform.Translate(-transform.up * fallSpeed * Time.deltaTime);

        if (transform.position.y <= -10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Bucket bucket = other.GetComponent<Bucket>();
        if (bucket != null)
        {
            Destroy(gameObject);
        }
    }
}
