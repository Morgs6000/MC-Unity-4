using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInventory : MonoBehaviour {
    [SerializeField] private GameObject toolbar;
    [SerializeField] private GameObject invetory;    
    [SerializeField] private List<ISlot> slots = new List<ISlot>();

    [SerializeField] private GameObject itemPrefab;

    private void Awake() {
        slots.AddRange(toolbar.GetComponentsInChildren<ISlot>());
        slots.AddRange(invetory.GetComponentsInChildren<ISlot>());
    }

    private void Start() {
        
    }

    private void Update() {
        
    }

    public bool AddItem(Item item) {
        // Verifique se algum slot tem o mesmo item com contagem menor que o máximo
        // Encontre qualquer slot vazio
        for(int i = 0; i < slots.Count; i++) {
            ISlot slot = slots[i];
            IItem itemInSlot = slot.GetComponentInChildren<IItem>();

            if(
                itemInSlot != null &&
                itemInSlot.getItem == item &&
                itemInSlot.getStack < item.maxStack
            ) {
                itemInSlot.getStack++;
                itemInSlot.RefreshCount();

                return true;
            }
            else if(itemInSlot == null) {
                SpawnNewItem(item, slot);

                return true;
            }
        }

        return false;
    }

    public void SpawnNewItem(Item item, ISlot slot) {
        GameObject newItemObject = Instantiate(itemPrefab, slot.transform);

        IItem iItem = newItemObject.GetComponent<IItem>();
        iItem.InitialiseItem(item);

        newItemObject.name = item.itemName;
    }
}
