using System;
using System.Collections.Generic;
using UnityEngine;

public class IntventoryModel
{
    public class Slot
    {
        public ItemSO item;
        public int quantity;
        
       public bool isEmpty() => item == null || quantity <= 0;
       public bool isFull() => quantity >= item.maxStack;
    }
    
    private readonly List<Slot> _slots;
    public int Capacity;
    
    public event Action<int> OnSlotChanged;
    
    public IntventoryModel(int capacity)
    {
        Capacity = capacity;
        _slots = new List<Slot>();
        for (int i = 0; i < capacity; i++)
        {
            _slots.Add(new Slot());
        }
    }
    
    public Slot GetSlot(int index) => _slots[index];
    public void SwapItems(int indexA, int indexB)
    {
        var temp = _slots[indexA];
        _slots[indexA] = _slots[indexB];
        _slots[indexB] = temp;
        OnSlotChanged?.Invoke(indexA);
        OnSlotChanged?.Invoke(indexB);
    }
    public int AddItem(ItemSO item, int quantity)
    {
        for (int i = 0; i < Capacity; i++)
        {
            var slot = _slots[i];
            if (!slot.isEmpty() && slot.item.itemID == item.itemID && item.isStackable)
            {
                int spaceLeft = item.maxStack - slot.quantity;
                int toAdd = Math.Min(spaceLeft, quantity);
                slot.quantity += toAdd;
                quantity -= toAdd;
                OnSlotChanged?.Invoke(i);
                if (quantity <= 0) return 0;
            }
        }
        
        for (int i = 0; i < Capacity; i++)
        {
            var slot = _slots[i];
            if (slot.isEmpty())
            {
                int toAdd = Math.Min(item.maxStack, quantity);
                slot.item = item;
                slot.quantity = toAdd;
                quantity -= toAdd;
                OnSlotChanged?.Invoke(i);
                if (quantity <= 0) return 0;
            }
        }
        
        return quantity; // Return remaining quantity that couldn't be added
    }
    
}
