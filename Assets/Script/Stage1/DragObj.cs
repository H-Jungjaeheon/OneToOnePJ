using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObj : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private bool Answering, IsLeftRotation, IsReturning;
    [SerializeField] private GameObject GM;
    [Header("오브젝트 회전 애니메이션 관련 변수")]
    [SerializeField] private float RotationZ;
    [SerializeField] private float RotateSpeed, MaxRotateR, MaxRotateL;
    [Header("드래그중 판별 / 오브젝트 비교 인덱스")]
    public bool IsDraging;
    public bool IsWrong;
    public int ObjIndex;
    public static Vector2 defaultposition;
    [SerializeField] private Vector3 NowMousePos;
    [SerializeField] private Camera Cam;
    [Header("비교 콜라이더 이름")]
    [SerializeField] private string ColliderName;
    [Header("스테이지 위치")]
    [SerializeField] private int StageIndex;

    [Tooltip("드래그 레이어 캔버스")]
    [SerializeField] private Canvas canvas;
    

    private void Awake()
    {
        StartSetting();
    }
  
    private void FixedUpdate()
    {
        HatIdleAnim();
        MousePos();
    }
    private void StartSetting()
    {
        switch (StageIndex)
        {
            case 1: GM = GameObject.Find("Stage1Manager"); break;
            case 4: GM = GameObject.Find("Stage4Manager"); break;
        }
    }
    private void MousePos()
    {
        NowMousePos = Input.mousePosition;
        NowMousePos = Cam.ScreenToWorldPoint(NowMousePos) + new Vector3(0, 0, 10);
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) => defaultposition = this.transform.position;
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
        if (!Answering && !IsReturning)
        {
            canvas.sortingOrder = 2;
            IsDraging = true;
            transform.position = NowMousePos + new Vector3(0,0,10);
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)//드래그 끝났을 때
    {
        canvas.sortingOrder = 1;
        IsReturning = true;
        Answering = true;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        IsDraging = false;
        StartCoroutine(DragEnd());
    }
    private IEnumerator DragEnd()
    {
        yield return new WaitForSeconds(0.02f);
        transform.position = defaultposition;
        IsWrong = false;
        yield return new WaitForSeconds(0.1f);
        Answering = false;
        IsReturning = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (StageIndex) 
        {
            case 1:
                if (collision.gameObject.CompareTag(ColliderName) && collision.gameObject.GetComponent<Stage1Guy>().GuyIndex == ObjIndex && IsDraging == false)
                {
                    GM.GetComponent<Stage1Manager>().ResultCount++;
                    Destroy(gameObject);
                }
                break;
            case 4:
                //미구현
                break;
        }
    }
}
