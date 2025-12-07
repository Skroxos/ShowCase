using System;
using UnityEngine;

public class HotBarPresenter : MonoBehaviour
{
    [SerializeField] private HotBarView hotBarView;
    [SerializeField] private InventoryPresenter presenter;
    [SerializeField] private int hotBarSize = 5;
    private HotBarModel hotBarModel;
    private IntventoryModel inventoryModel;

    private async void Start()
    {
        hotBarModel = new HotBarModel(hotBarSize);
        inventoryModel = presenter._model;

        hotBarModel.OnSelectionChanged += UpdateHotBarSelection;
        
        await System.Threading.Tasks.Task.Delay(100); // small delay to ensure everything is initialized
        UpdateHotBarSelection(0);
    }

    private void OnDestroy()
    {
        hotBarModel.OnSelectionChanged -= UpdateHotBarSelection;
    }
    
    private void Update()
    {
        HandleInput();
    }
    
    private void HandleInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            int direction = scroll > 0 ? 1 : -1;
            hotBarModel.ChangeSelection(direction);
        }
        for (int i = 0; i < hotBarSize; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                hotBarModel.SetSelection(i);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryUseCurrentItem();
        }
    }
    
    private void TryUseCurrentItem()
    {
        var slot = inventoryModel.GetSlot(hotBarModel.SelectedIndex);
        if (!slot.isEmpty() && slot.item != null)
        {
            Debug.Log($"Using item: {slot.item.itemName}");
            // Implement item usage logic here
        }
        else 
        {
            Debug.Log("No item in the selected hotbar slot.");
        }
    }

    private void UpdateHotBarSelection(int obj)
    {
       
        hotBarView.MoveSelector(obj);
    }
}
