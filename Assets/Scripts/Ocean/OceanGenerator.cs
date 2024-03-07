using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OceanGenerator : MonoBehaviour {
    [SerializeField] int tileSize;
    [SerializeField] int quadAmount;
    [SerializeField] Material material;

    GameObject tileObject;

    void Start() {
        GenerateTile();
    }

    void GenerateTile() {
        tileObject = new GameObject("OceanTile");

        Mesh mesh = GenerateOceanMesh();
        
        MeshFilter mf = tileObject.AddComponent<MeshFilter>();
        mf.mesh = mesh;
        MeshRenderer mr = tileObject.AddComponent<MeshRenderer>();
        mr.material = material;
    }

    Mesh GenerateOceanMesh() {
        List<Vector3> verts = new();
        List<int> tris = new();

        float qs = (float)tileSize / quadAmount;

        for (int y = 0; y < quadAmount; y++) {
            for (int x = 0; x < quadAmount; x++) {
                verts.AddRange(new Vector3[] {
                    new(x * qs, 0, y * qs),
                    new(x * qs, 0, y * qs + qs),
                    new(x * qs + qs, 0, y * qs),
                    new(x * qs + qs, 0, y * qs + qs),
                    new(x * qs + qs, 0, y * qs),
                    new(x * qs, 0, y * qs + qs),
                });
                for (int i = 0; i < 6; i++) {
                    tris.Add(tris.Count);
                }
            }
        }

        Mesh mesh = new();
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }
}
