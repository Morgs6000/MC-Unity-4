using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Toolbar : MonoBehaviour {
    [SerializeField] private GameObject slotGrid;
    [SerializeField] private ISlot[] slots;
    private ISlot slot;
    private IItem itemInSlot;

    [SerializeField] private Transform highlight;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    private int slotIndex;

    private IInterface iInterface;

    private void Awake() {
        slots = slotGrid.GetComponentsInChildren<ISlot>();
        
        iInterface = GameObject.Find("Interface Manager").GetComponent<IInterface>();
    }
    
    private void Start() {
        
    }

    private void Update() {
        bool isPaused = iInterface.getIsPaused;
        bool openMenu = iInterface.getOpenMenu;

        if(!isPaused && !openMenu) {
            KeyInputs();
            ScrollInputs();
        }

        highlight.position = slots[slotIndex].transform.position;
        
        GetItem();
        GetItemName();
    }

    private void GetItemName() {
        if(getVoxelID == EnumVoxels.air) {
            textMeshPro.text = null;
        }
        else {
            textMeshPro.text = getItemName;
        }
    }

    private string getItemName {
        get {
            if(slot.transform.childCount == 0) {
                return null;
            }
            else {                
                return itemInSlot.getItemName;
            }
        }
    }

    public EnumVoxels getVoxelID {
        get {
            if(slot.transform.childCount == 0) {
                return EnumVoxels.air;
            }
            else {                
                return itemInSlot.getVoxelID;
            }
        }
    }

    public Item GetSelectedItem(bool use) {
        if(itemInSlot != null) {
            Item item = itemInSlot.getItem;

            if(use) {
                itemInSlot.getStack--;

                if(itemInSlot.getStack <= 0) {
                    Destroy(itemInSlot.gameObject);
                }
                else {
                    itemInSlot.RefreshCount();
                }
            }

            return item;
        }

        return null;
    }

    private void GetItem() {
        slot = slots[slotIndex];
        itemInSlot = slot.GetComponentInChildren<IItem>();
    }

    private void KeyInputs() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            slotIndex = 0;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            slotIndex = 1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            slotIndex = 2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)) {
            slotIndex = 3;
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)) {
            slotIndex = 4;
        }
        if(Input.GetKeyDown(KeyCode.Alpha6)) {
            slotIndex = 5;
        }
        if(Input.GetKeyDown(KeyCode.Alpha7)) {
            slotIndex = 6;
        }
        if(Input.GetKeyDown(KeyCode.Alpha8)) {
            slotIndex = 7;
        }
        if(Input.GetKeyDown(KeyCode.Alpha9)) {
            slotIndex = 8;
        }
        if(Input.GetKeyDown(KeyCode.Alpha0)) {
            slotIndex = 9;
        }
    }

    private void ScrollInputs() {
        if(Input.GetAxis("Mouse ScrollWheel") > 0) {
            slotIndex--;
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0) {
            slotIndex++;
        }

        if(slotIndex > slots.Length - 1) {
            slotIndex = 0;
        }
        if(slotIndex < 0) {
            slotIndex = slots.Length - 1;
        }
    }
}
