using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelDestroy : MonoBehaviour {
    private Camera cam;
    private float rangeHit = 5.0f;
    private LayerMask groundMask;

    EnumVoxels voxelID;

    private IInterface iInterface;

    [Space(20)]
    [SerializeField] private IInventory iInventory;
    [SerializeField] private Item[] itemsToPickup;

    private void Awake() {
        cam = GetComponentInChildren<Camera>();
        groundMask = LayerMask.GetMask("Ground");
        
        iInterface = GameObject.Find("Interface Manager").GetComponent<IInterface>();
    }
    
    private void Start() {
        
    }

    private void Update() {
        bool openMenu = iInterface.getOpenMenu;

        if(!openMenu) {
            RaycastUpdate();
        }
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

                voxelID = c.GetVoxel(pointPos);

                SeiLaOque();

                c.SetVoxel(pointPos, EnumVoxels.air);
            }
        }
    }

    private void SeiLaOque() {
        if(voxelID == EnumVoxels.stone) {
            PickUpItem(0);
        }
        if(voxelID == EnumVoxels.grass) {
            PickUpItem(1);
        }
        if(voxelID == EnumVoxels.dirt) {
            PickUpItem(2);
        }
        if(voxelID == EnumVoxels.log) {
            PickUpItem(3);
        }
        if(voxelID == EnumVoxels.leaves) {
            PickUpItem(4);
        }
    }

    private void PickUpItem(int id) {
        iInventory.AddItem(itemsToPickup[id]);
    }
}
