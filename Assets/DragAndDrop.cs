using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler
{
    [SerializeField] 
    private Letter letter;
    private Transform prevParent;
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        prevParent = transform.parent;
        transform.SetParent(GameManager.instance.dragTransform);
    }

    public void OnDrop(PointerEventData eventData)
    {
        List<RaycastResult> resultList = new();
        EventSystem.current.RaycastAll(eventData, resultList);
        
        foreach (var raycastResult in resultList)
        {
            if (raycastResult.gameObject.CompareTag("Cell"))
            {
                var cell = raycastResult.gameObject.GetComponent<Cell>();
                cell.Set(letter);
                return;
            }
            
            if (raycastResult.gameObject.CompareTag("ListLetter"))
            {
                letter.Cell.Clear();
                transform.SetParent(raycastResult.gameObject.transform);
                return;
            }
        }
        
        //reset position
        transform.SetParent(prevParent);
    }
}
