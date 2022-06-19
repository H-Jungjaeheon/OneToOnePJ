using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stage1Hat : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private bool Answering, IsLeftRotation, IsReturning;
    [SerializeField] private GameObject Stage1GM;
    [Header("모자 회전 애니메이션 관련 변수")]
    [SerializeField] private float RotationZ;
    [SerializeField] private float RotateSpeed, MaxRotateR, MaxRotateL;
    [Header("드래그 판별 / 모자 인덱스")]
    public bool IsDraging;
    public int HatIndex;
    public static Vector2 defaultposition;
    [SerializeField] private Vector3 NowMousePos;
    [SerializeField] private Camera Cam;


    private void Awake()
    {
        Stage1GM = GameObject.Find("Stage1Manager");
    }
    private void FixedUpdate()
    {
        HatIdleAnim();
        MousePos();
    }
    private void MousePos()
    {
        NowMousePos = Input.mousePosition;
        NowMousePos = Cam.ScreenToWorldPoint(NowMousePos) + new Vector3(0, 0, 10);
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        defaultposition = this.transform.position;
    }
    private void HatIdleAnim()
    {
        if (IsLeftRotation)
        {
            RotationZ += Time.deltaTime * RotateSpeed;
            if (RotationZ >= MaxRotateR)
                IsLeftRotation = false;
        }
        else
        {
            RotationZ -= Time.deltaTime * RotateSpeed;
            if(RotationZ <= MaxRotateL)
                IsLeftRotation = true;
        }
        transform.rotation = Quaternion.Euler(0, 0, RotationZ);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)//드래그중일 때
    {
        if(Answering == false && IsReturning == false)
        {
            IsDraging = true;
            transform.position = NowMousePos + new Vector3(0,0,10);
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)//드래그 끝났을 때
    {
        IsReturning = true;
        Answering = true;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        IsDraging = false;
        StartCoroutine(DragEnd());
    }
    private IEnumerator DragEnd()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = defaultposition;
        yield return new WaitForSeconds(0.4f);
        Answering = false;
        IsReturning = false;
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
