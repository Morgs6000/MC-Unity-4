using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    [SerializeField] private GameObject chunkPrefab;
    private Chunk chunk;

    private void Start() {
        InstantiateChunk();
    }

    private void Update() {
        
    }

    private void InstantiateChunk() {
        GameObject chunkObject = Instantiate(chunkPrefab);
        chunkObject.transform.SetParent(transform);

        chunk = chunkObject.GetComponent<Chunk>();
        
        VoxelMapGen();
    }

    private void VoxelMapGen() {
        for(int x = 0; x < Chunk.ChunkSizeInVoxels.x; x++) {
            for(int y = 0; y < Chunk.ChunkSizeInVoxels.y; y++) {
                for(int z = 0; z < Chunk.ChunkSizeInVoxels.z; z++) {
                    LayerGen(new Vector3(x, y, z));
                }
            }
        }

        chunk.ChunkGen();
    }

    private void LayerGen(Vector3 offset) {
        int x = (int)offset.x;
        int y = (int)offset.y;
        int z = (int)offset.z;

        // STONE LAYER
        if(y < 64) {
            chunk.voxelMap[x, y, z] = EnumVoxels.stone;
        }

        // GRASS LAYER
        if(y == 64) {
            chunk.voxelMap[x, y, z] = EnumVoxels.grass;
        }
    }
}
