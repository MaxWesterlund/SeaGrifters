using System;
using System.Collections.Generic;
using UnityEngine;

public class OceanGenerator : MonoBehaviour {
    [SerializeField] int tileSize;
    [SerializeField] int seabedResolution;
    [SerializeField] float seabedHeightResolution;
    [SerializeField] float seabedHeight;
    [SerializeField] float oceanDepth; 
    [SerializeField] Material oceanMaterial;
    [SerializeField] Material seabedMaterial;

    GameObject oceanSurface;
    GameObject seabed;

    float seed;

    void Start() {
        oceanSurface = new("OceanSurface");
        oceanSurface.AddComponent<MeshFilter>().mesh = new();
        oceanSurface.AddComponent<MeshRenderer>().material = oceanMaterial;

        seabed = new("Seabed");
        seabed.AddComponent<MeshFilter>().mesh = new();
        seabed.AddComponent<MeshRenderer>().material = seabedMaterial;

        seed = UnityEngine.Random.Range(-1000f, 1000f);

        UpdateTile();
    }

    void UpdateTile() {
        UpdateOceanMesh(oceanSurface.GetComponent<MeshFilter>().mesh);
        UpdateSeabedMesh(seabed.GetComponent<MeshFilter>().mesh);
    }

    void UpdateOceanMesh(Mesh mesh) {
        int s = tileSize / 2;
        float h = seabedHeight + oceanDepth;

        mesh.vertices = new Vector3[] {
            new(-s, h, -s),
            new(-s, h, s),
            new(s, h, -s),
            new(s, h, s),
            new(s, h, -s),
            new(-s, h, s)
        };
        mesh.triangles = new int[] { 0, 1, 2, 3, 4, 5 };
        mesh.uv = new Vector2[] {
            new(-s, -s),
            new(-s, s),
            new(s, -s),
            new(s, s),
            new(s, -s),
            new(-s, s)
        };

        mesh.RecalculateNormals();
    }

    void UpdateSeabedMesh(Mesh mesh) {
        List<Vector3> verts = new();
        List<int> tris = new();

        float qs = (float)tileSize / seabedResolution;
        Vector3 off = new(tileSize / 2, 0, tileSize / 2);

        for (int y = 0; y < seabedResolution; y++) {
            for (int x = 0; x < seabedResolution; x++) {
                float val = Mathf.PerlinNoise(x + seed, y + seed);
                float h = val * seabedHeight;
                
                verts.AddRange(new Vector3[] {
                    new Vector3(x * qs, GetHeight(x, y), y * qs) - off,
                    new Vector3(x * qs, GetHeight(x, y + 1), y * qs + qs) - off,
                    new Vector3(x * qs + qs, GetHeight(x + 1, y), y * qs) - off,
                    new Vector3(x * qs + qs, GetHeight(x + 1, y + 1), y * qs + qs) - off,
                    new Vector3(x * qs + qs, GetHeight(x + 1, y), y * qs) - off,
                    new Vector3(x * qs, GetHeight(x, y + 1), y * qs + qs) - off,
                });
                for (int i = 0; i < 6; i++) {
                    tris.Add(tris.Count);
                }
            }
        }

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();

        mesh.RecalculateNormals();
    }

    float GetHeight(float x, float y) {
        float val = Mathf.PerlinNoise(x * seabedHeightResolution + seed, y * seabedHeightResolution + seed);
        return val * seabedHeight;
    }
}
