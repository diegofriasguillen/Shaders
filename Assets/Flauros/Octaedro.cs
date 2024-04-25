using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Octaedro : MonoBehaviour
{
    public Material material;
    Vector3[] vertices = {
         

                           new Vector3(1, 0, 0), //0
                           new Vector3(0.5f, 0, 1), //1
                           new Vector3(0.5f, 1, 0.5f), //2                          
                           new Vector3(1, 1, 1.5f),  //3
                           
                           //tips
                           new Vector3(1.5f, 1, 0.5f), //4
                           new Vector3(1.5f, 0, 1)  //5
                                                    };

    int[] triangles = { 
        
                        0,2,4,
                        0,1,2,
                        1,3,2,
                        1,5,3,
                        3,5,4,
                        0,5,1,
                        0,4,5,
                        2,3,4,
                        };




    void Cube()
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
        Cube();

    }
}
