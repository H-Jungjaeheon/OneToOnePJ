using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingObjs : MonoBehaviour
{
    [Tooltip("���� �������� �ε���")]
    [SerializeField] private int NowStage;

    [Tooltip("�������� ����� ȿ�� ������Ʈ")]
    [SerializeField] private GameObject[] MovingObj;

    [Header("����� ȿ�� �̵� ���� ����")]
    [SerializeField] private float MovingObjRightSpeed;
    [SerializeField] private float MovingObjUpSpeed;

    [Header("�� ������Ʈ �¿� �Ǻ�")]
    [SerializeField] private bool IsLeft;
    [SerializeField] private SpriteRenderer sR;

    [Tooltip("��Ű ������Ʈ �Ǻ�")]
    [SerializeField] private bool iscookie;

    [Tooltip("��Ű ������Ʈ �Ǻ�")]
    [SerializeField] private GameObject checkIconObj;
    private bool isObjDestroying;


    void Start() 
    {
        if (NowStage == 4 && iscookie)
            StartCoroutine(CookieAnim());
        else if(NowStage == 4 && iscookie == false)
            CookieDialogAnim(true);
    }

    void Update()
    {
        if(NowStage == 2 && !Stage2Manager.Instance.IsClickHelp)
        {
            ObjMove(true);
        }
        else if(NowStage == 3 && !Stage3Manager.Instance.IsClickHelp)
        {
            ObjMove(false);
        }
        if (transform.localScale.x == 0 && isObjDestroying)
        {
            Destroy(gameObject);
        }
    }

    public void CookieDialogAnim(bool IsSpawnAnim) 
    {
        Vector3 DestoryScale = new Vector3(0, 0, 0);
        Vector3 MaxScale = new Vector3(1.2f, 1, 1);
        if (IsSpawnAnim)
            transform.DOScale(MaxScale, 0.6f).SetEase(Ease.OutBack);
        else
        {
            transform.DOScale(DestoryScale, 0.6f).SetEase(Ease.InBack);
            isObjDestroying = true;
        }
    } 

    private void ObjMove(bool IsBubble)
    {
        if (IsBubble)
        {
            for (int BubbleIndex = 0; BubbleIndex < 2; BubbleIndex++)
            {
                MovingObj[BubbleIndex].transform.position += new Vector3(MovingObjRightSpeed * Time.deltaTime, MovingObjUpSpeed * Time.deltaTime, 0);
                if (MovingObj[BubbleIndex].transform.position.y >= 6.8f)
                {
                    MovingObj[BubbleIndex].transform.position = new Vector3(-20.6f, -10, 0);
                }
            }
        }
        else if(IsBubble == false)
        {
            for (int SnowIndex = 0; SnowIndex < 2; SnowIndex++) // -1 0
            {
                if (MovingObj[SnowIndex].transform.position.x >= -1 && !IsLeft)
                {
                    MovingObj[SnowIndex].transform.position += new Vector3(-MovingObjRightSpeed * Time.deltaTime, -MovingObjUpSpeed * Time.deltaTime, 0);
                    if(MovingObj[SnowIndex].transform.position.x <= -1)
                    {
                        IsLeft = true;
                    }
                }
                else if (MovingObj[SnowIndex].transform.position.x <= 0 && IsLeft)
                {
                    MovingObj[SnowIndex].transform.position += new Vector3(MovingObjRightSpeed * Time.deltaTime, -MovingObjUpSpeed * Time.deltaTime, 0);
                    if (MovingObj[SnowIndex].transform.position.x >= 0)
                    {
                        IsLeft = false;
                    }
                }
                if (MovingObj[SnowIndex].transform.position.y <= -10.16f)
                {
                    MovingObj[SnowIndex].transform.position = new Vector3(0, 10.3f, 0);
                }
            }
        }
    }

    private IEnumerator CookieAnim()
    {
        Vector3 targetTransform = transform.position + new Vector3(0, 2.2f, 0);
        Vector3 nowTransform = transform.position;
        float colorAlpha = 1;
        while (sR.color.a > 0)
        {
            transform.position = Vector3.Lerp(transform.position, targetTransform, 0.05f);
            if (transform.position.y >= nowTransform.y / 2) 
            {
                sR.color = new Color(1, 1, 1, colorAlpha);
                colorAlpha -= Time.deltaTime;
            }
            yield return null;
        }
        Destroy(gameObject);
    }
}
