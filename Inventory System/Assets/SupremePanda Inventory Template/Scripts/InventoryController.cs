using JetBrains.Annotations;
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
    private List<GameObject> _itemObjects = new List<GameObject>();

    [SerializeField]
    private int _inventoryNodeMaxItemAmount;

    private void Start()
    {
        _inventoryNodes = new GameObject[_inventoryNodesSize];
        Debug.Log(_inventory);
        int counter = 0;
        foreach(Transform child in _inventory.transform)
        {
            //Debug.Log(child.gameObject.name);
            _inventoryNodes[counter] = child.gameObject;
            _inventoryNodes[counter].GetComponent<InventoryNode>().SetMaxItemAmount(_inventoryNodeMaxItemAmount);
            Debug.Log(_inventoryNodes[counter]);
            counter++;
        }

        foreach(GameObject item in _itemObjects)
        {
            Debug.Log(item.GetComponent<ItemUI>().GetItemId());
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            RemoveItemFromInventory(0, 5);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            AddItemToInventory(_itemObjects[0], 10);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddItemToInventory(_itemObjects[1], 10);
        }
    }
    /// <summary>
    /// This function uses with create inventory tool. Spawning inventory is sending size amount to create array.
    /// </summary>
    /// <param name="size">Inventory node amount and array size</param>
    public void SetInventoryNodesSize(int size)
    {
        _inventoryNodesSize = size;
    }

    public void SetInventory(GameObject inventory)
    {
        _inventory = inventory;
        Debug.Log("setted inventory");
    }
 
    /// <summary>
    /// This function provides getting inventory node according to index of node.
    /// </summary>
    /// <param name="index">Array index of nodes</param>
    private GameObject GetInventoryNode(int index)
    {
        return _inventoryNodes[index];
    }

    /// <summary>
    /// This function provides add item to inventory. 
    /// </summary>
    /// <param name="item">GameObject item is that we create with "Tools/Create Item"</param>
    /// <param name="amount">Amount of item to show how many items in node</param>
    public void AddItemToInventory(GameObject item, int amount)
    {
        bool itemInNode = false;
        int nodeIndex = 0;
        while (!itemInNode)
        {
            Debug.Log(GetInventoryNode(0));
            GameObject node = GetInventoryNode(nodeIndex);
            InventoryNode inventoryNode = node.GetComponent<InventoryNode>();
            
            if(CheckIsNodeFilled(inventoryNode) == false)
            {
                ItemUI itemUI = item.GetComponent<ItemUI>();

                Debug.Log(itemUI.GetItemId());

                inventoryNode.SetIsFilled(true);
                inventoryNode.SetItemId(itemUI.GetItemId());
                inventoryNode.SetNodeItemSprite(itemUI.GetItemSprite());
                inventoryNode.SetItemAmount(inventoryNode.GetItemAmount() + amount);
                itemInNode = true;
            }
            else
            {
                if(nodeIndex < _inventoryNodes.Length - 1)
                {
                    nodeIndex++;
                }
                else
                {
                    Debug.LogError("Error: No empty inventory node");
                    return;
                }
            }
        }      
    }

    public void RemoveItemFromInventory(int nodeIndex, int amount)
    {
        GameObject node = GetInventoryNode(nodeIndex);
        InventoryNode inventoryNode = node.GetComponent<InventoryNode>();

        if(inventoryNode.GetIsFilled() == true)
        {
            if (inventoryNode.GetItemAmount() - amount > 0)
            {
                // item kalcak amount değişcek
                inventoryNode.SubtractItemAmount(amount);
            }
            else
            {
                //itemi yok et
                inventoryNode.RemoveItemFromNode();
            }
        }
        else
        {
            Debug.LogError("Error: There is no item to remove");
        }
        
    }

    /// <summary>
    /// This function checks InventoryNode is filled or not.
    /// </summary>
    /// <param name="inventoryNode">inventoryNode is a script component of node object</param>
    /// <returns></returns>
    private bool CheckIsNodeFilled(InventoryNode inventoryNode)
    {
        if (inventoryNode.GetIsFilled())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
   
}
