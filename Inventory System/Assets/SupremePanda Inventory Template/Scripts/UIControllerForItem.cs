using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class UIControllerForItem : MonoBehaviour
    //, IPointerClickHandler
    , IDragHandler
    , IDropHandler
    , IBeginDragHandler
    , IEndDragHandler
    //, IPointerEnterHandler
    //, IPointerExitHandler

{
    private GameObject _uiController;

    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    private Vector2 _startPosition;

    private InventoryNode _inventoryNode;


    private InventoryNode _targetInventoryNode;


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _uiController = GameObject.Find("UIController");
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();

        _inventoryNode = transform.GetComponentInParent<InventoryNode>();
    

    }

    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = Input.mousePosition;
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        Debug.Log("Dragged");      
    }

    public void OnDrop(PointerEventData eventData) 
    {
        Debug.Log("Dropped");
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = _rectTransform.anchoredPosition;
        Debug.Log("On begin drag");
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.7f;
        _uiController.GetComponent<UIController>().SetIsDragging(true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _uiController.GetComponent<UIController>().SetIsDragging(false);
        _rectTransform.anchoredPosition = _startPosition;
        Debug.Log("On end drag");
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;

        _targetInventoryNode = _uiController.GetComponent<UIController>().GetSelectedInventoryNode();

        Debug.Log(_targetInventoryNode.gameObject.tag);
        if (_targetInventoryNode.gameObject.tag == "TrashNode")
        {
            Debug.Log("trash");
            _inventoryNode.RemoveItemFromNode();
        }

        else if (_targetInventoryNode.GetIsFilled() == true)
        {
            Debug.Log(CheckNodesHaveSameID(_inventoryNode, _targetInventoryNode));
            if(CheckNodesHaveSameID(_inventoryNode, _targetInventoryNode))
            {
                if(_targetInventoryNode.GetItemAmount() + _inventoryNode.GetItemAmount() <= _targetInventoryNode.GetMaxItemAmount())
                {
                    _targetInventoryNode.AddItemAmount(_inventoryNode.GetItemAmount());
                    _inventoryNode.RemoveItemFromNode();
                }
            }
            else
            {
                int tempId = _targetInventoryNode.GetItemId();
                Sprite tempSprite = _targetInventoryNode.GetNodeItemSprite();
                int tempItemAmount = _targetInventoryNode.GetItemAmount();

                _targetInventoryNode.SetItemId(_inventoryNode.GetItemId());
                _targetInventoryNode.SetNodeItemSprite(_inventoryNode.GetNodeItemSprite());
                _targetInventoryNode.SetItemAmount(_inventoryNode.GetItemAmount());

                _inventoryNode.SetItemId(tempId);
                _inventoryNode.SetNodeItemSprite(tempSprite);
                _inventoryNode.SetItemAmount(tempItemAmount);
                _inventoryNode.SetIsFilled(true);
            }
            
        }
        else
        {
            _targetInventoryNode.SetItemId(_inventoryNode.GetItemId());
            _targetInventoryNode.SetNodeItemSprite(_inventoryNode.GetNodeItemSprite());
            _targetInventoryNode.SetItemAmount(_inventoryNode.GetItemAmount());
            _targetInventoryNode.SetIsFilled(true);

            _inventoryNode.RemoveItemFromNode();
        }
    }

    private bool CheckNodesHaveSameID(InventoryNode sourceNode, InventoryNode targetNode)
    {
        if(sourceNode.GetItemId() == targetNode.GetItemId())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}