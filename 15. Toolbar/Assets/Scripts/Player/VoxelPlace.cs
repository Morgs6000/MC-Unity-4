using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelPlace : MonoBehaviour {
    private Camera cam;
    private float rangeHit = 5.0f;
    private LayerMask groundMask;

    private Transform player;

    [SerializeField] private Toolbar toolbar;

    private void Awake() {
        cam = GetComponentInChildren<Camera>();
        groundMask = LayerMask.GetMask("Ground");

        player = GetComponent<Transform>();
    }
    
    private void Start() {
        
    }

    private void Update() {
        RaycastUpdate();
    }

    private void RaycastUpdate() {
        if(Input.GetMouseButtonDown(1)) {
             RaycastHit hit;

            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, rangeHit, groundMask)) {
                Vector3 pointPos = hit.point + hit.normal / 2;

                /* ---------- */

                float distance = 0.81f;
                float playerDistance = Vector3.Distance(player.position, pointPos);
                float camDistance = Vector3.Distance(cam.transform.position, pointPos);

                if(playerDistance < distance || camDistance < distance) {
                    return;
                }
                if(pointPos.y > World.WorldSizeInVoxels.y) {
                    return;
                }

                /* ---------- */

                Chunk c = Chunk.GetChunk(new Vector3(
                    Mathf.FloorToInt(pointPos.x),
                    Mathf.FloorToInt(pointPos.y),
                    Mathf.FloorToInt(pointPos.z)
                ));

                c.SetVoxel(pointPos, toolbar.getVoxelID);
            }
        }
    }
}
