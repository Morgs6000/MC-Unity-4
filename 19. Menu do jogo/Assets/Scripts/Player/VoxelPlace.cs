using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VoxelPlace : MonoBehaviour {
    private Camera cam;
    private float rangeHit = 5.0f;
    private LayerMask groundMask;

    private Transform player;

    [SerializeField] private Toolbar toolbar;
    private IInterface iInterface;

    [SerializeField] private TextMeshProUGUI textMeshPro;

    private void Awake() {
        cam = GetComponentInChildren<Camera>();
        groundMask = LayerMask.GetMask("Ground");

        player = GetComponent<Transform>();
        
        iInterface = GameObject.Find("Interface Manager").GetComponent<IInterface>();
    }
    
    private void Start() {
        textMeshPro.gameObject.SetActive(false);
    }

    private void Update() {
        bool isPaused = iInterface.getIsPaused;
        bool openMenu = iInterface.getOpenMenu;

        if(!isPaused && !openMenu) {
            RaycastUpdate();
        }
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
                    WarningMensage();
                    
                    return;
                }

                /* ---------- */

                Chunk c = Chunk.GetChunk(new Vector3(
                    Mathf.FloorToInt(pointPos.x),
                    Mathf.FloorToInt(pointPos.y),
                    Mathf.FloorToInt(pointPos.z)
                ));

                GetSelectedItem();
                UseSelectedItem();

                if(toolbar.getVoxelID != EnumVoxels.air) {
                    c.SetVoxel(pointPos, toolbar.getVoxelID);
                }                
            }
        }
    }

    public void GetSelectedItem() {
        Item receivedItem = toolbar.GetSelectedItem(false);

        if(receivedItem != null) {
            //Debug.Log("Recive item: " + receivedItem);
        }
        else {
            //Debug.Log("No item received!");
        }
    }

    public void UseSelectedItem() {
        Item receivedItem = toolbar.GetSelectedItem(true);

        if(receivedItem != null) {
            //Debug.Log("Used item: " + receivedItem);
        }
        else {
            //Debug.Log("No item used!");
        }
    }

    private void WarningMensage() {
        textMeshPro.text = "Height limit for building is " + World.WorldSizeInVoxels.y + " blocks";

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
