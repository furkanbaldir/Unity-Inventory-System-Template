using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryNode : MonoBehaviour
{
    private int _nodeId;
    private int _itemId;
    private int _itemAmount;
    private Sprite _nodeItemSprite;

    // Getters
    public int GetNodeId()
    {
        return _nodeId;
    }

    public int GetItemId()
    {
        return _itemId;
    }

    public int GetItemAmount()
    {
        return _itemAmount;
    }

    public Sprite GetNodeItemSprite()
    {
        return _nodeItemSprite;
    }

    // Setters
    public void SetNodeId(int id)
    {
        _nodeId = id;
    }

    public void SetItemId(int id)
    {
        _itemId = id;
    }

    public void SetItemAmount(int amount)
    {
        _itemAmount = amount;
    }

    public void SetNodeItemSprite(Sprite sprite)
    {
        _nodeItemSprite = sprite;
    }
}
