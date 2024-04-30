using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMatrices : MonoBehaviour
{
    Matriz modelMatriz = new Matriz();

    public GameObject cube;
    void Start()
    {
        
    }
    //private void FixedUpdate()
    //{
    //    cube.transform.position = modelMatriz.Traslation(cube.transform.position, new Vector3(0.00f, 0, 0.02f));

    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.position = modelMatriz.RotZ(this.transform.position, 30);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            this.transform.position = modelMatriz.RotX(this.transform.position, 30);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.position = modelMatriz.RotY(this.transform.position, 30);
        }

    }
}
