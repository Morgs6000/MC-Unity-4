using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelDestroy : MonoBehaviour {
    private Camera cam;
    private float rangeHit = 5.0f;
    private LayerMask groundMask;

    private void Awake() {
        cam = GetComponentInChildren<Camera>();
        groundMask = LayerMask.GetMask("Ground");
    }
    
    private void Start() {
        
    }

    private void Update() {
        RaycastUpdate();
    }

    private void RaycastUpdate() {
        if(Input.GetMouseButtonDown(0)) {
             RaycastHit hit;

            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, rangeHit, groundMask)) {
                Vector3 pointPos = hit.point - hit.normal / 2;

                Chunk c = Chunk.GetChunk(new Vector3(
                    Mathf.FloorToInt(pointPos.x),
                    Mathf.FloorToInt(pointPos.y),
                    Mathf.FloorToInt(pointPos.z)
                ));

                c.SetVoxel(pointPos, EnumVoxels.air);
            }
        }
    }
}
