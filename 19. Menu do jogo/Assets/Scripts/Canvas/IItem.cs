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
    
    [SerializeField] private GameObject itemPrefab;

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
        itemName = newItem.itemName;
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

    public string getItemName {
        get {
            return itemName;
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
            // Pegar a Pilha
            if(eventData.button == PointerEventData.InputButton.Left) {
                image.raycastTarget = false;
                parentAfterDrag = transform.parent;
                transform.SetParent(dragging.transform);
            }

            // Pegar metade da Pilha
            if(eventData.button == PointerEventData.InputButton.Right) {
                if(stack <= 1) {
                    image.raycastTarget = false;

                    parentAfterDrag = transform.parent;
                    transform.SetParent(dragging.transform);
                }                
                else {
                    GameObject itemObject = Instantiate(itemPrefab);

                    IItem item2 = itemObject.GetComponentInChildren<IItem>();
                    item2.InitialiseItem(item);

                    itemObject.name = item2.itemName;

                    item2.getImage.raycastTarget = false;

                    item2.parentAfterDrag = transform.parent;
                    item2.transform.SetParent(dragging.transform);

                    int result = stack / 2;
                    int remainder = stack % 2;
                    int valueRemaining = result + remainder;
                    
                    stack = result;
                    RefreshCount();

                    item2.stack = valueRemaining;
                    item2.RefreshCount();
                }
            }
        }
        else if(dragging.transform.childCount == 1) {
            IItem iItem3 = dragging.GetComponentInChildren<IItem>();

            if(stack < item.maxStack) {

                // Colocar o maximo na Pilha
                if(eventData.button == PointerEventData.InputButton.Left) {
                    if((stack + iItem3.stack) <= item.maxStack) {
                        stack += iItem3.stack;
                        RefreshCount();

                        iItem3.stack = 0;

                        if(iItem3.stack <= 0) {
                            Destroy(iItem3.gameObject);
                        }
                        else {
                            iItem3.RefreshCount();
                        }
                    }
                }

                // Colcar 1 na Pilha
                if(eventData.button == PointerEventData.InputButton.Right) {
                    stack++;
                    RefreshCount();

                    iItem3.stack--;

                    if(iItem3.stack <= 0) {
                        Destroy(iItem3.gameObject);
                    }
                    else {
                        iItem3.RefreshCount();
                    }
                }                    
            }
        }
    }
}
