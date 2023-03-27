using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInterface : MonoBehaviour {
    [SerializeField] private GameObject inventory;
    private bool openMenu;

    private void Start() {        
        inventory.SetActive(false);
    }

    private void Update() {
        InventoryInput();
    }

    private void InventoryInput() {
        if(Input.GetKeyDown(KeyCode.E)) {
            openMenu = !openMenu;

            inventory.SetActive(!inventory.activeSelf);
        }
    }

    public bool getOpenMenu {
        get {
            return openMenu;
        }
    }
}
