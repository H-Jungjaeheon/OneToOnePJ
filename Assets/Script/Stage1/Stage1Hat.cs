using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stage1Hat : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private bool Answering;
    [SerializeField] private GameObject Stage1GM;
    public bool IsDraging;
    public int HatIndex;
    public static Vector2 defaultposition;


    private void Awake()
    {
        Stage1GM = GameObject.Find("Stage1Manager");
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        defaultposition = this.transform.position;
    }


    void IDragHandler.OnDrag(PointerEventData eventData)//드래그중일 때
    {
        if(Answering == false)
        {
            IsDraging = true;
            Vector2 currentPos = Input.mousePosition;
            this.transform.position = currentPos;
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)//드래그 끝났을 때
    {
        Answering = true;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        IsDraging = false;
        StartCoroutine(DragEnd());
    }
    private IEnumerator DragEnd()
    {
        yield return new WaitForSeconds(0.5f);
        Answering = false;
        this.transform.position = defaultposition;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Guys") && collision.gameObject.GetComponent<Stage1Guy>().GuyIndex == HatIndex && IsDraging == false)
        {
            //맞음 효과음 출력
            Stage1GM.GetComponent<Stage1Manager>().ResultCount++;
            Destroy(gameObject);
        }
        else
        {
            //틀림 효과음 출력
        }
    }
}
