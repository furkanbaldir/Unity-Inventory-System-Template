using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        CheckSettedValues(2);
    }

    public void AddItemAmount(int amount)
    {
        _itemAmount += amount;
        CheckSettedValues(2);
    }

    public void SubtractItemAmount(int amount)
    {
        _itemAmount -= amount;
        CheckSettedValues(2);
    }

    public void SetNodeItemSprite(Sprite sprite)
    {
        _nodeItemSprite = sprite;
        CheckSettedValues(1);
    }

    public void SetIsFilled(bool isFilled)
    {
        _isFilled = isFilled;
    }

    public void RemoveItemFromNode()
    {
        GameObject nodeImage = this.transform.Find("NodeImage").gameObject;
        nodeImage.GetComponent<Image>().sprite = null;
        nodeImage.GetComponent<Image>().color = new Color32(255, 255, 255, 0);

        TMP_Text itemAmount = transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>();
        itemAmount.gameObject.SetActive(false);
        itemAmount.text = "";

        _isFilled = false;
        _itemAmount = 0;
        _itemId = 0;
        _nodeItemSprite = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="operationId">operation id 1: sprite, id 2: item amount</param> todo: itemleri ui üzerinden de sil
    private void CheckSettedValues(int operationId)
    {
        if(operationId == 1)
        {
            GameObject nodeImage = this.transform.Find("NodeImage").gameObject;
            nodeImage.GetComponent<Image>().sprite = _nodeItemSprite;
            nodeImage.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            Debug.Log("burası oluo");
        }
        else if(operationId == 2)
        {
            TMP_Text itemAmount = transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>();
            itemAmount.gameObject.SetActive(true);
            itemAmount.text = _itemAmount.ToString();   
        }
    }
}
