using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObj : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("������Ʈ ȸ�� �ִϸ��̼� ���� ����")]
    [SerializeField] private float RotateSpeed;
    [SerializeField] private float MaxRotateR, MaxRotateL;
    private float RotationZ;

    [HideInInspector]
    public bool IsWrong;
    [Space(20)]
    [Tooltip("������Ʈ �� �ε���")]
    public int ObjIndex;
    public static Vector2 defaultposition;

    [Space(20)]
    [Tooltip("�ٸ� �ݶ��̴� �� ������ �±� �̸�")]
    [SerializeField] private string ColliderName;

    [Tooltip("�������� ��ġ")]
    [SerializeField] private int StageIndex;

    [Tooltip("�巡�� ���̾� ĵ����")]
    [SerializeField] private Canvas canvas;

    [Header("5�������� ���� �� ����")]
    [SerializeField] private Vector3 compleatPosition;
    [SerializeField] private Vector2 compleatScale;
    [SerializeField] private Vector3 compleatRotation;

    private bool Answering, IsLeftRotation, IsReturning; //���� �Ǻ���, ȸ�� ��ġ, ���ڸ� ���ư����� �Ǻ�
    [HideInInspector]
    public bool isCorrect; //���� �Ǻ� �Ϸ�
    [HideInInspector]
    public bool IsDraging;
    private GameObject GM;
    private Vector3 NowMousePos;
    private Camera Cam;
    

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
        Cam = Camera.main;
        switch (StageIndex)
        {
            case 1: GM = GameObject.Find("Stage1Manager"); break;
            case 4: GM = GameObject.Find("Stage4Manager"); break;
            case 5: GM = GameObject.Find("Stage5Manager"); break;
        }
    }
    private void MousePos()
    {
        if (isCorrect == false)
            NowMousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) => defaultposition = this.transform.position;
    private void HatIdleAnim()
    {
        if (isCorrect == false) 
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
                if (RotationZ <= MaxRotateL)
                    IsLeftRotation = true;
            }
            transform.rotation = Quaternion.Euler(0, 0, RotationZ);
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)//�巡������ ��
    {
        if (Answering == false && IsReturning == false && isCorrect == false)
        {
            canvas.sortingOrder = 2;
            IsDraging = true;
            transform.position = NowMousePos + new Vector3(0,0,100);
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)//�巡�� ������ ��
    {
        IsDraging = false;
        canvas.sortingOrder = 1;
        IsReturning = true;
        Answering = true;
        StartCoroutine(DragEnd());
    }
    private IEnumerator DragEnd()
    {
        yield return new WaitForSeconds(0.01f);
        if(isCorrect == false)
           transform.position = defaultposition;
        IsWrong = false;
        yield return new WaitForSeconds(0.1f);
        Answering = false;
        IsReturning = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ColliderName) && collision.gameObject.GetComponent<DiscriminantObject>().objIndex == ObjIndex && IsDraging == false && isCorrect == false)
        {
            if (StageIndex == 5) 
                isCorrect = true;
            switch (StageIndex)
            {
                case 1:
                    GM.GetComponent<Stage1Manager>().ResultCount++;
                    Destroy(gameObject);
                    break;
                case 5:
                    collision.gameObject.GetComponent<DiscriminantObject>().CorrectAwnser();
                    Stage5Manager.Instance.ResultCount++;
                    Stage5Manager.Instance.jarPiece.Add(gameObject);
                    transform.position = compleatPosition;
                    transform.localScale = compleatScale;
                    transform.rotation = Quaternion.identity;
                    break;
            }
        }
    }
}
