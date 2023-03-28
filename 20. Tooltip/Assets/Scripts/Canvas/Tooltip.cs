using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour {
    private static Tooltip current;

    [SerializeField] private GameObject tooltip;

    [SerializeField] private TextMeshProUGUI headerField;
    [SerializeField] private TextMeshProUGUI contentField;
    [SerializeField] private TextMeshProUGUI idField;

    [SerializeField] private LayoutElement layoutElement;

    [SerializeField] private int characterWrapLimit;

    [SerializeField] private RectTransform rectTransform;

    private void Awake() {
        current = this;
    }
    
    private void Start() {
        current.tooltip.gameObject.SetActive(false);
    }

    private void Update() {
        TooltipPosition();
    }

    private void TooltipPosition() {
        // Obtém a posição do mouse na tela
        Vector2 mousePosition = Input.mousePosition;

        // Calcula a posição do tooltip em relação à tela
        Vector2 tooltipPosition = new Vector2(mousePosition.x + 15, mousePosition.y - 10);

        // Verifica se o tooltip está dentro dos limites da tela
        if (tooltipPosition.x + rectTransform.rect.width > Screen.width) {
            tooltipPosition.x = Screen.width - rectTransform.rect.width;
        }
        if (tooltipPosition.y - rectTransform.rect.height < 0) {
            tooltipPosition.y = rectTransform.rect.height;
        }

        // Define a posição do tooltip
        rectTransform.pivot = new Vector2(0, 1);
        rectTransform.position = tooltipPosition;
    }

    public static void Show(string header, string content, string id) {
        current.SetText(header, content, id);

        current.TooltipPosition();
        
        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide() {
        current.tooltip.gameObject.SetActive(false);
    }

    public void SetText(string header, string content, string id) {
        headerField.text = header;

        if(string.IsNullOrEmpty(content)) {
            contentField.gameObject.SetActive(false);
        }
        else {
            contentField.gameObject.SetActive(true);
            contentField.text = content;
        }

        idField.text = id;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (
            headerLength > characterWrapLimit ||
            contentLength > characterWrapLimit
        ) ? true : false;
    }
}
