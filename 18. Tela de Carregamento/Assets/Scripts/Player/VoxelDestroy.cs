using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VoxelDestroy : MonoBehaviour {
    private Camera cam;
    private float rangeHit = 5.0f;
    private LayerMask groundMask;

    EnumVoxels voxelID;

    private IInterface iInterface;

    [Space(20)]
    private bool result;
    [SerializeField] private IInventory iInventory;
    [SerializeField] private Item[] itemsToPickup;

    [SerializeField] private TextMeshProUGUI textMeshPro;

    private void Awake() {
        cam = GetComponentInChildren<Camera>();
        groundMask = LayerMask.GetMask("Ground");
        
        iInterface = GameObject.Find("Interface Manager").GetComponent<IInterface>();
    }
    
    private void Start() {
        textMeshPro.gameObject.SetActive(false);
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
        result = iInventory.AddItem(itemsToPickup[id]);

        if(result) {
            //Debug.Log("Item type added: " + voxelType);
        }
        else {
            //Debug.Log("ITEM NOT ADDED");

            WarningMensage();
        }
    }

    private void WarningMensage() {
        textMeshPro.text = "Inventario cheio";

        ColorUtility.TryParseHtmlString("#FC5454", out Color color);
        textMeshPro.color = color;

        textMeshPro.gameObject.SetActive(true);

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut() {
        float delay = 1.0f;

        yield return new WaitForSeconds(delay);

        float fadeTime = 1.0f;
        float elapsedTime = 0.0f;

        Color startColor = textMeshPro.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < fadeTime) {
            float alpha = Mathf.Lerp(startColor.a, endColor.a, elapsedTime / fadeTime);
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        textMeshPro.color = endColor;

        textMeshPro.gameObject.SetActive(false);
    }
}
