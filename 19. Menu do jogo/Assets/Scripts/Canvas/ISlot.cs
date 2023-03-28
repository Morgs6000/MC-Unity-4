using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ISlot : MonoBehaviour, IPointerDownHandler {
    private GameObject dragging;

    private IItem item;

    [SerializeField] private GameObject itemPrefab;

    private void Awake() {
        dragging = GameObject.Find("Dragging Item");
    }

    private void Start() {
        
    }

    private void Update() {
        
    }

    public void OnPointerDown(PointerEventData eventData) {
        if(dragging.transform.childCount == 1) {
            if(eventData.button == PointerEventData.InputButton.Left) {
                if(transform.childCount == 0) {
                    item = dragging.GetComponentInChildren<IItem>();
                    item.getImage.raycastTarget = true;

                    item.getParentAfterDrag = transform;
                    item.transform.SetParent(item.getParentAfterDrag);
                }
            }
            if(eventData.button == PointerEventData.InputButton.Right) {            
                if(item.getStack < 2) {
                    item.getImage.raycastTarget = true;

                    item.getParentAfterDrag = transform;
                    item.transform.SetParent(item.getParentAfterDrag);
                }
                else {
                    GameObject itemObject = Instantiate(itemPrefab);

                    IItem item2 = itemObject.GetComponentInChildren<IItem>();
                    item2.InitialiseItem(item.getItem);

                    itemObject.name = item2.getItemName;

                    item2.transform.SetParent(transform);

                    item.getStack--;
                    item.RefreshCount();                
                }
            }
        }
    }
}
