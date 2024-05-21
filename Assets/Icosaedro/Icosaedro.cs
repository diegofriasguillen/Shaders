using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Icosaedro : MonoBehaviour
{
    //[SerializeField]
    private Vector3 rotationSpeed = new Vector3(0, -90, 0); 

    //[SerializeField]
    private Vector3 rotationSpeed1 = new Vector3(0, -90, 0); 

    //[SerializeField]
    private Vector3 rotationSpeed2 = new Vector3(0, -90, 0); 

    public float speedX;
    private float speedy;
    private float speedz;

    private float a = 0.364f;
    private float b = 0.315f;

    public Material material;

    Vector3[] vertices = {
                           new Vector3(0f, 1f, 0f),
                           new Vector3(-0.7236f, 0.44721f, -0.52572f),
                           new Vector3(0.27639f, 0.44721f, -0.85064f),
                           new Vector3(0f, 1f, 0f),
                           new Vector3(0.89442f, 0.44721f, 0f),
                           new Vector3(0f, 1f, 0f),
                           new Vector3(0.27639f, 0.44721f, 0.85064f),
                           new Vector3(0f, 1f, 0f),
                           new Vector3(-0.7236f, 0.44721f, 0.52572f),
                           new Vector3(0f, 1f, 0f),
                           new Vector3(-0.7236f, 0.44721f, -0.52572f),
                           new Vector3(-0.27639f, -0.44721f, -0.85064f),
                           new Vector3(0f, -1f, 0f),
                           new Vector3(-0.89442f, -0.44721f, 0f),
                           new Vector3(0f, -1f, 0f),
                           new Vector3(-0.27639f, -0.44721f, 0.85064f),
                           new Vector3(0f, -1f, 0f),
                           new Vector3(0.7236f, -0.44721f, 0.52572f),
                           new Vector3(0f, -1f, 0f),
                           new Vector3(0.7236f, -0.44721f, -0.52572f),
                           new Vector3(0f, -1f, 0f),
                           new Vector3(-0.27639f, -0.44721f, -0.85064f)
                         };
    int[] triangles = {

                        //Top
                        0, 2, 1,//0
                        2, 3, 4,//1
                        5, 6, 4,//2
                        7, 8, 6,//3 
                        9,10, 8,//4

                        //Mid
                        10, 11, 13,//5
                        10,13, 8,//6
                        8, 13, 15,//7
                        8, 15, 6,//8
                        6, 15, 17,//9
                        6, 17, 4,//10 
                        19, 4, 17,//11 
                        2, 4, 19,//12 
                        2, 19, 21,//13
                        2, 21, 1,//14

                        //Button
                        11, 12, 13,//15
                        13, 14, 15,//16
                        15, 16, 17,//17
                        17, 18, 19,//18
                        19, 20, 21//19
                      };
    Vector2[] uvs; 

    void Start()
    {
        InitializeUVs();

        Icosaedro_(vertices);

        StartCoroutine(UpdateCube());

        rotate();

    }
    

    IEnumerator UpdateCube()
    {
        while (true) 
        {
            
            InitializeUVs();
            Icosaedro_(vertices);
            UpdateMesh();
            rotate();
            yield return null;
        }
    }

    void Icosaedro_(Vector3[] vertices)
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
        Icosaedro_(vertices);

    }
    void InitializeUVs()
    {
        uvs = new Vector2[]
        {
            new Vector2(0.091f, 0.472f),
            new Vector2(0f, 0.315f),
            new Vector2(0.182f, 0.315f),
            new Vector2(0.273f, 0.472f),
            new Vector2(a, b), 
            new Vector2(0.455f, 0.472f),
            new Vector2(0.545f, 0.315f),
            new Vector2(0.636f, 0.472f),
            new Vector2(0.727f, 0.315f),
            new Vector2(0.818f, 0.472f),
            new Vector2(0.909f, 0.315f),
            new Vector2(1f, 0.157f),
            new Vector2(0.909f, 0f),
            new Vector2(0.818f, 0.157f),
            new Vector2(0.727f, 0f),
            new Vector2(0.636f, 0.157f),
            new Vector2(0.545f, 0f),
            new Vector2(0.455f, 0.157f),
            new Vector2(0.364f, 0f),
            new Vector2(0.273f, 0.157f),
            new Vector2(0.182f, 0f),
            new Vector2(0.091f, 0.157f),
        };
    }

    public void rotate()
    {

        transform.Rotate(rotationSpeed * speedX);
        transform.Rotate(rotationSpeed1 * speedy);
        transform.Rotate(rotationSpeed2 * speedz);

    }

}


