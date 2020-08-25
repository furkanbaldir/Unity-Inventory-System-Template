using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[EditorTool("CreateInventoryNodes")]
public class CreateInventoryNodesEditorTool : EditorWindow
{
    //private string _objectName;                 // Inventory object name
    private int _horizontalNodeAmount = 0;      // Amount of horizontal nodes
    private int _verticalNodeAmount = 0;        // Amount of vertical nodes
    private int _horizontalDistance = 0;        // Distance between two nodes horizontally
    private int _verticalDistance = 0;          // Distance between two nodes vertically
    private GameObject _inventoryNodePrefab;    // Inventory node prefab

    [MenuItem("Tools/Create Inventory")]
    public static void ShowWindow()
    {
        GetWindow(typeof(CreateInventoryNodesEditorTool));
    }

    // Editor GUI components
    private void OnGUI()
    {
        GUILayout.Label("Create New Inventory", EditorStyles.boldLabel);
        //_objectName = EditorGUILayout.TextField("Object Name", _objectName);
        _horizontalNodeAmount = EditorGUILayout.IntField("Horizontal Node Amount", _horizontalNodeAmount);
        _verticalNodeAmount = EditorGUILayout.IntField("Vertical Node Amount", _verticalNodeAmount);
        _horizontalDistance = EditorGUILayout.IntField("Horizontal Distance", _horizontalDistance);
        _verticalDistance = EditorGUILayout.IntField("Vertical Distance", _verticalDistance);
        _inventoryNodePrefab = EditorGUILayout.ObjectField("Inventory Node Prefab", _inventoryNodePrefab, typeof(GameObject), false) as GameObject;

        if(GUILayout.Button("Spawn Inventory"))
        {
            SpawnInventory();
        }
    }

    /// <summary>
    /// This function spawns inventory.
    /// If there is empty field on tool GUI, returns error
    /// </summary>
    private void SpawnInventory()
    {
        /*if(_objectName == string.Empty)
        {
            Debug.LogError("Error: Please enter a object name\nError field: 'Object Name'");
            return;
        }*/
        if(_horizontalNodeAmount <= 0)
        {
            Debug.LogError("Error: Please enter a number that it is greater than zero\nError Field: 'Horizontal Node Amount'");
            return;
        }
        if(_verticalNodeAmount <= 0)
        {
            Debug.LogError("Error: Please enter a number that it is greater than zero\nError Field: 'Vertical Node Amount'");
            return;
        }
        if(_inventoryNodePrefab == null)
        {
            Debug.LogError("Error: Please assign a gameobject\nError Field: 'Inventory Node Prefab'");
            return;
        }
        if(_horizontalDistance <= 0)
        {
            Debug.LogError("Error: Please enter a number to determine distance between two nodes horizontally." +
                " For advice, Use sprite resolution values like 100x120 For horizontal, this value is 100" +
                "\nError Field: 'Horizontal Distance'");
        }
        if(_verticalDistance <= 0)
        {
            Debug.LogError("Error: Please enter a number to determine distance between two nodes vertically." +
                " For advice, Use sprite resolution values like 100x120 For vertical, this value is 120" +
                "\nError Field: 'Vertical Distance'");
        }


        Vector3 spawnPosition = new Vector3(0, 0, 0);       // Spawn position of inventory node prefab
        Quaternion spawnQuaternion = Quaternion.identity;   // Spawn Quaternion values of inventory node prefab

        // If canvas is not exist, create canvas object and set parent object of every instantiated node objects
        if(GameObject.Find("Canvas") != null)
        {
            GameObject canvasObject;
            canvasObject = GameObject.Find("Canvas");

            Vector2 editableAnchoredPosition;               // This variables uses to change next instantiated object position
            editableAnchoredPosition = Vector2.zero;

            GameObject inventory = new GameObject();
            inventory.name = "Inventory";
            inventory.transform.SetParent(canvasObject.transform);
            

            int numberOfNodes = 0;                          // This variables uses to name next instantiated object

            for(int i = 0; i < _horizontalNodeAmount; i++)
            {
                
                for (int j = 0; j <_verticalNodeAmount; j++)
                {
                    numberOfNodes++;
                    GameObject newInventory = Instantiate(_inventoryNodePrefab, spawnPosition, spawnQuaternion);
                    newInventory.transform.SetParent(inventory.transform);
                    newInventory.name = "Node" + numberOfNodes.ToString();
                    newInventory.transform.SetParent(inventory.transform);

                    newInventory.GetComponent<RectTransform>().localScale = Vector3.one;
                    newInventory.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
                    newInventory.GetComponent<RectTransform>().anchoredPosition = editableAnchoredPosition;

                    float tempX;
                    tempX = editableAnchoredPosition.x + _horizontalDistance;
                    editableAnchoredPosition = new Vector2(tempX, editableAnchoredPosition.y);

                    
                }
                float tempY;
                tempY = editableAnchoredPosition.y - _verticalDistance; 
                editableAnchoredPosition = new Vector2(0, tempY);
            }

        }
        // If canvas object exists, set parent object of every instantiated node objects
        else
        {
            GameObject newGameobject;
            Canvas newCanvas;

            newGameobject = new GameObject();
            newGameobject.name = "Canvas";
            newGameobject.AddComponent<Canvas>();

            GameObject inventory = new GameObject();
            inventory.name = "Inventory";
            inventory.transform.SetParent(newGameobject.transform);

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
                    numberOfNodes++;
                    GameObject newInventory = Instantiate(_inventoryNodePrefab, spawnPosition, spawnQuaternion);
                    newInventory.name = "Node" + numberOfNodes.ToString();
                    newInventory.transform.SetParent(inventory.transform);

                    newInventory.GetComponent<RectTransform>().localScale = Vector3.one;
                    newInventory.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    newInventory.GetComponent<RectTransform>().anchoredPosition = editableAnchoredPosition;

                    float tempX;
                    tempX = editableAnchoredPosition.x + _horizontalDistance;
                    editableAnchoredPosition = new Vector2(tempX, editableAnchoredPosition.y);
                }
                float tempY;
                tempY = editableAnchoredPosition.y - _verticalDistance;
                editableAnchoredPosition = new Vector2(0, tempY);

                
            }

            if(GameObject.Find("EventSystem") == null)
            {
                GameObject newEventSystem = new GameObject();
                newEventSystem.name = "EventSystem";
                newEventSystem.AddComponent<EventSystem>();
                newEventSystem.AddComponent<StandaloneInputModule>();
                //Instantiate(newEventSystem, Vector3.zero, Quaternion.identity);
            }
        }

    }
}
