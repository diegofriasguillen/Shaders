using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Tetraedro3 : MonoBehaviour
{
    Matriz modelMatriz;
    float angle = 30f;
    float rad;

    public Material material;
    Vector3[] vertices = {
                            new Vector3(0, 0, 0), //0
                           new Vector3(1, 0, 0), //1
                           new Vector3(.5f, 0, .87f), //2
                           new Vector3(.5f, .82f, .29f),  //3
                                                    };

    int[] triangles = {
                        0, 1, 2,
                        0, 3, 1,
                        1, 3, 2,
                        2, 3, 0,


                                };

    Vector3 initialPosition;
    void Tetra(Vector3[] vertices)
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
        modelMatriz = new Matriz();
        initialPosition = transform.position;
        rad = angle * Mathf.Deg2Rad;
        Vector3 center = Vector3.zero;
        foreach (Vector3 vertex in vertices)
        {
            center += vertex;
        }
        center /= vertices.Length;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] -= center;
        }

        Tetra(vertices);

        StartCoroutine(TransformSequence());
    }

    IEnumerator TransformSequence()
    {
        yield return StartCoroutine(RotateXY(180.0f, 3.0f));
    }



    IEnumerator RotateXY(float targetAngle, float duration)
    {
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(targetAngle, targetAngle, 0);
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
    }

}
