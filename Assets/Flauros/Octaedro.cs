using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Octaedro : MonoBehaviour
{
    Matriz matriz;

    public Material material;

    Vector3[] vertices = {
                           //base
                           new Vector3(0, Mathf.Sqrt(6)/3, Mathf.Sqrt(3)/3),//0
                           new Vector3(0.5f, Mathf.Sqrt(6)/3, Mathf.Sqrt(3)*2.5f/3),//1
                           new Vector3(-0.5f, Mathf.Sqrt(6)/3, Mathf.Sqrt(3)*2.5f/3),//2                          
                           new Vector3(0, 0, Mathf.Sqrt(3)),//3                          
                           new Vector3(-0.5f, 0, Mathf.Sqrt(3)/2),//4
                           new Vector3(0, Mathf.Sqrt(6)/3, Mathf.Sqrt(3)/3),//5
                           new Vector3(0.5f, 0, Mathf.Sqrt(3)/2),//6
                           new Vector3(0, 0, Mathf.Sqrt(3)),//7
                           new Vector3(0.5f, Mathf.Sqrt(6)/3, Mathf.Sqrt(3)*2.5f/3),//8
                           new Vector3(0, Mathf.Sqrt(6)/3, Mathf.Sqrt(3)/3),//9

                                                            };

    int[] triangles = { 
                        //Octaedro
                        0,2,1,
                        1,2,3,
                        2,5,4,
                        4,3,2,
                        5,6,4,
                        6,7,4,
                        6,8,7,
                        6,9,8
                        };
    Vector2[] uvs = {

            new Vector2(0, 1),//0   
            new Vector2(0, 0.340f),//2
            new Vector2(0.243f, 0.662f),//1
            new Vector2(0.244f, 0),//3
            new Vector2(0.5f, 0.34f),//4
            new Vector2(0.5f, 1),//5
            new Vector2(0.75f, 0.66f),//6
            new Vector2(0.74f, 0),//7         
            new Vector2(1, 0.334f),//8
            new Vector2(1, 1),//9
            
        };

    Vector3 pivot; 

    public float translationDistance; 
    public float rotationXAngle; 
    public float rotationYAngle;
    public float rotationZAngle; 

    void Start()
    {
        Prisma(vertices);

        CalculatePivot();

        matriz = GetComponent<Matriz>();

        StartCoroutine(AnimationCoroutine());
    }

    IEnumerator AnimationCoroutine()
    {
        //traslation
        yield return Move(translationDistance, 2f);
        yield return new WaitForSeconds(2f);

        //X
        yield return RotateX(rotationXAngle, 0.05f);
        yield return new WaitForSeconds(2f);

        //Y
        yield return RotateY(rotationYAngle, 0.05f);
        yield return new WaitForSeconds(2f);


    }

    void CalculatePivot()
    {
        pivot = Vector3.zero;
        foreach (Vector3 vertex in vertices)
        {
            pivot += vertex;
        }
        pivot /= vertices.Length;
    }

    IEnumerator Move(float distance, float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = matriz.Traslation(vertices[i], new Vector4(distance * t, 0, 0, 0));
            }
            UpdateMesh();
            yield return null;
        }
    }

    IEnumerator RotateX(float angle, float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = matriz.RotX(vertices[i], angle * t);
            }
            UpdateMesh();
            yield return null;
        }
    }

    IEnumerator RotateY(float angle, float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = matriz.RotY(vertices[i], angle * t);
            }
            UpdateMesh();
            yield return null;
        }
    }

    void Prisma(Vector3[] vertices)
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.Optimize();
        mesh.RecalculateNormals();
    }

    void UpdateMesh()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        Prisma(vertices);
    }

}
