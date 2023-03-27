using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour {
    [SerializeField] private Transform highlight;

    [SerializeField] private GameObject slotGrid;
    [SerializeField] private IItem[] slots;

    private int slotIndex;

    private void Awake() {
        slots = slotGrid.GetComponentsInChildren<IItem>();
    }
    
    private void Start() {
        
    }

    private void Update() {
        KeyInputs();
        ScrollInputs();

        highlight.position = slots[slotIndex].transform.position;
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

    public EnumVoxels getVoxelID {
        get {
            return slots[slotIndex].getVoxelID;
        }
    }
}
