using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI quantityText;

    private void OnEnable()
    {
        ClearData();
    }

    public void SetData(Sprite icon, int quantity)
    {
        if (icon != null && quantity > 0)
        {
            iconImage.sprite = icon;
            iconImage.color = Color.white;
            quantityText.text = quantity > 1 ? quantity.ToString() : "";
        }
        else
        {
            ClearData();
        }
    }
    public void ClearData()
    {
        iconImage.sprite = null;
        iconImage.color = Color.clear;
        quantityText.text = "";
    }
}
