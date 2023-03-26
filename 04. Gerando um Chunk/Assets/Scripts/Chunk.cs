using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {
    private Mesh voxelMesh;
    
    private List<Vector3> vertices = new List<Vector3>();    
    private List<int> triangles = new List<int>();
    private int vertexIndex;

    private List<Vector2> uv = new List<Vector2>();

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    public static Vector3 ChunkSizeInVoxels = new Vector3(16, 16, 16);

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

        for(int x = 0; x < ChunkSizeInVoxels.x; x++) {
            for(int y = 0; y < ChunkSizeInVoxels.y; y++) {
                for(int z = 0; z < ChunkSizeInVoxels.z; z++) {
                    VoxelGen(new Vector3(x, y, z));
                }
            }
        }
        
        MeshGen();
    }

    private void MeshGen() {
        voxelMesh.vertices = vertices.ToArray();
        voxelMesh.triangles = triangles.ToArray();
        voxelMesh.uv = uv.ToArray();

        voxelMesh.RecalculateBounds();
        voxelMesh.RecalculateNormals();
        voxelMesh.Optimize();

        meshFilter.mesh = voxelMesh;
    }

    private void VoxelGen(Vector3 offset) {
        for(int voxelSide = 0; voxelSide < 6; voxelSide++) {
            for(int verts = 0; verts < 4; verts++) {
                vertices.Add(VoxelData.vertices[voxelSide, verts] + offset);

                uv.Add(VoxelData.uv(new Vector2(1, 0))[verts]);
            }

            for(int tris = 0; tris < 6; tris++) {
                triangles.Add(VoxelData.triangles[tris] + vertexIndex);
            }

            vertexIndex += 4;
        }
    }
}
