using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SC_target_mesh3 : MonoBehaviour
{
    [SerializeField]
    private Vector2Int size = new Vector2Int(5, 5);
    private MeshFilter meshFilter;
    private Vector3[] vertices;
    private int[] indices;
    private Vector2[] uv;
    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        GenerateMesh();
    }


    private void GenerateMesh()
    {
        vertices = new Vector3[(size.x + 1) * (size.y + 1)];
        uv = new Vector2[vertices.Length];
        for (int y = 0; y <= size.y; ++y)
        {
            for (int x = 0; x <= size.x; ++x)
            {
                int index = y * (size.x + 1) + x;
                vertices[index] = new Vector3(x, y);
                uv[index] = new Vector2((float)x / size.x, (float)y / size.y);
            }
        }
        indices = new int[size.x * size.y * 6];
        int indicesIndex = 0;
        int verticesIndex = 0;
        for (int y = 0; y < size.y; ++y)
        {
            for (int x = 0; x < size.x; ++x)
            {
                indices[indicesIndex] = verticesIndex;
                indices[indicesIndex + 1] = verticesIndex + size.x + 1;
                indices[indicesIndex + 2] = verticesIndex + 1;
                indices[indicesIndex + 3] = verticesIndex + 1;
                indices[indicesIndex + 4] = verticesIndex + size.x + 1;
                indices[indicesIndex + 5] = verticesIndex + size.x + 2;
                indicesIndex += 6;
                verticesIndex++;
            }
            verticesIndex++;
        }

        Mesh mesh = new Mesh();
        mesh.name = "PrimitivePlane";
        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.uv = uv;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }


}
