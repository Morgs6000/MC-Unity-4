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
    private MeshCollider meshCollider;

    public static Vector3 ChunkSizeInVoxels = new Vector3(16, 256, 16);

    public EnumVoxels[,,] voxelMap = new EnumVoxels[(int)ChunkSizeInVoxels.x, (int)ChunkSizeInVoxels.y, (int)ChunkSizeInVoxels.z];

    private EnumVoxels voxelID;

    private static List<Chunk> chunkData = new List<Chunk>();

    private void Awake() {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
    }
    
    private void Start() {
        chunkData.Add(this);
    }

    private void Update() {
        
    }

    public EnumVoxels GetVoxel(Vector3 worldPos) {
        Vector3 localPos = worldPos - transform.position;

        int x = Mathf.FloorToInt(localPos.x);
        int y = Mathf.FloorToInt(localPos.y);
        int z = Mathf.FloorToInt(localPos.z);

        if(
            x < 0 || x >= ChunkSizeInVoxels.x ||
            y < 0 || y >= ChunkSizeInVoxels.y ||
            z < 0 || z >= ChunkSizeInVoxels.z
        ) {
            Debug.LogError("Coordinates out of range");

            return default(EnumVoxels);
        }

        return voxelMap[x, y, z];
    }

    public void SetVoxel(Vector3 worldPos, EnumVoxels voxelID) {
        Vector3 localPos = worldPos - transform.position;

        int x = Mathf.FloorToInt(localPos.x);
        int y = Mathf.FloorToInt(localPos.y);
        int z = Mathf.FloorToInt(localPos.z);

        voxelMap[x, y, z] = voxelID;

        ChunkGen();
    }

    public static Chunk GetChunk(Vector3 pos) {        
        for(int i = 0; i < chunkData.Count; i++) {            
            Vector3 chunkPos = chunkData[i].transform.position;

            if(
                pos.x < chunkPos.x || pos.x >= chunkPos.x + ChunkSizeInVoxels.x || 
                pos.y < chunkPos.y || pos.y >= chunkPos.y + ChunkSizeInVoxels.y || 
                pos.z < chunkPos.z || pos.z >= chunkPos.z + ChunkSizeInVoxels.z
            ) {
                continue;
            }

            return chunkData[i];
        }

        return null;
    }

    public void ChunkGen() {
        voxelMesh = new Mesh();
        voxelMesh.name = "Chunk";

        vertices.Clear();
        triangles.Clear();
        vertexIndex = 0;

        uv.Clear();

        for(int x = 0; x < ChunkSizeInVoxels.x; x++) {
            for(int y = 0; y < ChunkSizeInVoxels.y; y++) {
                for(int z = 0; z < ChunkSizeInVoxels.z; z++) {
                    if(voxelMap[x, y, z] != EnumVoxels.air) {
                        VoxelGen(new Vector3(x, y, z));
                    }
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
        meshCollider.sharedMesh = voxelMesh;
    }

    private void VoxelGen(Vector3 offset) {
        int x = (int)offset.x;
        int y = (int)offset.y;
        int z = (int)offset.z;

        voxelID = voxelMap[x, y, z];
        
        for(int voxelSide = 0; voxelSide < 6; voxelSide++) {
            if(!HasSolidNeighbor(VoxelData.voxelSide[voxelSide] + offset)) {
                for(int verts = 0; verts < 4; verts++) {
                    vertices.Add(VoxelData.vertices[voxelSide, verts] + offset);

                    uv.Add(VoxelData.uv(VoxelUV.GetUV(voxelID, voxelSide))[verts]);
                }

                for(int tris = 0; tris < 6; tris++) {
                    triangles.Add(VoxelData.triangles[tris] + vertexIndex);
                }

                vertexIndex += 4;
            }
        }
    }
    
    bool HasSolidNeighbor(Vector3 offset) {
        int x = (int)offset.x;
        int y = (int)offset.y;
        int z = (int)offset.z;

        if(
            x < 0 || x > ChunkSizeInVoxels.x - 1 ||
            y < 0 || y > ChunkSizeInVoxels.y - 1 ||
            z < 0 || z > ChunkSizeInVoxels.z - 1
        ) {
            return false;
        }
        if(voxelMap[x, y, z] == EnumVoxels.air) {
            return false;
        }

        return true;
    }
}