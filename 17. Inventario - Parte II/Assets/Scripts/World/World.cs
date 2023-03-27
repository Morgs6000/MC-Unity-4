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
    private bool playerSpawn;
    private int viewDistance = 5;

    private GameObject mainCamera;

    private void Awake() {
        mainCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("Player").transform;
    }

    private void Start() {
        player.gameObject.SetActive(false);
        mainCamera.SetActive(true);

        //StartCoroutine(WorldGen());
    }

    private void Update() {
        StartCoroutine(LoadingChunks());
    }

    private IEnumerator LoadingChunks() {
        int posX = Mathf.FloorToInt(player.transform.position.x / Chunk.ChunkSizeInVoxels.x);
        int posZ = Mathf.FloorToInt(player.transform.position.z / Chunk.ChunkSizeInVoxels.z);

        for(int x = -viewDistance; x < viewDistance; x++) {
            for(int z = -viewDistance; z < viewDistance; z++) {

                for(int y = 0; y < WorldSizeInChunks.y; y++) {
                    InstantiateChunk(new Vector3(x + posX, y,  z + posZ));
                }

                yield return null;
            }
        }

        if(!playerSpawn) {
            SetPlayerSpawn();
        }        
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
        playerSpawn = true;

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

        Chunk c = Chunk.GetChunk(new Vector3(
            Mathf.FloorToInt(chunkOffset.x),
            Mathf.FloorToInt(chunkOffset.y),
            Mathf.FloorToInt(chunkOffset.z)
        ));

        if(c == null) {
            GameObject chunkObject = Instantiate(chunkPrefab);
            chunkObject.transform.SetParent(transform);
            
            chunkObject.transform.position = chunkOffset;
            chunkObject.name = "Chunk: " + x + ", " + z;

            chunk = chunkObject.GetComponent<Chunk>();
            
            VoxelMapGen();
        }
    }

    private void VoxelMapGen() {
        for(int x = 0; x < Chunk.ChunkSizeInVoxels.x; x++) {
            for(int y = 0; y < Chunk.ChunkSizeInVoxels.y; y++) {
                for(int z = 0; z < Chunk.ChunkSizeInVoxels.z; z++) {
                    LayerGen(new Vector3(x, y, z));
                    TreeGen(new Vector3(x, y, z));
                }
            }
        }

        chunk.ChunkGen();
    }

    private void LayerGen(Vector3 offset) {
        int x = (int)offset.x;
        int y = (int)offset.y;
        int z = (int)offset.z;

        float _x = x + chunk.transform.position.x;
        float _y = y + chunk.transform.position.y;
        float _z = z + chunk.transform.position.z;

        _x += WorldSizeInVoxels.x;
        //_y += WorldSizeInVoxels.y;
        _z += WorldSizeInVoxels.z;
        
        // BEDROCK LAYER
        if(_y == 0) {
            chunk.voxelMap[x, y, z] = EnumVoxels.bedrock;
        }

        int noise = Noise.Perlin(_x, _z);

        // STONE LAYER
        if(_y < noise - 4) {
            chunk.voxelMap[x, y, z] = EnumVoxels.stone;
        }

        // DIRT LAYER
        else if(_y < noise) {
            chunk.voxelMap[x, y, z] = EnumVoxels.dirt;
        }

        // GRASS LAYER
        else if(_y == noise) {
            chunk.voxelMap[x, y, z] = EnumVoxels.grass;
        }
    }

    private void TreeGen(Vector3 offset) {
        int x = (int)offset.x;
        int y = (int)offset.y;
        int z = (int)offset.z;

        float _x = x + chunk.transform.position.x;
        float _y = y + chunk.transform.position.y;
        float _z = z + chunk.transform.position.z;

        _x += WorldSizeInVoxels.x;
        //_y += WorldSizeInVoxels.y;
        _z += WorldSizeInVoxels.z;

        int noise = Noise.Perlin(_x, _z);

        if(
            Random.Range(0, 100) < 1 &&
            _y == noise + 1
        ) {   
            //int leavesWidth = 5;
            int leavesHeight = Random.Range(3, 5);

            int iter = 0;
            
            for(int yL = y + 0; yL < y + leavesHeight; yL++) {
                for(int xL = x - 2 + iter / 2; xL <  x + 3 - iter / 2; xL++) {                
                    for(int zL = z - 2 + iter / 2; zL <  z + 3 - iter / 2; zL++) {
                        if(
                            xL >= 0 && xL < Chunk.ChunkSizeInVoxels.x &&
                            yL >= 0 && yL < Chunk.ChunkSizeInVoxels.y &&
                            zL >= 0 && zL < Chunk.ChunkSizeInVoxels.z
                        ) {
                            chunk.voxelMap[xL, yL + 3, zL] = EnumVoxels.leaves;
                        } 
                    }                   
                }

                iter++;
            }

            int treeHeight = Random.Range(3, 5);

            for(int i = 0; i < treeHeight; i++) {
                if(y + i < Chunk.ChunkSizeInVoxels.y) {
                    chunk.voxelMap[x, y + i, z] = EnumVoxels.log;
                }                
            }
        }
    }
}
