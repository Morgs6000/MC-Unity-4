using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {
    private Mesh voxelMesh;
    
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private void Awake() {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    
    private void Start() {
        ChunkGen();
    }

    private void Update() {
        
    }

    public void ChunkGen() {
        voxelMesh = new Mesh();
        voxelMesh.name = "Chunk";

        VoxelGen();
        
        MeshGen();
    }

    private void MeshGen() {
        voxelMesh.vertices = vertices.ToArray();
        voxelMesh.triangles = triangles.ToArray();

        voxelMesh.RecalculateBounds();
        voxelMesh.RecalculateNormals();
        voxelMesh.Optimize();

        meshFilter.mesh = voxelMesh;
    }

    private void VoxelGen() {
        for(int verts = 0; verts < 4; verts++) {
            vertices.Add(VoxelData.vertices[0, verts]);
        }

        for(int tris = 0; tris < 6; tris++) {
            triangles.Add(VoxelData.triangles[tris]);
        }
    }
}
