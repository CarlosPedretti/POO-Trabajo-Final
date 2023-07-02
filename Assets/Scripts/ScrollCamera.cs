using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamera : MonoBehaviour
{

    public float cameraSpeed;

    void Update()
    {
        transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
    }
}
