using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IDrag : MonoBehaviour {
    private IItem item;

    private IInterface iInterface;

    private void Awake() {
        iInterface = GameObject.Find("Interface Manager").GetComponent<IInterface>();
    }

    private void Start() {
        
    }

    private void Update() {
        Drag();
    }

    private void Drag() {
        bool openMenu = iInterface.getOpenMenu;

        if(transform.childCount == 1) {
            item = GetComponentInChildren<IItem>();
            //item.getImage.sprite = item.getImage.sprite;

            RectTransform rectTransform = item.GetComponent<RectTransform>();
            rectTransform.localScale = transform.localScale;

            item.transform.position = Input.mousePosition;

            if(Input.GetMouseButtonDown(0) || !openMenu) {
                if(!EventSystem.current.IsPointerOverGameObject()) {
                    item.getImage.raycastTarget = true;
                    item.transform.SetParent(item.getParentAfterDrag);
                }
            }
        }
    }
}
