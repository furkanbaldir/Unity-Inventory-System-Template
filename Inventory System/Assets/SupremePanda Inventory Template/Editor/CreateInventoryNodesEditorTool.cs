using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine.UI;

[EditorTool("CreateInventoryNodes")]
public class CreateInventoryNodesEditorTool : EditorWindow
{
    private string _objectName;
    private int _horizontalNodeAmount = 0;
    private int _verticalNodeAmount = 0;
    private GameObject _inventoryNodePrefab;

    [MenuItem("Tools/Create Inventory")]
    public static void ShowWindow()
    {
        GetWindow(typeof(CreateInventoryNodesEditorTool));
    }

    private void OnGUI()
    {
        GUILayout.Label("Create New Inventory", EditorStyles.boldLabel);
        _objectName = EditorGUILayout.TextField("Object Name", _objectName);
        _horizontalNodeAmount = EditorGUILayout.IntField("Horizontal Node Amount", _horizontalNodeAmount);
        _verticalNodeAmount = EditorGUILayout.IntField("Vertical Node Amount", _verticalNodeAmount);
        _inventoryNodePrefab = EditorGUILayout.ObjectField("Inventory Node Prefab", _inventoryNodePrefab, typeof(GameObject), false) as GameObject;

        if(GUILayout.Button("Spawn Inventory"))
        {
            SpawnInventory();
        }
    }

    private void SpawnInventory()
    {
        if(_objectName == string.Empty)
        {
            Debug.LogError("Error: Please enter a object name\nError field: 'Object Name'");
        }
        if(_horizontalNodeAmount <= 0)
        {
            Debug.LogError("Error: Please enter a number that it is greater than zero\nError Field: 'Horizontal Node Amount'");
        }
        if(_verticalNodeAmount <= 0)
        {
            Debug.LogError("Error: Please enter a number that it is greater than zero\nError Field: 'Vertical Node Amount'");
        }
        if(_inventoryNodePrefab == null)
        {
            Debug.LogError("Error: Please assign a gameobject\nError Field: 'Inventory Node Prefab'");
        }

        Vector3 spawnPosition = new Vector3(0, 0, 0);
        Quaternion spawnQuaternion = Quaternion.identity;

        

        if(GameObject.Find("Canvas") != null)
        {
            GameObject _canvasObject;
            _canvasObject = GameObject.Find("Canvas");

            Vector2 editableAnchoredPosition;
            editableAnchoredPosition = Vector2.zero;

            int numberOfNodes = 0;

            for(int i = 0; i < _horizontalNodeAmount; i++)
            {
                for(int j = 0; j <_verticalNodeAmount; j++)
                {
                    GameObject newInventory = Instantiate(_inventoryNodePrefab, spawnPosition, spawnQuaternion);
                    newInventory.name = "Node" + numberOfNodes.ToString();
                    newInventory.transform.SetParent(_canvasObject.transform);

                    newInventory.GetComponent<RectTransform>().localScale = Vector3.one;
                    newInventory.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
                    newInventory.GetComponent<RectTransform>().anchoredPosition = editableAnchoredPosition;

                    float tempX;
                    tempX = editableAnchoredPosition.x + 100;
                    editableAnchoredPosition = new Vector2(tempX, editableAnchoredPosition.y);
                }
                float tempY;
                tempY = editableAnchoredPosition.y - 100;
                editableAnchoredPosition = new Vector2(0, tempY);
            }

            //GameObject newInventory = Instantiate(_inventoryNodePrefab, spawnPosition, spawnQuaternion);
            //newInventory.name = "Inventory";
            //newInventory.transform.SetParent(_canvasObject.transform);

            //newInventory.GetComponent<RectTransform>().localScale = Vector3.one;
            //newInventory.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            //newInventory.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        }
        else
        {
            GameObject newGameobject;
            Canvas newCanvas;

            newGameobject = new GameObject();
            newGameobject.name = "Canvas";
            newGameobject.AddComponent<Canvas>();

            newCanvas = newGameobject.GetComponent<Canvas>();
            newCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            newGameobject.AddComponent<CanvasScaler>();
            newGameobject.AddComponent<GraphicRaycaster>();

            Vector2 editableAnchoredPosition;
            editableAnchoredPosition = Vector2.zero;

            int numberOfNodes = 0;

            for (int i = 0; i < _horizontalNodeAmount; i++)
            {
                for (int j = 0; j < _verticalNodeAmount; j++)
                {
                    GameObject newInventory = Instantiate(_inventoryNodePrefab, spawnPosition, spawnQuaternion);
                    newInventory.name = "Node" + numberOfNodes.ToString();
                    newInventory.transform.SetParent(newGameobject.transform);

                    newInventory.GetComponent<RectTransform>().localScale = Vector3.one;
                    newInventory.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    newInventory.GetComponent<RectTransform>().anchoredPosition = editableAnchoredPosition;

                    float tempX;
                    tempX = editableAnchoredPosition.x + 100;
                    editableAnchoredPosition = new Vector2(tempX, editableAnchoredPosition.y);
                }
                float tempY;
                tempY = editableAnchoredPosition.y - 100;
                editableAnchoredPosition = new Vector2(0, tempY);
            }
        }

    }
}
