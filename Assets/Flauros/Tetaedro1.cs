using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Tetaedro1 : MonoBehaviour
{

    Matriz modelMatriz;

    public Material material;

    Vector3[] vertices = {
        new Vector3(0, Mathf.Sqrt(6) / 3, Mathf.Sqrt(3) / 3),  // 0
        new Vector3(0, 0, 0),                                   // 1a
        new Vector3(0.5f, 0, Mathf.Sqrt(3) / 2),                // 2b
        new Vector3(0, Mathf.Sqrt(6) / 3, Mathf.Sqrt(3) / 3),  // 3
        new Vector3(-0.5f, 0, Mathf.Sqrt(3) / 2),               // 4c
        new Vector3(0, Mathf.Sqrt(6) / 3, Mathf.Sqrt(3) / 3)   // 5
    };

    int[] triangles = {
        4, 2, 5,  //inside
        1, 4, 3,
        1, 0, 2,
        1, 2, 4
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
    public float rotationXAngle; 
    public float rotationYAngle; 
    public float rotationZAngle; 

    void Start()
    {
        Prisma(vertices);

        CalculatePivot();

        modelMatriz = GetComponent<Matriz>();

        StartCoroutine(AnimationCoroutine());
    }

    IEnumerator AnimationCoroutine()
    {
        //traslation
        yield return Move(translationDistance, 2f);
        yield return new WaitForSeconds(2f);

        //X
        yield return RotateX(rotationXAngle, .05f);
        yield return new WaitForSeconds(2f);

        //Y
        yield return RotateY(rotationYAngle, .05f);
        yield return new WaitForSeconds(2f);

        // Z
        yield return RotateZ(rotationZAngle, .05f);
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
