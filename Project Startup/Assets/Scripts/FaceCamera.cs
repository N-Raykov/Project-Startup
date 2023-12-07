using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Camera cameraToLookAt;

    private void Start()
    {
        cameraToLookAt = Camera.main;
    }

    void Update()
    {
        Vector3 camPos = cameraToLookAt.transform.position;

        transform.LookAt(
            new Vector3(-camPos.x, camPos.y, -camPos.z)
            );

        transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }
}
