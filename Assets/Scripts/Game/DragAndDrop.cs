using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] 
    private Letter letter;
    private RectTransform m_transform;
    
    void Start () 
    {
        m_transform = GetComponent<RectTransform>();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (letter.IsDone) return;
        
        Vector3 vec = Camera.main.WorldToScreenPoint(m_transform.position);
        vec.x += eventData.delta.x;
        vec.y += eventData.delta.y;
        m_transform.position = Camera.main.ScreenToWorldPoint(vec);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (letter.Cell != null) letter.Cell.Clear();
        letter.Cell = null;
        transform.SetParent(GameManager.instance.dragTransform);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> resultList = new();
        EventSystem.current.RaycastAll(eventData, resultList);
        
        foreach (var raycastResult in resultList)
        {
            if (raycastResult.gameObject.CompareTag("Cell"))
            {
                var cell = raycastResult.gameObject.GetComponent<Cell>();
                if (cell.IsDone) break;
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
