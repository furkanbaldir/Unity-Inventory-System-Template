using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
public class UIControllerForNode : MonoBehaviour
    //, IPointerClickHandler 
    //, IDragHandler
    , IPointerEnterHandler
    , IPointerExitHandler

{

    private Image _itemImage;
    private GameObject _uiController;

    private void Awake()
    {
        _itemImage = GetComponent<Image>();
        _uiController = GameObject.Find("UIController");
    }

    /*public void OnPointerClick(PointerEventData eventData) // 3
    {

        print("I was clicked");
    }*/
 
    /*public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        print("I'm being dragged!");
    }*/
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_uiController.GetComponent<UIController>().GetIsDragging() && GetComponent<InventoryNode>().GetIsFilled())
        {
            transform.SetAsLastSibling();
            gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        
        _itemImage.color = new Color32(174, 174, 174, 255);
        Debug.Log("pointer entered");
        _uiController.GetComponent<UIController>().SetSelectedInventoryNode(GetComponent<InventoryNode>());
       
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        _itemImage.color = new Color32(255, 255, 255, 255);
        Debug.Log("pointer exited");
        _uiController.GetComponent<UIController>().SetSelectedInventoryNode(null);
       

        gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
 }