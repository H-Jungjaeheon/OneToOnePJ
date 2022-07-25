using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingObjs : MonoBehaviour
{
    [Tooltip("현재 스테이지 인덱스")]
    [SerializeField] private int NowStage;

    [Tooltip("스테이지 물방울 효과 오브젝트")]
    [SerializeField] private GameObject[] MovingObj;

    [Header("물방울 효과 이동 관련 변수")]
    [SerializeField] private float MovingObjRightSpeed;
    [SerializeField] private float MovingObjUpSpeed;

    [Header("눈 오브젝트 좌우 판별")]
    [SerializeField] private bool IsLeft;
    [SerializeField] private SpriteRenderer sR;

    [Tooltip("쿠키 오브젝트 판별")]
    [SerializeField] private bool iscookie;

    [Tooltip("쿠키 오브젝트 판별")]
    [SerializeField] private GameObject checkIconObj;
    private bool isObjDestroying;

    void Start() 
    {
        if (NowStage == 4 && iscookie)
            StartCoroutine(CookieAnim());
        else if (NowStage == 4 && iscookie == false)
            CookieDialogAnim(true);
        else if (NowStage == 6)
            StartCoroutine(CleaningAnim(2.8f));
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

    IEnumerator CleaningAnim(float MaxCleaningAnimCount)
    {
        bool IsUp = false;
        float NowAnimCount = 0;
        float ChangeGoingUpConditionY = 2.8f;
        float ChangeGoingDownConditionY = 4f;
        Vector2 MaxUpPosition = new Vector2(-2.2f, 4.1f);
        Vector2 MaxDownPosition = new Vector2(-2.2f, 2.7f);
        while (true)
        {
            if(IsUp)
                transform.position = Vector2.Lerp(transform.position, MaxUpPosition, 0.05f);
            else
                transform.position = Vector2.Lerp(transform.position, MaxDownPosition, 0.05f);
            if (transform.position.y <= ChangeGoingUpConditionY)
                IsUp = true;
            else if(transform.position.y >= ChangeGoingDownConditionY)
                IsUp = false;
            NowAnimCount += Time.deltaTime;
            if (NowAnimCount >= MaxCleaningAnimCount)
                break;
            yield return null;
        }
        float nowAlpha = 1;
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        while (nowAlpha > 0)
        {
            sr.color = new Color(1, 1, 1, nowAlpha -= Time.deltaTime);
            yield return null;
        }
    }

    public void DestoryObj() => Destroy(gameObject);

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
