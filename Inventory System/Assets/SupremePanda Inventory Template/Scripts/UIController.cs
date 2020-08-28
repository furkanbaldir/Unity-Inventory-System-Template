using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private InventoryNode _selectedInventoryNode;

    private bool _isDragging;

    public bool GetIsDragging()
    {
        return _isDragging;
    }

    public InventoryNode GetSelectedInventoryNode()
    {
        return _selectedInventoryNode;
    }

  

    public void SetIsDragging(bool isDragging)
    {
        _isDragging = isDragging;
    }

    public void SetSelectedInventoryNode(InventoryNode inventoryNode)
    {
        _selectedInventoryNode = inventoryNode;
    }

   
}
