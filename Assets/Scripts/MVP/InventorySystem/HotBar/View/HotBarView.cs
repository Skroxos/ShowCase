using System.Collections.Generic;
using UnityEngine;

public class HotBarView : MonoBehaviour
{
    [SerializeField] private RectTransform selectorTransform;
    [SerializeField] private List<RectTransform> hotbarSlots;

    
    public void MoveSelector(int index)
    {
        if (index < 0 || index >= hotbarSlots.Count) return;
        selectorTransform.position = hotbarSlots[index].position;
    }

}
