using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Tetraedro4 : MonoBehaviour
{
    Matriz modelMatriz;

    public Material material;

    Vector3[] vertices = {
                           new Vector3(0, 1.63299316f,  Mathf.Sqrt(3)*2/3),//3 d
                           new Vector3(0, Mathf.Sqrt(6)/3, Mathf.Sqrt(3)/3),//0 a
                           new Vector3(0.5f, Mathf.Sqrt(6)/3, Mathf.Sqrt(3)*2.5f/3),//1 b
                           new Vector3(0, 1.63299316f,  Mathf.Sqrt(3)*2/3),//3 d
                           new Vector3(-0.5f, Mathf.Sqrt(6)/3, Mathf.Sqrt(3)*2.5f/3),//2 c
                           new Vector3(0, 1.63299316f,  Mathf.Sqrt(3)*2/3),//3 d

                                                                    };

    int[] triangles = {
                        4,2,5,
                        1,4,3,
                        1,0,2,
                        1,2,4 //inside
                        };

    Vector2[] uvs = {

            new Vector2(0, 0),
            new Vector2(0.25f, 0.5f),
            new Vector2(0.5f, 0),
            new Vector2(0.5f, 1),
            new Vector2(0.75f, 0.5f),
            new Vector2(1, 0)

        };

    Vector3 pivot; 

    public float translationDistance; 
    public float rotationXAngle = 0.001f; 
    public float rotationYAngle = 0.003f; 
    public float rotationZAngle = 0.002f; 

    void Start()
    {
        Prisma(vertices);

        CalculatePivot();

        modelMatriz = GetComponent<Matriz>();

        StartCoroutine(AnimationCoroutine());
    }

    IEnumerator AnimationCoroutine()
    {
        yield return Move(translationDistance, 2f);
        yield return new WaitForSeconds(2f);

        //x
        yield return RotateX(rotationXAngle, 0.05f);
        yield return new WaitForSeconds(2f);

        //y
        yield return RotateY(rotationYAngle, 0.05f);
        yield return new WaitForSeconds(2f);

        //z
        yield return RotateZ(rotationZAngle, 0.05f);
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
                vertices[i] = modelMatriz.Traslation(vertices[i], new Vector4(distance * t, 0, 0, 0));
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
                vertices[i] = modelMatriz.RotX(vertices[i], angle * t);
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
                vertices[i] = modelMatriz.RotY(vertices[i], angle * t);
            }
            UpdateMesh();
            yield return null;
        }
    }

    IEnumerator RotateZ(float angle, float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = modelMatriz.RotZ(vertices[i], angle * t);
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