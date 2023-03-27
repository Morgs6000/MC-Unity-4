using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ISlot : MonoBehaviour, IPointerDownHandler {
    private GameObject dragging;

    private IItem item;

    private void Awake() {
        dragging = GameObject.Find("Dragging Item");
    }

    private void Start() {
        
    }

    private void Update() {
        
    }

    public void OnPointerDown(PointerEventData eventData) {
        if(dragging.transform.childCount == 1) {
            if(transform.childCount == 0) {
                item = dragging.GetComponentInChildren<IItem>();
                item.getImage.raycastTarget = true;

                item.getParentAfterDrag = transform;
                item.transform.SetParent(item.getParentAfterDrag);
            }
        }
    }
}
