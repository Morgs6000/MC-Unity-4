using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IItem : MonoBehaviour {
    [SerializeField] private Item item;
    
    private EnumVoxels voxelID;
    private Image image;

    private void Awake() {
        image = GetComponentInChildren<Image>();
    }
    
    private void Start() {
        InitialiseItem(item);
    }

    private void Update() {
        
    }

    public void InitialiseItem(Item newItem) {
        voxelID = newItem.voxelID;
        image.sprite = newItem.sprite;
    }

    public EnumVoxels getVoxelID {
        get {
            return voxelID;
        }
    }
}
