using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool world;
    [SerializeField] float _speed = 1f;
    [SerializeField] Rigidbody _rb;

    private void FixedUpdate()
    {
        if (world == true)
        {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _speed;
            direction.y = _rb.velocity.y;
            _rb.velocity = direction;
        }
        else
        {
            Vector3 direction = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _speed);
            direction.y = _rb.velocity.y;
            _rb.velocity = direction;
        }
    }
}
