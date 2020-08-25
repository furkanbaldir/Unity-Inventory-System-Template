using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine.UI;

[EditorTool("Create Item")]
public class CreateItem : EditorWindow
{
    private int _itemId = 0;
    private Sprite _itemSprite = null;
    private string _itemName = "";

    [MenuItem("Tools/Create Item")]
    public static void ShowWindow()
    {
        GetWindow(typeof(CreateItem));
    }

    private void OnGUI()
    {
        GUILayout.Label("Create New Item", EditorStyles.boldLabel);
        _itemName = EditorGUILayout.TextField("Item Name", _itemName);
        _itemId = EditorGUILayout.IntField("Item ID", _itemId);
        _itemSprite = EditorGUILayout.ObjectField("Item Sprite", _itemSprite, typeof(Sprite), false) as Sprite;

        if(GUILayout.Button("Create Item"))
        {
            CreateItemObject();
        }
    }

    private void CreateItemObject()
    {
        if(_itemId == 0)
        {
            Debug.LogWarning("Warning: Id = 0 is a default value. You can not detect difference with zero id." +
                "\nWarningField: 'Item Id'");
        }

        if(_itemSprite == null)
        {
            Debug.LogError("Error: Item sprite can not be null." +
                "\nErrorField: 'Item Sprite'");
            return;
        }

        if(_itemName == "")
        {
            Debug.LogError("Error: Item name can not be null" +
                "\nErrorField: 'Item name'");
        }

        if(GameObject.Find("Canvas") == null)
        {
            GameObject newCanvasObject;
            Canvas newCanvas;

            newCanvasObject = new GameObject();
            newCanvasObject.name = "Canvas";
            newCanvasObject.AddComponent<Canvas>();
            newCanvasObject.AddComponent<CanvasScaler>();
            newCanvasObject.AddComponent<GraphicRaycaster>();

            newCanvas = newCanvasObject.GetComponent<Canvas>();
            newCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

            GameObject newItem = new GameObject();
            newItem.transform.SetParent(newCanvasObject.transform);
            newItem.name = _itemName;
            newItem.AddComponent<ItemUI>();
            newItem.GetComponent<ItemUI>().SetItemId(_itemId);
            newItem.GetComponent<ItemUI>().SetItemSprite(_itemSprite);
            newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        else
        {
            GameObject canvasObject = GameObject.Find("Canvas");
            GameObject newItem = new GameObject();
            newItem.transform.SetParent(canvasObject.transform);
            newItem.name = _itemName;
            newItem.AddComponent<ItemUI>();
            newItem.GetComponent<ItemUI>().SetItemId(_itemId);
            newItem.GetComponent<ItemUI>().SetItemSprite(_itemSprite);
            newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        
    }
}
