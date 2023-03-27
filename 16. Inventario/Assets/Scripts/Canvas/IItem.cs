using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class IItem : MonoBehaviour, IPointerDownHandler {
    private Item item;
    
    private EnumVoxels voxelID;
    private Image image;
    private string itemName;
    
    private int stack = 1;
    private TextMeshProUGUI textMeshPro;

    private Transform parentAfterDrag;
    private GameObject dragging;

    private void Awake() {
        image = GetComponentInChildren<Image>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        
        dragging = GameObject.Find("Dragging Item");
    }
    
    private void Start() {
        
    }

    private void Update() {
        RefreshCount();
    }

    public void InitialiseItem(Item newItem) {
        item = newItem;

        voxelID = newItem.voxelID;
        image.sprite = newItem.sprite;
    }

    public void RefreshCount() {
        textMeshPro.text = stack.ToString();

        bool textActive = stack > 1;
        textMeshPro.gameObject.SetActive(textActive);
    }

    public Item getItem {
        get {
            return item;
        }
    }

    public EnumVoxels getVoxelID {
        get {
            return voxelID;
        }
    }

    public Image getImage {
        get {
            return image;
        }
    }

    public int getStack {
        get {
            return stack;
        }
        set {
            stack = value;
        }
    }

    public Transform getParentAfterDrag {
        get {
            return parentAfterDrag;
        }
        set {
            parentAfterDrag = value;
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        if(dragging.transform.childCount == 0) {
            image.raycastTarget = false;
            parentAfterDrag = transform.parent;
            transform.SetParent(dragging.transform);
        }
    }
}
