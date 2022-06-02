using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObj : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static Vector2 defaultposition;
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        defaultposition = this.transform.position;
    }


    void IDragHandler.OnDrag(PointerEventData eventData)//�巡������ ��
    {
        Vector2 currentPos = Input.mousePosition;
        this.transform.position = currentPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)//�巡�� ������ ��
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        this.transform.position = defaultposition;
    }
}
