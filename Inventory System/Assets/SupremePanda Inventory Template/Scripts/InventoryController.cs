using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private GameObject _inventory;

    [SerializeField]
    private int _inventoryNodesSize;
    private GameObject[] _inventoryNodes;

    [SerializeField]
    private Sprite denemeSprite;
    private void Start()
    {
        _inventoryNodes = new GameObject[_inventoryNodesSize];
        Debug.Log(_inventory);
        int counter = 0;
        foreach(Transform child in _inventory.transform)
        {
            //Debug.Log(child.gameObject.name);
            _inventoryNodes[counter] = child.gameObject;
            Debug.Log(_inventoryNodes[counter]);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            AddItem(3,1,denemeSprite, 0);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(GetInventoryNode(0).GetComponent<InventoryNode>().GetItemId());
            Debug.Log(GetInventoryNode(0).GetComponent<InventoryNode>().GetItemAmount());
        }
    }

    public void SetInventory(GameObject inventory)
    {
        _inventory = inventory;
        Debug.Log("setted inventory");
    }

    public void SetInventoryNodesSize(int size)
    {
        _inventoryNodesSize = size;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"> index = node number - 1</param>
    private GameObject GetInventoryNode(int index)
    {
        return _inventoryNodes[index];
    }

    public void AddItem(int itemId, int itemAmount, Sprite itemSprite, int nodeIndex)
    {
        GameObject node = GetInventoryNode(nodeIndex);
        InventoryNode inventoryNode = node.GetComponent<InventoryNode>();

        if (!inventoryNode.GetIsFilled())
        {
            inventoryNode.SetIsFilled(true);
            inventoryNode.SetItemId(itemId);
            inventoryNode.SetItemAmount(itemAmount);
            inventoryNode.SetNodeItemSprite(itemSprite);
        }
        
    }
   
}
