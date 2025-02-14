using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CustomPlane : MonoBehaviour
{
    public int gridSize = 10; // 10x10 grid = 100 vertices
    public Material material;

    private void Start()
    {
        GenerateMesh();
        GetComponent<MeshRenderer>().material = material;
    }

    private void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        // get shader from material
        Shader shader = material.shader;

        // get _WaveAmplitude property from shader
        int waveAmplitudeID = Shader.PropertyToID("Wave Amplitude");

        // set to a random value
        material.SetFloat(waveAmplitudeID, Random.Range(0.1f, 1.0f));
        

        // Vertices
        Vector3[] vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];
        for (int i = 0, y = 0; y <= gridSize; y++)
        {
            for (int x = 0; x <= gridSize; x++, i++)
            {
                vertices[i] = new Vector3(x, 0, y);
            }
        }

        // Triangles
        int[] triangles = new int[gridSize * gridSize * 6];
        for (int ti = 0, vi = 0, y = 0; y < gridSize; y++, vi++)
        {
            for (int x = 0; x < gridSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 1] = vi + gridSize + 1;
                triangles[ti + 2] = vi + 1;
                triangles[ti + 3] = vi + 1;
                triangles[ti + 4] = vi + gridSize + 1;
                triangles[ti + 5] = vi + gridSize + 2;
            }
        }

        // UVs
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0, y = 0; y <= gridSize; y++)
        {
            for (int x = 0; x <= gridSize; x++, i++)
            {
                uvs[i] = new Vector2((float)x / gridSize, (float)y / gridSize);
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}
