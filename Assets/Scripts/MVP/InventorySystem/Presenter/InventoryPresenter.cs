using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPresenter : MonoBehaviour
{
    [SerializeField] private int _inventoryCapacity = 20;
    
    [SerializeField] private Transform _slotContainer;
    [SerializeField] private GameObject _slotPrefab;
    
    [SerializeField] private ItemSO _testItem;
    [SerializeField] private ItemSO _testItem2;
    
    private IntventoryModel _model;
    private List<InventoryView> _views;

    private void Awake()
    {
        {
            // 1. Vytvoření Modelu
            _model = new IntventoryModel(_inventoryCapacity);
        
            // 2. Příprava Views (Instantiate prefabů)
            _views = new List<InventoryView>();
            for (int i = 0; i < _inventoryCapacity ; i++)
            {
                GameObject obj = Instantiate(_slotPrefab, _slotContainer);
                InventoryView view = obj.GetComponentInChildren<InventoryView>();
                _views.Add(view);
            }

            // 3. Propojení (Subscribe)
            _model.OnSlotChanged += UpdateSlot;
        }

        
    }
    private void OnDestroy()
    {
        if (_model != null)
            _model.OnSlotChanged -= UpdateSlot;
    }
    
    private void UpdateSlot(int index)
    {
        // Vytáhneme data z Modelu
        var slotData = _model.GetSlot(index);
        
        // Najdeme odpovídající View
        var view = _views[index];

        // Nakrmíme View
        if (slotData.isEmpty())
        {
            view.ClearData();
        }
        else
        {
            view.SetData(slotData.item.itemIcon, slotData.quantity);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Přidání testovací položky do inventáře
            _model.AddItem(_testItem, 1);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Přidání druhé testovací položky do inventáře
            _model.AddItem(_testItem2, 3);
        }
    }
}

