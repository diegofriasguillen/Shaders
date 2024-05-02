using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Octaedro : MonoBehaviour
{
    Matriz modelMatriz;
    float angle = 30f;
    float rad;

    public Material material;
    Vector3[] vertices = {


                           new Vector3(1, 0, 0), //0
                           new Vector3(0.5f, 0, .87f), //1
                           new Vector3(0.5f, .82f, 0.29f), //2                          
                           new Vector3(1, .82f, 1.15f),  //3
                           
                           //tips
                           new Vector3(1.5f, .82f, 0.29f), //4
                           new Vector3(1.5f, 0, .87f)  //5
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



    Vector3 initialPosition;
    void Octa(Vector3[] vertices)
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
        rad = angle * Mathf.Deg2Rad;

        initialPosition = transform.position;

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

        Octa(vertices);

        StartCoroutine(TransformSequence(3.0f));
    }

    IEnumerator TransformSequence(float delay)
    {
        yield return new WaitForSeconds(delay);

        yield return StartCoroutine(TranslateZX(0, 3.0f));


    }

    IEnumerator TranslateZX(float amount, float duration)
    {
        Vector3 targetPosition = initialPosition + new Vector3(amount, 0, amount);
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }



}
