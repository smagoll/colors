using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler
{
    [SerializeField] 
    private Letter letter;
    
    public void OnDrag(PointerEventData eventData)
    {
        if (letter.IsDone) return;
        
        transform.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (letter.Cell != null) letter.Cell.Clear();
        letter.Cell = null;
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
                transform.SetParent(raycastResult.gameObject.transform);
                return;
            }
        }
        
        //reset position
        transform.SetParent(GameManager.instance.listLetters);
    }
}
