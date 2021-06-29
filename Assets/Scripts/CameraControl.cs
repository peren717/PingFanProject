using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float cameraSpeed = 1f;
    void Start()
    {
    }

    void Update()
    {
        transform.position += new Vector3(0, 0, cameraSpeed * Time.deltaTime);
    }
}
