using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryNode : MonoBehaviour
{
    private int _nodeId;
    private bool _isFilled;
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

    public bool GetIsFilled()
    {
        return _isFilled;
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
        CheckSettedValues(3);
    }

    public void SetIsFilled(bool isFilled)
    {
        _isFilled = isFilled;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="operationId">operation id: 1 -> itemId, 2 -> itemAmount, 3 -> itemSprite, 4 -> nodeId</param>
    private void CheckSettedValues(int operationId)
    {
        if(operationId == 1)
        {
            // toDo: gerekli bir şey olmayabilir
        }
        else if(operationId == 2)
        {
            // toDo: node içindeki texti düzenle
        }
        else if(operationId == 3)
        {
            GameObject nodeImage = this.transform.Find("NodeImage").gameObject;
            nodeImage.GetComponent<Image>().sprite = _nodeItemSprite;
            nodeImage.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }
}
