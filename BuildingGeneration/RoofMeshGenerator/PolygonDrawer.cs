using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Lcities
{
  public class PolygonDrawer : MonoBehaviour
  {
    public Material material;
    public GameObject build;
    public GameObject prefab;
    // public Transform[] vertices;
    List<Vector3> vertices = new List<Vector3>();
    private MeshRenderer mRenderer;
    private MeshFilter mFilter;

    void Start()
    {
        Invoke("Draw", 0.01f);
        // vertices = build.GetComponent<BuildingVisualizer>().nodes;
        // vertices = gameObject.GetComponent<SimpleVisualizer>().placePoints;

    }

    void Update()
    {
        // Draw();
    }

    [ContextMenu("Draw")]
    public void Draw()
    {
        vertices = build.GetComponent<BuildingVisualizer>().nodes;

        Vector2[] vertices2D = new Vector2[vertices.Count];
        Vector3[] vertices3D = new Vector3[vertices.Count];
        for (int i = 0; i < vertices.Count; i++)
        {
            Vector3 vertice = vertices[i];
            vertices2D[i] = new Vector2(vertice.x, vertice.z);
            vertices3D[i] = vertice;
        }

        Triangulator tr = new Triangulator(vertices2D);
        int[] triangles = tr.Triangulate();

        Mesh mesh = new Mesh();
        mesh.vertices = vertices3D;
        mesh.triangles = triangles;

        if (mRenderer == null)
        {
            mRenderer = gameObject.GetOrAddComponent<MeshRenderer>();
        }
        mRenderer.material = material;
        if (mFilter == null)
        {
            mFilter = gameObject.GetOrAddComponent<MeshFilter>();
        }
        mFilter.mesh = mesh;
    }
  }

}