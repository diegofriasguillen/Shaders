using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Tetraedro3 : MonoBehaviour
{
    public Material material;
    Vector3[] vertices = {
                            new Vector3(0.5f, 0, 1), //0
                           new Vector3(1, 0, 2), //1
                           new Vector3(1.5f, 0, 1), //2
                           new Vector3(1, 1, 1.5f),  //3
                                                    };

    int[] triangles = { 
                        0, 2, 1,
                        0, 1, 3,
                        1, 2, 3,
                        2, 0, 3,


                                };
    void Tetra()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.Optimize();
        mesh.RecalculateNormals();
    }

    private void Start()
    {

        Tetra();
    }
}
