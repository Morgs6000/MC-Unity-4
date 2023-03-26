using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    [SerializeField] private GameObject chunkPrefab;
    private Chunk chunk;

    public static Vector3 WorldSizeInVoxels = new Vector3(32, 256, 32);

    private Vector3 WorldSizeInChunks = new Vector3(
        WorldSizeInVoxels.x / Chunk.ChunkSizeInVoxels.x,
        WorldSizeInVoxels.y / Chunk.ChunkSizeInVoxels.y,
        WorldSizeInVoxels.z / Chunk.ChunkSizeInVoxels.z
    );

    private Transform player;

    private GameObject mainCamera;

    private void Awake() {
        mainCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("Player").transform;
    }

    private void Start() {
        player.gameObject.SetActive(false);
        mainCamera.SetActive(true);

        StartCoroutine(WorldGen());
    }

    private void Update() {
        
    }

    private IEnumerator WorldGen() {
        Vector3 WorldSize = new Vector3(
            WorldSizeInChunks.x / 2,
            WorldSizeInChunks.y,            
            WorldSizeInChunks.z / 2            
        );

        for(int x = -(int)WorldSize.x; x < WorldSize.x; x++) {
            for(int z = -(int)WorldSize.z; z < WorldSize.z; z++) {

                for(int y = 0; y < WorldSize.y; y++) {
                    InstantiateChunk(new Vector3(x, y, z));
                }

                yield return null;
            }
        }

        SetPlayerSpawn();
    }

    private void SetPlayerSpawn() {
        Vector3 spawnPosition = new Vector3(
            0,
            WorldSizeInVoxels.y,
            0
        );

        player.position = spawnPosition;

        player.gameObject.SetActive(true);
        mainCamera.SetActive(false);
    }

    private void InstantiateChunk(Vector3 chunkPos) {
        int x = (int)chunkPos.x;
        int y = (int)chunkPos.y;
        int z = (int)chunkPos.z;
        
        Vector3 chunkOffset = new Vector3(
            x * Chunk.ChunkSizeInVoxels.x,
            y * Chunk.ChunkSizeInVoxels.y,
            z * Chunk.ChunkSizeInVoxels.z
        );

        GameObject chunkObject = Instantiate(chunkPrefab);
        chunkObject.transform.SetParent(transform);
        
        chunkObject.transform.position = chunkOffset;
        chunkObject.name = "Chunk: " + x + ", " + z;

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
