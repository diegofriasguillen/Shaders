using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(50f, 50f, 50f); 

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
